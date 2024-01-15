using Cinemachine;
using Data.ScriptableObject;
using Frame.Tool.Pool;
using Item;
using UnityEngine;

namespace LevelEditor
{
    public class EntranceData : ItemData
    {
        public EntranceData(ItemProduct itemProduct) : base(itemProduct)
        {

        }

        public override ItemData Copy(ItemData saveData)
        {
            return saveData;
        }

        public override ItemDataType ItemDataType => ItemDataType.Entrance;

        public override void SetActivePlay(bool active)
        {
            base.SetActivePlay(active);
            if (active)
            {
                m_itemObjPlay.GetComponent<ItemPlay>().Play();
            }
            else
            {
                m_itemObjPlay.GetComponent<ItemPlay>().Stop();
            }
        }
    }
}
