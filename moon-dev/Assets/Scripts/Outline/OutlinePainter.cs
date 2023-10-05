using System;
using System.Collections.Generic;
using System.Linq;
using Frame.Tool;
using UnityEngine;

public enum OUTLINEMODE {
  OutlineAll,
  OutlineVisible,
  OutlineHidden,
  OutlineAndSilhouette,
  SilhouetteOnly
}

public class OutlinePainter
{
  private static Dictionary<GameObject, HashSet<Mesh>> m_registeredMeshesDic = new Dictionary<GameObject, HashSet<Mesh>>();
    
  [Serializable]
  private class ListVector3 {
    public List<Vector3> Data;
  }
    
  public OUTLINEMODE OutlineMode;
    
  public Color OutlineColor = Color.white;
    
  public float OutlineWidth = 2f;
    
  private bool m_precomputeOutline;
    
  private List<Mesh> m_bakeKeys = new List<Mesh>();
    
  private List<ListVector3> m_bakeValues = new List<ListVector3>();
  
  private List<GameObject> m_targetObj = new List<GameObject>();

  public List<GameObject> SetTargetObj
  {
    set
    {
      OnDisable();
      
      m_targetObj.Clear();
      m_targetObj.AddRange(value);
      
      foreach (var obj in m_targetObj)
      {
        if(m_registeredMeshesDic.ContainsKey(obj)) continue;
        m_registeredMeshesDic.Add(obj,new HashSet<Mesh>());
      }
      // Retrieve or generate smooth normals
      LoadSmoothNormals();

      OnEnable();

      
      UpdateMaterialProperties();
    }
  }

  public List<GameObject> GetTargetObj => m_targetObj;
  
  private Material outlineMaskMaterial;
  private Material outlineFillMaterial;

  public OutlinePainter()
  {
    // Instantiate outline materials
    outlineMaskMaterial = GameObject.Instantiate(Resources.Load<Material>(@"Materials/OutlineMask"));
    outlineFillMaterial = GameObject.Instantiate(Resources.Load<Material>(@"Materials/OutlineFill"));

    outlineMaskMaterial.name = "OutlineMask (Instance)";
    outlineFillMaterial.name = "OutlineFill (Instance)";
  }

  void OnEnable() {
    foreach (var obj in m_targetObj)
    {
      Renderer[] renderers = obj.GetComponentsInChildren<Renderer>();
      foreach (var renderer in renderers) {

        // Append outline shaders
        var materials = renderer.sharedMaterials.ToList();
        materials.Add(outlineMaskMaterial);
        materials.Add(outlineFillMaterial);

        renderer.materials = materials.ToArray();
      }
    }
  }

  void OnDisable()
  {
    foreach (var obj in m_targetObj)
    {
      Renderer[] renderers = obj.GetComponentsInChildren<Renderer>();
      foreach (var renderer in renderers) {

        // Remove outline shaders
        var materials = renderer.sharedMaterials.ToList();
        materials.Remove(outlineMaskMaterial);
        materials.Remove(outlineFillMaterial);
        renderer.materials = materials.ToArray();
      }
    }
  }

  void LoadSmoothNormals() {

    // Retrieve or generate smooth normals
    foreach (var obj in m_targetObj)
    {
      MeshFilter[] meshFilters = obj.GetComponentsInChildren<MeshFilter>();
      foreach (var meshFilter in meshFilters) {
        
        // Skip if smooth normals have already been adopted
        if (!m_registeredMeshesDic[obj].Add(meshFilter.sharedMesh)) {
          continue;
        }

        // Retrieve or generate smooth normals
        var index = m_bakeKeys.IndexOf(meshFilter.sharedMesh);
        var smoothNormals = (index >= 0) ? m_bakeValues[index].Data : SmoothNormals(meshFilter.sharedMesh);

        // Store smooth normals in UV3
        meshFilter.sharedMesh.SetUVs(3, smoothNormals);

        // Combine submeshes
        var renderer = meshFilter.GetComponent<Renderer>();

        if (renderer != null) {
          CombineSubmeshes(meshFilter.sharedMesh, renderer.sharedMaterials);
        }
      }
    }
    // Clear UV3 on skinned mesh renderers
    foreach (var obj in m_targetObj)
    {
      SkinnedMeshRenderer[] skinnedMeshRenderers = obj.GetComponentsInChildren<SkinnedMeshRenderer>();
      foreach (var skinnedMeshRenderer in skinnedMeshRenderers) {

        // Skip if UV3 has already been reset
        if (!m_registeredMeshesDic[obj].Add(skinnedMeshRenderer.sharedMesh)) {
          continue;
        }

        // Clear UV3
        skinnedMeshRenderer.sharedMesh.uv4 = new Vector2[skinnedMeshRenderer.sharedMesh.vertexCount];

        // Combine submeshes
        CombineSubmeshes(skinnedMeshRenderer.sharedMesh, skinnedMeshRenderer.sharedMaterials);
      }
    }
  }

