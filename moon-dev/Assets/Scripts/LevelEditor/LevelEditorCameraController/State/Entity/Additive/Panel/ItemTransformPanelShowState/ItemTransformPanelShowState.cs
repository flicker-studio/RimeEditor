using System.Collections;
using System.Collections.Generic;
using Frame.StateMachine;
using Frame.Tool;
using UnityEngine;

namespace LevelEditor
{
    public class ItemTransformPanelShowState : AdditiveState
{
    private ObservableList<ItemData> TargetItems => m_information.TargetItems;

    private List<GameObject> TargetObjs => m_information.TargetObjs;

    private CommandExcute GetExcute => m_information.GetLevelEditorCommandExcute;

    private ItemTransformPanel GetItemTransformPanel => m_information.GetUI.GetItemTransformPanel;

    private bool GetPositionChange => m_information.GetUI.GetItemTransformPanel.GetPositionChange;

    private bool GetRotationChange => m_information.GetUI.GetItemTransformPanel.GetRotationChange;

    private bool GetScaleChange => m_information.GetUI.GetItemTransformPanel.GetScaleChange;

    private List<Vector3> m_lastPositon = new List<Vector3>();

    private List<Quaternion> m_lastRotation = new List<Quaternion>();

    private List<Vector3> m_lastScale = new List<Vector3>();
    
    public ItemTransformPanelShowState(BaseInformation baseInformation, MotionCallBack motionCallBack) : base(baseInformation, motionCallBack)
    {
        // EventCenterManager.Instance.AddEventListener(GameEvent.UNDO_AND_REDO,SetFieldUIValue);
    }

    public override void Motion(BaseInformation information)
    {
        CheckPositionChange();
        CheckRotationChange();
        CheckScaleChange();
        ShowTransformPanel();
    }

    private void CheckPositionChange()
    {
        if (!GetPositionChange) return;
        List<Vector3> nextPosition = new List<Vector3>();
        Vector3 value = GetItemTransformPanel.GetPosition;
        for (int i = 0; i < TargetObjs.Count; i++)
        {
            GameObject target = TargetObjs[i];
            nextPosition.Add(new(float.IsNaN(value.x) ? target.transform.position.x : value.x,
                float.IsNaN(value.y) ? target.transform.position.y : value.y,
                float.IsNaN(value.z) ? target.transform.position.z : value.z));
        }
        GetExcute?.Invoke(new ItemPositionCommand(TargetItems,m_lastPositon,nextPosition));
    }

    private void CheckRotationChange()
    {
        if (!GetRotationChange) return;
        List<Vector3> nextPosition = new List<Vector3>();
        nextPosition.AddRange(m_lastPositon);
        List<Quaternion> nextRotation = new List<Quaternion>();
        Vector3 value = GetItemTransformPanel.GetRotation;
        for (int i = 0; i < TargetObjs.Count; i++)
        {
            GameObject target = TargetObjs[i];
            nextRotation.Add(Quaternion.Euler(new(float.IsNaN(value.x) ? target.transform.position.x : value.x,
                float.IsNaN(value.y) ? target.transform.position.y : value.y,
                float.IsNaN(value.z) ? target.transform.position.z : value.z)));
        }
        GetExcute?.Invoke(new ItemRotationCommand(TargetItems,m_lastPositon,nextPosition,m_lastRotation,nextRotation));
    }

    private void CheckScaleChange()
    {
        if (!GetScaleChange) return;
        List<Vector3> nextScale = new List<Vector3>();
        Vector3 value = GetItemTransformPanel.GetScale;
        for (int i = 0; i < TargetObjs.Count; i++)
        {
            GameObject target = TargetObjs[i];
            nextScale.Add(new(float.IsNaN(value.x) ? target.transform.localScale.x : value.x,
                float.IsNaN(value.y) ? target.transform.localScale.y : value.y,
                float.IsNaN(value.z) ? target.transform.localScale.z : value.z));
        }
        GetExcute?.Invoke(new ItemScaleCommand(TargetItems,m_lastScale,nextScale));
    }

    private (List<Vector3>, List<Quaternion>, List<Vector3>) GetTransformPropertyFromGameObjectList(
        List<GameObject> gameobjectList)
    {
        List<Vector3> positionList = new List<Vector3>();
        List<Quaternion> rotationList = new List<Quaternion>();
        List<Vector3> scaleList = new List<Vector3>();
        foreach (var obj in gameobjectList)
        {
            positionList.Add(obj.transform.position);
            rotationList.Add(obj.transform.rotation);
            scaleList.Add(obj.transform.localScale);
        }

        return (positionList, rotationList, scaleList);
    }

