using UnityEngine;

namespace LevelEditor
{
    public class Collect : MonoBehaviour
    {
        public GameObject target;

        private void Start()
        {
            LevelEditorController.Instance.AssetsLoaderAsync();
        }
    }
}