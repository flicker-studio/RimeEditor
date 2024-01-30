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
using RectTransform = UnityEngine.RectTransform;

namespace LevelEditor
{
    public class InspectorShowState : AdditiveState
    {
        public class UpdateInspectorSignal
        {
            public UpdateInspectorItemExcute UpdateInspectorItemExcute;
        }

        public delegate void UpdateInspectorItemExcute();

        private InspectorPanel GetInspectorPanel => m_information.UIManager.GetInspectorPanel;
        private GameObject GetInspectorRootObj => GetInspectorPanel.GetInspectorRootRect.gameObject;

        private RectTransform GetInspectorContent => GetInspectorPanel.GetInspectorContentRect;

        private UIProperty.InspectorItemProperty GetInspectorItemProperty => GetInspectorPanel.GetInspectorItemProperty;

        private ObservableList<ItemData> TargetDatas => m_information.DataManager.TargetItems;

        private GameObject GetBoolItemPrefab => m_information.PrefabManager.GetBoolItem;


        private Dictionary<Type, List<GameObject>> m_inspectorItemDic = new Dictionary<Type, List<GameObject>>();

        private Dictionary<string, GameObject> m_inspectorNameDic = new Dictionary<string, GameObject>();

        private Dictionary<string, Type> m_commonFields = new Dictionary<string, Type>();

        private Dictionary<string, Dictionary<ItemData, FieldInfo>> m_fieldInfoDic = new Dictionary<string, Dictionary<ItemData, FieldInfo>>();

        private static UpdateInspectorSignal m_updateInspectorSignal = new UpdateInspectorSignal();

        private bool m_isRedoOrUndo;

        [RuntimeInitializeOnLoadMethod]
        static void ResetStaticVar()
        {
            m_updateInspectorSignal = null;
            m_updateInspectorSignal = new UpdateInspectorSignal();
        }

        public InspectorShowState(BaseInformation baseInformation, MotionCallBack motionCallBack) : base(baseInformation, motionCallBack)
        {
            InitState();
        }

        public override void Motion(BaseInformation information)
        {
            if (TargetDatas.Count == 0)
            {
                TargetDatas.OnAdd -= FindSameField;
                TargetDatas.OnAddRange -= FindSameField;
                m_updateInspectorSignal.UpdateInspectorItemExcute -= UpdateAllUpdateInspectorItem;
                CommandInvoker.UndoAdditiveEvent -= SetDo;
                CommandInvoker.RedoAdditiveEvent -= SetDo;

                RemoveState();
                return;
            }
        }

        private void InitState()
        {
            GetInspectorRootObj.SetActive(true);

            TargetDatas.OnAdd += FindSameField;
            TargetDatas.OnAddRange += FindSameField;

            CommandInvoker.UndoAdditiveEvent += SetDo;
            CommandInvoker.RedoAdditiveEvent += SetDo;

            m_updateInspectorSignal.UpdateInspectorItemExcute += UpdateAllUpdateInspectorItem;

            FindSameField();
        }

        protected override void RemoveState()
        {
            base.RemoveState();
            ClearInspectorItem();
            GetInspectorRootObj.SetActive(false);
        }

        public void FindSameField(ItemData itemData)
        {
            FindSameField();
        }

        public void FindSameField(List<ItemData> itemData)
        {
            FindSameField();
        }

