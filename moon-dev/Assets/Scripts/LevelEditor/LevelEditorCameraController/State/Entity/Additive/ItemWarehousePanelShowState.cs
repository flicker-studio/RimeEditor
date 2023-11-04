using System;
using System.Collections.Generic;
using System.Linq;
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

        private bool GetMouseLeftButtonUp => m_information.GetInput.GetMouseLeftButtonUp;

        private string GetItemRootPath => GetItemWarehousePanel.GetItemRootPath;

        private string GetDetailGroupTextName => GetItemWarehousePanel.GetDetailGroupTextName;

        private string GetDetailGroupContentName => GetItemWarehousePanel.GetDetailGroupContentName;

        private string GetItemLatticeImageName => GetItemWarehousePanel.GetItemLatticeImageName;
        
        private string GetItemLatticeTextName => GetItemWarehousePanel.GetItemLatticeTextName;
        
        private string GetItemTypeTextName => GetItemWarehousePanel.GetItemTypeTextName;
        
        private List<GameObject> TargetList => m_information.TargetList;

        private OutlinePainter GetOutlinePainter => m_information.GetOutlinePainter;

        private CommandExcute GetExcute => m_information.GetLevelEditorCommandExcute;

        private ItemProduct m_currentChoose;

        private static bool m_isInit = true;

        private static Dictionary<ITEMTYPE, List<GameObject>> m_searchItemProductDic =
            new Dictionary<ITEMTYPE, List<GameObject>>();

        private static List<ItemProduct> m_searchItemProduct = new List<ItemProduct>();
        
        private static GameObject m_searchItemGroupObj;

        private static GameObject m_searchItemTypeObj;

        private static Dictionary<ITEMTYPE, List<ItemProduct>> m_itemDictionary;

        private static List<GameObject> m_itemGroupObjList;
        
        private static List<GameObject> m_itemTypeObjList;

        private static List<GameObject> m_itemProductObjList;
        
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

        protected override void RemoveState()
        {
            base.RemoveState();
            SetPanelActive(false);
        }

        private void ResetState()
        {
            ResetText();
            ResetSearch();
            ResetButton();
            m_isInit = false;
        }

        private void ResetText()
        {
            GetSelectPromptText.text = "";
            GetScrollbar.value = 1f;
        }
        
        private void ResetSearch()
        {
            if(m_searchItemGroupObj != null)
            {
                m_searchItemGroupObj.SetActive(false);
            }

            if (m_searchItemTypeObj != null)
            {
                m_searchItemTypeObj.SetActive(false);
            }

            if (m_itemGroupObjList != null)
            {
                foreach (var itemGroupObj in m_itemGroupObjList)
                {
                    itemGroupObj.SetActive(true);
                }
            }
            
            if (m_itemTypeObjList != null)
            {
                m_itemTypeObjList[0].GetComponent<Button>().onClick.Invoke();
            }
            GetSearchField.text = "";
            GetNothingFindText.gameObject.SetActive(false);
        }

        private void ResetButton()
        {
            if (m_itemProductObjList != null)
            {
                foreach (var itemProductObj in m_itemProductObjList)
                {
                    itemProductObj.GetComponent<Button>().interactable = true;
                }
            }
        }

        private void SetPanelActive(bool active)
        {
            GetPopoverPanelObj.SetActive(active);
            GetItemWarehousePanelObj.SetActive(active);
        }

        private void InitListener()
        {
            GetCreateButton.interactable = false;
            
            if(!m_isInit) return;
            
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
                ResetSearch();
            });
            
            GetSearchField.onSubmit.AddListener((value) =>
            {
                if (value == "")
                {
                    ResetSearch();
                }
                else
                {
                    SearchItem(value);
                }
            });
            
            GetScrollbar.onValueChanged.AddListener((value) =>
            {
                SetItemTypeByScrollBarValue();
            });
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
                foreach (var itemProductObj in itemProductPair.Value)
                {
                    ObjectPool.Instance.OnRelease(itemProductObj);
                    itemProductObj.GetComponent<Button>().onClick.RemoveAllListeners();
                    m_itemProductObjList.Remove(itemProductObj);
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
                                ,new List<GameObject>(){CreateItemProductObj(itemProduct,content)});
                        }
                    }
                }
            }

            if (addItem == 0) GetNothingFindText.gameObject.SetActive(true);
        }

        private void FinishSearchAndSetPanel()
        {
            GetScrollbar.value = 1f;
            
            if (m_searchItemTypeObj == null)
            {
                m_searchItemTypeObj = CreateItemType("All");
                m_searchItemTypeObj.transform.SetAsFirstSibling();
            }
            m_searchItemTypeObj.SetActive(true);
            m_searchItemTypeObj.GetComponent<Button>().onClick.Invoke();
        }

        private void SetItemTypeByScrollBarValue()
        {
            if(m_searchItemTypeObj != null && m_searchItemTypeObj.gameObject.activeInHierarchy) return;
            float contentPosY = GetItemDetailGroupContent.GetComponent<RectTransform>().anchoredPosition.y;
            float itemDtailHeightSum = 0;
            for (int i = 0; i < GetItemDetailGroupContent.transform.childCount; i++)
            {
                itemDtailHeightSum += (GetItemDetailGroupContent.transform.GetChild(i) as RectTransform).sizeDelta.y;
                if (itemDtailHeightSum > contentPosY)
                {
                    m_itemTypeObjList[i].GetComponent<Button>().onClick.Invoke();
                    return;
                }
            }
        }
        
        private void LoadItemsFromPoject()
        {
            if(!m_isInit) return;
            m_itemDictionary = new Dictionary<ITEMTYPE, List<ItemProduct>>();
            foreach (ITEMTYPE type in Enum.GetValues(typeof(ITEMTYPE)))
            {
                List<ItemProduct> items = Resources.LoadAll<ItemProduct>
                    (GetItemRootPath + '\\' + Enum.GetName(typeof(ITEMTYPE), type)).ToList();
                m_itemDictionary.Add(type,items);
            }
        }
        
        private void LoadItemWarehouseFromItems()
        {
            if(!m_isInit) return;
            m_itemGroupObjList = new List<GameObject>();
            m_itemTypeObjList = new List<GameObject>();
            m_itemProductObjList = new List<GameObject>();
            
            foreach (var keyValuePair in m_itemDictionary)
            {
                string itemType = Enum.GetName(typeof(ITEMTYPE), keyValuePair.Key);
                
                GameObject itemDtailGroup = CreateItemDtailGroup(itemType);
                
                m_itemGroupObjList.Add(itemDtailGroup);
                m_itemTypeObjList.Add(CreateItemType(itemType));
                
                List<ItemProduct> itemList = keyValuePair.Value;

                Transform content = itemDtailGroup.transform.Find(GetDetailGroupContentName);
                foreach (var itemProduct in itemList)
                {
                    m_itemProductObjList.Add(CreateItemProductObj(itemProduct, content));
                }
            }
        }

        private GameObject CreateItemProductObj(ItemProduct itemProduct,Transform content)
        {
            GameObject newItemProductObj = ObjectPool.Instance.OnTake(GetItemLattice);
            newItemProductObj.transform.SetParent(content);
            newItemProductObj.transform.Find(GetItemLatticeImageName).GetComponent<Image>().sprite =
                itemProduct.ItemIcon;
            newItemProductObj.transform.Find(GetItemLatticeTextName).GetComponent<TextMeshProUGUI>().text =
                itemProduct.Name;
            newItemProductObj.GetComponent<Button>().onClick
                .AddListener(() =>
                {
                    ChooseItemProduct(newItemProductObj, itemProduct);
                });
            return newItemProductObj;
        }

        private GameObject CreateItemType(string itemType)
        {
            GameObject newItemTypeObj = ObjectPool.Instance.OnTake(GetItemType);
            newItemTypeObj.transform.SetParent(GetItemTypeGroup);
            newItemTypeObj.transform.Find(GetItemTypeTextName)
                .GetComponent<TextMeshProUGUI>().text = itemType;
            newItemTypeObj.GetComponent<Button>().onClick.AddListener(() =>
            {
                ItemTypeChoose(newItemTypeObj);
                
                if (GetMouseLeftButtonUp)
                {
                    if (m_searchItemTypeObj == null ||
                        m_searchItemTypeObj != null && !m_searchItemTypeObj.activeInHierarchy)
                    {
                        SetContentPosByItemType(newItemTypeObj);
                    }
                    else
                    {
                        ClassifiedSearch(newItemTypeObj);
                    }
                }
            });
            return newItemTypeObj;
        }
        
        private void ItemTypeChoose(GameObject newItemTypeObj)
        {
            foreach (var itemTypObj in m_itemTypeObjList)
            {
                if (itemTypObj != newItemTypeObj)
                {
                    itemTypObj.GetComponent<Button>().interactable = true;
                }
            }

            if (m_searchItemTypeObj != null && m_searchItemTypeObj != newItemTypeObj)
            {
                m_searchItemTypeObj.GetComponent<Button>().interactable = true;
            }
            newItemTypeObj.GetComponent<Button>().interactable = false;
        }

        private void SetContentPosByItemType(GameObject newItemTypeObj)
        {
            float itemDtailHeightSum = 0;
            for (int i = 0;m_itemTypeObjList[i] != newItemTypeObj ; i++)
            {
                itemDtailHeightSum += (GetItemDetailGroupContent.transform.GetChild(i) as RectTransform).sizeDelta.y;
            }
            RectTransform content = GetItemDetailGroupContent.GetComponent<RectTransform>();
            content.anchoredPosition = content.anchoredPosition.NewY(itemDtailHeightSum);
        }

        private void ClassifiedSearch(GameObject newItemTypeObj)
        {
            int activeObjNum = 0;
            
            if (newItemTypeObj == m_searchItemTypeObj)
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
                        
            string itemType = newItemTypeObj.transform.Find(GetItemTypeTextName)
                .GetComponent<TextMeshProUGUI>().text;
            foreach (var itemProductPair in m_searchItemProductDic)
            {
                foreach (var itemProductObj in itemProductPair.Value)
                {
                    if (Enum.GetName(typeof(ITEMTYPE), itemProductPair.Key) == itemType)
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
            GetExcute?.Invoke(new ItemCreateCommand(TargetList,GetOutlinePainter,m_currentChoose.ItemObject));
        }

        private void ChooseItemProduct(GameObject newItemProductObj,ItemProduct itemProduct)
        {
            GetSelectPromptText.text = $"{GetSelectPromptText.gameObject.name}: {itemProduct.Name}";
            m_currentChoose = itemProduct;
            GetCreateButton.interactable = true;
            foreach (var itemProductObj in m_itemProductObjList)
            {
                if (itemProductObj != newItemProductObj)
                {
                    itemProductObj.GetComponent<Button>().interactable = true;
                }
            }
            foreach (var keyValuePair in m_searchItemProductDic)
            {
                foreach (var itemProductObj in keyValuePair.Value)
                {
                    if (itemProductObj != newItemProductObj)
                    {
                        itemProductObj.GetComponent<Button>().interactable = true;
                    }
                }
            }

            newItemProductObj.GetComponent<Button>().interactable = false;
        }
    }
}
