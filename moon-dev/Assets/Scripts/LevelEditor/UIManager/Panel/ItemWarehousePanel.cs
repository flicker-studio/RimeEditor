using Frame.Static.Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace LevelEditor
{
    public class ItemWarehousePanel
    {
        public Transform GetItemTypeGroup => m_itemTypeGroup;
        public Transform GetItemDetailGroupContent => m_itemDetailGroupContent;

        public TextMeshProUGUI GetSelectPromptText => m_selectPromptText;

        public Button GetCloseButton => m_closeButton;

        public Button GetCreateButton => m_createButton;

        public Button GetClearSearchButton => m_clearSearchButton;

        public Scrollbar GetScrollbar => m_scrollbar;

        public GameObject GetPopoverPanelObj => m_popoverPanelObj;

        public GameObject GetItemWarehousePanelObj => m_itemWarehousePanelObj;

        public TMP_InputField GetSearchField => m_searchField;
        public string GetItemRootPath => m_itemRootPath;

        public string GetDetailGroupTextName => m_detailGroupTextName;

        public string GetDetailGroupContentName => m_detailGroupContentName;

        public string GetItemLatticeImageName => m_itemLatticeImageName;
        
        public string GetItemLatticeTextName => m_itemLatticeTextName;

        public string GetItemTypeTextName => m_itemTypeTextName;
        
        private TMP_InputField m_searchField;
        private TextMeshProUGUI m_selectPromptText;
        private Button m_closeButton;
        private Button m_createButton;
        private Button m_clearSearchButton;
        private GameObject m_popoverPanelObj;
        private GameObject m_itemWarehousePanelObj;
        private Transform m_itemTypeGroup;
        private Transform m_itemDetailGroupContent;
        private Scrollbar m_scrollbar;
        private string m_itemRootPath;
        private string m_detailGroupTextName;
        private string m_detailGroupContentName;
        private string m_itemLatticeImageName;
        private string m_itemLatticeTextName;
        private string m_itemTypeTextName;

        private UIProperty.ItemWarehousePanelUI m_property;
        
        public ItemWarehousePanel(RectTransform levelEditorCanvasRect,UIProperty levelEditorUIProperty)
        {
            InitComponent(levelEditorCanvasRect, levelEditorUIProperty);
        }
        
        private void InitComponent(RectTransform levelEditorCanvasRect,UIProperty levelEditorUIProperty)
        {
            m_property = levelEditorUIProperty.GetItemWarehousePanelUI;
            UIProperty.ItemWarehousePanelUIName uiName = m_property.GetItemWarehousePanelUIName;
            m_searchField = levelEditorCanvasRect.FindPath(uiName.SEARCH_INPUT_FIELD).GetComponent<TMP_InputField>();
            m_selectPromptText = levelEditorCanvasRect.FindPath(uiName.SELECT_TEXT).GetComponent<TextMeshProUGUI>();
            m_closeButton = levelEditorCanvasRect.FindPath(uiName.CLOSE_BUTTON).GetComponent<Button>();
            m_createButton = levelEditorCanvasRect.FindPath(uiName.CREATE_BUTTON).GetComponent<Button>();
            m_clearSearchButton = levelEditorCanvasRect.FindPath(uiName.CLEAR_SEARCH_BUTTON).GetComponent<Button>();
            m_itemTypeGroup = levelEditorCanvasRect.FindPath(uiName.ITEM_TYPE_GROUP);
            m_itemDetailGroupContent = levelEditorCanvasRect.FindPath(uiName.ITEM_DETAIL_GROUP_CONTENT);
            m_popoverPanelObj = levelEditorCanvasRect.FindPath(uiName.POPOVER_PANEL).gameObject;
            m_itemWarehousePanelObj = levelEditorCanvasRect.FindPath(uiName.ITEM_WAREHOURSE_PANEL).gameObject;
            m_scrollbar = levelEditorCanvasRect.FindPath(uiName.ITEM_DETAIL_GROUP_SCROLL_BAR).GetComponent<Scrollbar>();
            m_detailGroupTextName = uiName.ITEM_DETAIL_GROUP_PREFAB_TEXT;
            m_detailGroupContentName = uiName.ITEM_DETAIL_GROUP_PREFAB_CONTENT;
            m_itemLatticeImageName = uiName.ITEM_LATTICE_IMAGE;
            m_itemLatticeTextName = uiName.ITEM_LATTICE_TEXT;
            m_itemTypeTextName = uiName.ITEM_TYPE_TEXT;
            UIProperty.ItemWarehouseProperty uiProperty = m_property.GetItemWarehouseProperty;
            m_itemRootPath = uiProperty.ITEM_ROOT_PATH;
        }
    }
}
