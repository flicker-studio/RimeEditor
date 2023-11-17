using System.Collections.Generic;
using Frame.Static.Extensions;
using Frame.Static.Global;
using Frame.Tool;
using Frame.Tool.Pool;
using UnityEngine;

namespace LevelEditor
{
    public class LevelPlay : Singleton<LevelPlay>
    {
        private List<LevelData> m_levelDatas = new List<LevelData>();

        private List<GameObject> m_levelObjs = new List<GameObject>();

        private int index = 0;

        public async void Play(List<LevelData> levelDatas, int index = 0)
        {
            await SceneLoader.Instance.AddScene(GlobalSetting.Scenes.LEVEL_PLAY);
            m_levelDatas.Clear();
            m_levelDatas.AddRange(levelDatas);
            ReadLevel();
        }

        public void NextLevel()
        {
            index++;
            ReadLevel();
        }

        public void ReadLevel()
        {
            ClearLevelObjs();
            LevelData currentLevel = m_levelDatas[index];
            foreach (var itemAsset in currentLevel.ItemAssets)
            {
                GameObject itemObj = ObjectPool.Instance.OnTake(itemAsset.GetItemProduct.ItemObject);
                (Vector3 position, Quaternion rotation, Vector3 scale) = itemAsset.GetTransformFromData();
                itemObj.transform.SetTransformValue(position,rotation,scale);
            }
        }

        public void ClearLevelObjs()
        {
            foreach (var obj in m_levelObjs)
            {
                ObjectPool.Instance.OnRelease(obj);
            }
            m_levelObjs.Clear();
        }

        public void Stop()
        {
            
        }
    }
}
