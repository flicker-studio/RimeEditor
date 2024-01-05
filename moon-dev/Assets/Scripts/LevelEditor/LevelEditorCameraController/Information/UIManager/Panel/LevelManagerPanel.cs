using Frame.Static.Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LevelEditor
{
    public class LevelManagerPanel
    {
        
        public RectTransform GetLevelManagerRootRect => m_levelManagerRootRect;

        public RectTransform GetLevelListContentRect => m_levelListContentRect;

        public RectTransform GetFullPanelRect => m_fullPanelRect;

        public Button GetOpenButton => m_openButton;

        public Button GetCreateButton => m_createButton;

        public Button GetDeclarationButton => m_declarationButton;

        public Button GetExitButton => m_exitButton;

        public TextMeshProUGUI GetLevelName => m_levelName;

        public TextMeshProUGUI GetAnthorName => m_anthorName;

        public TextMeshProUGUI GetDateTime => m_dateTime;

        public TextMeshProUGUI GetInstroduction => m_instroduction;
        
        private RectTransform m_levelManagerRootRect;

        private RectTransform m_levelListContentRect;

        private RectTransform m_fullPanelRect;

        private Button m_openButton;

        private Button m_createButton;

        private Button m_declarationButton;

        private Button m_exitButton;

        private TextMeshProUGUI m_levelName;

        private TextMeshProUGUI m_anthorName;

        private TextMeshProUGUI m_dateTime;

        private TextMeshProUGUI m_instroduction;
        public LevelManagerPanel(RectTransform rect,UIProperty levelEditorUIProperty)
        {
            InitComponent(rect, levelEditorUIProperty);
        }
        
        private void InitComponent(RectTransform rect,UIProperty levelEditorUIProperty)
        {
            UIProperty.LevelManagerPanelUIName uiProperty =
                levelEditorUIProperty.GetLevelManagerPanelUI.GetLevelManagerPanelUIName;
            m_levelManagerRootRect = rect.FindPath(uiProperty.PANEL_ROOT) as RectTransform;
            m_levelListContentRect = rect.FindPath(uiProperty.LEVEL_LIST_CONTENT) as RectTransform;
            m_fullPanelRect = rect.FindPath(uiProperty.FULL_PANEL) as RectTransform;
            m_openButton = rect.FindPath(uiProperty.OPEN_BUTTON).GetComponent<Button>();
            m_createButton = rect.FindPath(uiProperty.CREATE_BUTTON).GetComponent<Button>();
            m_declarationButton = rect.FindPath(uiProperty.DECLARATION_BUTTON).GetComponent<Button>();
            m_exitButton = rect.FindPath(uiProperty.EXIT_BUTTON).GetComponent<Button>();
            m_levelName = rect.FindPath(uiProperty.LEVEL_NAME).GetComponent<TextMeshProUGUI>();
            m_anthorName = rect.FindPath(uiProperty.AUTHOR_NAME).GetComponent<TextMeshProUGUI>();
            m_dateTime = rect.FindPath(uiProperty.DATE_TIME).GetComponent<TextMeshProUGUI>();
            m_instroduction = rect.FindPath(uiProperty.INSTRODUCTION).GetComponent<TextMeshProUGUI>();
        }
    }
}