using System.Collections.Generic;
using Frame.StateMachine;
using LevelEditor;

public class EditorViewState : AdditiveState
{
    private ObservableList<AbstractItem> TargetItems; //=> m_information.DataManager.TargetItems;

    public EditorViewState(BaseInformation baseInformation, MotionCallBack motionCallBack) : base(baseInformation, motionCallBack)
    {
        ChangeMotionState(typeof(ActionPanelShowState));
        ChangeMotionState(typeof(ControlHandlePanelShowState));
        ChangeMotionState(typeof(HierarchyPanelShowState));
        ChangeMotionState(typeof(AreaPanelShowState));
        ChangeMotionState(typeof(LevelPanelShowState));

        TargetItems.OnAddRange += ShowTransformPanel;
        TargetItems.OnAdd      += ShowTransformPanel;
    }


    public override void Motion(BaseInformation information)
    {
    }

    private void ShowTransformPanel(List<AbstractItem> itemDatas)
    {
        ShowTransformPanel();
    }

    private void ShowTransformPanel(AbstractItem abstractItem)
    {
        ShowTransformPanel();
    }

    private void ShowTransformPanel()
    {
        if (TargetItems.Count > 0 && !CheckStates.Contains(typeof(ItemTransformPanelShowState)))
            ChangeMotionState(typeof(ItemTransformPanelShowState));

        if (TargetItems.Count > 0 && !CheckStates.Contains(typeof(InspectorShowState)))
            ChangeMotionState(typeof(InspectorShowState));
    }
}