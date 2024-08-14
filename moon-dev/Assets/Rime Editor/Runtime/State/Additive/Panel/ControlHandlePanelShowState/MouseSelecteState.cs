using System;
using System.Collections.Generic;
using System.Linq;
using Frame.StateMachine;
using LevelEditor.Command;
using LevelEditor.Extension;
using LevelEditor.Item;
using LevelEditor.Manager;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace LevelEditor
{
    public class MouseSelecteState : AdditiveState
    {
        private static readonly ContactFilter2D m_contactFilter2D = new();

        private readonly List<Collider2D> m_selectList = new();

        private Vector2 m_currentMousePosition;

        private Vector2 m_originMousePositon;

        private BoxCollider2D m_selectCollider;

        private GameObject m_selectObj;

        private float selectUiWidth, SelectUiHeight;

        public MouseSelecteState(Information information, MotionCallBack motionCallBack) : base(information, motionCallBack)
        {
            StateInit();
            SetSelectRectUI();
            SetSelectCollider();
            SelectTarget();
            GetSelectionUIRect.gameObject.SetActive(true);
        }

        private RectTransform GetSelectionUIRect => m_information.UIManager.GetControlHandlePanel.GetSelectionRect;

        private Image     GetSelectionImage  => m_information.UIManager.GetControlHandlePanel.GetSelectionImage;
        private Transform GetCameraTransform => Camera.main.transform;

        private OutlineManager GetOutlinePainter   => m_information.OutlineManager;
        private Vector2        GetSelectionMinSize => m_information.UIManager.GetControlHandlePanel.GetSelectionProperty.SELECTION_MIN_SIZE;

        private Color GetSelectionColor => m_information.UIManager.GetControlHandlePanel.GetSelectionProperty.SELECTION_COLOR;

        private bool GetShiftButton => throw new InvalidOperationException(); //m_information.InputManager.GetShiftButton;

        private bool GetCtrlButton => throw new InvalidOperationException(); //m_information.InputManager.GetCtrlButton;

        private Vector2 GetScreenScale => m_information.CameraManager.ScreenScale;

        private Vector3 GetMousePosition => m_information.CameraManager.MousePosition
                                                         .NewX(Mathf.Clamp(m_information.CameraManager.MousePosition.x * GetScreenScale.x, 0,
                                                                           Screen.width * GetScreenScale.x))
                                                         .NewY(Mathf.Clamp(m_information.CameraManager.MousePosition.y * GetScreenScale.y, 0,
                                                                           Screen.height * GetScreenScale.y));

        private List<ItemBase> SelectedList => m_information.Controller.SelectedItems;

        private List<ItemBase> ItemAssets => m_information.Controller.ItemAssets;

        public override void Motion(Information information)
        {
            // if (m_information.InputManager.GetMouseLeftButtonUp)
            // {
            //     ReturnTargetList();
            //     StateDestroy();
            //     RemoveState();
            //     return;
            // }

            SetSelectRectUI();
            SetSelectCollider();
            SelectTarget();
        }

        private void SetSelectRectUI()
        {
            m_currentMousePosition = GetMousePosition;
            var centerMousePositon = (m_originMousePositon + m_currentMousePosition) / 2;
            GetSelectionUIRect.anchoredPosition = centerMousePositon;
            selectUiWidth                       = Mathf.Abs(m_currentMousePosition.x - m_originMousePositon.x);
            SelectUiHeight                      = Mathf.Abs(m_currentMousePosition.y - m_originMousePositon.y);
            GetSelectionUIRect.sizeDelta        = new Vector2(selectUiWidth, SelectUiHeight);
        }

        private void SetSelectCollider()
        {
            var m_currentMouseScreenPosition = new Vector2(m_currentMousePosition.x / GetScreenScale.x,
                                                           m_currentMousePosition.y / GetScreenScale.y);

            var m_originMouseScreenPositon = new Vector2(m_originMousePositon.x / GetScreenScale.x,
                                                         m_originMousePositon.y / GetScreenScale.y);

            Vector2 selectColliderCenter = ScreenToWorldPoint((m_currentMouseScreenPosition + m_originMouseScreenPositon) / 2);

            var frustumHeight = ScreenToWorldPoint(new Vector2(0, Screen.height)).y
                              - ScreenToWorldPoint(new Vector2(0, 0)).y;

            var frustumWidth = ScreenToWorldPoint(new Vector2(Screen.width, 0)).x
                             - ScreenToWorldPoint(new Vector2(0,            0)).x;

            var selectColliderWidth  = selectUiWidth / Screen.width * frustumWidth / GetScreenScale.x;
            var selectColliderHeight = SelectUiHeight / Screen.height * frustumHeight / GetScreenScale.y;

            selectColliderWidth  = Mathf.Clamp(selectColliderWidth,  GetSelectionMinSize.x, selectColliderWidth);
            selectColliderHeight = Mathf.Clamp(selectColliderHeight, GetSelectionMinSize.y, selectColliderHeight);

            m_selectObj.transform.position = selectColliderCenter;

            m_selectCollider.size = new Vector2(selectColliderWidth, selectColliderHeight);
        }

        private void SelectTarget()
        {
            if (m_currentMousePosition.x <= m_originMousePositon.x)
                SelectTargetRightToLeft();
            else
                SelectTargetLeftToRight();
        }

        private void SelectTargetRightToLeft()
        {
            var colliders = new List<Collider2D>();

            m_selectCollider.OverlapCollider(m_contactFilter2D, colliders);

            m_selectList.Clear();

            m_selectList.AddRange(colliders);
        }

        private void SelectTargetLeftToRight()
        {
            var colliders = new List<Collider2D>();

            m_selectCollider.OverlapCollider(m_contactFilter2D, colliders);

            m_selectList.Clear();

            foreach (var collider in colliders)
            {
                var targetBounds = collider.GetComponent<MeshRenderer>().bounds;

                if (m_selectCollider.bounds.Contains(targetBounds.max.NewZ(m_selectObj.transform.position.z)) &&
                    m_selectCollider.bounds.Contains(targetBounds.min.NewZ(m_selectObj.transform.position.z)))
                    m_selectList.Add(collider);
            }
        }

        private void StateInit()
        {
            GetSelectionImage.color = GetSelectionColor;
            m_originMousePositon    = GetMousePosition;
            m_selectObj             = Object.Instantiate(m_information.PrefabManager.GetEmptyGameObject);

            m_selectCollider = m_selectObj.GetComponent<BoxCollider2D>() == null
                ? m_selectObj.AddComponent<BoxCollider2D>()
                : m_selectObj.GetComponent<BoxCollider2D>();

            m_selectCollider.enabled = true;
        }

        private void StateDestroy()
        {
            GetSelectionUIRect.gameObject.SetActive(false);
            m_selectCollider.enabled = false;
            Object.Destroy(m_selectObj);
        }

        private Vector3 ScreenToWorldPoint(Vector2 screenPoint)
        {
            Vector3 worldPoint = screenPoint;
            return Camera.main.ScreenToWorldPoint(worldPoint.NewZ(Mathf.Abs(GetCameraTransform.position.z)));
        }

        private void ReturnTargetList()
        {
            var tempList = new List<ItemBase>();

            if (GetShiftButton)
            {
                tempList.AddRange(ItemAssets.CheckItemObjs(GetOutlinePainter.RenderObject));
                tempList.AddRange(ChangeCollidersToDatas(m_selectList));
            }
            else if (GetCtrlButton)
            {
                if (m_selectCollider.size == GetSelectionMinSize)
                {
                    tempList.AddRange(ItemAssets.CheckItemObjs(GetOutlinePainter.RenderObject));

                    foreach (var collider in m_selectList)
                    {
                        var itemData = ItemAssets.CheckItemObj(collider.gameObject);

                        if (tempList.Contains(itemData))
                            tempList.Remove(itemData);
                        else
                            tempList.Add(itemData);
                    }
                }
                else
                {
                    tempList.AddRange(ItemAssets.CheckItemObjs(GetOutlinePainter.RenderObject));

                    foreach (var collider in m_selectList)
                    {
                        var itemData = ItemAssets.CheckItemObj(collider.gameObject);
                        tempList.Remove(itemData);
                    }
                }
            }
            else
            {
                tempList.AddRange(ChangeCollidersToDatas(m_selectList));
            }

            tempList = tempList.Distinct().ToList();
            GetOutlinePainter.SetRenderObjects(tempList.GetItemObjs());
            CommandInvoker.Execute(new Select(SelectedList, tempList, GetOutlinePainter));
        }

        private List<ItemBase> ChangeCollidersToDatas(List<Collider2D> colliders)
        {
            var tempList = new List<ItemBase>();

            foreach (var collider in colliders)
            {
                var itemData = ItemAssets.CheckItemObj(collider.gameObject);

                if (itemData != null) tempList.Add(itemData);
            }

            return tempList;
        }
    }
}