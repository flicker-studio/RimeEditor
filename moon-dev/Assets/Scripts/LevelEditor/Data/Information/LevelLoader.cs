using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Cysharp.Threading.Tasks;
using Moon.Kernel.Extension;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;
using static Frame.Static.Global.GlobalSetting;

namespace LevelEditor
{
    /// <summary>
    /// </summary>
    public static class LevelLoader
    {
        /// <summary>
        ///     Load all levels
        /// </summary>
        public static async UniTask<List<LevelData>> LoadLevelDatas()
        {
            List<LevelData> levelDatas = new();

            if (!Directory.Exists(PersistentFileProperty.LEVEL_DATA_PATH))
            {
                Directory.CreateDirectory(PersistentFileProperty.LEVEL_DATA_PATH);
            }

            var direction = new DirectoryInfo(PersistentFileProperty.LEVEL_DATA_PATH);
            var files = direction.GetFiles("*.json", SearchOption.AllDirectories).ToList();

            foreach (var fileInfo in files)
            {
                var levelPath = $"{PersistentFileProperty.LEVEL_DATA_PATH}" +
                                $"/{fileInfo.Name.Replace(".json", "")}";

                if (Directory.Exists(levelPath))
                {
                    var streamReader = fileInfo.OpenText();
                    var json = await streamReader.ReadToEndAsync();
                    var levelData = Deserialize(json);
                    streamReader.Close();
                    streamReader.Dispose();

                    levelData.Path = $"Path:{levelPath}";

                    levelDatas.Add(levelData);

                    var imagePath =
                        $"{levelPath}" +
                        $"/{PersistentFileProperty.IMAGES_DATA_NAME}" +
                        $"/{PersistentFileProperty.COVER_IMAGE_NAME}";

                    if (File.Exists(imagePath))
                    {
                        await UpdateImage(levelData, imagePath);
                    }
                }
            }

            return levelDatas;
        }

        /// <summary>
        ///     Open the level file (currently existing as a directory)
        /// </summary>
        /// <param name="path">Target file </param>
        /// <param name="levelDatas">List of levels</param>
        /// <returns>True when a different level is successfully loaded </returns>
        public static bool OpenLocalLevelDirectory(string path, ref List<LevelData> levelDatas)
        {
            var levelDirectoryName = path.GetSuffix('/');
            var levelDataFolderPath = $"{path}/{PersistentFileProperty.GAMES_DATA_NAME}";
            var imageDataFolderPath = $"{path}/{PersistentFileProperty.IMAGES_DATA_NAME}";
            var soundsDataFolderPath = $"{path}/{PersistentFileProperty.SOUNDS_DATA_NAME}";
            var levelDataFilePath = $"{levelDataFolderPath}/{levelDirectoryName}.json";

            if (!Directory.Exists(levelDataFolderPath))
            {
                return false;
            }

            if (!Directory.Exists(imageDataFolderPath))
            {
                return false;
            }

            if (!Directory.Exists(soundsDataFolderPath))
            {
                return false;
            }

            if (!File.Exists($"{levelDataFilePath}"))
            {
                return false;
            }

            var streamReader = File.OpenText(levelDataFilePath);
            var levelData = Deserialize(streamReader.ReadToEnd());
            streamReader.Close();
            streamReader.Dispose();

            // Determine whether it is configured for the same level
            foreach (var data in levelDatas)
                if (data.HashKey == levelData.HashKey)
                {
                    return false;
                }

            return DirectoryExtension.MoveSpanningDisk(path, $"{PersistentFileProperty.LEVEL_DATA_PATH}/{levelDirectoryName}");
        }

        /// <summary>
        ///     Delete a level and remove the cache
        /// </summary>
        /// <param name="levelData">The target level data to be deleted</param>
        /// <returns>Whether the deletion was successfu </returns>
        public static bool DeleteLevel(LevelData levelData)
        {
            var targetPath = levelData.Path.Replace("Path:", "");

            if (!Directory.Exists(targetPath))
            {
                return false;
            }

            Directory.Delete(targetPath, true);
            return true;
        }

        /// <summary>
        ///     Serialize <paramref name="data" /> and write to a file
        /// </summary>
        /// <param name="data"></param>
        public static void ToJson(LevelData data)
        {
            data.Update();
            var hashKey = data.HashKey;
            var json = JsonConvert.SerializeObject(data, Formatting.Indented);

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

            var fileName = $"{gamesPath}//{hashKey}.json";
            var levelText = new FileInfo(fileName);
            var streamWriter = levelText.CreateText();
            streamWriter.WriteLine(json);
            streamWriter.Close();
            streamWriter.Dispose();

            if (data.Cover != null)
            {
                var dataBytes = data.Cover.EncodeToPNG();
                var savePath = $"{imagesPath}/{PersistentFileProperty.COVER_IMAGE_NAME}";
                var fileStream = File.Open(savePath, FileMode.OpenOrCreate);
                fileStream.Write(dataBytes, 0, dataBytes.Length);
                fileStream.Close();
            }
            else
            {
                // TODO: correct citation
                var cullUICamera = Camera.main!.transform.GetChild(0).GetComponent<Camera>();
                cullUICamera.gameObject.SetActive(true);
                var screenTexture = new RenderTexture(Screen.width, Screen.height, 16);
                cullUICamera.targetTexture = screenTexture;
                RenderTexture.active = screenTexture;
                cullUICamera.Render();
                var renderedTexture = new Texture2D(Screen.width, Screen.height);
                renderedTexture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
                RenderTexture.active = null;
                var byteArray = renderedTexture.EncodeToPNG();
                File.WriteAllBytes($"{imagesPath}/{PersistentFileProperty.COVER_IMAGE_NAME}", byteArray);
                cullUICamera.gameObject.SetActive(false);
            }
        }

        /// <summary>
        ///     Deserialize json into level data
        /// </summary>
        public static LevelData Deserialize(string json)
        {
            return JsonConvert.DeserializeObject<LevelData>(json);
        }

        /// <summary>
        ///     Write the json string to the target file
        /// </summary>
        public static void WriteToFile(string json, string filePath)
        {
            throw new NotImplementedException();
        }

        private static async UniTask UpdateImage(LevelData levelData, string path)
        {
            var uwr = UnityWebRequestTexture.GetTexture("file:///" + path);
            await uwr.SendWebRequest();
            levelData.Cover = DownloadHandlerTexture.GetContent(uwr);
        }
    }
}