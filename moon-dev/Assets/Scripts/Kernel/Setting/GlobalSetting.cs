using System.IO;
using Moon.Kernel.Attribute;
using UnityEngine;

namespace Moon.Kernel.Setting
{
    /// <summary>
    ///     Global profiles
    /// </summary>
    [SystemSetting("Assets/Settings/GlobalSettings/GlobalSetting.asset")]
    public class GlobalSetting : SettingBase
    {
        /// <summary>
        ///     The name of the Level Editor scene
        /// </summary>
        public string LevelEditor => levelEditor;

        /// <summary>
        ///     The name of the level player scene
        /// </summary>

        public string LevelPlay => levelPlay;

        /// <summary>
        ///     Ground Layer
        /// </summary>

        public LayerMask Ground => ground;

        /// <summary>
        /// </summary>

        public string ControlHandle => controlHandle;

        /// <summary>
        /// </summary>

        public string RigidbodyTag => rigidbodyTag;

        /// <summary>
        /// </summary>

        public string CanCopyTag => canCopyTag;

        /// <summary>
        /// </summary>

        public Vector2 ScreenSizeStandard => screenSizeStandard;

        /// <summary>
        /// </summary>

        public string ItemFilePath => itemFilePath;

        /// <summary>
        /// </summary>

        public string LevelDataName => levelDataName;

        /// <summary>
        /// </summary>

        public string GamesDataName => gamesDataName;

        /// <summary>
        /// </summary>

        public string ImagesDataName => imagesDataName;

        /// <summary>
        /// </summary>

        public string SoundsDataName => soundsDataName;

        /// <summary>
        /// </summary>

        public string CoverImageName => coverImageName;

        public string InformationFile => informationFile;

        /// <summary>
        /// </summary>
        public Vector2 ReferenceResolution => new(ScreenSizeStandard.x, ScreenSizeStandard.y / Screen.width * Screen.height);

        /// <summary>
        /// </summary>
        public string LevelPath => Path.Combine(Application.persistentDataPath, LevelDataName);

        public string RootPath => Path.Combine(Application.persistentDataPath, LevelDataName);

        [CustomLabel("关卡编辑器场景")]
        [SceneSelect]
        [SerializeField]
        private string levelEditor = "LevelEditor";

        [CustomLabel("关卡播放器场景")]
        [SceneSelect]
        [SerializeField]
        private string levelPlay = "LevelPlay";

        [CustomLabel("地面层级")]
        [SerializeField]
        private LayerMask ground = 8;

        [CustomLabel("控制柄标签")]
        [SerializeField]
        private string controlHandle = "ControlHandle";

        [SerializeField]
        [CustomLabel("刚体标签")]
        private string rigidbodyTag = "<rigidbody>";

        [CustomLabel("可复制物体标签")]
        [SerializeField]
        private string canCopyTag = "<canCopy>";

        [CustomLabel("屏幕标准尺寸")]
        [SerializeField]
        private Vector2 screenSizeStandard = new(1920f, 1080f);

        [CustomLabel("物件文件路径")]
        [SerializeField]
        private string itemFilePath = "Items\\ScriptableObject";

        [CustomLabel("关卡数据名字")]
        [SerializeField]
        private string levelDataName = "LevelDatas";

        [CustomLabel("关卡数据文件夹")]
        [SerializeField]
        private string gamesDataName = "GameDatas";

        [CustomLabel("图像数据文件夹")]
        [SerializeField]
        private string imagesDataName = "Images";

        [CustomLabel("声音数据文件夹")]
        [SerializeField]
        private string soundsDataName = "Sounds";

        [CustomLabel("背景图片文件名")]
        [SerializeField]
        private string coverImageName = "CoverImage.png";

        [CustomLabel("创建信息文件名")]
        [SerializeField]
        private string informationFile = "package.json";
    }
}