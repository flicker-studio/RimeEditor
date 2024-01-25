using System.Collections.Generic;
using System.Linq;
using Frame.StateMachine;
using Frame.Tool.Pool;
using Moon.Kernel.Extension;
using UnityEngine;
using UnityEngine.UI;
using RectTransform = UnityEngine.RectTransform;

namespace LevelEditor
{
    public class MouseSelecteState : AdditiveState
    {
        private RectTransform GetSelectionUIRect => m_information.GetUI.GetControlHandlePanel.GetSelectionRect;

        private Image GetSelectionImage => m_information.GetUI.GetControlHandlePanel.GetSelectionImage;
        private Transform GetCameraTransform => Camera.main.transform;

        private OutlinePainter GetOutlinePainter => m_information.GetCamera.GetOutlinePainter;

        private Vector2 GetSelectionMinSize => m_information.GetUI.GetControlHandlePanel.GetSelectionProperty.SELECTION_MIN_SIZE;

        private Color GetSelectionColor => m_information.GetUI.GetControlHandlePanel.GetSelectionProperty.SELECTION_COLOR;

        private bool GetShiftButton => m_information.GetInput.GetShiftButton;

        private bool GetCtrlButton => m_information.GetInput.GetCtrlButton;

        private Vector2 GetScreenScale => m_information.GetCamera.GetScreenScale;

        private GameObject m_selectObj;

        private BoxCollider2D m_selectCollider;

        private Vector3 GetMousePosition => m_information.GetCamera.GetMousePosition
            .NewX(Mathf.Clamp(m_information.GetCamera.GetMousePosition.x * GetScreenScale.x, 0, Screen.width * GetScreenScale.x))
            .NewY(Mathf.Clamp(m_information.GetCamera.GetMousePosition.y * GetScreenScale.y, 0, Screen.height * GetScreenScale.y));

        private ObservableList<ItemData> TargetList => m_information.GetData.TargetItems;

        private List<Collider2D> m_selectList = new List<Collider2D>();

        private ObservableList<ItemData> ItemAssets => m_information.GetData.ItemAssets;

        private CommandExcute GetExcute => m_information.GetCommandSet.GetExcute;

        private Vector2 m_originMousePositon;

        private Vector2 m_currentMousePosition;

        private float selectUiWidth, SelectUiHeight;

        private static ContactFilter2D m_contactFilter2D = new ContactFilter2D();

        public MouseSelecteState(BaseInformation information, MotionCallBack motionCallBack) : base(information, motionCallBack)
        {
            StateInit();
            SetSelectRectUI();
            SetSelectCollider();
            SelectTarget();
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
            SetSelectCollider();
            SelectTarget();
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

        private void SetSelectCollider()
        {
            Vector2 m_currentMouseScreenPosition = new Vector2(m_currentMousePosition.x / GetScreenScale.x,
                m_currentMousePosition.y / GetScreenScale.y);

            Vector2 m_originMouseScreenPositon = new Vector2(m_originMousePositon.x / GetScreenScale.x,
                m_originMousePositon.y / GetScreenScale.y);

            Vector2 selectColliderCenter = ScreenToWorldPoint((m_currentMouseScreenPosition + m_originMouseScreenPositon) / 2);

            float frustumHeight = ScreenToWorldPoint(new Vector2(0, Screen.height)).y
                                  - ScreenToWorldPoint(new Vector2(0, 0)).y;

            float frustumWidth = ScreenToWorldPoint(new Vector2(Screen.width, 0)).x
                                 - ScreenToWorldPoint(new Vector2(0, 0)).x;

            float selectColliderWidth = selectUiWidth / (Screen.width) * frustumWidth / GetScreenScale.x;
            float selectColliderHeight = SelectUiHeight / (Screen.height) * frustumHeight / GetScreenScale.y;

            selectColliderWidth = Mathf.Clamp(selectColliderWidth, GetSelectionMinSize.x, selectColliderWidth);
            selectColliderHeight = Mathf.Clamp(selectColliderHeight, GetSelectionMinSize.y, selectColliderHeight);

            m_selectObj.transform.position = selectColliderCenter;

            m_selectCollider.size = new Vector2(selectColliderWidth, selectColliderHeight);
        }

        private void SelectTarget()
        {
            if (m_currentMousePosition.x <= m_originMousePositon.x)
            {
                SelectTargetRightToLeft();
            }
            else
            {
                SelectTargetLeftToRight();
            }
        }

        private void SelectTargetRightToLeft()
        {
            List<Collider2D> colliders = new List<Collider2D>();

            m_selectCollider.OverlapCollider(m_contactFilter2D, colliders);

            m_selectList.Clear();

            m_selectList.AddRange(colliders);
        }

        private void SelectTargetLeftToRight()
        {
            List<Collider2D> colliders = new List<Collider2D>();

            m_selectCollider.OverlapCollider(m_contactFilter2D, colliders);

            m_selectList.Clear();

            foreach (var collider in colliders)
            {
                Bounds targetBounds = collider.GetComponent<MeshRenderer>().bounds;

                if (m_selectCollider.bounds.Contains(targetBounds.max.NewZ(m_selectObj.transform.position.z)) &&
                    m_selectCollider.bounds.Contains(targetBounds.min.NewZ(m_selectObj.transform.position.z)))
                {
                    m_selectList.Add(collider);
                }
            }
        }

        private void StateInit()
        {
            GetSelectionImage.color = GetSelectionColor;
            m_originMousePositon = GetMousePosition;
            m_selectObj = ObjectPool.Instance.OnTake(m_information.GetPrefab.GetEmptyGameObject);

            m_selectCollider = m_selectObj.GetComponent<BoxCollider2D>() == null
                ? m_selectObj.AddComponent<BoxCollider2D>()
                : m_selectObj.GetComponent<BoxCollider2D>();

            m_selectCollider.enabled = true;
        }

        private void StateDestroy()
        {
            GetSelectionUIRect.gameObject.SetActive(false);
            m_selectCollider.enabled = false;
            ObjectPool.Instance.OnRelease(m_selectObj);
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
            GetExcute?.Invoke(new ItemSelectCommand(TargetList, tempList, GetOutlinePainter));
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