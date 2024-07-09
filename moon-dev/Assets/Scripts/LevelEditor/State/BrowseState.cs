using Cysharp.Threading.Tasks;
using LevelEditor.View.Canvas;
using SimpleFileBrowser;
using UnityEngine;

namespace LevelEditor.State
{
    /// <summary>
    ///     The level browsing state of the state machine.
    ///     It is used to manipulate the normal citation
    ///     and destruction of each part.
    /// </summary>
    internal sealed class BrowseState : IState
    {
        private readonly BrowseCanvas _browseCanvas;

        public BrowseState(RectTransform rect)
        {
            _browseCanvas = new BrowseCanvas(rect);
        }

        /// <inheritdoc />
        public void OnEnter()
        {
            //Load the data first
            _browseCanvas.Active();
        }

        /// <inheritdoc />
        public void OnUpdate()
        {
        }

        /// <inheritdoc />
        public void OnExit()
        {
            _browseCanvas.Inactive();
        }

        private async UniTaskVoid OpenLevelFileAsync()
        {
            await FileBrowser.WaitForLoadDialog(FileBrowser.PickMode.Folders, false, null, null, "Open a level directory", "Load");
            if (FileBrowser.Success)
            {
                var path = FileBrowser.Result[0].Replace("\\", "/");

                // if (!DataManager.OpenLocalLevelDirectory(path))
                // {
                //     PopoverLauncher.Instance.LaunchTip
                //         (
                //          GetLevelManagerRoot,
                //          GetPopoverProperty.POPOVER_LOCATION,
                //          GetPopoverProperty.SIZE,
                //          GetPopoverProperty.POPOVER_ERROR_COLOR,
                //          GetPopoverProperty.CHECK_ERROR_LEVEL_DIRECTORY,
                //          GetPopoverProperty.DURATION
                //         );
                //
                //     return;
                // }
            }
        }
    }
}