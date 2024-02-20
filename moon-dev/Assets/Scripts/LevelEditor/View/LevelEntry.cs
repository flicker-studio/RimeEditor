using System;
using Frame.CompnentExtensions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LevelEditor.View
{
    /// <summary>
    ///     Level entries displayed in the Browse screen
    /// </summary>
    internal class LevelEntry : GridItemButton
    {
        public LevelData Level => _level;

        private readonly LevelData       _level;
        private readonly TextMeshProUGUI _name;
        private readonly TextMeshProUGUI _path;
        private readonly RawImage        _cover;

        /// <summary>
        ///     Default constructor
        /// </summary>
        public LevelEntry
        (
            GameObject buttonPrefab,
            Action     onSelect,
            Transform  parent,
            ScrollRect scrollRect,
            LevelData  level,
            string     levelTextName,
            string     levelPathTextName,
            string     levelImageName
        )
            : base(buttonPrefab, onSelect, parent, scrollRect)
        {
            //     _name      = ButtonObj.transform.Find(levelTextName).GetComponent<TextMeshProUGUI>();
            //     _path      = ButtonObj.transform.Find(levelPathTextName).GetComponent<TextMeshProUGUI>();
            //     _cover     = ButtonObj.transform.Find(levelImageName).GetComponent<RawImage>();
            //     _level     = level;
            //     _name.text = _level.LevelName;
            //     _path.text = _level.Path;
            //
            //     if (_level.Cover != null) _cover.texture = _level.Cover;
            //
        }
    }
}