using System.Collections.Generic;
using Frame.Static.Extensions;
using UnityEngine;
using Frame.Tool;
namespace LevelEditor
{
    public class ItemCopyCommand : LevelEditCommand
    {
        private List<ItemData> m_copyTarget = new List<ItemData>();
        private List<ItemData> m_saveDatas = new List<ItemData>();
        
        private ObservableList<ItemData> m_targetAssets;
        private ObservableList<ItemData> m_itemAssets;
        
        private OutlinePainter m_outlinePainter;
        private List<ItemData> m_lastAssets = new List<ItemData>();

        private ItemFactory m_itemFactory = new ItemFactory();

        public ItemCopyCommand(ObservableList<ItemData> targetAssets,ObservableList<ItemData> itemAssets,OutlinePainter outlinePainter,List<ItemData> copyTarget)
        {
            m_targetAssets = targetAssets;
            m_itemAssets = itemAssets;
            m_outlinePainter = outlinePainter;
            m_copyTarget.AddRange(copyTarget);
        }
    
        public override void Execute()
        {
            if (m_saveDatas.Count == 0)
            {
                m_saveDatas = CopyItems(m_copyTarget,m_saveDatas);
            }
            else
            {
                SetDatasActive(m_saveDatas, true);
            }
            m_lastAssets.Clear();
            m_lastAssets.AddRange(m_targetAssets);
            m_itemAssets.AddRange(m_saveDatas);
            m_targetAssets.Clear();
            m_targetAssets.AddRange(m_saveDatas);
            m_outlinePainter.SetTargetObj = m_targetAssets.GetItemObjs();
        }

        public override void Undo()
        {
            m_targetAssets.Clear();
            m_targetAssets.AddRange(m_lastAssets);
            m_itemAssets.RemoveAll(m_saveDatas);
            m_outlinePainter.SetTargetObj = m_targetAssets.GetItemObjs();
            SetDatasActive(m_saveDatas, false);
        }

        private List<ItemData> CopyItems(List<ItemData> copyDatas,List<ItemData> saveDatas)
        {
            Vector3 oriPos = Vector3.zero;
            
            foreach (var copyData in copyDatas)
            {
                ItemData newData = copyData.Copy(m_itemFactory.CreateItem(copyData.GetItemProduct));
                (Vector3 position, Quaternion rotation, Vector3 scale) = copyData.GetItemObjEditor.transform.GetTransformValue();
                newData.GetItemObjEditor.transform.SetTransformValue(position,rotation,scale);
                oriPos += newData.GetItemObjEditor.transform.position;
                saveDatas.Add(newData);
            }

            oriPos /= saveDatas.Count;
            
            Vector3 targetPos = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width/2, Screen.height/2,
                Mathf.Abs(Camera.main.transform.position.z)));

            Vector3 direction = targetPos - oriPos;
            
            foreach (var saveData in saveDatas)
            {
                Vector3 oldPosition = saveData.GetItemObjEditor.transform.position;
                saveData.GetItemObjEditor.transform.position = oldPosition + direction;
            }
            
            return saveDatas;
        }

        private void SetDatasActive(List<ItemData> itemDatas,bool active)
        {
            foreach (var itemData in itemDatas)
            {
                itemData.SetActiveEditor(active);
            }
        }
    }

}