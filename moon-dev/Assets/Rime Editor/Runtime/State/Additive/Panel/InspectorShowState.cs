using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Frame.StateMachine;
using LevelEditor.Command;
using LevelEditor.Extension;
using LevelEditor.Item;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Action = System.Action;
using Object = UnityEngine.Object;
using RectTransform = UnityEngine.RectTransform;

namespace LevelEditor
{
    public class InspectorShowState : AdditiveState
    {
        private static   UpdateInspectorSignal                               _updateInspectorSignal = new();
        private readonly Dictionary<string, Type>                            _commonFields          = new();
        private readonly Dictionary<string, Dictionary<ItemBase, FieldInfo>> _fieldInfoDic          = new();

        private readonly Dictionary<Type, List<GameObject>> _inspectorItemDic = new();
        private readonly Dictionary<string, GameObject>     _inspectorNameDic = new();
        private          bool                               _isRedoOrUndo;

        public InspectorShowState(Information baseInformation, MotionCallBack motionCallBack) : base(baseInformation, motionCallBack)
        {
            InitState();
        }

        private InspectorPanel                  GetInspectorPanel        => m_information.UIManager.GetInspectorPanel;
        private GameObject                      GetInspectorRootObj      => GetInspectorPanel.RootRect.gameObject;
        private RectTransform                   GetInspectorContent      => GetInspectorPanel.ContentRect;
        private UISetting.InspectorItemProperty GetInspectorItemProperty => GetInspectorPanel.InspectorItemProperty;
        private List<ItemBase>                  SelectedDatas            => m_information.Controller.SelectedItems;
        private GameObject                      GetBoolItemPrefab        => m_information.PrefabManager.GetBoolItem;

        [RuntimeInitializeOnLoadMethod]
        private static void ResetStaticVar()
        {
            _updateInspectorSignal = null;
            _updateInspectorSignal = new UpdateInspectorSignal();
        }

        public override void Motion(Information information)
        {
            if (SelectedDatas.Count == 0)
            {
                //  TargetDatas.OnAdd -= FindSameField;
                //  TargetDatas.OnAddRange -= FindSameField;
                _updateInspectorSignal.UpdateInspectorItemExcute -= UpdateAllUpdateInspectorItem;
                CommandInvoker.UndoAdditiveEvent                 -= SetDo;
                CommandInvoker.RedoAdditiveEvent                 -= SetDo;
                RemoveState();
            }
        }

        private void InitState()
        {
            GetInspectorRootObj.SetActive(true);

            //  TargetDatas.OnAdd += FindSameField;
            //   TargetDatas.OnAddRange += FindSameField;
            CommandInvoker.UndoAdditiveEvent                 += SetDo;
            CommandInvoker.RedoAdditiveEvent                 += SetDo;
            _updateInspectorSignal.UpdateInspectorItemExcute += UpdateAllUpdateInspectorItem;
            FindSameField();
        }

        protected override void RemoveState()
        {
            base.RemoveState();
            ClearInspectorItem();
            GetInspectorRootObj.SetActive(false);
        }

        public void FindSameField(ItemBase itemBase)
        {
            FindSameField();
        }

        public void FindSameField(List<ItemBase> itemData)
        {
            FindSameField();
        }

        public void FindSameField()
        {
            _fieldInfoDic.Clear();
            _commonFields.Clear();
            foreach (var item in SelectedDatas)
            {
                var type   = item.GetType();
                var fields = type.GetFields();
                foreach (var field in fields)
                {
                    if (_commonFields.ContainsKey(field.Name) && _commonFields[field.Name] != field.FieldType)
                    {
                        _commonFields.Remove(field.Name);
                        _fieldInfoDic.Remove(field.Name);
                        continue;
                    }

                    if (!_fieldInfoDic.ContainsKey(field.Name))
                    {
                        _commonFields.Add(field.Name, field.FieldType);
                        _fieldInfoDic.Add(field.Name, new Dictionary<ItemBase, FieldInfo>());
                    }

                    _fieldInfoDic[field.Name].Add(item, field);
                }
            }

            ClearInspectorItem();
            foreach (var keyValuePair in _commonFields)
            {
                var inspectorItem = CreateInspectorItem(keyValuePair.Value);
                _inspectorNameDic.Add(keyValuePair.Key, inspectorItem);
                UpdateInspectorItem(inspectorItem, keyValuePair.Value, keyValuePair.Key, _fieldInfoDic[keyValuePair.Key]);
                AddEventToInspectorItem(inspectorItem, keyValuePair.Value, _fieldInfoDic[keyValuePair.Key]);
            }
        }

        private void ClearInspectorItem()
        {
            foreach (var inspectorItemPair in _inspectorItemDic)
                if (inspectorItemPair.Key == typeof(bool))
                    foreach (var inspectorItem in inspectorItemPair.Value)
                    {
                        inspectorItem.GetComponent<Toggle>().onValueChanged.RemoveAllListeners();
                        Object.Destroy(inspectorItem);
                    }

            _inspectorItemDic.Clear();
            _inspectorNameDic.Clear();
        }

        private GameObject CreateInspectorItem(Type type)
        {
            if (type == typeof(bool))
            {
                if (!_inspectorItemDic.ContainsKey(type)) _inspectorItemDic.Add(type, new List<GameObject>());

                var inspectorItem = Object.Instantiate(GetBoolItemPrefab);
                inspectorItem.transform.SetParent(GetInspectorContent);
                _inspectorItemDic[type].Add(inspectorItem);
                return inspectorItem;
            }

            return null;
        }

        private void AddEventToInspectorItem(GameObject inspectorItem, Type type, Dictionary<ItemBase, FieldInfo> fieldInfoDic)
        {
            if (type == typeof(bool))
                inspectorItem.GetComponent<Toggle>().onValueChanged.AddListener(value =>
                {
                    if (_isRedoOrUndo)
                    {
                        _isRedoOrUndo = false;
                        return;
                    }

                    CommandInvoker.Execute(new FieldInfoChangeCommand(fieldInfoDic.Keys.ToList(), fieldInfoDic.Values.ToList(), value,
                                                                      _updateInspectorSignal));
                });
        }

        private void UpdateAllUpdateInspectorItem()
        {
            foreach (var keyValuePair in _commonFields)
                UpdateInspectorItem(_inspectorNameDic[keyValuePair.Key], keyValuePair.Value, keyValuePair.Key, _fieldInfoDic[keyValuePair.Key]);
        }

        private void UpdateInspectorItem(GameObject inspectorItem, Type type, string name, Dictionary<ItemBase, FieldInfo> fieldInfoDic)
        {
            if (type == typeof(bool))
            {
                inspectorItem.transform.FindPath(GetInspectorItemProperty.BOOLEAN_ITEM_TEXT).GetComponent<TextMeshProUGUI>().text = name;
                var sameValue      = true;
                var inspectorValue = false;
                var count          = 0;
                foreach (var keyValuePair in fieldInfoDic)
                {
                    if (count > 0 && inspectorValue != (bool)keyValuePair.Value.GetValue(keyValuePair.Key)) sameValue = false;

                    inspectorValue = (bool)keyValuePair.Value.GetValue(keyValuePair.Key);
                    count++;
                }

                if (!sameValue)
                    inspectorItem.GetComponent<Toggle>().isOn = default;
                else
                    inspectorItem.GetComponent<Toggle>().isOn = inspectorValue;
            }
        }

        private void SetDo()
        {
            _isRedoOrUndo = true;
        }

        public class UpdateInspectorSignal
        {
            public Action UpdateInspectorItemExcute;
        }
    }
}