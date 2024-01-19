using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Cysharp.Threading.Tasks;
using Frame.Static.Extensions;
using Frame.Static.Global;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;

namespace LevelEditor
{
    public delegate void SyncLevelData(SubLevelData subLevelData);
    public class DataManager
    {
        public ObservableList<ItemData> TargetItems = new ObservableList<ItemData>();

        public List<GameObject> TargetObjs = new List<GameObject>();

        public ObservableList<ItemData> ItemAssets => GetCurrentSubLevel?.ItemAssets;
        
        public SubLevelData GetCurrentSubLevel => GetCurrentLevel?.GetSubLevelDatas[m_subLevelIndex];

        public LevelData GetCurrentLevel => m_chooseLevelData;

        public List<LevelData> GetAllLevels => m_levelDatas;

        public int GetCurrentSubLevelIndex => m_subLevelIndex;

        public SyncLevelData SyncLevelData;
        
        public void SetActiveEditors(bool value)
        {
            if(ItemAssets == null) return;
            foreach (var itemAsset in ItemAssets)
            {
                itemAsset.SetActiveEditor(value);
            }
        }
        
        public SubLevelData AddSubLevel()
        {
            SetItemAssetActive(ItemAssets,false);
            TargetItems.Clear();
            m_subLevelDatas.Add(new SubLevelData($"Level {m_subLevelDatas.Count}"));
            m_subLevelIndex = m_subLevelDatas.Count - 1;
            SetItemAssetActive(ItemAssets,true);
            SyncLevelData?.Invoke(GetCurrentSubLevel);
            return m_subLevelDatas.Last();
        }

        public List<SubLevelData> SetSubLevels(List<SubLevelData> levelDatas)
        {
            m_subLevelDatas.Clear();
            m_subLevelDatas.AddRange(levelDatas);
            m_subLevelIndex = Mathf.Clamp(m_subLevelIndex, 0, m_subLevelDatas.Count - 1);
            SyncLevelData?.Invoke(GetCurrentSubLevel);
            return m_subLevelDatas;
        }

        public void DeleteSubLevel()
        {
            SetItemAssetActive(ItemAssets,false);
            TargetItems.Clear();
            m_subLevelDatas.RemoveAt(m_subLevelIndex);
            m_subLevelIndex = Mathf.Clamp(m_subLevelIndex, 0, m_subLevelDatas.Count - 1);
            SetItemAssetActive(ItemAssets,true);
            SyncLevelData?.Invoke(GetCurrentSubLevel);
        }

        public void SetSubLevelIndex(int index, bool isReload = false)
        {
            if(m_subLevelIndex == index && !isReload) return;
            if (isReload)
            {
                for (var i = 0; i < m_subLevelDatas.Count; i++)
                {
                    SetItemAssetActive(m_subLevelDatas[i].ItemAssets,false,isReload);
                }
            }
            else
            {
                SetItemAssetActive(ItemAssets,false,isReload);
            }
            TargetItems.Clear();
            m_subLevelIndex = Mathf.Clamp(index, 0, m_subLevelDatas.Count - 1);
            SetItemAssetActive(ItemAssets,true,isReload);
            SyncLevelData?.Invoke(GetCurrentSubLevel);
        }

        public List<SubLevelData> ShowSubLevels()
        {
            List<SubLevelData> tempList = new List<SubLevelData>();
            tempList.AddRange(m_subLevelDatas);
            return tempList;
        }
        
