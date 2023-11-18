using System.Collections.Generic;
using Frame.Static.Global;
using Frame.Tool;

namespace LevelEditor
{
    public class LevelPlay : Singleton<LevelPlay>
    {
        private List<LevelData> m_levelDatas = new List<LevelData>();

        private LevelData currentLevel;

        private int m_index = 0;
        
        public async void Play(List<LevelData> levelDatas, int index = 0)
        {
            m_index = index;
            await SceneLoader.Instance.AddScene(GlobalSetting.Scenes.LEVEL_PLAY);
            m_levelDatas.Clear();
            m_levelDatas.AddRange(levelDatas);
            InputManager.Instance.AddEscapeButtonDownAction = Stop;
            ReadLevel();
        }

        public void NextLevel()
        {
            if (++m_index >= m_levelDatas.Count)
            {
                Stop();
                return;
            }
            ReadLevel();
        }

        public void ReadLevel()
        {
            ClearLevelObjs();
            currentLevel = m_levelDatas[m_index];
            foreach (var itemData in currentLevel.ItemAssets)
            {
                itemData.SetActivePlay(true);
            }
        }

        public void ClearLevelObjs()
        {
            if (currentLevel == null) return;
            foreach (var itemData in currentLevel.ItemAssets)
            {
                itemData.SetActivePlay(false);
            }
        }

        public void Stop()
        {
            InputManager.Instance.RemoveEscapeButtonDownAction = Stop;
            m_index = 0;
            ClearLevelObjs();
            SceneLoader.Instance.RemoveCurrentScene();
        }
        
    }
}
