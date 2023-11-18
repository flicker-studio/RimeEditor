using Cinemachine;
using Data.ScriptableObject;
using Frame.Tool.Pool;
using UnityEngine;

namespace LevelEditor
{
    public class EntranceData : ItemData
    {
        public EntranceData(ItemProduct itemProduct) : base(itemProduct)
        {

        }

        public override void SetActivePlay(bool active)
        {
            base.SetActivePlay(active);
        }
    }
}
