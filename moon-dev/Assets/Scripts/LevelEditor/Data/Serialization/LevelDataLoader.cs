using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using Cysharp.Threading.Tasks;
using Moon.Kernel.Setting;
using Newtonsoft.Json;
using UnityEngine.Networking;

namespace LevelEditor
{
    /// <summary>
    /// </summary>
    public static class LevelDataLoader
    {
        /// <summary>
        ///     Load all levels in this computer
        /// </summary>
        public static async UniTask<List<LevelData>> LoadLevelData(GlobalSetting setting)
        {
            var levelDataPath = setting.LevelPath;
            var dataList      = new List<LevelData>();

            if (!Directory.Exists(levelDataPath))
            {
                Directory.CreateDirectory(setting.LevelPath);
                return dataList;
            }

            var directoryInfo = new DirectoryInfo(setting.LevelPath);
            var files         = directoryInfo.GetFiles("*.json", SearchOption.AllDirectories).ToList();

            foreach (var fileInfo in files)
            {
                var dataPath = Path.ChangeExtension(fileInfo.FullName, null);

                if (!Directory.Exists(dataPath))
                {
                    continue;
                }

                var streamReader = fileInfo.OpenText();
                var json         = await streamReader.ReadToEndAsync();
                var levelData    = Deserialize(json);
                streamReader.Close();
                streamReader.Dispose();

                levelData.Path = $"Path:{dataPath}";

                dataList.Add(levelData);

                var imagePath =
                    $"{dataPath}" +
                    $"/{setting.ImagesDataName}" +
                    $"/{setting.CoverImageName}";

                if (File.Exists(imagePath))
                {
                    await UpdateImage(levelData, imagePath);
                }
            }

            return dataList;
        }

        /// <summary>
        ///     Open the level data file
        /// </summary>
        /// <param name="setting"></param>
        /// <param name="zipPath">Target zip file</param>
        public static async UniTask<LevelData> LoadArchive(GlobalSetting setting, string zipPath)
        {
            using var archive = ZipFile.Open(zipPath, ZipArchiveMode.Read);

            var entry = archive.GetEntry(setting.InformationFile);

            if (entry == null)
            {
                throw new Exception("Not the proper file.");
            }

            using var reader = new StreamReader(entry.Open());

            var infoJson = await reader.ReadToEndAsync();

            LevelInfo info;

            try
            {
                info = JsonConvert.DeserializeObject<LevelInfo>(infoJson);
            }
            catch (Exception)
            {
                throw new Exception("File information is corrupted.");
            }

            var storeFolder = Path.Combine(setting.RootPath, new Guid().ToString());

            //Unzip it to a persistent folder
            ZipFile.ExtractToDirectory(zipPath, storeFolder);

            //   await info.UpdateCover();
            return new LevelData();
        }

        /// <summary>
        ///     Delete a level and remove the cache
        /// </summary>
        /// <param name="levelData">The target level data to be deleted</param>
        /// <returns>Whether the deletion was successful </returns>
        public static bool Delete(LevelData levelData)
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
            //TODO:需加载SO
            throw new Exception("需加载SO");

            // data.Update();
            // var hashKey = data.HashKey;
            // var json = JsonConvert.SerializeObject(data, Formatting.Indented);
            //
            // if (!Directory.Exists(PersistentFileProperty.LEVEL_DATA_PATH))
            // {
            //     Directory.CreateDirectory(PersistentFileProperty.LEVEL_DATA_PATH);
            // }
            //
            // var levelPath = $"{PersistentFileProperty.LEVEL_DATA_PATH}/{hashKey}";
            // var gamesPath = $"{levelPath}/{PersistentFileProperty.GAMES_DATA_NAME}";
            // var imagesPath = $"{levelPath}/{PersistentFileProperty.IMAGES_DATA_NAME}";
            // var soundsPath = $"{levelPath}/{PersistentFileProperty.SOUNDS_DATA_NAME}";
            //
            // if (!Directory.Exists(levelPath))
            // {
            //     Directory.CreateDirectory(levelPath);
            //     Directory.CreateDirectory(gamesPath);
            //     Directory.CreateDirectory(imagesPath);
            //     Directory.CreateDirectory(soundsPath);
            // }
            //
            // var fileName = $"{gamesPath}//{hashKey}.json";
            // var levelText = new FileInfo(fileName);
            // var streamWriter = levelText.CreateText();
            // streamWriter.WriteLine(json);
            // streamWriter.Close();
            // streamWriter.Dispose();
            //
            // if (data.Cover != null)
            // {
            //     var dataBytes = data.Cover.EncodeToPNG();
            //     var savePath = $"{imagesPath}/{PersistentFileProperty.COVER_IMAGE_NAME}";
            //     var fileStream = File.Open(savePath, FileMode.OpenOrCreate);
            //     fileStream.Write(dataBytes, 0, dataBytes.Length);
            //     fileStream.Close();
            // }
            // else
            // {
            //     // TODO: correct citation
            //     var cullUICamera = Camera.main!.transform.GetChild(0).GetComponent<Camera>();
            //     cullUICamera.gameObject.SetActive(true);
            //     var screenTexture = new RenderTexture(Screen.width, Screen.height, 16);
            //     cullUICamera.targetTexture = screenTexture;
            //     RenderTexture.active = screenTexture;
            //     cullUICamera.Render();
            //     var renderedTexture = new Texture2D(Screen.width, Screen.height);
            //     renderedTexture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
            //     RenderTexture.active = null;
            //     var byteArray = renderedTexture.EncodeToPNG();
            //     File.WriteAllBytes($"{imagesPath}/{PersistentFileProperty.COVER_IMAGE_NAME}", byteArray);
            //     cullUICamera.gameObject.SetActive(false);
            // }
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

        /// <summary>
        ///     Write the json string to the target file
        /// </summary>
        public static void WriteToFile(byte[] bytes, string filePath)
        {
            File.WriteAllBytes(filePath, bytes);
        }

        private static async UniTask UpdateImage(LevelData levelData, string path)
        {
            var uwr = UnityWebRequestTexture.GetTexture("file:///" + path);
            await uwr.SendWebRequest();
            levelData.Cover = DownloadHandlerTexture.GetContent(uwr);
        }
    }
}