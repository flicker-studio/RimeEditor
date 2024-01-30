using Moon.Kernel.Extension;
using TMPro;
using UnityEngine.UI;
using RectTransform = UnityEngine.RectTransform;

namespace LevelEditor
{
    public class LevelManagerPanel
    {
        public string GetLevelTextName => m_levelTextName;

        public string GetLevelPathTextName => m_levelPathTextName;

        public string GetLevelImageName => m_levelImageName;

        public RawImage GetLevelCoverImage => m_levelCoverImage;
        public ScrollRect GetLevelScrollRect => m_levelScrollRect;
        public RectTransform GetLevelManagerRootRect => m_levelManagerRootRect;

        public RectTransform GetLevelListContentRect => m_levelListContentRect;

        public RectTransform GetFullPanelRect => m_fullPanelRect;

        public Button GetOpenButton => m_openButton;

        public Button GetCreateButton => m_createButton;

        public Button GetDeclarationButton => m_declarationButton;

        public Button GetExitButton => m_exitButton;

        public Button GetRefreshButton => m_refreshButton;

        public Button GetDeleteButton => m_deleteButton;

        public Button GetOpenLocalDirectoryButton => m_openLocalDirectoryButton;

        public Button GetWorksShopButton => m_worksShopButton;

        public TextMeshProUGUI GetSubLevelNumber => m_subLevelNumber;

        public TextMeshProUGUI GetLevelName => m_levelName;

        public TextMeshProUGUI GetAnthorName => m_anthorName;

        public TextMeshProUGUI GetDateTime => m_dateTime;

        public TextMeshProUGUI GetInstroduction => m_instroduction;

        public TextMeshProUGUI GetVersion => m_version;

        public UISetting.PopoverProperty GetPopoverProperty { get; private set; }

        private RawImage m_levelCoverImage;

        private ScrollRect m_levelScrollRect;

        private string m_levelTextName;

        private string m_levelPathTextName;

        private string m_levelImageName;

        private RectTransform m_levelManagerRootRect;

        private RectTransform m_levelListContentRect;

        private RectTransform m_fullPanelRect;

        private Button m_openButton;

        private Button m_createButton;

        private Button m_declarationButton;

        private Button m_exitButton;

        private Button m_refreshButton;

        private Button m_openLocalDirectoryButton;

        private Button m_worksShopButton;

        private Button m_localLevelButton;

        private Button m_deleteButton;

        private TextMeshProUGUI m_levelName;

        private TextMeshProUGUI m_anthorName;

        private TextMeshProUGUI m_dateTime;

        private TextMeshProUGUI m_instroduction;

        private TextMeshProUGUI m_version;

        private TextMeshProUGUI m_subLevelNumber;

        public LevelManagerPanel(RectTransform rect, UISetting levelEditorUISetting)
        {
            InitComponent(rect, levelEditorUISetting);
        }

        private void InitComponent(RectTransform rect, UISetting levelEditorUISetting)
        {
            var uiProperty =
                levelEditorUISetting.GetLevelManagerPanelUI.GetLevelManagerPanelUIName;

            GetPopoverProperty = levelEditorUISetting.GetPopoverProperty;
            m_levelTextName = uiProperty.ITEM_LEVEL_NAME;
            m_levelPathTextName = uiProperty.ITEM_LEVEL_PATH;
            m_levelImageName = uiProperty.ITEM_LEVEL_ICON;
            m_levelCoverImage = rect.FindPath(uiProperty.LEVEL_COVER_NAME).GetComponent<RawImage>();
            m_levelScrollRect = rect.FindPath(uiProperty.SCROLL_RECT).GetComponent<ScrollRect>();
            m_levelManagerRootRect = rect.FindPath(uiProperty.PANEL_ROOT) as RectTransform;
            m_levelListContentRect = rect.FindPath(uiProperty.LEVEL_LIST_CONTENT) as RectTransform;
            m_fullPanelRect = rect.FindPath(uiProperty.FULL_PANEL) as RectTransform;
            m_openButton = rect.FindPath(uiProperty.OPEN_BUTTON).GetComponent<Button>();
            m_createButton = rect.FindPath(uiProperty.CREATE_BUTTON).GetComponent<Button>();
            m_declarationButton = rect.FindPath(uiProperty.DECLARATION_BUTTON).GetComponent<Button>();
            m_exitButton = rect.FindPath(uiProperty.EXIT_BUTTON).GetComponent<Button>();
            m_refreshButton = rect.FindPath(uiProperty.REFRESH_BUTTON).GetComponent<Button>();
            m_deleteButton = rect.FindPath(uiProperty.DELETE_LEVEL_BUTTON).GetComponent<Button>();
            m_openLocalDirectoryButton = rect.FindPath(uiProperty.OEPN_LOCAL_DIRECTORY_BUTTON).GetComponent<Button>();
            m_worksShopButton = rect.FindPath(uiProperty.WORKS_SHOP_BUTTON).GetComponent<Button>();
            m_localLevelButton = rect.FindPath(uiProperty.LOCAL_LEVEL_BUTTON).GetComponent<Button>();
            m_subLevelNumber = rect.FindPath(uiProperty.SUB_LEVEL_NUMBER).GetComponent<TextMeshProUGUI>();
            m_levelName = rect.FindPath(uiProperty.LEVEL_NAME).GetComponent<TextMeshProUGUI>();
            m_anthorName = rect.FindPath(uiProperty.AUTHOR_NAME).GetComponent<TextMeshProUGUI>();
            m_dateTime = rect.FindPath(uiProperty.DATE_TIME).GetComponent<TextMeshProUGUI>();
            m_instroduction = rect.FindPath(uiProperty.INSTRODUCTION).GetComponent<TextMeshProUGUI>();
            m_version = rect.FindPath(uiProperty.VERSION).GetComponent<TextMeshProUGUI>();
        }
    }
}