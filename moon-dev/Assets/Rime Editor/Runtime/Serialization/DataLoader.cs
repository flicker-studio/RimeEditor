using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using LevelEditor;
using Newtonsoft.Json;
using UnityEngine;

namespace RimeEditor.Runtime
{
    /// <summary>
    ///     A static class that loads data
    /// </summary>
    public static class DataLoader
    {
        /// <summary>
        ///     A specific file that records data information
        /// </summary>
        public const string DataFilePattern = ".inf";

        public static string StoreFolderPath = Application.persistentDataPath;

        /// <summary>
        ///     Loads data stored on the local computer
        /// </summary>
        public static async Task<List<LevelInfo>> LoadLocal()
        {
            var data_list = new List<LevelInfo>();

            // Check the folder location
            if (!Directory.Exists(StoreFolderPath))
            {
                Directory.CreateDirectory(StoreFolderPath);
                return data_list;
            }

            // Load all the adapted files
            var directory_info      = new DirectoryInfo(StoreFolderPath);
            var directory_info_list = directory_info.GetDirectories().ToList();

            // Check all the directory information and load it
            // ReSharper disable once ForeachCanBePartlyConvertedToQueryUsingAnotherGetEnumerator
            foreach (var directory in directory_info_list)
            {
                var data_file_info_list = directory.GetFiles(DataFilePattern).ToList();

                if (data_file_info_list.Count != 1) continue;

                var data_file_stream_reader = data_file_info_list[0].OpenText();
                var data_file_json_text     = await data_file_stream_reader.ReadToEndAsync();

                data_file_stream_reader.Close();
                data_file_stream_reader.Dispose();

                var level_data = JsonConvert.DeserializeObject<LevelInfo>(data_file_json_text);

                data_list.Add(level_data);
            }

            return data_list;
        }

        /// <summary>
        ///      Loads data stored in a archive file
        /// </summary>
        /// <param name="zipPath">Target zip file</param>
        public static async Task<LevelInfo> LoadArchive([NotNull] string zipPath)
        {
            using var archive = ZipFile.Open(zipPath, ZipArchiveMode.Read);

            var entry = archive.GetEntry(DataFilePattern);

            if (entry == null) throw new Exception("Not the proper file.");

            using var data_file_stream_reader = new StreamReader(entry.Open());

            var data_file_json_text = await data_file_stream_reader.ReadToEndAsync();

            try
            {
                var info           = JsonConvert.DeserializeObject<LevelInfo>(data_file_json_text);
                var extract_folder = Path.Combine(StoreFolderPath, info.ID.ToString());
                //Unzip it to a persistent folder
                ZipFile.ExtractToDirectory(zipPath, extract_folder);
                return info;
            }
            catch (Exception)
            {
                throw new Exception("File information is corrupted.");
            }
        }

        /// <summary>
        ///     Delete a level and remove the cache
        /// </summary>
        /// <param name="levelData">The target level data to be deleted</param>
        /// <returns>Whether the deletion was successful </returns>
        public static bool Delete(LevelData levelData)
        {
            throw new NotImplementedException();
            // var targetPath = levelData.Path.Replace("Path:", "");
            //
            // if (!Directory.Exists(targetPath)) return false;
            //
            // Directory.Delete(targetPath, true);
            // return true;
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

        public static void Export(string targetPath)
        {
        }
    }
}