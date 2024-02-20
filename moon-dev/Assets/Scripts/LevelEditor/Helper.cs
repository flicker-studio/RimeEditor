using Moon.Kernel;
using UnityEngine;

namespace LevelEditor
{
    /// <summary>
    ///     The helper script is responsible for automatically enabling the controller
    /// </summary>
    public class Helper : MonoBehaviour
    {
        private void Start()
        {
            Boot.PostBoot += Controller.Instance.AssetsLoaderAsync;
        }
    }
}