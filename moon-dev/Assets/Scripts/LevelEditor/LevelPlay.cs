using System;
using System.Collections.Generic;

namespace LevelEditor
{
    public class LevelPlay : MonoSingleton<LevelPlay>, IAutoCreateSingleton
    {
        private readonly List<SubLevel> m_levelDatas = new();
        private          SubLevel       m_currentSubLevel;
        private          int            m_index;
        public static    LevelPlay      Instance => Singleton;

        public async void Play(List<SubLevel> levelDatas, int index = 0)
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
            foreach (var itemData in m_currentSubLevel.ItemAssets) itemData.Preview();
        }

        public void ClearLevelObjs()
        {
            foreach (var itemData in m_currentSubLevel.ItemAssets) itemData.Reset();
        }

        public void Stop()
        {
            throw new NotImplementedException();

            // Moon.Runtime.InputManager.Instance.RemoveEscapeButtonDownAction = Stop;
            m_index = 0;
            ClearLevelObjs();
            //SceneLoader.Instance.RemoveCurrentScene();
        }
    }
}