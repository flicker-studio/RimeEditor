using Frame.StateMachine;
using LevelEditor;
using RimeEditor.Runtime;
using TMPro;
using UnityEngine.UI;

public class AreaPanelShowState : AdditiveState
{
    public AreaPanelShowState(Information baseInformation, MotionCallBack motionCallBack) : base(baseInformation, motionCallBack)
    {
        InitEvents();
        InitState();
    }

    private BrowseController GetController   => m_information.Controller;
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
        GetController.SwitchSubLevel(index);
    }

    private void AddLevel()
    {
        GetController.AddSubLevel();
        GetAreaDropdown.options.Add(new TMP_Dropdown.OptionData(GetController.CurrentSubLevel.Name));
        GetAreaDropdown.value = GetController.CurrentSubLevelIndex;
    }

    private void AddLevel(string levelName)
    {
        GetAreaDropdown.options.Add(new TMP_Dropdown.OptionData(levelName));
    }

    private void DeleteLevel()
    {
        if (GetController.ShowSubLevels().Count <= 1) return;

        GetAreaDropdown.options.RemoveAt(GetController.CurrentSubLevelIndex);
        GetController.DeleteSubLevel();
        GetAreaDropdown.value = GetController.CurrentSubLevelIndex;
        GetAreaDropdown.RefreshShownValue();
    }

    private void ReloadLevel()
    {
        GetAreaDropdown.ClearOptions();
        var subLevelDatas = GetController.CurrentCustomLevel.SubLevelDataList;

        for (var index = 0; index < subLevelDatas.Count; index++) AddLevel(subLevelDatas[index].Name);

        GetAreaDropdown.value = 0;
        GetAreaDropdown.RefreshShownValue();
    }
}