    private void ShowTransformPanel()
    {
        if (TargetObjs.Count == 0) return;
        if (GetItemTransformPanel.GetOnSelect)
        {
            SetItemValue();
            return;
        }
        (m_lastPositon,m_lastRotation,m_lastScale) = GetTransformPropertyFromGameObjectList(TargetObjs);
        SetFieldUIValue();
    }

    private void SetFieldUIValue()
    {
        if (TargetObjs.Count == 0) return;
        Vector3 position = TargetObjs[0].transform.position;
        Vector3 rotation = TargetObjs[0].transform.rotation.eulerAngles;
        Vector3 scale = TargetObjs[0].transform.localScale;
        for (var i = 1; i < TargetObjs.Count; i++)
        {
            if (position.x != TargetObjs[i].transform.position.x) position.x = float.NaN;
            if (position.y != TargetObjs[i].transform.position.y) position.y = float.NaN;
            if (position.z != TargetObjs[i].transform.position.z) position.z = float.NaN;
            if (rotation.x != TargetObjs[i].transform.rotation.eulerAngles.x) rotation.x = float.NaN;
            if (rotation.y != TargetObjs[i].transform.rotation.eulerAngles.y) rotation.y = float.NaN;
            if (rotation.z != TargetObjs[i].transform.rotation.eulerAngles.z) rotation.z = float.NaN;
            if (scale.x != TargetObjs[i].transform.localScale.x) scale.x = float.NaN;
            if (scale.y != TargetObjs[i].transform.localScale.y) scale.y = float.NaN;
            if (scale.z != TargetObjs[i].transform.localScale.z) scale.z = float.NaN;
        }

        GetItemTransformPanel.SetPosition = position;
        GetItemTransformPanel.SetRotation = rotation;
        GetItemTransformPanel.SetScale = scale;
    }

    private void SetItemValue()
    {
        SetPosition();
        SetRotation();
        SetScale();
    }

    private void SetPosition()
    {
        (string inputFieldX,string inputFieldY,string inputFieldZ) = GetItemTransformPanel.GetPositionField;
        bool canParseX = float.TryParse(inputFieldX,out float valueX);
        bool chnParseY = float.TryParse(inputFieldY,out float valueY);
        bool chnParseZ = float.TryParse(inputFieldZ,out float valueZ);
        for (int i = 0; i < TargetObjs.Count; i++)
        {
            GameObject target = TargetObjs[i];
            target.transform.position = new(
                canParseX ? valueX : target.transform.position.x,
                chnParseY ? valueY : target.transform.position.y,
                chnParseZ ? valueZ : target.transform.position.z);
        }
    }

    private void SetRotation()
    {
        (string inputFieldX,string inputFieldY,string inputFieldZ) = GetItemTransformPanel.GetRotationField;
        bool canParseX = float.TryParse(inputFieldX,out float valueX);
        bool chnParseY = float.TryParse(inputFieldY,out float valueY);
        bool chnParseZ = float.TryParse(inputFieldZ,out float valueZ);
        for (int i = 0; i < TargetObjs.Count; i++)
        {
            GameObject target = TargetObjs[i];
            target.transform.rotation = Quaternion.Euler(new(
                canParseX ? valueX : target.transform.rotation.x,
                chnParseY ? valueY : target.transform.rotation.y,
                chnParseZ ? valueZ : target.transform.rotation.z));
        }
    }

    private void SetScale()
    {
        (string inputFieldX,string inputFieldY,string inputFieldZ) = GetItemTransformPanel.GetScaleField;
        bool canParseX = float.TryParse(inputFieldX,out float valueX);
        bool chnParseY = float.TryParse(inputFieldY,out float valueY);
        bool chnParseZ = float.TryParse(inputFieldZ,out float valueZ);
        for (int i = 0; i < TargetObjs.Count; i++)
        {
            GameObject target = TargetObjs[i];
            target.transform.localScale = new(
                canParseX ? valueX : target.transform.localScale.x,
                chnParseY ? valueY : target.transform.localScale.y,
                chnParseZ ? valueZ : target.transform.localScale.z);
        }
    }
}

}