using UnityEngine;

namespace LevelEditor
{
    /// <summary>
    ///     The helper script is responsible for automatically enabling the controller
    /// </summary>
    public class Helper : MonoBehaviour
    {
        private void Awake()
        {
            Controller.Instance.AssetsLoaderAsync();
        }
    }
}