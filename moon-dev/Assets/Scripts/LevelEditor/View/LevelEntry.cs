using System;
using Frame.CompnentExtensions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace LevelEditor.View
{
    /// <summary>
    ///     Level entries displayed in the Browse screen
    /// </summary>
    internal class LevelEntry : ListEntry
    {
        public LevelInfo Info => _info;

        private readonly LevelInfo _info;

        private readonly TextMeshProUGUI _name;
        private readonly TextMeshProUGUI _path;
        private readonly RawImage        _cover;

        /// <summary>
        ///     Default constructor
        /// </summary>
        public LevelEntry
        (
            GameObject         buttonPrefab,
            Action<LevelEntry> onSelect,
            Transform          parent,
            ScrollRect         scrollRect,
            LevelInfo          level,
            string             levelTextName,
            string             levelPathTextName,
            string             levelImageName
        )
            : base(buttonPrefab, null, parent, scrollRect)
        {
            _name      = ButtonObj.transform.Find(levelTextName).GetComponent<TextMeshProUGUI>();
            _path      = ButtonObj.transform.Find(levelPathTextName).GetComponent<TextMeshProUGUI>();
            _cover     = ButtonObj.transform.Find(levelImageName).GetComponent<RawImage>();
            _info      = level;
            _name.text = _info.Name;
            _path.text = "_info.Path";

            _cover.texture = _info.Cover;

            _eventButton.AddEvents(() => { onSelect?.Invoke(this); });
        }

        public void Selected()
        {
            _eventButton._button.interactable = false;
        }

        public void Uncheck()
        {
            _eventButton._button.interactable = true;
        }

        public void Open()
        {
            Debug.Log("Level Opened.");
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

            Object.Destroy(ButtonObj);
        }
    }
}