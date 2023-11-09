using System.Collections.Generic;
using System.Linq;
using Frame.StateMachine;
using Frame.Static.Extensions;
using Frame.Tool.Pool;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace LevelEditor
{
    public class MouseSelecteState : AdditiveState
{
    private RectTransform GetSelectionUIRect => m_information.GetUI.GetControlHandlePanel.GetSelectionRect;

    private Image GetSelectionImage => m_information.GetUI.GetControlHandlePanel.GetSelectionImage;
    private Transform GetCameraTransform => Camera.main.transform;

    private OutlinePainter GetOutlinePainter => m_information.GetOutlinePainter;
    private OUTLINEMODE GetOutlineMode => m_information.GetUI.GetControlHandlePanel.GetOutlineProperty.OUTLINE_MODE;

    private Color GetOutlineColor => m_information.GetUI.GetControlHandlePanel.GetOutlineProperty.OUTLINE_COLOR;

    private float GetOutlineWidth => m_information.GetUI.GetControlHandlePanel.GetOutlineProperty.OUTLINE_WIDTH;

    private Vector2 GetSelectionMinSize => m_information.GetUI.GetControlHandlePanel.GetSelectionProperty.SELECTION_MIN_SIZE;

    private Color GetSelectionColor => m_information.GetUI.GetControlHandlePanel.GetSelectionProperty.SELECTION_COLOR;

    private bool GetShiftButton => m_information.GetInput.GetShiftButton;

    private bool GetCtrlButton => m_information.GetInput.GetCtrlButton;

    private ItemTransformPanel GetItemTransformPanel => m_information.GetUI.GetItemTransformPanel;

    private GameObject m_emptyObj;

    private BoxCollider2D m_selectCollider;
    private Vector3 GetMousePosition => m_information.GetMousePosition
        .NewX(Mathf.Clamp(Mouse.current.position.ReadValue().x, 0, Screen.width))
        .NewY(Mathf.Clamp(Mouse.current.position.ReadValue().y, 0, Screen.height));

    private ObservableList<ItemData> TargetList => m_information.TargetItems;

    private List<Collider2D> m_selectList = new List<Collider2D>();

    private ObservableList<ItemData> ItemAssets => m_information.ItemAssets;

    private CommandExcute GetExcute => m_information.GetLevelEditorCommandExcute;
    
    private Vector2 m_originMousePositon;

    private Vector2 m_currentMousePosition;

    private float selectUiWidth, SelectUiHeight;
    
    private static ContactFilter2D m_contactFilter2D = new ContactFilter2D();
    
    public MouseSelecteState(BaseInformation information, MotionCallBack motionCallBack) : base(information, motionCallBack)
    {
        StateInit();
        SetSelectRectUI();
        SetSelectColliderBox();
        GetSelectionUIRect.gameObject.SetActive(true);
    }
    
    

    public override void Motion(BaseInformation information)
    {
        if (m_information.GetInput.GetMouseLeftButtonUp)
        {
            ReturnTargetList();
            StateDestroy();
            RemoveState();
            return;
        }
        SetSelectRectUI();
        SetSelectColliderBox();
    }

    private void SetSelectRectUI()
    {
        m_currentMousePosition = GetMousePosition;
        Vector2 centerMousePositon = (m_originMousePositon + m_currentMousePosition) / 2;
        GetSelectionUIRect.anchoredPosition = centerMousePositon;
        selectUiWidth = Mathf.Abs(m_currentMousePosition.x - m_originMousePositon.x);
        SelectUiHeight = Mathf.Abs(m_currentMousePosition.y - m_originMousePositon.y);
        GetSelectionUIRect.sizeDelta = new Vector2(selectUiWidth, SelectUiHeight);
    }

    private void SetSelectColliderBox()
    {
        Vector2 selectColliderCenter = ScreenToWorldPoint((m_currentMousePosition + m_originMousePositon)/2);
        float frustumHeight = ScreenToWorldPoint(new Vector2(0, Screen.height)).y
                              - ScreenToWorldPoint(new Vector2(0, 0)).y;
        float frustumWidth = ScreenToWorldPoint(new Vector2(Screen.width, 0)).x
                             - ScreenToWorldPoint(new Vector2(0, 0)).x;
        float selectColliderWidth = selectUiWidth / Screen.width * frustumWidth;
        float selectColliderHeight = SelectUiHeight / Screen.height * frustumHeight;

        selectColliderWidth = Mathf.Clamp(selectColliderWidth, GetSelectionMinSize.x, selectColliderWidth);
        selectColliderHeight = Mathf.Clamp(selectColliderHeight, GetSelectionMinSize.y, selectColliderHeight);
        
        m_emptyObj.transform.position = selectColliderCenter;

        m_selectCollider.size = new Vector2(selectColliderWidth, selectColliderHeight);
        
        List<Collider2D> colliders = new List<Collider2D>();

        m_selectCollider.OverlapCollider(m_contactFilter2D,colliders);
        
        m_selectList.Clear();
        
        m_selectList.AddRange(colliders);
    }

    private void StateInit()
    {
        GetSelectionImage.color = GetSelectionColor;
        m_originMousePositon = GetMousePosition;
        m_emptyObj = ObjectPool.Instance.OnTake(m_information.GetEmptyGameObject);
        m_selectCollider = m_emptyObj.GetComponent<BoxCollider2D>() == null
            ? m_emptyObj.AddComponent<BoxCollider2D>()
            : m_emptyObj.GetComponent<BoxCollider2D>();
        m_selectCollider.enabled = true;
    }

    private void StateDestroy()
    {
        GetSelectionUIRect.gameObject.SetActive(false);
        m_selectCollider.enabled = false;
        ObjectPool.Instance.OnRelease(m_emptyObj);
    }

    private Vector3 ScreenToWorldPoint(Vector2 screenPoint)
    {
        Vector3 worldPoint = screenPoint;
        return Camera.main.ScreenToWorldPoint(worldPoint.NewZ(Mathf.Abs(GetCameraTransform.position.z)));
    }

    private void ReturnTargetList()
    {
        List<ItemData> tempList = new List<ItemData>();
        if (GetShiftButton)
        {
            tempList.AddRange(ItemAssets.CheckItemObjs(GetOutlinePainter.GetTargetObj));
            tempList.AddRange(ChangeCollidersToDatas(m_selectList));
        }
        else if (GetCtrlButton)
        {
            if (m_selectCollider.size == GetSelectionMinSize)
            {
                tempList.AddRange(ItemAssets.CheckItemObjs(GetOutlinePainter.GetTargetObj));
                foreach (var collider in m_selectList)
                {
                    ItemData itemData = ItemAssets.CheckItemObj(collider.gameObject);
                    if (tempList.Contains(itemData))
                    {
                        tempList.Remove(itemData);
                    }
                    else
                    {
                        tempList.Add(itemData);
                    }
                }
            }
            else
            {
                tempList.AddRange(ItemAssets.CheckItemObjs(GetOutlinePainter.GetTargetObj));
                foreach (var collider in m_selectList)
                {
                    ItemData itemData = ItemAssets.CheckItemObj(collider.gameObject);
                    tempList.Remove(itemData);
                }
            }
        }
        else
        {
            tempList.AddRange(ChangeCollidersToDatas(m_selectList));
        }
        tempList = tempList.Distinct().ToList();
        GetOutlinePainter.SetTargetObj = tempList.GetItemObjs();
        GetExcute?.Invoke(new ItemSelectCommand(TargetList,tempList,GetOutlinePainter));
    }

    private List<ItemData> ChangeCollidersToDatas(List<Collider2D> colliders)
    {
        List<ItemData> tempList = new List<ItemData>();
        foreach (var collider in colliders)
        {
            ItemData itemData = ItemAssets.CheckItemObj(collider.gameObject);
            if (itemData != null)
            {
                tempList.Add(itemData);
            }
        }

        return tempList;
    }
}

}