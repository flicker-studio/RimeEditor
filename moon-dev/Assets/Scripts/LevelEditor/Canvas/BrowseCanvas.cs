using System;
using Cysharp.Threading.Tasks;
using Moon.Kernel.Extension;
using SimpleFileBrowser;
using TMPro;
using UnityEngine.UI;
using RectTransform = UnityEngine.RectTransform;

namespace LevelEditor.View
{
    internal sealed class BrowseCanvas : IDisposable
    {
        public string GetLevelTextName     => _levelTextName;
        public string GetLevelPathTextName => _levelPathTextName;
        public string GetLevelImageName    => _levelImageName;

        public RawImage      GetLevelCoverImage      => _levelCoverImage;
        public ScrollRect    GetLevelScrollRect      => _levelScrollRect;
        public RectTransform GetLevelManagerRootRect => _levelManagerRootRect;
        public RectTransform GetLevelListContentRect => _levelListContentRect;
        public RectTransform GetFullPanelRect        => _fullPanelRect;

        public Button GetOpenButton        => _openButton;
        public Button GetCreateButton      => _createButton;
        public Button GetDeclarationButton => _declarationButton;
        public Button GetExitButton        => _exitButton;
        public Button GetRefreshButton     => _refreshButton;
        public Button GetDeleteButton      => _deleteButton;
        public Button GetImportButton      => _importButton;
        public Button GetWorksShopButton   => _worksShopButton;

        public TextMeshProUGUI           GetSubLevelNumber  => _subLevelNumber;
        public TextMeshProUGUI           GetLevelName       => _levelName;
        public TextMeshProUGUI           GetAnthorName      => _anthorName;
        public TextMeshProUGUI           GetDateTime        => _dateTime;
        public TextMeshProUGUI           GetInstroduction   => _instroduction;
        public TextMeshProUGUI           GetVersion         => _version;
        public UISetting.PopoverProperty GetPopoverProperty { get; private set; }

        private readonly RawImage      _levelCoverImage;
        private readonly ScrollRect    _levelScrollRect;
        private readonly string        _levelTextName;
        private readonly string        _levelPathTextName;
        private readonly string        _levelImageName;
        private readonly RectTransform _levelManagerRootRect;
        private readonly RectTransform _levelListContentRect;
        private readonly RectTransform _fullPanelRect;

        private readonly Button _openButton;
        private readonly Button _createButton;
        private readonly Button _declarationButton;
        private readonly Button _exitButton;
        private readonly Button _refreshButton;
        private readonly Button _importButton;
        private readonly Button _worksShopButton;
        private          Button _localLevelButton;
        private readonly Button _deleteButton;

        private readonly TextMeshProUGUI _levelName;
        private readonly TextMeshProUGUI _anthorName;
        private readonly TextMeshProUGUI _dateTime;
        private readonly TextMeshProUGUI _instroduction;
        private readonly TextMeshProUGUI _version;
        private readonly TextMeshProUGUI _subLevelNumber;

        public BrowseCanvas(RectTransform rect, UISetting levelEditorUISetting)
        {
            #region GET

            var uiProperty = levelEditorUISetting.GetLevelManagerPanelUI.GetLevelManagerPanelUIName;
            GetPopoverProperty    = levelEditorUISetting.GetPopoverProperty;
            _levelTextName        = uiProperty.ITEM_LEVEL_NAME;
            _levelPathTextName    = uiProperty.ITEM_LEVEL_PATH;
            _levelImageName       = uiProperty.ITEM_LEVEL_ICON;
            _levelCoverImage      = rect.FindPath(uiProperty.LEVEL_COVER_NAME).GetComponent<RawImage>();
            _levelScrollRect      = rect.FindPath(uiProperty.SCROLL_RECT).GetComponent<ScrollRect>();
            _levelManagerRootRect = rect.FindPath(uiProperty.PANEL_ROOT) as RectTransform;
            _levelListContentRect = rect.FindPath(uiProperty.LEVEL_LIST_CONTENT) as RectTransform;
            _fullPanelRect        = rect.FindPath(uiProperty.FULL_PANEL) as RectTransform;
            _openButton           = rect.FindPath(uiProperty.OPEN_BUTTON).GetComponent<Button>();
            _createButton         = rect.FindPath(uiProperty.CREATE_BUTTON).GetComponent<Button>();
            _declarationButton    = rect.FindPath(uiProperty.DECLARATION_BUTTON).GetComponent<Button>();
            _exitButton           = rect.FindPath(uiProperty.EXIT_BUTTON).GetComponent<Button>();
            _refreshButton        = rect.FindPath(uiProperty.REFRESH_BUTTON).GetComponent<Button>();
            _deleteButton         = rect.FindPath(uiProperty.DELETE_LEVEL_BUTTON).GetComponent<Button>();
            _importButton         = rect.FindPath(uiProperty.OEPN_LOCAL_DIRECTORY_BUTTON).GetComponent<Button>();
            _worksShopButton      = rect.FindPath(uiProperty.WORKS_SHOP_BUTTON).GetComponent<Button>();
            _localLevelButton     = rect.FindPath(uiProperty.LOCAL_LEVEL_BUTTON).GetComponent<Button>();
            _subLevelNumber       = rect.FindPath(uiProperty.SUB_LEVEL_NUMBER).GetComponent<TextMeshProUGUI>();
            _levelName            = rect.FindPath(uiProperty.LEVEL_NAME).GetComponent<TextMeshProUGUI>();
            _anthorName           = rect.FindPath(uiProperty.AUTHOR_NAME).GetComponent<TextMeshProUGUI>();
            _dateTime             = rect.FindPath(uiProperty.DATE_TIME).GetComponent<TextMeshProUGUI>();
            _instroduction        = rect.FindPath(uiProperty.INSTRODUCTION).GetComponent<TextMeshProUGUI>();
            _version              = rect.FindPath(uiProperty.VERSION).GetComponent<TextMeshProUGUI>();

            #endregion

            _createButton.onClick.AddListener(CreateLevel);
            _openButton.onClick.AddListener(OpenLevel);

            _refreshButton.onClick.AddListener(ReloadLevels);
            _deleteButton.onClick.AddListener(DeleteLevelPopover);
            _importButton.onClick.AddListener(OpenLevelFile);
            _exitButton.onClick.AddListener(ExitGamePopover);
        }

        public BrowseCanvas()
        {
        }

        ~BrowseCanvas()
        {
            _createButton.onClick.RemoveAllListeners();
            _openButton.onClick.RemoveAllListeners();
            _refreshButton.onClick.RemoveAllListeners();
            _deleteButton.onClick.RemoveAllListeners();
            _importButton.onClick.RemoveAllListeners();
            _exitButton.onClick.RemoveAllListeners();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this); // System resource trash can resource recycling
        }

        public void Active()
        {
        }

        public void Inactive()
        {
        }

        private void CreateLevel()
        {
        }

        private void OpenLevel()
        {
        }

        private void DeleteLevelPopover()
        {
        }

        private void DeleteLevel()
        {
        }

        private async void ReloadLevels()
        {
        }

        private void ExitGamePopover()
        {
        }

        private void OpenLevelFile()
        {
            OpenLevelFileAsync().Forget();
        }

        private async UniTaskVoid OpenLevelFileAsync()
        {
            await FileBrowser.WaitForLoadDialog(FileBrowser.PickMode.Folders, false, null, null, "Open a level directory", "Load");

            if (FileBrowser.Success)
            {
                var path = FileBrowser.Result[0].Replace("\\", "/");

                ReloadLevels();
            }
        }
    }
}