  List<Vector3> SmoothNormals(Mesh mesh) {

    // Group vertices by location
    var groups = mesh.vertices.Select((vertex, index) => new KeyValuePair<Vector3, int>(vertex, index)).GroupBy(pair => pair.Key);

    // Copy normals to a new list
    var smoothNormals = new List<Vector3>(mesh.normals);

    // Average normals for grouped vertices
    foreach (var group in groups) {

      // Skip single vertices
      if (group.Count() == 1) {
        continue;
      }

      // Calculate the average normal
      var smoothNormal = Vector3.zero;

      foreach (var pair in group) {
        smoothNormal += smoothNormals[pair.Value];
      }

      smoothNormal.Normalize();

      // Assign smooth normal to each vertex
      foreach (var pair in group) {
        smoothNormals[pair.Value] = smoothNormal;
      }
    }

    return smoothNormals;
  }

  void CombineSubmeshes(Mesh mesh, Material[] materials) {

    // Skip meshes with a single submesh
    if (mesh.subMeshCount == 1) {
      return;
    }

    // Skip if submesh count exceeds material count
    if (mesh.subMeshCount > materials.Length) {
      return;
    }

    // Append combined submesh
    mesh.subMeshCount++;
    mesh.SetTriangles(mesh.triangles, mesh.subMeshCount - 1);
  }

  void UpdateMaterialProperties() {

    // Apply properties according to mode
    outlineFillMaterial.SetColor("_OutlineColor", OutlineColor);

    switch (OutlineMode) {
      case OUTLINEMODE.OutlineAll:
        outlineMaskMaterial.SetFloat("_ZTest", (float)UnityEngine.Rendering.CompareFunction.Always);
        outlineFillMaterial.SetFloat("_ZTest", (float)UnityEngine.Rendering.CompareFunction.Always);
        outlineFillMaterial.SetFloat("_OutlineWidth", OutlineWidth);
        break;

      case OUTLINEMODE.OutlineVisible:
        outlineMaskMaterial.SetFloat("_ZTest", (float)UnityEngine.Rendering.CompareFunction.Always);
        outlineFillMaterial.SetFloat("_ZTest", (float)UnityEngine.Rendering.CompareFunction.LessEqual);
        outlineFillMaterial.SetFloat("_OutlineWidth", OutlineWidth);
        break;

      case OUTLINEMODE.OutlineHidden:
        outlineMaskMaterial.SetFloat("_ZTest", (float)UnityEngine.Rendering.CompareFunction.Always);
        outlineFillMaterial.SetFloat("_ZTest", (float)UnityEngine.Rendering.CompareFunction.Greater);
        outlineFillMaterial.SetFloat("_OutlineWidth", OutlineWidth);
        break;

      case OUTLINEMODE.OutlineAndSilhouette:
        outlineMaskMaterial.SetFloat("_ZTest", (float)UnityEngine.Rendering.CompareFunction.LessEqual);
        outlineFillMaterial.SetFloat("_ZTest", (float)UnityEngine.Rendering.CompareFunction.Always);
        outlineFillMaterial.SetFloat("_OutlineWidth", OutlineWidth);
        break;

      case OUTLINEMODE.SilhouetteOnly:
        outlineMaskMaterial.SetFloat("_ZTest", (float)UnityEngine.Rendering.CompareFunction.LessEqual);
        outlineFillMaterial.SetFloat("_ZTest", (float)UnityEngine.Rendering.CompareFunction.Greater);
        outlineFillMaterial.SetFloat("_OutlineWidth", 0f);
        break;
    }
  }
}