        public void FindSameField()
        {
            m_fieldInfoDic.Clear();
            m_commonFields.Clear();

            foreach (ItemData item in TargetDatas)
            {
                Type type = item.GetType();

                FieldInfo[] fields = type.GetFields();

                foreach (FieldInfo field in fields)
                {
                    if (m_commonFields.ContainsKey(field.Name) && m_commonFields[field.Name] != field.FieldType)
                    {
                        m_commonFields.Remove(field.Name);
                        m_fieldInfoDic.Remove(field.Name);
                        continue;
                    }

                    if (!m_fieldInfoDic.ContainsKey(field.Name))
                    {
                        m_commonFields.Add(field.Name, field.FieldType);
                        m_fieldInfoDic.Add(field.Name, new Dictionary<ItemData, FieldInfo>());
                    }

                    m_fieldInfoDic[field.Name].Add(item, field);
                }
            }

            ClearInspectorItem();

            foreach (var keyValuePair in m_commonFields)
            {
                GameObject inspectorItem = CreateInspectorItem(keyValuePair.Value);
                m_inspectorNameDic.Add(keyValuePair.Key, inspectorItem);
                UpdateInspectorItem(inspectorItem, keyValuePair.Value, keyValuePair.Key, m_fieldInfoDic[keyValuePair.Key]);
                AddEventToInspectorItem(inspectorItem, keyValuePair.Value, m_fieldInfoDic[keyValuePair.Key]);
            }
        }

        private void ClearInspectorItem()
        {
            foreach (var inspectorItemPair in m_inspectorItemDic)
            {
                if (inspectorItemPair.Key == typeof(bool))
                {
                    foreach (var inspectorItem in inspectorItemPair.Value)
                    {
                        inspectorItem.GetComponent<Toggle>().onValueChanged.RemoveAllListeners();
                        ObjectPool.Instance.OnRelease(inspectorItem);
                    }
                }
            }

            m_inspectorItemDic.Clear();
            m_inspectorNameDic.Clear();
        }

        private GameObject CreateInspectorItem(Type type)
        {
            if (type == typeof(bool))
            {
                if (!m_inspectorItemDic.ContainsKey(type))
                {
                    m_inspectorItemDic.Add(type, new List<GameObject>());
                }

                GameObject inspectorItem = ObjectPool.Instance.OnTake(GetBoolItemPrefab);
                inspectorItem.transform.SetParent(GetInspectorContent);
                m_inspectorItemDic[type].Add(inspectorItem);
                return inspectorItem;
            }

            return null;
        }

        private void AddEventToInspectorItem(GameObject inspectorItem, Type type, Dictionary<ItemData, FieldInfo> fieldInfoDic)
        {
            if (type == typeof(bool))
            {
                inspectorItem.GetComponent<Toggle>().onValueChanged.AddListener((value) =>
                {
                    if (m_isRedoOrUndo)
                    {
                        m_isRedoOrUndo = false;
                        return;
                    }

                    CommandInvoker.Execute(
                        new FieldInfoChangeCommand(fieldInfoDic.Keys.ToList(), fieldInfoDic.Values.ToList(), value, m_updateInspectorSignal));
                });
            }
        }

        private void UpdateAllUpdateInspectorItem()
        {
            foreach (var keyValuePair in m_commonFields)
            {
                UpdateInspectorItem(m_inspectorNameDic[keyValuePair.Key], keyValuePair.Value, keyValuePair.Key, m_fieldInfoDic[keyValuePair.Key]);
            }
        }

        private void UpdateInspectorItem(GameObject inspectorItem, Type type, string name, Dictionary<ItemData, FieldInfo> fieldInfoDic)
        {
            if (type == typeof(bool))
            {
                inspectorItem.transform.FindPath(GetInspectorItemProperty.BOOLEAN_ITEM_TEXT)
                    .GetComponent<TextMeshProUGUI>()
                    .text = name;

                bool sameValue = true;
                bool inspectorValue = false;
                int count = 0;

                foreach (var keyValuePair in fieldInfoDic)
                {
                    if (count > 0 && inspectorValue != (bool)keyValuePair.Value.GetValue(keyValuePair.Key))
                    {
                        sameValue = false;
                    }

                    inspectorValue = (bool)keyValuePair.Value.GetValue(keyValuePair.Key);
                    count++;
                }

                if (!sameValue)
                {
                    inspectorItem.GetComponent<Toggle>().isOn = default;
                }
                else
                {
                    inspectorItem.GetComponent<Toggle>().isOn = inspectorValue;
                }
            }
        }

        private void SetDo()
        {
            m_isRedoOrUndo = true;
        }
    }
}