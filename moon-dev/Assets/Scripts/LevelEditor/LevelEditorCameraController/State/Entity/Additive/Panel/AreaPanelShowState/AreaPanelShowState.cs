using Frame.StateMachine;
using LevelEditor;
using TMPro;
using UnityEngine.UI;

public class AreaPanelShowState : AdditiveState
{
    private DataManager GetDataManager => m_information.GetData;
    private TMP_Dropdown GetAreaDropdown => m_information.GetUI.GetAreaPanel.GetAreaDropdown;

    private Button GetAddButton => m_information.GetUI.GetAreaPanel.GetAddButton;
    
    private Button GetDeleteButton => m_information.GetUI.GetAreaPanel.GetDeleteButton;

    private Button GetManageButton => m_information.GetUI.GetAreaPanel.GetManageButton;

    private Button GetAreaSettingButton => m_information.GetUI.GetAreaPanel.GetAreaSettingButton;

    private Button GetEnvironmentSetting => m_information.GetUI.GetAreaPanel.GetEnvironmentSettingButton;
    public AreaPanelShowState(BaseInformation baseInformation, MotionCallBack motionCallBack) : base(baseInformation, motionCallBack)
    {
        InitEvents();
    }

    public override void Motion(BaseInformation information)
    {
        InitState();
    }

    private void InitState()
    {
        GetDataManager.ReloadLevelAction += ReloadLevel;
    }
    
    protected override void RemoveState()
    {
        GetDataManager.ReloadLevelAction -= ReloadLevel;
        base.RemoveState();
    }

    private void InitEvents()
    {
        GetAreaDropdown.onValueChanged.AddListener((value) => SetLevelIndex(value));
        GetAddButton.onClick.AddListener(AddLevel);
        GetDeleteButton.onClick.AddListener(DeleteLevel);
    }

    private void SetLevelIndex(int index)
    {
        GetDataManager.SetLevelIndex(index);
    }

    private void AddLevel()
    {
        GetDataManager.AddLevel();
        GetAreaDropdown.options.Add(new TMP_Dropdown.OptionData(GetDataManager.GetCurrentSubLevel.Name));
        GetAreaDropdown.value = GetDataManager.GetCurrentIndex;
    }

    private void DeleteLevel()
    {
        if(GetDataManager.ShowLevels().Count <= 1) return;
        GetAreaDropdown.options.RemoveAt(GetDataManager.GetCurrentIndex);
        GetDataManager.DeleteLevel();
        GetAreaDropdown.value = GetDataManager.GetCurrentIndex;
        GetAreaDropdown.RefreshShownValue();
    }

    private void ReloadLevel()
    {
        GetAreaDropdown.value = 0;
    }
}
