using System.Collections.Generic;
using System.Reflection;
using Moon.Kernel.Extension;

namespace LevelEditor
{
    public class FieldInfoChangeCommand : ICommand
    {
        private readonly List<ItemDataBase> m_targetList = new();

        private List<FieldInfo> m_targetFieldInfos = new List<FieldInfo>();

        private List<object> m_lastValues = new List<object>();

        private object m_nextValue = new object();

        private InspectorShowState.UpdateInspectorSignal m_updateInspectorSignal;

        public FieldInfoChangeCommand(List<ItemDataBase> targetList, List<FieldInfo> targetFieldInfos
            , object nextValue, InspectorShowState.UpdateInspectorSignal updateInspectorSignal)
        {
            m_targetList.AddRange(targetList);
            m_targetFieldInfos.AddRange(targetFieldInfos);
            m_nextValue = nextValue.Copy();

            for (var index = 0; index < targetFieldInfos.Count; index++)
            {
                m_lastValues.Add(targetFieldInfos[index].GetValue(targetList[index]).Copy());
            }

            m_updateInspectorSignal = updateInspectorSignal;
        }

        public void Execute()
        {
            for (var index = 0; index < m_targetFieldInfos.Count; index++)
            {
                m_targetFieldInfos[index].SetValue(m_targetList[index], m_nextValue);
            }

            m_updateInspectorSignal.UpdateInspectorItemExcute?.Invoke();
        }

        public void Undo()
        {
            for (var index = 0; index < m_targetFieldInfos.Count; index++)
            {
                m_targetFieldInfos[index].SetValue(m_targetList[index], m_lastValues[index]);
            }

            m_updateInspectorSignal.UpdateInspectorItemExcute?.Invoke();
        }
    }
}