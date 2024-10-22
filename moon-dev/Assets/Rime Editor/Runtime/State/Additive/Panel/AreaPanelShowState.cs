using Frame.StateMachine;
using LevelEditor;
using TMPro;
using UnityEngine.UI;

public class AreaPanelShowState : AdditiveState
{
    public AreaPanelShowState(Information baseInformation, MotionCallBack motionCallBack) : base(baseInformation, motionCallBack)
    {
        InitEvents();
        InitState();
    }

    private LevelDataManager GetDataManager  => m_information.DataManager;
    private TMP_Dropdown     GetAreaDropdown => m_information.UIManager.GetAreaPanel.GetAreaDropdown;

    private Button GetAddButton => m_information.UIManager.GetAreaPanel.GetAddButton;

    private Button GetDeleteButton => m_information.UIManager.GetAreaPanel.GetDeleteButton;

    private Button GetManageButton => m_information.UIManager.GetAreaPanel.GetManageButton;

    private Button GetAreaSettingButton => m_information.UIManager.GetAreaPanel.GetAreaSettingButton;

    private Button GetEnvironmentSetting => m_information.UIManager.GetAreaPanel.GetEnvironmentSettingButton;

    public override void Motion(Information information)
    {
    }

    private void InitState()
    {
        ReloadLevel();
    }

    protected override void RemoveState()
    {
        base.RemoveState();
    }

    private void InitEvents()
    {
        GetAreaDropdown.onValueChanged.AddListener(value => SetLevelIndex(value));
        GetAddButton.onClick.AddListener(AddLevel);
        GetDeleteButton.onClick.AddListener(DeleteLevel);
    }

    private void SetLevelIndex(int index)
    {
        GetDataManager.SetSubLevelIndex(index);
    }

    private void AddLevel()
    {
        GetDataManager.AddSubLevel();
        GetAreaDropdown.options.Add(new TMP_Dropdown.OptionData(((SubLevel)GetDataManager.CurrentSubLevel).Name));
        GetAreaDropdown.value = GetDataManager.CurrentSubLevelIndex;
    }

    private void AddLevel(string levelName)
    {
        GetAreaDropdown.options.Add(new TMP_Dropdown.OptionData(levelName));
    }

    private void DeleteLevel()
    {
        if (GetDataManager.ShowSubLevels().Count <= 1) return;

        GetAreaDropdown.options.RemoveAt(GetDataManager.CurrentSubLevelIndex);
        GetDataManager.DeleteSubLevel();
        GetAreaDropdown.value = GetDataManager.CurrentSubLevelIndex;
        GetAreaDropdown.RefreshShownValue();
    }

    private void ReloadLevel()
    {
        GetAreaDropdown.ClearOptions();
        var subLevelDatas = GetDataManager.CurrentLevel.SubLevelDataList;

        for (var index = 0; index < subLevelDatas.Count; index++) AddLevel(subLevelDatas[index].Name);

        GetAreaDropdown.value = 0;
        GetAreaDropdown.RefreshShownValue();
    }
}