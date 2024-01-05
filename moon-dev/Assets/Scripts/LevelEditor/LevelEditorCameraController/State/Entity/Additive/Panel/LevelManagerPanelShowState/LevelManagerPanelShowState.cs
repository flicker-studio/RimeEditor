using Frame.StateMachine;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LevelEditor
{
    public class LevelManagerPanelShowState : AdditiveState
    {
        private LevelManagerPanel GetLevelManagerPanel => m_information.GetUI.GetLevelManagerPanel;
        
        private RectTransform GetLevelManagerRoot => GetLevelManagerPanel.GetLevelManagerRootRect;
        
        private RectTransform GetLevelListContent => GetLevelManagerPanel.GetLevelListContentRect;
        
        private RectTransform GetFullPanel => GetLevelManagerPanel.GetFullPanelRect;

        private Button GetOpenButton => GetLevelManagerPanel.GetOpenButton;

        private Button GetCreateButton => GetLevelManagerPanel.GetCreateButton;

        private Button GetDeclarationButton => GetLevelManagerPanel.GetDeclarationButton;

        private Button GetExitButton => GetLevelManagerPanel.GetExitButton;

        private TextMeshProUGUI GetLevelName => GetLevelManagerPanel.GetLevelName;

        private TextMeshProUGUI GetAnthorName => GetLevelManagerPanel.GetAnthorName;

        private TextMeshProUGUI GetDateTime => GetLevelManagerPanel.GetDateTime;

        private TextMeshProUGUI GetInstroduction => GetLevelManagerPanel.GetInstroduction;
        
        public LevelManagerPanelShowState(BaseInformation baseInformation, MotionCallBack motionCallBack) : base(baseInformation, motionCallBack)
        {
            InitState();
            InitEvent();
        }

        private void InitState()
        {
            GetFullPanel.gameObject.SetActive(true);
            GetLevelManagerRoot.gameObject.SetActive(true);
        }
        
        private void InitEvent()
        {
            GetCreateButton.onClick.AddListener(JumpToEditorViewState);
        }

        private void RemoveEvent()
        {
            GetCreateButton.onClick.RemoveAllListeners();
        }

        protected override void RemoveState()
        {
            base.RemoveState();
            RemoveEvent();
            GetLevelManagerRoot.gameObject.SetActive(false);
            GetFullPanel.gameObject.SetActive(false);
        }

        private void JumpToEditorViewState()
        {
            ChangeMotionState(typeof(EditorViewState));
            RemoveState();
        }

        public override void Motion(BaseInformation information)
        {
            
        }
    }

}