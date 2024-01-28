using System;
using System.Collections.Generic;
using System.IO;
using Cysharp.Threading.Tasks;
using Moon.Kernel.Extension;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;
using static Frame.Static.Global.GlobalSetting;

namespace LevelEditor
{
    public abstract class LevelLoader
    {
        public abstract UniTask LoadLevelFiles(List<LevelData> levelDatas);

        public virtual bool OpenLocalLevelDirectory(string path, List<LevelData> levelDatas)
        {
            string levelDirectoryName = path.GetSuffix('/');
            var levelDataFolderPath = $"{path}/{PersistentFileProperty.GAMES_DATA_NAME}";
            var imageDataFolderPath = $"{path}/{PersistentFileProperty.IMAGES_DATA_NAME}";
            var soundsDataFolderPath = $"{path}/{PersistentFileProperty.SOUNDS_DATA_NAME}";
            string levelDataFilePath = $"{levelDataFolderPath}/{levelDirectoryName}.json";
            if (!Directory.Exists(levelDataFolderPath)) return false;
            if (!Directory.Exists(imageDataFolderPath)) return false;
            if (!Directory.Exists(soundsDataFolderPath)) return false;
            if (!File.Exists($"{levelDataFilePath}")) return false;

            var streamReader = File.OpenText(levelDataFilePath);
            LevelData levelData = FromJson(streamReader.ReadToEnd());
            streamReader.Close();
            streamReader.Dispose();
            if (levelData == null) return false;

            foreach (var data in levelDatas)
            {
                if (data.GetKey == levelData.GetKey) return false;
            }
            
            return DirectoryExtension.MoveSpanningDisk(path, $"{PersistentFileProperty.LEVEL_DATA_PATH}/{levelDirectoryName}");
        }

        public bool DeleteLevel(LevelData levelData)
        {
            if (!Directory.Exists(levelData.Path.Replace("Path:", "")))
            {
                return false;
            }

            Directory.Delete(levelData.Path.Replace("Path:", ""), true);
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

            if (!Directory.Exists(PersistentFileProperty.LEVEL_DATA_PATH))
            {
                Directory.CreateDirectory(PersistentFileProperty.LEVEL_DATA_PATH);
            }

            var levelPath = $"{PersistentFileProperty.LEVEL_DATA_PATH}/{hashKey}";
            var gamesPath = $"{levelPath}/{PersistentFileProperty.GAMES_DATA_NAME}";
            var imagesPath = $"{levelPath}/{PersistentFileProperty.IMAGES_DATA_NAME}";
            var soundsPath = $"{levelPath}/{PersistentFileProperty.SOUNDS_DATA_NAME}";

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
                var savePath = $"{imagesPath}/{PersistentFileProperty.COVER_IMAGE_NAME}";
                var fileStream = File.Open(savePath, FileMode.OpenOrCreate);
                fileStream.Write(dataBytes, 0, dataBytes.Length);
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
                File.WriteAllBytes($"{imagesPath}/{PersistentFileProperty.COVER_IMAGE_NAME}", byteArray);
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

        protected virtual async UniTask UpdateImage(LevelData levelData, string path)
        {
            UnityWebRequest uwr = UnityWebRequestTexture.GetTexture("file:///" + path);
            await uwr.SendWebRequest();
            ;
            levelData.SetLevelCoverImage = DownloadHandlerTexture.GetContent(uwr);
        }
    }
}