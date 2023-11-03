using System;
using System.Collections.Generic;
using System.Linq;
using Frame.StateMachine;
using Frame.Tool.Pool;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LevelEditor
{
    public class ItemWarehousePanelShowState : AdditiveState
    {
        private ItemWarehousePanel GetItemWarehousePanel => m_information.GetUI.GetItemWarehousePanel;
        
        private Transform GetItemTypeGroup => GetItemWarehousePanel.GetItemTypeGroup;

        private Transform GetItemDetailGroupContent => GetItemWarehousePanel.GetItemDetailGroupContent;
        
        private GameObject GetItemDtailGroup => m_information.GetItemDetailGroup;

        private GameObject GetItemLattice => m_information.GetItemLattice;

        private GameObject GetItemType => m_information.GetItemType;

        private TextMeshProUGUI GetSelectPromptText => GetItemWarehousePanel.GetSelectPromptText;

        private GameObject GetPopoverPanelObj => GetItemWarehousePanel.GetPopoverPanelObj;
        
        private GameObject GetItemWarehousePanelObj => GetItemWarehousePanel.GetItemWarehousePanelObj;
        
        private Button GetCloseButton => GetItemWarehousePanel.GetCloseButton;
        
        private Button GetCreateButton => GetItemWarehousePanel.GetCreateButton;

        private Button GetClearSearchButton => GetItemWarehousePanel.GetClearSearchButton;

        private TMP_InputField GetSearchField => GetItemWarehousePanel.GetSearchField;

        private Scrollbar GetScrollbar => GetItemWarehousePanel.GetScrollbar;

        private string GetItemRootPath => GetItemWarehousePanel.GetItemRootPath;

        private string GetDetailGroupTextName => GetItemWarehousePanel.GetDetailGroupTextName;

        private string GetDetailGroupContentName => GetItemWarehousePanel.GetDetailGroupContentName;

        private string GetItemLatticeImageName => GetItemWarehousePanel.GetItemLatticeImageName;
        
        private string GetItemLatticeTextName => GetItemWarehousePanel.GetItemLatticeTextName;
        
        private string GetItemTypeTextName => GetItemWarehousePanel.GetItemTypeTextName;
        
        private List<GameObject> TargetList => m_information.TargetList;

        private OutlinePainter GetOutlinePainter => m_information.GetOutlinePainter;

        private Vector3 GetScreenMiddlePoint =>
            Camera.main.ScreenToWorldPoint(new Vector3(Screen.width/2, Screen.height/2,
                Mathf.Abs(Camera.main.transform.position.z)));

        private CommandExcute GetExcute => m_information.GetLevelEditorCommandExcute;

        private ItemProduct m_currentChoose;

        private static List<GameObject> m_searchItemProductObj = new List<GameObject>();
        
        private static GameObject m_searchItemGroupObj;

        private static Dictionary<ITEMTYPE, List<ItemProduct>> m_itemDictionary;

        private static List<GameObject> m_itemGroupObjList;
        
        public ItemWarehousePanelShowState(BaseInformation baseInformation, MotionCallBack motionCallBack) : base(baseInformation, motionCallBack)
        {
            ResetState();
            InitListener();
            SetPanelActive(true);
            LoadItemsFromPoject();
            LoadItemWarehouseFormItems();
        }

        public override void Motion(BaseInformation information)
        {
            
        }

        protected override void RemoveState()
        {
            base.RemoveState();
            SetPanelActive(false);
            GetScrollbar.value = 1f;
        }

        private void ResetState()
        {
            GetSelectPromptText.text = "";
            ClearSearch();
        }

        private void ClearSearch()
        {
            if(m_searchItemGroupObj != null)
            {
                m_searchItemGroupObj.SetActive(false);
            }
            if(m_itemGroupObjList == null) return;
            foreach (var itemGroupObj in m_itemGroupObjList)
            {
                itemGroupObj.SetActive(true);
            }

            GetSearchField.text = "";
        }

        private void SetPanelActive(bool active)
        {
            GetPopoverPanelObj.SetActive(active);
            GetItemWarehousePanelObj.SetActive(active);
        }

        private void InitListener()
        {
            GetCreateButton.interactable = false;
            
            if(m_itemDictionary != null) return;
            
            GetCreateButton.onClick.AddListener(() =>
            {
                CreateNewItem();
                RemoveState();
                return;
            });

            GetCloseButton.onClick.AddListener(() =>
            {
                RemoveState();
                return;
            });
            
            GetClearSearchButton.onClick.AddListener(() =>
            {
                ClearSearch();
            });
            
            GetSearchField.onSubmit.AddListener((value) =>
            {
                if (value == "")
                {
                    ClearSearch();
                }
                else
                {
                    SearchItem(value);
                }
            });
        }

        private void SearchItem(string searchValue)
        {
            foreach (var itemGroupObj in m_itemGroupObjList)
            {
                itemGroupObj.SetActive(false);
            }

            foreach (var itemProductObj in m_searchItemProductObj)
            {
                ObjectPool.Instance.OnRelease(itemProductObj);
                itemProductObj.GetComponent<Button>().onClick.RemoveAllListeners();
            }
            m_searchItemProductObj.Clear();
            
            if (m_searchItemGroupObj == null)
            {
                m_searchItemGroupObj = ObjectPool.Instance.OnTake(GetItemDtailGroup);
                m_searchItemGroupObj.transform.SetParent(GetItemDetailGroupContent);
            }
            m_searchItemGroupObj.SetActive(true);
            m_searchItemGroupObj.transform.Find(GetDetailGroupTextName)
                .GetComponent<TextMeshProUGUI>().text = searchValue;

            
            Transform content = m_searchItemGroupObj.transform.Find(GetDetailGroupContentName);
            
            foreach (var keyValuePair in m_itemDictionary)
            {
                foreach (var itemProduct in keyValuePair.Value)
                {
                    if (itemProduct.Name.Contains(searchValue))
                    {
                        m_searchItemProductObj.Add(CreateItemProductObj(itemProduct,content));
                    }
                }
            }
            GetScrollbar.value = 1f;
        }
        
        private void LoadItemsFromPoject()
        {
            if(m_itemDictionary != null) return;
            m_itemDictionary = new Dictionary<ITEMTYPE, List<ItemProduct>>();
            foreach (ITEMTYPE type in Enum.GetValues(typeof(ITEMTYPE)))
            {
                List<ItemProduct> items = Resources.LoadAll<ItemProduct>
                    (GetItemRootPath + '\\' + Enum.GetName(typeof(ITEMTYPE), type)).ToList();
                m_itemDictionary.Add(type,items);
            }
        }
        
        private void LoadItemWarehouseFormItems()
        {
            if(m_itemGroupObjList != null) return;
            m_itemGroupObjList = new List<GameObject>();
            
            foreach (var keyValuePair in m_itemDictionary)
            {
                GameObject itemDtailGroup = CreateItemDtailGroup(keyValuePair.Key);

                CreateItemType(keyValuePair.Key);
                
                List<ItemProduct> itemList = keyValuePair.Value;

                Transform content = itemDtailGroup.transform.Find(GetDetailGroupContentName);
                foreach (var itemProduct in itemList)
                {
                    CreateItemProductObj(itemProduct, content);
                }
            }
        }

        private GameObject CreateItemProductObj(ItemProduct itemProduct,Transform content)
        {
            GameObject itemProductObj = ObjectPool.Instance.OnTake(GetItemLattice);
            itemProductObj.transform.SetParent(content);
            itemProductObj.transform.Find(GetItemLatticeImageName).GetComponent<Image>().sprite =
                itemProduct.ItemIcon;
            itemProductObj.transform.Find(GetItemLatticeTextName).GetComponent<TextMeshProUGUI>().text =
                itemProduct.Name;
            itemProductObj.GetComponent<Button>().onClick.AddListener(() =>
            {
                GetSelectPromptText.text = $"{GetSelectPromptText.gameObject.name}: {itemProduct.Name}";
                m_currentChoose = itemProduct;
                GetCreateButton.interactable = true;
            });
            return itemProductObj;
        }

        private GameObject CreateItemType(ITEMTYPE itemType)
        {
            string itemTypeName = Enum.GetName(typeof(ITEMTYPE), itemType);
            GameObject itemTypeObj = ObjectPool.Instance.OnTake(GetItemType);
            itemTypeObj.transform.SetParent(GetItemTypeGroup);
            itemTypeObj.transform.Find(GetItemTypeTextName)
                .GetComponent<TextMeshProUGUI>().text = itemTypeName;
            return itemTypeObj;
        }

        private GameObject CreateItemDtailGroup(ITEMTYPE itemType)
        {
            string itemTypeName = Enum.GetName(typeof(ITEMTYPE), itemType);
                
            GameObject itemDtailGroup = ObjectPool.Instance.OnTake(GetItemDtailGroup);
            m_itemGroupObjList.Add(itemDtailGroup);
            itemDtailGroup.transform.SetParent(GetItemDetailGroupContent);
            itemDtailGroup.transform.Find(GetDetailGroupTextName)
                .GetComponent<TextMeshProUGUI>().text = itemTypeName;
            
            return itemDtailGroup;
        }

        private void CreateNewItem()
        {
            GetExcute?.Invoke(new ItemCreateCommand(TargetList,GetOutlinePainter,m_currentChoose.ItemObject));
        }
    }
}
