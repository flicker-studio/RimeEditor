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
    public abstract class LevelLoader
    {
        public abstract UniTask LoadLevelFiles(List<LevelData> levelDatas);

        public virtual bool OpenLocalLevelDirectory(string path,List<LevelData> levelDatas)
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
            foreach (var data in levelDatas)
            {
                if (data.GetKey == levelData.GetKey) return false;
            }
            return FileMethod.Move(path, $"{GlobalSetting.PersistentFileProperty.LEVEL_DATA_PATH}/{levelDirectoryName}");
        } 
        
        public bool DeleteLevel(LevelData levelData)
        {
            if (!Directory.Exists(levelData.Path.Replace("Path:", "")))
            {
                return false;
            }
            Directory.Delete(levelData.Path.Replace("Path:",""),true);
            return true;
        }
        
        public virtual void ToJson(LevelData chooseLevelData)
        {
            string hashKey = chooseLevelData.GetKey;
            chooseLevelData.UpdateTime();
            if (hashKey == null || hashKey == "")
            {
                chooseLevelData.SetKey = $"{chooseLevelData.GetName}{chooseLevelData.GetTime}".ToSHA256();
                hashKey = chooseLevelData.GetKey;
            }
            string json = JsonConvert.SerializeObject(chooseLevelData, Formatting.Indented);
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
            if (chooseLevelData.GetLevelCoverImage != null)
            {
                byte[] dataBytes = chooseLevelData.GetLevelCoverImage.EncodeToPNG();
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
        
        public virtual LevelData FromJson(string json)
        {
            try
            {
                return JsonConvert.DeserializeObject<LevelData>(json);
            }
            catch (Exception e)
            {
                return null;
            }
        }
        
        protected virtual async UniTask UpdateImage(LevelData levelData,string path)
        {
            UnityWebRequest uwr = UnityWebRequestTexture.GetTexture("file:///" + path);
            await uwr.SendWebRequest();;
            levelData.SetLevelCoverImage = DownloadHandlerTexture.GetContent(uwr);
        }
    }
}
