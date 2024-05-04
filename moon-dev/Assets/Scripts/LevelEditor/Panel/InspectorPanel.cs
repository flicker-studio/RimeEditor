using System.Collections.Generic;
using LevelEditor.Command;
using Moon.Kernel.Extension;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LevelEditor
{
    internal sealed class InspectorPanel
    {
        public RectTransform                   RootRect              { get; }
        public RectTransform                   ContentRect           { get; }
        public TextMeshProUGUI                 Describe              { get; }
        public UISetting.InspectorItemProperty InspectorItemProperty { get; private set; }

        public bool GetPositionChange => _positionInputFieldVector3.GetVector3Change;
        public bool GetRotationChange => _rotationInputFieldVector3.GetVector3Change;
        public bool GetScaleChange    => _scaleInputFieldVector3.GetVector3Change;

        public bool GetOnSelect => _positionInputFieldVector3.OnSelect
                                || _rotationInputFieldVector3.OnSelect
                                || _scaleInputFieldVector3.OnSelect;

        private          Button            _editButton;
        private readonly GameObject        _transformPanelObj;
        private readonly InputFieldVector3 _positionInputFieldVector3;
        private readonly InputFieldVector3 _rotationInputFieldVector3;
        private readonly InputFieldVector3 _scaleInputFieldVector3;

        public InspectorPanel(Transform rect, UISetting setting)
        {
            var property1 = setting.GetInspectorPanelUI.GetInspectorPanelUIName;
            InspectorItemProperty = setting.GetInspectorPanelUI.GetInspectorItemProperty;
            RootRect              = rect.FindPath(property1.INSPECTOR_ROOT) as RectTransform;
            ContentRect           = rect.FindPath(property1.INSPECTOR_CONTENT) as RectTransform;
            Describe              = rect.FindPath(property1.DESCRIBE_TEXT).GetComponent<TextMeshProUGUI>();

            var property = setting.GetItemTransformPanelUI.GetItemTransformPanelUIName;
            _editButton        = rect.FindPath(property.EDIT_BUTTON).GetComponent<Button>();
            _transformPanelObj = rect.FindPath(property.ROOT_PANEL).gameObject;

            _positionInputFieldVector3 =
                new InputFieldVector3(
                                      rect.FindPath(property.POSITION_INPUT_X).GetComponent<TMP_InputField>(),
                                      rect.FindPath(property.POSITION_INPUT_Y).GetComponent<TMP_InputField>(),
                                      rect.FindPath(property.POSITION_INPUT_Z).GetComponent<TMP_InputField>()
                                     );

            _rotationInputFieldVector3 =
                new InputFieldVector3(
                                      rect.FindPath(property.ROTATION_INPUT_X).GetComponent<TMP_InputField>(),
                                      rect.FindPath(property.ROTATION_INPUT_Y).GetComponent<TMP_InputField>(),
                                      rect.FindPath(property.ROTATION_INPUT_Z).GetComponent<TMP_InputField>()
                                     );

            _scaleInputFieldVector3 =
                new InputFieldVector3(
                                      rect.FindPath(property.SCALE_INPUT_X).GetComponent<TMP_InputField>(),
                                      rect.FindPath(property.SCALE_INPUT_Y).GetComponent<TMP_InputField>(),
                                      rect.FindPath(property.SCALE_INPUT_Z).GetComponent<TMP_InputField>()
                                     );
        }

        public void Add()
        {
            var prefab        = Controller.Configure.PrefabManager.GetBoolItem;
            var inspectorItem = Object.Instantiate(prefab, ContentRect, true);
        }

        public void TransformBind(List<AbstractItem> items)
        {
            var count = items.Count;
            if (count <= 0) return;

            var newPos = new List<Vector3>(count);
            if (_positionInputFieldVector3.GetVector3Change)
            {
                for (var i = 0; i < count; i++) newPos.Add(_positionInputFieldVector3.Data);

                var cmd = new PositionCommand(items, newPos);
                CommandInvoker.Execute(cmd);
            }

            var newRot = new List<Quaternion>(count);
            if (_rotationInputFieldVector3.GetVector3Change)
            {
                for (var i = 0; i < count; i++) newRot.Add(Quaternion.Euler(_rotationInputFieldVector3.Data));

                var cmd = new Rotation(items, newRot);
                CommandInvoker.Execute(cmd);
            }

            var newScale = new List<Vector3>(count);
            if (_scaleInputFieldVector3.GetVector3Change)
            {
                for (var i = 0; i < count; i++) newScale.Add(_scaleInputFieldVector3.Data);

                var cmd = new Scale(items, newScale);
                CommandInvoker.Execute(cmd);
            }
        }
    }
}