        public async UniTask LoadLevelFiles()
        {
            SetActiveEditors(false);
            m_levelDatas.Clear();
            if (!Directory.Exists(GlobalSetting.PersistentFileProperty.LEVEL_DATA_PATH))
            {
                Directory.CreateDirectory(GlobalSetting.PersistentFileProperty.LEVEL_DATA_PATH);
            }
            DirectoryInfo direction = new DirectoryInfo(GlobalSetting.PersistentFileProperty.LEVEL_DATA_PATH);
            List<FileInfo> files = direction.GetFiles("*.json", SearchOption.AllDirectories).ToList();
            foreach (var fileInfo in files)
            {
                string levelPath = $"{GlobalSetting.PersistentFileProperty.LEVEL_DATA_PATH}" +
                                   $"/{fileInfo.Name.Replace(".json", "")}";
                if (Directory.Exists(levelPath))
                {
                    string imagePath = $"{levelPath}" +
                                       $"/{GlobalSetting.PersistentFileProperty.IMAGES_DATA_NAME}" +
                                       $"/{GlobalSetting.PersistentFileProperty.COVER_IMAGE_NAME}";
                    StreamReader streamReader = fileInfo.OpenText();
                    LevelData levelData = FromJson(streamReader.ReadToEnd());
                    streamReader.Close();
                    streamReader.Dispose();
                    if (levelData == null)
                    {
                        continue;
                    }
                    levelData.Path = $"Path:{levelPath}";
                    m_levelDatas.Add(levelData);
                    if (File.Exists(imagePath))
                    {
                        await UpdateImage(levelData, imagePath);
                    }
                }
            }
        }

        public bool OpenLocalLevelDirectory(string path)
        {
            string levelDirectoryName = path.ReserveReciprocal('/');
            string levelDataFolderPath = $"{path}/{GlobalSetting.PersistentFileProperty.GAMES_DATA_NAME}";
            string imageDataFolderPath = $"{path}/{GlobalSetting.PersistentFileProperty.IMAGES_DATA_NAME}";
            string soundsDataFolderPath = $"{path}/{GlobalSetting.PersistentFileProperty.SOUNDS_DATA_NAME}";
            string levelDataFilePath = $"{levelDataFolderPath}/{levelDirectoryName}.json";
            if (!Directory.Exists(levelDataFolderPath)) return false;
            if (!Directory.Exists(imageDataFolderPath)) return false;
            if (!Directory.Exists(soundsDataFolderPath)) return false;
            if (!File.Exists($"{levelDataFilePath}")) return false;
            StreamReader streamReader = File.OpenText(levelDataFilePath);
            LevelData levelData = FromJson(streamReader.ReadToEnd());
            streamReader.Close();
            streamReader.Dispose();
            if (levelData == null) return false;
            foreach (var data in m_levelDatas)
            {
                if (data.GetKey == levelData.GetKey) return false;
            }
            FileUtil.MoveFileOrDirectory(path, $"{GlobalSetting.PersistentFileProperty.LEVEL_DATA_PATH}/{levelDirectoryName}");
            return true;
        } 
        
        public void ToJson()
        {
            foreach (var itemAsset in GetCurrentSubLevel.ItemAssets)
            {
                itemAsset.GetTransformToData();
            }
            string hashKey = m_chooseLevelData.GetKey;
            m_chooseLevelData.UpdateTime();
            if (hashKey == null || hashKey == "")
            {
                m_chooseLevelData.SetKey = $"{m_chooseLevelData.GetName}{m_chooseLevelData.GetTime}".ToSHA256();
                hashKey = m_chooseLevelData.GetKey;
            }
            string json = JsonConvert.SerializeObject(m_chooseLevelData, Formatting.Indented);
            if (!Directory.Exists(GlobalSetting.PersistentFileProperty.LEVEL_DATA_PATH))
            {
                Directory.CreateDirectory(GlobalSetting.PersistentFileProperty.LEVEL_DATA_PATH);
            }
            string levelPath = $"{GlobalSetting.PersistentFileProperty.LEVEL_DATA_PATH}/{hashKey}";
            string gamesPath = $"{levelPath}/{GlobalSetting.PersistentFileProperty.GAMES_DATA_NAME}";
            string imagesPath = $"{levelPath}/{GlobalSetting.PersistentFileProperty.IMAGES_DATA_NAME}";
            string soundsPath = $"{levelPath}/{GlobalSetting.PersistentFileProperty.SOUNDS_DATA_NAME}";
            if (!Directory.Exists(levelPath))
            {
                Directory.CreateDirectory(levelPath);
                Directory.CreateDirectory(gamesPath);
                Directory.CreateDirectory(imagesPath);
                Directory.CreateDirectory(soundsPath);
            }
            StreamWriter streamWriter;
            FileInfo levelText = new FileInfo($"{gamesPath}//{hashKey}.json");
            streamWriter = levelText.CreateText();
            streamWriter.WriteLine(json);
            streamWriter.Close();
            streamWriter.Dispose();
            if (m_chooseLevelData.GetLevelCoverImage != null)
            {
                byte[] dataBytes = m_chooseLevelData.GetLevelCoverImage.EncodeToPNG();
                string savePath = $"{imagesPath}/{GlobalSetting.PersistentFileProperty.COVER_IMAGE_NAME}";
                FileStream fileStream = File.Open(savePath,FileMode.OpenOrCreate);
                fileStream.Write(dataBytes,0,dataBytes.Length);
                fileStream.Close();
            }
            else
            {
                Camera cullUICamera = Camera.main.transform.GetChild(0).GetComponent<Camera>();
                cullUICamera.gameObject.SetActive(true);
                RenderTexture screenTexture = new RenderTexture(Screen.width, Screen.height, 16);
                cullUICamera.targetTexture = screenTexture;
                RenderTexture.active = screenTexture;
                cullUICamera.Render();
                Texture2D renderedTexture = new Texture2D(Screen.width, Screen.height);
                renderedTexture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
                RenderTexture.active = null;
                byte[] byteArray = renderedTexture.EncodeToPNG();
                File.WriteAllBytes($"{imagesPath}/{GlobalSetting.PersistentFileProperty.COVER_IMAGE_NAME}", byteArray);
                cullUICamera.gameObject.SetActive(false);
            }
        }
        
