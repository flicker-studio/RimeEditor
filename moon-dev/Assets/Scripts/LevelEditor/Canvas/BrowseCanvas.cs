using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Frame.Tool.Popover;
using JetBrains.Annotations;
using LevelEditor.View;
using Moon.Kernel.Extension;
using SimpleFileBrowser;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using RectTransform = UnityEngine.RectTransform;

namespace LevelEditor.Canvas
{
    internal sealed class BrowseCanvas : IDisposable
    {
        private UISetting.PopoverProperty PopoverProperty { get; }

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

        private          LevelEntry       _activeEntry;
        private readonly List<LevelEntry> _levelEntries = new();

        public BrowseCanvas([NotNull] RectTransform rect, [NotNull] UISetting levelEditorUISetting)
        {
            #region GET

            var uiProperty = levelEditorUISetting.GetLevelManagerPanelUI.GetLevelManagerPanelUIName;
            PopoverProperty       = levelEditorUISetting.GetPopoverProperty;
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

            _createButton.onClick.AddListener(Create);
            _openButton.onClick.AddListener(OpenLevel);
            _refreshButton.onClick.AddListener(ReloadLevels);
            _deleteButton.onClick.AddListener(DeleteLevelPopover);
            _importButton.onClick.AddListener(OpenLevelFile);
            _exitButton.onClick.AddListener(ExitGamePopover);
        }

        ~BrowseCanvas()
        {
            Dispose(false);
        }

        public void Active()
        {
            _createButton.interactable = false;
            _fullPanelRect.gameObject.SetActive(true);
            _levelManagerRootRect.gameObject.SetActive(true);
            var pre = Resources.Load("Prefabs/LevelItem") as GameObject;

            for (int i = 0; i < 12; i++)
            {
                var levelEntry = new LevelEntry
                    (pre,
                     () => { Debug.Log("ha"); },
                     _levelScrollRect.content,
                     _levelScrollRect,
                     new LevelData(),
                     null,
                     null,
                     null
                    );
            }
            _activeEntry = new LevelEntry
                (pre,
                 () => { Debug.Log("ha"); },
                 _levelScrollRect.content,
                 _levelScrollRect,
                 new LevelData(),
                 null,
                 null,
                 null
                );
        }

        public void Inactive()
        {
            _levelManagerRootRect.gameObject.SetActive(false);
            _fullPanelRect.gameObject.SetActive(false);
        }

        public void Dispose()
        {
            Dispose(false);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
            }
        }

        private void Create()
        {
            // register event

            // show create canvas
        }

        private void OpenLevel()
        {
            _activeEntry.Invoke();
        }

        private void DeleteLevelPopover()
        {
        }

        private void DeleteLevel()
        {
        }

        private void ReloadLevels()
        {
        }

        private void ExitGamePopover()
        {
            PopoverLauncher.Instance.LaunchSelector(_levelManagerRootRect, PopoverProperty.POPOVER_TEXT_EXIT, Application.Quit);
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