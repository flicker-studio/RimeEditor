using System.Collections.Generic;
using Frame.StateMachine;
using LevelEditor;

public class PanelDefultState : AdditiveState
{
    private ObservableList<ItemData> TargetItems => m_information.GetData.TargetItems;
    
    public PanelDefultState(BaseInformation baseInformation, MotionCallBack motionCallBack) : base(baseInformation, motionCallBack)
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
    
    private void ShowTransformPanel(List<ItemData> itemDatas)
    {
        ShowTransformPanel();
    }
    
    private void ShowTransformPanel(ItemData itemData)
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