        public LevelData FromJson(string json)
        {
            SetActiveEditors(false);
            try
            {
                return JsonConvert.DeserializeObject<LevelData>(json);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public void CreateLevel()
        {
            m_chooseLevelData = new LevelData();
            InitLevel();
        }
        
        public void OpenLevel(LevelData levelData)
        {
            m_chooseLevelData = levelData;
            InitLevel(levelData);
        }

        public bool DeleteLevel(LevelData levelData)
        {
            return FileUtil.DeleteFileOrDirectory(levelData.Path.Replace("Path:",""));
        }

        private List<SubLevelData> m_subLevelDatas => GetCurrentLevel.GetSubLevelDatas;
        
        private List<LevelData> m_levelDatas = new List<LevelData>();

        private LevelData m_chooseLevelData;

        private int m_subLevelIndex;

        public DataManager()
        {
            InitEvent();
        }

        private void ClearLevel()
        {
            foreach (var subLevelData in m_subLevelDatas)
            {
                foreach (var itemAsset in subLevelData.ItemAssets)
                {
                    itemAsset.SetActiveEditor(false);
                }
            }
        }

        private void InitLevel()
        {
            m_subLevelIndex = 0;
            m_subLevelDatas.Add(new SubLevelData($"Level {m_subLevelDatas.Count}"));
        }
        
        private void InitLevel(LevelData levelData)
        {
            SetSubLevelIndex(0, true);
        }
        
        private void InitEvent()
        {
            TargetItems.OnAddRange += SyncTargetObj;
            TargetItems.OnAdd += SyncTargetObj;
            TargetItems.OnClear += SyncTargetObj;
        }
        
        private void SetItemAssetActive(ObservableList<ItemData> itemDatas,bool active,bool isReload = false)
        {
            foreach (var itemData in itemDatas)
            {
                itemData.SetActiveEditor(active,isReload);
            }
        }

        private void SyncTargetObj(List<ItemData> list)
        {
            SyncTargetObj();
        }
        
        private void SyncTargetObj(ItemData list)
        {
            SyncTargetObj();
        }
        
        private void SyncTargetObj()
        {
            TargetObjs = TargetItems.GetItemObjs();
        }
        
        private async UniTask UpdateImage(LevelData levelData,string path)
        {
            UnityWebRequest uwr = UnityWebRequestTexture.GetTexture("file:///" + path);
            await uwr.SendWebRequest();;
            levelData.SetLevelCoverImage = DownloadHandlerTexture.GetContent(uwr);
        }
    }
}
