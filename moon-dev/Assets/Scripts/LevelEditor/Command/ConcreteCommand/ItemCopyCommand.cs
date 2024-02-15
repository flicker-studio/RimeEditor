using System.Collections.Generic;
using System.Linq;
using Moon.Kernel.Extension;
using Moon.Kernel.Utils;
using UnityEngine;

namespace LevelEditor
{
    /// <summary>
    /// </summary>
    public class CopyCommand : ICommand
    {
        private readonly List<ItemDataBase> m_copyTarget = new();
        private          List<ItemDataBase> m_saveDatas  = new();
        private readonly List<ItemDataBase> m_targetAssets;
        private readonly List<ItemDataBase> m_itemAssets;
        private readonly OutlineManager     m_outlinePainter;
        private readonly List<ItemDataBase> m_lastAssets  = new();
        private readonly ItemFactory        m_itemFactory = new();

        public CopyCommand
        (
            ObservableList<ItemDataBase> targetAssets,
            ObservableList<ItemDataBase> itemAssets,
            OutlineManager               outlinePainter,
            List<ItemDataBase>           copyTarget
        )
        {
            m_targetAssets   = targetAssets.ToList();
            m_itemAssets     = itemAssets.ToList();
            m_outlinePainter = outlinePainter;
            m_copyTarget.AddRange(copyTarget);
        }

        public void Execute()
        {
            if (m_saveDatas.Count == 0)
                m_saveDatas = CopyItems(m_copyTarget, m_saveDatas);
            else
                SetDatasActive(m_saveDatas, true);

            m_lastAssets.Clear();
            m_lastAssets.AddRange(m_targetAssets);
            m_itemAssets.AddRange(m_saveDatas);
            m_targetAssets.Clear();
            m_targetAssets.AddRange(m_saveDatas);
            m_outlinePainter.SetRenderObjects(m_targetAssets.GetItemObjs());
        }

        public void Undo()
        {
            m_targetAssets.Clear();
            m_targetAssets.AddRange(m_lastAssets);
            //m_itemAssets.RemoveAll(m_saveDatas);
            m_outlinePainter.SetRenderObjects(m_targetAssets.GetItemObjs());
            SetDatasActive(m_saveDatas, false);
        }

        private List<ItemDataBase> CopyItems(List<ItemDataBase> copyDatas, List<ItemDataBase> saveDatas)
        {
            var oriPos = Vector3.zero;

            foreach (var copyData in copyDatas)
            {
                var newData = copyData.Copy(m_itemFactory.CreateItem(copyData.GetItemProduct));
                var (position, rotation, scale) = copyData.GetItemObjEditor.transform.GetTransformValue();
                newData.GetItemObjEditor.transform.SetTransformValue(position, rotation, scale);
                oriPos += newData.GetItemObjEditor.transform.position;
                saveDatas.Add(newData);
            }

            oriPos /= saveDatas.Count;

            var targetPos = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2f, Screen.height / 2f,
                Mathf.Abs(Camera.main.transform.position.z)));

            var direction = targetPos - oriPos;

            foreach (var saveData in saveDatas)
            {
                var oldPosition = saveData.GetItemObjEditor.transform.position;
                saveData.GetItemObjEditor.transform.position = oldPosition + direction;
            }

            return saveDatas;
        }

        private void SetDatasActive(List<ItemDataBase> itemDatas, bool active)
        {
            foreach (var itemData in itemDatas) itemData.SetActiveEditor(active);
        }
    }
}