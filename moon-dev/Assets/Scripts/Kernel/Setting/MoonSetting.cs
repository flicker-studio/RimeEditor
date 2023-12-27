using Moon.Kernel.Attribute;
using UnityEngine;

namespace Moon.Kernel.Setting
{
    /// <summary>
    /// 
    /// </summary>
    [CreateAssetMenu(fileName = "MoonSetting", menuName = "Moon/Setting/MoonSetting")]
    public class MoonSetting : SettingBase
    {
        [SerializeField] public bool isCheck = true;

        [SerializeField, SceneSelect] public string startScene;
    }
}