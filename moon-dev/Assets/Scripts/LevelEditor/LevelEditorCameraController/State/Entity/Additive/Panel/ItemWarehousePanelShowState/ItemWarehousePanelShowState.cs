using System;
using System.Collections.Generic;
using System.Linq;
using Frame.Data;
using Frame.StateMachine;
using Frame.Static.Extensions;
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
        
        private TextMeshProUGUI GetNothingFindText => GetItemWarehousePanel.GetNothingFindText;

        private GameObject GetPopoverPanelObj => GetItemWarehousePanel.GetPopoverPanelObj;
        
        private GameObject GetItemWarehousePanelObj => GetItemWarehousePanel.GetItemWarehousePanelObj;
        
        private Button GetCloseButton => GetItemWarehousePanel.GetCloseButton;
        
        private Button GetCreateButton => GetItemWarehousePanel.GetCreateButton;

        private Button GetClearSearchButton => GetItemWarehousePanel.GetClearSearchButton;

        private TMP_InputField GetSearchField => GetItemWarehousePanel.GetSearchField;

        private Scrollbar GetScrollbar => GetItemWarehousePanel.GetScrollbar;

        private ScrollRect GetScrollView => GetItemWarehousePanel.GetScrollView;

        private string GetItemRootPath => GetItemWarehousePanel.GetItemRootPath;

        private string GetDetailGroupTextName => GetItemWarehousePanel.GetDetailGroupTextName;

        private string GetDetailGroupContentName => GetItemWarehousePanel.GetDetailGroupContentName;

        private string GetItemLatticeImageName => GetItemWarehousePanel.GetItemLatticeImageName;
        
        private string GetItemLatticeTextName => GetItemWarehousePanel.GetItemLatticeTextName;
        
        private string GetItemTypeTextName => GetItemWarehousePanel.GetItemTypeTextName;
        
        private ObservableList<ItemData> ItemAssets => m_information.ItemAssets;
        
        private ObservableList<ItemData> TargetAssets => m_information.TargetItems;

        private OutlinePainter GetOutlinePainter => m_information.GetOutlinePainter;

        private CommandExcute GetExcute => m_information.GetLevelEditorCommandExcute;

        private static ItemProduct m_currentChoose;

        private static bool m_isInit = true;

        private static Dictionary<ITEMTYPEENUM, List<ItemProductButton>> m_searchItemProductDic =
            new Dictionary<ITEMTYPEENUM, List<ItemProductButton>>();

        private static List<ItemProduct> m_searchItemProduct = new List<ItemProduct>();
        
        private static GameObject m_searchItemGroupObj;

        private static ItemTypeButton m_searchItemTypeButton;

        private static Dictionary<ITEMTYPEENUM, List<ItemProduct>> m_itemDictionary;

        private static List<GameObject> m_itemGroupObjList;
        
        private static List<ItemTypeButton> m_itemTypeList;

        private static List<ItemProductButton> m_itemProductButtonList;
        
        public ItemWarehousePanelShowState(BaseInformation baseInformation, MotionCallBack motionCallBack) : base(baseInformation, motionCallBack)
        {
            LoadItemsFromPoject();
            LoadItemWarehouseFromItems();
            InitListener();
            ResetState();
            SetPanelActive(true);
        }

        public override void Motion(BaseInformation information)
        {

        }

        private void InitListener()
        {
            GetCreateButton.interactable = false;
            
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
                ClearSearchInput();
                ResetSearchPanel();
            });
            
            GetSearchField.onSubmit.AddListener((value) =>
            {
                if (value == "")
                {
                    ClearSearchInput();
                    ResetSearchPanel();
                }
                else
                {
                    ResetSearchPanel();
                    SearchItem(value);
                }
            });
            
            GetScrollbar.onValueChanged.AddListener((value) =>
            {
                SetItemTypeByScrollBarValue();
            });
        }
        
        protected override void RemoveState()
        {
            RemoveListener();
            base.RemoveState();
            SetPanelActive(false);
        }

        private void ResetState()
        {
            ResetText();
            ResetSearchPanel();
            ResetButton();
            m_isInit = false;
        }
        

        private void ResetText()
        {
            GetSelectPromptText.text = "";
            GetScrollbar.value = 1f;
        }
        
        private void ResetSearchPanel()
        {
            if(m_searchItemGroupObj != null)
            {
                m_searchItemGroupObj.SetActive(false);
            }

            if (m_searchItemTypeButton != null)
            {
                m_searchItemTypeButton.SetActive(false);
            }

            if (m_itemGroupObjList != null)
            {
                foreach (var itemGroupObj in m_itemGroupObjList)
                {
                    itemGroupObj.SetActive(true);
                }
            }
            
            GetNothingFindText.gameObject.SetActive(false);
        }

        private void ClearSearchInput()
        {
            GetSearchField.text = "";
        }

        private void ResetButton()
        {
            if (m_itemProductButtonList != null)
            {
                foreach (var itemProductObj in m_itemProductButtonList)
                {
                    itemProductObj.IsSelected = false;
                }
            }

            if (m_itemTypeList != null && m_itemTypeList.Count > 0)
            {
                m_itemTypeList[0].Invoke();
            }
        }

        private void SetPanelActive(bool active)
        {
            GetPopoverPanelObj.SetActive(active);
            GetItemWarehousePanelObj.SetActive(active);
        }

        private void RemoveListener()
        {
            m_currentChoose = null;
            
            GetScrollbar.value = 1f;
            
            GetCreateButton.onClick.RemoveAllListeners();

            GetCloseButton.onClick.RemoveAllListeners();
            
            GetClearSearchButton.onClick.RemoveAllListeners();
            
            GetSearchField.onSubmit.RemoveAllListeners();
            
            GetScrollbar.onValueChanged.RemoveAllListeners();
        }

        private void SearchItem(string searchValue)
        {
            SearchReady(searchValue);

            SearchingItem(searchValue);

            FinishSearchAndSetPanel();
        }

        private void SearchReady(string searchValue)
        {
            foreach (var itemGroupObj in m_itemGroupObjList)
            {
                itemGroupObj.SetActive(false);
            }

            foreach (var itemProductPair in m_searchItemProductDic)
            {
                foreach (var itemProductButton in itemProductPair.Value)
                {
                    itemProductButton.Remove();
                    m_itemProductButtonList.Remove(itemProductButton);
                }
            }
            
            m_searchItemProductDic.Clear();
            m_searchItemProduct.Clear();
            
            if (m_searchItemGroupObj == null)
            {
                m_searchItemGroupObj = CreateItemDtailGroup(searchValue);
            }
            m_searchItemGroupObj.SetActive(true);
            m_searchItemGroupObj.transform.Find(GetDetailGroupTextName)
                .GetComponent<TextMeshProUGUI>().text = searchValue;
        }

        private void SearchingItem(string searchValue)
        {
            Transform content = m_searchItemGroupObj.transform.Find(GetDetailGroupContentName);

            int addItem = 0;
            
            foreach (var keyValuePair in m_itemDictionary)
            {
                foreach (var itemProduct in keyValuePair.Value)
                {
                    if (itemProduct.Name.Contains(searchValue))
                    {
                        addItem++;
                        if (m_searchItemProductDic.ContainsKey(itemProduct.ItemType))
                        {
                            m_searchItemProductDic[itemProduct.ItemType].Add(CreateItemProductObj(itemProduct,content));
                        }
                        else
                        {
                            m_searchItemProductDic.Add(itemProduct.ItemType
                                ,new List<ItemProductButton>(){CreateItemProductObj(itemProduct,content)});
                        }
                    }
                }
            }

            if (addItem == 0) GetNothingFindText.gameObject.SetActive(true);
        }

        private void FinishSearchAndSetPanel()
        {
            GetScrollbar.value = 1f;
            
            if (m_searchItemTypeButton == null)
            {
                m_searchItemTypeButton = CreateItemType("All");
                m_searchItemTypeButton.GetButtonObj.transform.SetAsFirstSibling();
            }
            m_searchItemTypeButton.SetActive(true);
            m_searchItemTypeButton.Invoke();
        }

        private void SetItemTypeByScrollBarValue()
        {
            if(m_searchItemTypeButton != null && m_searchItemTypeButton.GetButtonObj.activeInHierarchy) return;
            float contentPosY = GetItemDetailGroupContent.GetComponent<RectTransform>().anchoredPosition.y;
            float itemDtailHeightSum = 0;
            for (int i = 0; i < GetItemDetailGroupContent.transform.childCount; i++)
            {
                itemDtailHeightSum += (GetItemDetailGroupContent.transform.GetChild(i) as RectTransform).sizeDelta.y;
                if (itemDtailHeightSum > contentPosY)
                {
                    m_itemTypeList[i].Invoke();
                    return;
                }
            }
        }
        
        private void LoadItemsFromPoject()
        {
            if(!m_isInit) return;
            m_itemDictionary = new Dictionary<ITEMTYPEENUM, List<ItemProduct>>();
            foreach (ITEMTYPEENUM type in Enum.GetValues(typeof(ITEMTYPEENUM)))
            {
                List<ItemProduct> items = Resources.LoadAll<ItemProduct>
                    (GetItemRootPath + '\\' + Enum.GetName(typeof(ITEMTYPEENUM), type)).ToList();
                m_itemDictionary.Add(type,items);
            }
        }
        
        private void LoadItemWarehouseFromItems()
        {
            if(!m_isInit) return;
            m_itemGroupObjList = new List<GameObject>();
            m_itemTypeList = new List<ItemTypeButton>();
            m_itemProductButtonList = new List<ItemProductButton>();
            
            foreach (var keyValuePair in m_itemDictionary)
            {
                string itemType = Enum.GetName(typeof(ITEMTYPEENUM), keyValuePair.Key);
                
                GameObject itemDtailGroup = CreateItemDtailGroup(itemType);
                
                m_itemGroupObjList.Add(itemDtailGroup);
                m_itemTypeList.Add(CreateItemType(itemType));
                
                List<ItemProduct> itemList = keyValuePair.Value;

                Transform content = itemDtailGroup.transform.Find(GetDetailGroupContentName);
                foreach (var itemProduct in itemList)
                {
                    m_itemProductButtonList.Add(CreateItemProductObj(itemProduct, content));
                }
            }
        }

        private ItemProductButton CreateItemProductObj(ItemProduct itemProduct,Transform content)
        {
            ItemProductButton newItemProductButton = new ItemProductButton(GetItemLattice,itemProduct, ChooseItemProduct, content,GetScrollView,
                GetItemLatticeTextName, GetItemLatticeImageName);
            return newItemProductButton;
        }

        private ItemTypeButton CreateItemType(string itemTypeName)
        {
            ItemTypeButton itemTypeButton = new ItemTypeButton(GetItemType, ItemTypeSelectEvent, GetItemTypeGroup,GetScrollView, GetItemTypeTextName);
            itemTypeButton.GetText.text = itemTypeName;
            return itemTypeButton;
        }

        private void ItemTypeSelectEvent(GridItemButton gridItemButton)
        {
            ItemTypeButton itemTypeButton = gridItemButton as ItemTypeButton;
            
            ItemTypeChoose(itemTypeButton);
                
            if (m_searchItemTypeButton == null ||
                m_searchItemTypeButton != null && !m_searchItemTypeButton.GetActive())
            {
                SetContentPosByItemType(itemTypeButton);
            }
            else
            {
                ClassifiedSearch(itemTypeButton);
            }
        }
        
        private void ItemTypeChoose(ItemTypeButton newItemTypeButton)
        {
            foreach (var itemTypObj in m_itemTypeList)
            {
                if (itemTypObj != newItemTypeButton)
                {
                    itemTypObj.IsSelected = false;
                }
            }

            if (m_searchItemTypeButton != null && m_searchItemTypeButton != newItemTypeButton)
            {
                m_searchItemTypeButton.IsSelected = false;
            }
            newItemTypeButton.IsSelected = true;
        }

        private void SetContentPosByItemType(ItemTypeButton newItemTypeButton)
        {
            float itemDtailHeightSum = 0;
            for (int i = 0;m_itemTypeList[i] != newItemTypeButton ; i++)
            {
                itemDtailHeightSum += (GetItemDetailGroupContent.transform.GetChild(i) as RectTransform).sizeDelta.y;
            }
            RectTransform content = GetItemDetailGroupContent.GetComponent<RectTransform>();
            content.anchoredPosition = content.anchoredPosition.NewY(itemDtailHeightSum);
        }

        private void ClassifiedSearch(ItemTypeButton newItemTypeButton)
        {
            int activeObjNum = 0;
            
            if (newItemTypeButton == m_searchItemTypeButton)
            {
                foreach (var itemProductPair in m_searchItemProductDic)
                {
                    foreach (var itemProductObj in itemProductPair.Value)
                    {
                        activeObjNum++;
                        itemProductObj.SetActive(true);
                    }
                }
                
                if(activeObjNum == 0) GetNothingFindText.gameObject.SetActive(true);
                else GetNothingFindText.gameObject.SetActive(false);
                GetScrollbar.value = 1f;
                return;
            }
                        
            string itemType = newItemTypeButton.GetButtonObj.transform.Find(GetItemTypeTextName)
                .GetComponent<TextMeshProUGUI>().text;
            foreach (var itemProductPair in m_searchItemProductDic)
            {
                foreach (var itemProductObj in itemProductPair.Value)
                {
                    if (Enum.GetName(typeof(ITEMTYPEENUM), itemProductPair.Key) == itemType)
                    {
                        itemProductObj.SetActive(true);
                        activeObjNum++;
                    }
                    else
                    {
                        itemProductObj.SetActive(false);
                    }
                }
            }
            if(activeObjNum == 0) GetNothingFindText.gameObject.SetActive(true);
            else GetNothingFindText.gameObject.SetActive(false);
            GetScrollbar.value = 1f;
        }

        private GameObject CreateItemDtailGroup(string itemType)
        {
            GameObject itemDtailGroup = ObjectPool.Instance.OnTake(GetItemDtailGroup);
            itemDtailGroup.transform.SetParent(GetItemDetailGroupContent);
            itemDtailGroup.transform.Find(GetDetailGroupTextName)
                .GetComponent<TextMeshProUGUI>().text = itemType;
            
            return itemDtailGroup;
        }

        private void CreateNewItem()
        {
            GetExcute?.Invoke(new ItemCreateCommand(TargetAssets,ItemAssets,GetOutlinePainter,m_currentChoose));
        }

        private void ChooseItemProduct(GridItemButton gridItemButton)
        {
            ItemProductButton itemProductButton = gridItemButton as ItemProductButton;
            ItemProduct itemProduct = itemProductButton.GetItemProduct;
            GetSelectPromptText.text = $"{GetSelectPromptText.gameObject.name}: {itemProduct.Name}";
            m_currentChoose = itemProduct;
            GetCreateButton.interactable = true;
            foreach (var itemProductObj in m_itemProductButtonList)
            {
                if (itemProductObj != gridItemButton)
                {
                    itemProductObj.IsSelected = false;
                }
            }
            foreach (var keyValuePair in m_searchItemProductDic)
            {
                foreach (var itemProductObj in keyValuePair.Value)
                {
                    if (itemProductObj != gridItemButton)
                    {
                        itemProductObj.IsSelected = false;
                    }
                }
            }

            gridItemButton.IsSelected = true;
        }
    }
}