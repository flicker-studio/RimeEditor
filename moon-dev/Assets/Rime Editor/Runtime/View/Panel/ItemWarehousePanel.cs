using LevelEditor.Extension;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using RectTransform = UnityEngine.RectTransform;

namespace LevelEditor
{
    public class ItemWarehousePanel
    {
        private Button m_clearSearchButton;

        private Button m_closeButton;

        private Button m_createButton;

        private string m_detailGroupContentName;

        private string m_detailGroupTextName;

        private Transform m_itemDetailGroupContent;

        private string m_itemLatticeImageName;

        private string m_itemLatticeTextName;

        private string m_itemRootPath;

        private Transform m_itemTypeGroup;

        private string m_itemTypeTextName;

        private GameObject m_itemWarehousePanelObj;

        private TextMeshProUGUI m_nothingFindText;

        private GameObject m_popoverPanelObj;

        private UISetting.ItemWarehousePanelUI m_property;

        private Scrollbar m_scrollbar;

        private ScrollRect m_scrollView;

        private TMP_InputField m_searchField;

        private TextMeshProUGUI m_selectPromptText;

        public ItemWarehousePanel(RectTransform levelEditorCanvasRect, UISetting levelEditorUISetting)
        {
            InitComponent(levelEditorCanvasRect, levelEditorUISetting);
        }

        public Transform GetItemTypeGroup          => m_itemTypeGroup;
        public Transform GetItemDetailGroupContent => m_itemDetailGroupContent;

        public TextMeshProUGUI GetSelectPromptText => m_selectPromptText;

        public TextMeshProUGUI GetNothingFindText => m_nothingFindText;

        public Button GetCloseButton => m_closeButton;

        public Button GetCreateButton => m_createButton;

        public Button GetClearSearchButton => m_clearSearchButton;

        public Scrollbar GetScrollbar => m_scrollbar;

        public ScrollRect GetScrollView => m_scrollView;

        public GameObject GetPopoverPanelObj => m_popoverPanelObj;

        public GameObject GetItemWarehousePanelObj => m_itemWarehousePanelObj;

        public TMP_InputField GetSearchField  => m_searchField;
        public string         GetItemRootPath => m_itemRootPath;

        public string GetDetailGroupTextName => m_detailGroupTextName;

        public string GetDetailGroupContentName => m_detailGroupContentName;

        public string GetItemLatticeImageName => m_itemLatticeImageName;

        public string GetItemLatticeTextName => m_itemLatticeTextName;

        public string GetItemTypeTextName => m_itemTypeTextName;

        private void InitComponent(RectTransform levelEditorCanvasRect, UISetting levelEditorUISetting)
        {
            m_property = levelEditorUISetting.GetItemWarehousePanelUI;
            var uiName = m_property.GetItemWarehousePanelUIName;
            m_searchField            = levelEditorCanvasRect.FindPath(uiName.SEARCH_INPUT_FIELD).GetComponent<TMP_InputField>();
            m_selectPromptText       = levelEditorCanvasRect.FindPath(uiName.SELECT_TEXT).GetComponent<TextMeshProUGUI>();
            m_nothingFindText        = levelEditorCanvasRect.FindPath(uiName.NOTHING_FIND_TEXT).GetComponent<TextMeshProUGUI>();
            m_closeButton            = levelEditorCanvasRect.FindPath(uiName.CLOSE_BUTTON).GetComponent<Button>();
            m_createButton           = levelEditorCanvasRect.FindPath(uiName.CREATE_BUTTON).GetComponent<Button>();
            m_clearSearchButton      = levelEditorCanvasRect.FindPath(uiName.CLEAR_SEARCH_BUTTON).GetComponent<Button>();
            m_itemTypeGroup          = levelEditorCanvasRect.FindPath(uiName.ITEM_TYPE_GROUP);
            m_itemDetailGroupContent = levelEditorCanvasRect.FindPath(uiName.ITEM_DETAIL_GROUP_CONTENT);
            m_popoverPanelObj        = levelEditorCanvasRect.FindPath(uiName.POPOVER_PANEL).gameObject;
            m_itemWarehousePanelObj  = levelEditorCanvasRect.FindPath(uiName.ITEM_WAREHOURSE_PANEL).gameObject;
            m_scrollbar              = levelEditorCanvasRect.FindPath(uiName.ITEM_DETAIL_GROUP_SCROLL_BAR).GetComponent<Scrollbar>();

            m_scrollView = levelEditorCanvasRect.FindPath(uiName.ITEM_DETAIL_GROUP_SCROLL_VIEW)
                                                .GetComponent<ScrollRect>();

            m_detailGroupTextName    = uiName.ITEM_DETAIL_GROUP_PREFAB_TEXT;
            m_detailGroupContentName = uiName.ITEM_DETAIL_GROUP_PREFAB_CONTENT;
            m_itemLatticeImageName   = uiName.ITEM_LATTICE_IMAGE;
            m_itemLatticeTextName    = uiName.ITEM_LATTICE_TEXT;
            m_itemTypeTextName       = uiName.ITEM_TYPE_TEXT;
            var uiProperty = m_property.GetItemWarehouseProperty;
            m_itemRootPath = uiProperty.ITEM_ROOT_PATH;
        }
    }
}