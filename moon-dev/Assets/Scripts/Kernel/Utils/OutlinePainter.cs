using System;
using System.Collections.Generic;
using System.Linq;
using Moon.Kernel.Service;
using UnityEngine;
using UnityEngine.Rendering;
using Object = UnityEngine.Object;

namespace Moon.Kernel.Utils
{
    /// <summary>
    /// </summary>
    public enum OutlineMode
    {
        OutlineAll,

        OutlineVisible,

        OutlineHidden,

        OutlineAndSilhouette,

        SilhouetteOnly
    }

    /// <summary>
    /// </summary>
    public class OutlinePainter
    {
        private static readonly Dictionary<GameObject, HashSet<Mesh>> RegisteredMeshesDic = new();

        private Material m_outlineMaskMaterial;

        private Material m_outlineFillMaterial;

        private static readonly int ZTest = Shader.PropertyToID("_ZTest");

        private static readonly int Width = Shader.PropertyToID("_OutlineWidth");

        private static readonly int OutlineColor1 = Shader.PropertyToID("_OutlineColor");

        [Serializable]
        private class ListVector3
        {
            public List<Vector3> data;
        }

        public OutlineMode OutlineMode;

        public Color OutlineColor = Color.white;

        public float OutlineWidth = 2f;

        private bool m_precomputeOutline;

        private readonly List<Mesh> m_bakeKeys = new();

        private readonly List<ListVector3> m_bakeValues = new();

        public List<GameObject> SetTargetObj
        {
            set
            {
                OnDisable();

                GetTargetObj.Clear();

                if (value != null)
                {
                    GetTargetObj.AddRange(value);
                }

                foreach (var obj in GetTargetObj)
                {
                    if (RegisteredMeshesDic.ContainsKey(obj))
                    {
                        continue;
                    }

                    RegisteredMeshesDic.Add(obj, new HashSet<Mesh>());
                }

                // Retrieve or generate smooth normals
                LoadSmoothNormals();

                OnEnable();

                UpdateMaterialProperties();
            }
        }

        public List<GameObject> GetTargetObj { get; } = new();

        public OutlinePainter()
        {
            A();
        }

        private async void A()
        {
            // Instantiate outline materials
            m_outlineMaskMaterial = await (ResourcesService.LoadAssetAsync<Material>("Assets/Materials/OutlineMask.mat"));
            m_outlineFillMaterial = await (ResourcesService.LoadAssetAsync<Material>("Assets/Materials/OutlineFill.mat"));

            m_outlineMaskMaterial.name = "OutlineMask (Instance)";
            m_outlineFillMaterial.name = "OutlineFill (Instance)";
        }

        private void OnEnable()
        {
            foreach (var obj in GetTargetObj)
            {
                var renderers = obj.GetComponentsInChildren<Renderer>();

                foreach (var renderer in renderers)
                {
                    // Append outline shaders
                    var materials = renderer.sharedMaterials.ToList();
                    materials.Add(m_outlineMaskMaterial);
                    materials.Add(m_outlineFillMaterial);

                    renderer.materials = materials.ToArray();
                }
            }
        }

        private void OnDisable()
        {
            foreach (var obj in GetTargetObj)
            {
                var renderers = obj.GetComponentsInChildren<Renderer>();

                foreach (var renderer in renderers)
                {
                    // Remove outline shaders
                    var materials = renderer.sharedMaterials.ToList();
                    materials.Remove(m_outlineMaskMaterial);
                    materials.Remove(m_outlineFillMaterial);
                    renderer.materials = materials.ToArray();
                }
            }
        }

