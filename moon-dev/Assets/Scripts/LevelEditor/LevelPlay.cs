using System;
using System.Collections.Generic;
using Frame.Tool;

namespace LevelEditor
{
    public class LevelPlay : UnityToolkit.MonoSingleton<LevelPlay>,UnityToolkit.IAutoCreateSingleton
    {
        public static LevelPlay Instance => Singleton;
        
        private List<SubLevelData> m_levelDatas = new List<SubLevelData>();

        private SubLevelData m_currentSubLevel;

        private int m_index = 0;
        
        public async void Play(List<SubLevelData> levelDatas, int index = 0)
        {
            //TODO:需加载SO
            throw new Exception("需加载SO");
            // m_index = index;
            // await SceneLoader.Instance.AddScene(GlobalSetting.Scenes.LEVEL_PLAY);
            // m_levelDatas.Clear();
            // m_levelDatas.AddRange(levelDatas);
            // Frame.Tool.InputManager.Instance.AddEscapeButtonDownAction = Stop;
            // ReadLevel();
        }

        public void NextLevel()
        {
            if (++m_index >= m_levelDatas.Count)
            {
                Stop();
                return;
            }
            ClearLevelObjs();
            ReadLevel();
        }

        public void ReadLevel()
        {
            m_currentSubLevel = m_levelDatas[m_index];
            foreach (var itemData in m_currentSubLevel.ItemAssets)
            {
                itemData.SetActivePlay(true);
            }
        }

        public void ClearLevelObjs()
        {
            foreach (var itemData in m_currentSubLevel.ItemAssets)
            {
                itemData.SetActivePlay(false);
            }
        }

        public void Stop()
        {
            Frame.Tool.InputManager.Instance.RemoveEscapeButtonDownAction = Stop;
            m_index = 0;
            ClearLevelObjs();
            SceneLoader.Instance.RemoveCurrentScene();
        }
        
    }
}
