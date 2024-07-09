using System;
using System.Collections.Generic;
using System.Linq;
using Frame.CompnentExtensions;
using Frame.StateMachine;
using LevelEditor.Command;
using LevelEditor.Data;
using LevelEditor.Item;
using Moon.Kernel.Extension;
using Moon.Kernel.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace LevelEditor
{
    public class ItemWarehousePanelShowState : AdditiveState
    {
        private static          ItemProduct                                   m_currentChoose;
        private static          bool                                          m_isInit               = true;
        private static readonly Dictionary<ItemType, List<ItemProductButton>> m_searchItemProductDic = new();
        private static readonly List<ItemProduct>                             m_searchItemProduct    = new();
        private static          GameObject                                    m_searchItemGroupObj;
        private static          ItemTypeButton                                m_searchItemTypeButton;
        private static          Dictionary<ItemType, List<ItemProduct>>       m_itemDictionary;
        private static          List<GameObject>                              m_itemGroupObjList;
        private static          List<ItemTypeButton>                          m_itemTypeList;
        private static          List<ItemProductButton>                       m_itemProductButtonList;

        public ItemWarehousePanelShowState(BaseInformation baseInformation, MotionCallBack motionCallBack) : base(baseInformation, motionCallBack)
        {
            LoadItemsFromPoject();
            LoadItemWarehouseFromItems();
            InitState();
            InitListener();
            SetPanelActive(true);
        }

        private InputManager       GetInput                  => m_information.InputManager;
        private LevelAction        GetLevelAction            => m_information.LevelAction;
        private ItemWarehousePanel GetItemWarehousePanel     => m_information.UIManager.GetItemWarehousePanel;
        private Transform          GetItemTypeGroup          => GetItemWarehousePanel.GetItemTypeGroup;
        private Transform          GetItemDetailGroupContent => GetItemWarehousePanel.GetItemDetailGroupContent;
        private GameObject         GetItemDtailGroup         => m_information.PrefabManager.GetItemDetailGroup;
        private GameObject         GetItemLattice            => m_information.PrefabManager.GetItemLattice;
        private GameObject         GetItemType               => m_information.PrefabManager.GetItemType;
        private TextMeshProUGUI    GetSelectPromptText       => GetItemWarehousePanel.GetSelectPromptText;
        private TextMeshProUGUI    GetNothingFindText        => GetItemWarehousePanel.GetNothingFindText;
        private GameObject         GetPopoverPanelObj        => GetItemWarehousePanel.GetPopoverPanelObj;
        private GameObject         GetItemWarehousePanelObj  => GetItemWarehousePanel.GetItemWarehousePanelObj;
        private Button             GetCloseButton            => GetItemWarehousePanel.GetCloseButton;
        private Button             GetCreateButton           => GetItemWarehousePanel.GetCreateButton;
        private Button             GetClearSearchButton      => GetItemWarehousePanel.GetClearSearchButton;
        private TMP_InputField     GetSearchField            => GetItemWarehousePanel.GetSearchField;
        private Scrollbar          GetScrollbar              => GetItemWarehousePanel.GetScrollbar;
        private ScrollRect         GetScrollView             => GetItemWarehousePanel.GetScrollView;
        private string             GetItemRootPath           => GetItemWarehousePanel.GetItemRootPath;
        private string             GetDetailGroupTextName    => GetItemWarehousePanel.GetDetailGroupTextName;
        private string             GetDetailGroupContentName => GetItemWarehousePanel.GetDetailGroupContentName;
        private string             GetItemLatticeImageName   => GetItemWarehousePanel.GetItemLatticeImageName;
        private string             GetItemLatticeTextName    => GetItemWarehousePanel.GetItemLatticeTextName;
        private string             GetItemTypeTextName       => GetItemWarehousePanel.GetItemTypeTextName;
        private List<ItemBase>     ItemAssets                => m_information.DataManager.ItemAssets;
        private List<ItemBase>     TargetAssets              => m_information.DataManager.TargetItems;

        private OutlineManager GetOutlinePainter => m_information.OutlineManager;

        [RuntimeInitializeOnLoadMethod]
        private static void ResetStaticVar()
        {
            m_isInit                = true;
            m_searchItemGroupObj    = null;
            m_itemDictionary        = null;
            m_itemGroupObjList      = null;
            m_searchItemTypeButton  = null;
            m_itemTypeList          = null;
            m_itemProductButtonList = null;
            m_searchItemProductDic.Clear();
            m_searchItemProduct.Clear();
        }

        public override void Motion(BaseInformation information)
        {
        }

        private void InitState()
        {
            throw new InvalidOperationException(); // GetInput.SetCanInput(false);
        }

        private void InitListener()
        {
            GetCreateButton.interactable = false;
            GetCreateButton.onClick.AddListener(() =>
            {
                CreateNewItem();
                ClosePanel();
            });
            GetCloseButton.onClick.AddListener(ClosePanel);
            GetClearSearchButton.onClick.AddListener(() =>
            {
                ClearSearchInput();
                ResetSearchPanelState();
            });
            GetSearchField.onSubmit.AddListener(value =>
            {
                if (value == "")
                {
                    ClearSearchInput();
                    ResetSearchPanelState();
                }
                else
                {
                    ResetSearchPanelState();
                    SearchItem(value);
                }
            });
            GetScrollbar.onValueChanged.AddListener(value => { SetItemTypeByScrollBarValue(); });
        }

        /// <inheritdoc />
        protected override void RemoveState()
        {
            ResetState();
            RemoveListener();
            base.RemoveState();
            SetPanelActive(false);
        }

        private void ClosePanel()
        {
            ClearSearchInput();
            RemoveState();
        }

        private void ResetState()
        {
            throw new InvalidOperationException(); //   GetInput.SetCanInput(true);
            ResetTextState();
            ResetSearchPanelState();
            ResetButtonState();
            m_isInit = false;
        }

        private void ResetTextState()
        {
            GetSelectPromptText.text = "";
        }

        private void ResetSearchPanelState()
        {
            if (m_searchItemGroupObj == null || !m_searchItemGroupObj.activeInHierarchy) return;
            m_searchItemGroupObj.SetActive(false);
            m_searchItemTypeButton.SetActive(false);
            foreach (var itemGroupObj in m_itemGroupObjList) itemGroupObj.SetActive(true);
            m_itemTypeList[0].Invoke();
            GetScrollbar.value = 1f;
            GetNothingFindText.gameObject.SetActive(false);
        }

        private void ClearSearchInput()
        {
            GetSearchField.text = "";
        }

        private void ResetButtonState()
        {
            if (m_itemProductButtonList != null)
                foreach (var itemProductObj in m_itemProductButtonList)
                    itemProductObj.SetSelected(false);
        }

        private void SetPanelActive(bool active)
        {
            GetPopoverPanelObj.SetActive(active);
            GetItemWarehousePanelObj.SetActive(active);
        }

        private void RemoveListener()
        {
            m_currentChoose               =  null;
            GetLevelAction.ExitEditorView -= ResetInitBool;
            GetLevelAction.ExitEditorView += ResetInitBool;
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
            foreach (var itemGroupObj in m_itemGroupObjList) itemGroupObj.SetActive(false);
            foreach (var itemProductPair in m_searchItemProductDic)
            foreach (var itemProductButton in itemProductPair.Value)
                m_itemProductButtonList.Remove(itemProductButton);
            m_searchItemProductDic.Clear();
            m_searchItemProduct.Clear();
            if (m_searchItemGroupObj == null) m_searchItemGroupObj = CreateItemDtailGroup(searchValue);
            m_searchItemGroupObj.SetActive(true);
            m_searchItemGroupObj.transform.Find(GetDetailGroupTextName).GetComponent<TextMeshProUGUI>().text = searchValue;
        }

        private void SearchingItem(string searchValue)
        {
            var content = m_searchItemGroupObj.transform.Find(GetDetailGroupContentName);
            var addItem = 0;
            foreach (var keyValuePair in m_itemDictionary)
            foreach (var itemProduct in keyValuePair.Value)
                if (itemProduct.Name.Contains(searchValue))
                {
                    addItem++;
                    if (m_searchItemProductDic.ContainsKey(itemProduct.ItemType))
                        m_searchItemProductDic[itemProduct.ItemType].Add(CreateItemProductObj(itemProduct, content));
                    else
                        m_searchItemProductDic.Add(itemProduct.ItemType, new List<ItemProductButton> { CreateItemProductObj(itemProduct, content) });
                }

            if (addItem == 0) GetNothingFindText.gameObject.SetActive(true);
        }

        private void FinishSearchAndSetPanel()
        {
            GetScrollbar.value = 1f;
            if (m_searchItemTypeButton == null)
            {
                m_searchItemTypeButton = CreateItemType("All");
                m_searchItemTypeButton.GameObject.transform.SetAsFirstSibling();
            }

            m_searchItemTypeButton.SetActive(true);
            m_searchItemTypeButton.Invoke();
        }

        private void SetItemTypeByScrollBarValue()
        {
            if (m_searchItemTypeButton != null && m_searchItemTypeButton.GameObject.activeInHierarchy) return;
            var   contentPosY        = GetItemDetailGroupContent.GetComponent<RectTransform>().anchoredPosition.y;
            float itemDtailHeightSum = 0;
            for (var i = 0; i < GetItemDetailGroupContent.transform.childCount; i++)
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
            if (!m_isInit) return;
            m_itemDictionary = new Dictionary<ItemType, List<ItemProduct>>();
            foreach (ItemType type in Enum.GetValues(typeof(ItemType)))
            {
                var items = Resources.LoadAll<ItemProduct>(GetItemRootPath + '\\' + Enum.GetName(typeof(ItemType), type)).ToList();
                m_itemDictionary.Add(type, items);
            }
        }

        private void LoadItemWarehouseFromItems()
        {
            if (!m_isInit) return;
            m_itemGroupObjList      = new List<GameObject>();
            m_itemTypeList          = new List<ItemTypeButton>();
            m_itemProductButtonList = new List<ItemProductButton>();
            foreach (var keyValuePair in m_itemDictionary)
            {
                var itemType       = Enum.GetName(typeof(ItemType), keyValuePair.Key);
                var itemDtailGroup = CreateItemDtailGroup(itemType);
                m_itemGroupObjList.Add(itemDtailGroup);
                m_itemTypeList.Add(CreateItemType(itemType));
                var itemList = keyValuePair.Value;
                var content  = itemDtailGroup.transform.Find(GetDetailGroupContentName);
                foreach (var itemProduct in itemList) m_itemProductButtonList.Add(CreateItemProductObj(itemProduct, content));
            }
        }

        private ItemProductButton CreateItemProductObj(ItemProduct itemProduct, Transform content)
        {
            var newItemProductButton = new ItemProductButton(GetItemLattice, itemProduct, ChooseItemProduct, content, GetScrollView,
                                                             GetItemLatticeTextName, GetItemLatticeImageName);
            return newItemProductButton;
        }

        private ItemTypeButton CreateItemType(string itemTypeName)
        {
            var itemTypeButton = new ItemTypeButton(GetItemType, ItemTypeSelectEvent, GetItemTypeGroup, null, GetItemTypeTextName);
            itemTypeButton.Text.text = itemTypeName;
            return itemTypeButton;
        }

        private void ItemTypeSelectEvent(ListEntry listEntry)
        {
            var itemTypeButton = listEntry as ItemTypeButton;
            ItemTypeChoose(itemTypeButton);
            if (m_searchItemTypeButton == null || (m_searchItemTypeButton != null && !m_searchItemTypeButton.GetActive()))
                SetContentPosByItemType(itemTypeButton);
            else
                ClassifiedSearch(itemTypeButton);
        }

        private void ItemTypeChoose(ItemTypeButton newItemTypeButton)
        {
            foreach (var itemTypObj in m_itemTypeList)
                if (itemTypObj != newItemTypeButton)
                    itemTypObj.SetSelected(false);
            if (m_searchItemTypeButton != null && m_searchItemTypeButton != newItemTypeButton) m_searchItemTypeButton.SetSelected(false);
            newItemTypeButton.SetSelected(true);
        }

        private void SetContentPosByItemType(ItemTypeButton newItemTypeButton)
        {
            float itemDtailHeightSum = 0;
            for (var i = 0; m_itemTypeList[i] != newItemTypeButton; i++)
                itemDtailHeightSum += (GetItemDetailGroupContent.transform.GetChild(i) as RectTransform).sizeDelta.y;
            var content = GetItemDetailGroupContent.GetComponent<RectTransform>();
            content.anchoredPosition = content.anchoredPosition.NewY(itemDtailHeightSum);
        }

        private void ClassifiedSearch(ItemTypeButton newItemTypeButton)
        {
            var activeObjNum = 0;
            if (newItemTypeButton == m_searchItemTypeButton)
            {
                foreach (var itemProductPair in m_searchItemProductDic)
                foreach (var itemProductObj in itemProductPair.Value)
                {
                    activeObjNum++;
                    itemProductObj.SetActive(true);
                }

                if (activeObjNum == 0)
                    GetNothingFindText.gameObject.SetActive(true);
                else
                    GetNothingFindText.gameObject.SetActive(false);
                GetScrollbar.value = 1f;
                return;
            }

            var itemType = newItemTypeButton.GameObject.transform.Find(GetItemTypeTextName).GetComponent<TextMeshProUGUI>().text;
            foreach (var itemProductPair in m_searchItemProductDic)
            foreach (var itemProductObj in itemProductPair.Value)
                if (Enum.GetName(typeof(ItemType), itemProductPair.Key) == itemType)
                {
                    itemProductObj.SetActive(true);
                    activeObjNum++;
                }
                else
                {
                    itemProductObj.SetActive(false);
                }

            if (activeObjNum == 0)
                GetNothingFindText.gameObject.SetActive(true);
            else
                GetNothingFindText.gameObject.SetActive(false);
            GetScrollbar.value = 1f;
        }

        private GameObject CreateItemDtailGroup(string itemType)
        {
            var itemDtailGroup = Object.Instantiate(GetItemDtailGroup);
            itemDtailGroup.transform.SetParent(GetItemDetailGroupContent);
            itemDtailGroup.transform.Find(GetDetailGroupTextName).GetComponent<TextMeshProUGUI>().text = itemType;
            return itemDtailGroup;
        }

        private void CreateNewItem()
        {
            CommandInvoker.Execute(new Create(ItemAssets[0].Type));
        }

        private void ChooseItemProduct(ListEntry listEntry)
        {
            var itemProductButton = listEntry as ItemProductButton;
            var itemProduct       = itemProductButton.GetItemProduct;
            GetSelectPromptText.text     = $"{GetSelectPromptText.gameObject.name}: {itemProduct.Name}";
            m_currentChoose              = itemProduct;
            GetCreateButton.interactable = true;
            foreach (var itemProductObj in m_itemProductButtonList)
                if (itemProductObj != listEntry)
                    itemProductObj.SetSelected(false);
            foreach (var keyValuePair in m_searchItemProductDic)
            foreach (var itemProductObj in keyValuePair.Value)
                if (itemProductObj != listEntry)
                    itemProductObj.SetSelected(false);
            listEntry.SetSelected(true);
        }

        private void ResetInitBool()
        {
            m_isInit = true;
        }
    }
}