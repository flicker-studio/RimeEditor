using System.Collections.Generic;
using Frame.StateMachine;

namespace LevelEditor.State
{
    /// <summary>
    ///     The state-machine is switched to edit-mode
    /// </summary>
    public sealed class EditorState : AdditiveState
    {
        private List<AbstractItem> TargetItems => m_information.DataManager.TargetItems;

        public EditorState(BaseInformation baseInformation, MotionCallBack motionCallBack) : base(baseInformation, motionCallBack)
        {
            ChangeMotionState(typeof(ActionPanelShowState));
            ChangeMotionState(typeof(ControlHandlePanelShowState));
            ChangeMotionState(typeof(HierarchyPanelShowState));
            ChangeMotionState(typeof(AreaPanelShowState));
            ChangeMotionState(typeof(LevelPanelShowState));
        }

        /// <inheritdoc />
        public override void Motion(BaseInformation information)
        {
        }

        private void ShowTransformPanel()
        {
            if (TargetItems.Count > 0 && !CheckStates.Contains(typeof(ItemTransformPanelShowState)))
                ChangeMotionState(typeof(ItemTransformPanelShowState));

            if (TargetItems.Count > 0 && !CheckStates.Contains(typeof(InspectorShowState)))
                ChangeMotionState(typeof(InspectorShowState));
        }
    }
}