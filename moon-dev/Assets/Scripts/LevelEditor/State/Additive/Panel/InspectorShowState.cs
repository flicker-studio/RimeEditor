using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Frame.StateMachine;
using Frame.Tool.Pool;
using LevelEditor.Command;
using Moon.Kernel.Extension;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Action = System.Action;
using RectTransform = UnityEngine.RectTransform;

namespace LevelEditor
{
    public class InspectorShowState : AdditiveState
    {
        public class UpdateInspectorSignal
        {
            public Action UpdateInspectorItemExcute;
        }

        private InspectorPanel                  GetInspectorPanel        => m_information.UIManager.GetInspectorPanel;
        private GameObject                      GetInspectorRootObj      => GetInspectorPanel.RootRect.gameObject;
        private RectTransform                   GetInspectorContent      => GetInspectorPanel.ContentRect;
        private UISetting.InspectorItemProperty GetInspectorItemProperty => GetInspectorPanel.InspectorItemProperty;
        private List<AbstractItem>              TargetDatas              => m_information.DataManager.TargetItems;
        private GameObject                      GetBoolItemPrefab        => m_information.PrefabManager.GetBoolItem;

        private readonly Dictionary<Type, List<GameObject>>                      _inspectorItemDic      = new();
        private readonly Dictionary<string, GameObject>                          _inspectorNameDic      = new();
        private readonly Dictionary<string, Type>                                _commonFields          = new();
        private readonly Dictionary<string, Dictionary<AbstractItem, FieldInfo>> _fieldInfoDic          = new();
        private static   UpdateInspectorSignal                                   _updateInspectorSignal = new();
        private          bool                                                    _isRedoOrUndo;

        [RuntimeInitializeOnLoadMethod]
        private static void ResetStaticVar()
        {
            _updateInspectorSignal = null;
            _updateInspectorSignal = new UpdateInspectorSignal();
        }

        public InspectorShowState(BaseInformation baseInformation, MotionCallBack motionCallBack) : base(baseInformation, motionCallBack)
        {
            InitState();
        }

        public override void Motion(BaseInformation information)
        {
            if (TargetDatas.Count == 0)
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

        public void FindSameField(AbstractItem abstractItem)
        {
            FindSameField();
        }

        public void FindSameField(List<AbstractItem> itemData)
        {
            FindSameField();
        }

        public void FindSameField()
        {
            _fieldInfoDic.Clear();
            _commonFields.Clear();
            foreach (var item in TargetDatas)
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
                        _fieldInfoDic.Add(field.Name, new Dictionary<AbstractItem, FieldInfo>());
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
                        ObjectPool.Instance.OnRelease(inspectorItem);
                    }

            _inspectorItemDic.Clear();
            _inspectorNameDic.Clear();
        }

        private GameObject CreateInspectorItem(Type type)
        {
            if (type == typeof(bool))
            {
                if (!_inspectorItemDic.ContainsKey(type)) _inspectorItemDic.Add(type, new List<GameObject>());

                var inspectorItem = ObjectPool.Instance.OnTake(GetBoolItemPrefab);
                inspectorItem.transform.SetParent(GetInspectorContent);
                _inspectorItemDic[type].Add(inspectorItem);
                return inspectorItem;
            }

            return null;
        }

        private void AddEventToInspectorItem(GameObject inspectorItem, Type type, Dictionary<AbstractItem, FieldInfo> fieldInfoDic)
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

        private void UpdateInspectorItem(GameObject inspectorItem, Type type, string name, Dictionary<AbstractItem, FieldInfo> fieldInfoDic)
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
    }
}