        private void LoadSmoothNormals()
        {
            // Retrieve or generate smooth normals
            foreach (var obj in GetTargetObj)
            {
                var meshFilters = obj.GetComponentsInChildren<MeshFilter>();

                foreach (var meshFilter in meshFilters)
                {
                    // Skip if smooth normals have already been adopted
                    if (!RegisteredMeshesDic[obj].Add(meshFilter.sharedMesh))
                    {
                        continue;
                    }

                    // Retrieve or generate smooth normals
                    var index = m_bakeKeys.IndexOf(meshFilter.sharedMesh);
                    var smoothNormals = index >= 0 ? m_bakeValues[index].data : SmoothNormals(meshFilter.sharedMesh);

                    // Store smooth normals in UV3
                    meshFilter.sharedMesh.SetUVs(3, smoothNormals);

                    // Combine sub meshes
                    var renderer = meshFilter.GetComponent<Renderer>();

                    if (renderer != null)
                    {
                        CombineSubmeshes(meshFilter.sharedMesh, renderer.sharedMaterials);
                    }
                }
            }

            // Clear UV3 on skinned mesh renderers
            foreach (var obj in GetTargetObj)
            {
                var skinnedMeshRenderers = obj.GetComponentsInChildren<SkinnedMeshRenderer>();

                foreach (var skinnedMeshRenderer in skinnedMeshRenderers)
                {
                    // Skip if UV3 has already been reset
                    if (!RegisteredMeshesDic[obj].Add(skinnedMeshRenderer.sharedMesh))
                    {
                        continue;
                    }

                    // Clear UV3
                    var sharedMesh = skinnedMeshRenderer.sharedMesh;
                    sharedMesh.uv4 = new Vector2[sharedMesh.vertexCount];

                    // Combine sub meshes
                    CombineSubmeshes(sharedMesh, skinnedMeshRenderer.sharedMaterials);
                }
            }
        }

        private List<Vector3> SmoothNormals(Mesh mesh)
        {
            // Group vertices by location
            var groups = mesh.vertices.Select((vertex, index) => new KeyValuePair<Vector3, int>(vertex, index)).GroupBy(pair => pair.Key);

            // Copy normals to a new list
            var smoothNormals = new List<Vector3>(mesh.normals);

            // Average normals for grouped vertices
            foreach (var group in groups)
            {
                // Skip single vertices
                if (group.Count() == 1)
                {
                    continue;
                }

                // Calculate the average normal
                var smoothNormal = Vector3.zero;

                foreach (var pair in group) smoothNormal += smoothNormals[pair.Value];

                smoothNormal.Normalize();

                // Assign smooth normal to each vertex
                foreach (var pair in group) smoothNormals[pair.Value] = smoothNormal;
            }

            return smoothNormals;
        }

        private static void CombineSubmeshes(Mesh mesh, IReadOnlyCollection<Material> materials)
        {
            // Skip meshes with a single submenu
            if (mesh.subMeshCount == 1)
            {
                return;
            }

            // Skip if sub mesh count exceeds material count
            if (mesh.subMeshCount > materials.Count)
            {
                return;
            }

            // Append combined sub mesh
            mesh.subMeshCount++;
            mesh.SetTriangles(mesh.triangles, mesh.subMeshCount - 1);
        }

        private void UpdateMaterialProperties()
        {
            // Apply properties according to mode
            m_outlineFillMaterial.SetColor(OutlineColor1, OutlineColor);

            switch (OutlineMode)
            {
                case OutlineMode.OutlineAll:
                    m_outlineMaskMaterial.SetFloat(ZTest, (float)CompareFunction.Always);
                    m_outlineFillMaterial.SetFloat(ZTest, (float)CompareFunction.Always);
                    m_outlineFillMaterial.SetFloat(Width, OutlineWidth);
                    break;

                case OutlineMode.OutlineVisible:
                    m_outlineMaskMaterial.SetFloat(ZTest, (float)CompareFunction.Always);
                    m_outlineFillMaterial.SetFloat(ZTest, (float)CompareFunction.LessEqual);
                    m_outlineFillMaterial.SetFloat(Width, OutlineWidth);
                    break;

                case OutlineMode.OutlineHidden:
                    m_outlineMaskMaterial.SetFloat(ZTest, (float)CompareFunction.Always);
                    m_outlineFillMaterial.SetFloat(ZTest, (float)CompareFunction.Greater);
                    m_outlineFillMaterial.SetFloat(Width, OutlineWidth);
                    break;

                case OutlineMode.OutlineAndSilhouette:
                    m_outlineMaskMaterial.SetFloat(ZTest, (float)CompareFunction.LessEqual);
                    m_outlineFillMaterial.SetFloat(ZTest, (float)CompareFunction.Always);
                    m_outlineFillMaterial.SetFloat(Width, OutlineWidth);
                    break;

                case OutlineMode.SilhouetteOnly:
                    m_outlineMaskMaterial.SetFloat(ZTest, (float)CompareFunction.LessEqual);
                    m_outlineFillMaterial.SetFloat(ZTest, (float)CompareFunction.Greater);
                    m_outlineFillMaterial.SetFloat(Width, 0f);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}