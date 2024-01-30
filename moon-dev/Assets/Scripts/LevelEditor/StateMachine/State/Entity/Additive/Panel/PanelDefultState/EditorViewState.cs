using System.Collections.Generic;
using Frame.StateMachine;
using LevelEditor;

public class EditorViewState : AdditiveState
{
    private ObservableList<ItemDataBase> TargetItems => m_information.DataManager.TargetItems;

    public EditorViewState(BaseInformation baseInformation, MotionCallBack motionCallBack) : base(baseInformation, motionCallBack)
    {
        InitStates();
        InitEvents();
    }

    private void InitEvents()
    {
        TargetItems.OnAddRange += ShowTransformPanel;
        TargetItems.OnAdd += ShowTransformPanel;
    }

    private void InitStates()
    {
        ChangeMotionState(typeof(ActionPanelShowState));
        ChangeMotionState(typeof(ControlHandlePanelShowState));
        ChangeMotionState(typeof(HierarchyPanelShowState));
        ChangeMotionState(typeof(AreaPanelShowState));
        ChangeMotionState(typeof(LevelPanelShowState));
    }

    public override void Motion(BaseInformation information)
    {
    }

    private void ShowTransformPanel(List<ItemDataBase> itemDatas)
    {
        ShowTransformPanel();
    }

    private void ShowTransformPanel(ItemDataBase itemData)
    {
        ShowTransformPanel();
    }

    private void ShowTransformPanel()
    {
        if (TargetItems.Count > 0 && !CheckStates.Contains(typeof(ItemTransformPanelShowState)))
        {
            ChangeMotionState(typeof(ItemTransformPanelShowState));
        }

        if (TargetItems.Count > 0 && !CheckStates.Contains(typeof(InspectorShowState)))
        {
            ChangeMotionState(typeof(InspectorShowState));
        }
    }
}