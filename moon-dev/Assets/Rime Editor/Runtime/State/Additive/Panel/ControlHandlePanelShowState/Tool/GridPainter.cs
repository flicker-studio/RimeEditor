using System.Collections.Generic;
using UnityEngine;

namespace LevelEditor
{
    public class GridPainter
    {
        private float m_cellSize;

        private MeshFilter m_filter;

        private Color m_gridColor;
        private int   m_gridSize;

        private int m_growthFactor;

        private Mesh m_mesh;

        private MeshRenderer m_meshRenderer;

        private GameObject m_targetObj;

        public GridPainter(GameObject targetObj, int girdSize, float cellSize, int growthFactor, Color gridColor)
        {
            Init(targetObj, girdSize, cellSize, growthFactor, gridColor);
            UpdateGrid(m_gridSize);
        }

        public void SetActive(bool active)
        {
            m_targetObj.SetActive(active);
        }

        public void UpdateGrid()
        {
            var bounds = m_meshRenderer.bounds;

            var topRight = Camera.main.ScreenToWorldPoint
                (new Vector3(Screen.width, Screen.height, Mathf.Abs(Camera.main.transform.position.z)));

            var bottomLeft = Camera.main.ScreenToWorldPoint
                (new Vector3(0, 0, Mathf.Abs(Camera.main.transform.position.z)));

            if (topRight.x > bounds.max.x || topRight.y > bounds.max.y || bottomLeft.x < bounds.min.x ||
                bottomLeft.y < bounds.min.y)
                UpdateGrid(m_gridSize + m_growthFactor);
        }

        private void Init(GameObject targetObj, int girdSize, float cellSize, int growthFactor, Color gridColor)
        {
            m_targetObj                   = targetObj;
            m_gridSize                    = girdSize;
            m_cellSize                    = cellSize;
            m_growthFactor                = growthFactor;
            m_gridColor                   = gridColor;
            m_meshRenderer                = m_targetObj.AddComponent<MeshRenderer>();
            m_filter                      = m_targetObj.AddComponent<MeshFilter>();
            m_meshRenderer.material       = new Material(Shader.Find("Sprites/Default"));
            m_meshRenderer.material.color = m_gridColor;
        }

        private void UpdateGrid(int targetGridSize)
        {
            m_gridSize = targetGridSize;
            m_mesh     = m_filter?.mesh != null ? m_filter.mesh : new Mesh();
            var verticies = new List<Vector3>();

            var indices = new List<int>();

            for (var index = 0; index < m_gridSize; index++)
            {
                verticies.Add(new Vector3(index * m_cellSize, 0,                       0));
                verticies.Add(new Vector3(index * m_cellSize, m_gridSize * m_cellSize, 0));

                indices.Add(4 * index + 0);
                indices.Add(4 * index + 1);

                verticies.Add(new Vector3(0,                       index * m_cellSize, 0));
                verticies.Add(new Vector3(m_gridSize * m_cellSize, index * m_cellSize, 0));

                indices.Add(4 * index + 2);
                indices.Add(4 * index + 3);
            }

            m_mesh.vertices = verticies.ToArray();
            m_mesh.SetIndices(indices.ToArray(), MeshTopology.Lines, 0);

            var dir = Vector3.zero - m_meshRenderer.bounds.center;
            m_targetObj.transform.position += dir;
        }
    }
}