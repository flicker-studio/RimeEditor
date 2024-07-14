using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using Cysharp.Threading.Tasks;
using LevelEditor.Settings;
using Newtonsoft.Json;
using UnityEngine;

namespace LevelEditor
{
    /// <summary>
    ///     A custom level data structure with both information and data
    /// </summary>
    [Obsolete]
    public class Level
    {
        private readonly GlobalSetting m_setting;

        /// <summary>
        ///     Specific level data can be instantiated in the scene
        /// </summary>
        public List<LevelData> LevelDataList;

        /// <summary>
        ///     Basic information about the level
        /// </summary>
        public LevelInfo LevelInfo;

        private Texture2D m_cover;

        private Guid m_guid;

        private string m_path;

        public Level(GlobalSetting setting)
        {
            m_setting = setting;
        }

        /// <summary>
        ///     The ID of the level, randomly generated and unique
        /// </summary>
        public Guid Guid => m_guid;

        public async UniTask Create()
        {
            m_guid = Guid.NewGuid();
            m_path = Path.Combine(m_setting.RootPath, m_guid.ToString());

            if (Directory.Exists(m_path)) throw new InvalidOperationException("A file with the same name already exists");

            // Create folders
            Directory.CreateDirectory(m_path);
            var infoPath = Path.Combine(m_path, ".inf");
            var dataPath = Path.Combine(m_path, m_setting.GamesDataName);
            var resPath  = Path.Combine(m_path, m_setting.ImagesDataName);

            // Initialization information file
            LevelInfo = new LevelInfo();
            var             json         = JsonConvert.SerializeObject(LevelInfo);
            var             levelText    = new FileInfo(infoPath);
            await using var streamWriter = levelText.CreateText();
            await streamWriter.WriteLineAsync(json);
        }

        public void Delete()
        {
            Directory.Delete(m_path, true);
        }

        /// <summary>
        ///     Deserialize class based on file under current path
        /// </summary>
        /// <param name="path">The path where the current folder is stored</param>
        public async UniTask Open(string path)
        {
            // Avoid to pass in a file address
            if (Path.HasExtension(path)) throw new InvalidOperationException();

            // Get folder name
            var guid = Path.GetFileNameWithoutExtension(path);
            m_guid = new Guid(guid);

            var       infoPath = Path.Combine(path, ".inf");
            using var reader   = new StreamReader(infoPath);
            var       infoJson = await reader.ReadToEndAsync();
            LevelInfo = JsonConvert.DeserializeObject<LevelInfo>(infoJson);

            // Collect data files
            var dataPath  = Path.Combine(path, m_setting.GamesDataName);
            var dirInfo   = new DirectoryInfo(dataPath);
            var fileInfos = dirInfo.GetFiles("*.dat", SearchOption.TopDirectoryOnly).ToList();

            // Deserialize data
            foreach (var fileInfo in fileInfos)
            {
                using var streamReader = fileInfo.OpenText();
                var       dataJson     = await streamReader.ReadToEndAsync();
                var       levelData    = JsonConvert.DeserializeObject<LevelData>(dataJson);
                LevelDataList.Add(levelData);
            }

            //TODO: Load resource files
            var resPath = Path.Combine(path, m_setting.ImagesDataName);
        }

        /// <summary>
        ///     Generate classes based on zip file and store them on local
        /// </summary>
        /// <param name="zipPath">Zip file location</param>
        public async UniTask Import(string zipPath)
        {
            // Check correctness
            try
            {
                var exr = Path.GetExtension(zipPath);

                if (exr != "zip") throw new InvalidOperationException("Wrong file suffix");

                using var archive = ZipFile.Open(zipPath, ZipArchiveMode.Read);
                var       entry   = archive.GetEntry(".inf");

                if (entry == null) throw new NullReferenceException("Not the proper file.");

                using var reader   = new StreamReader(entry.Open());
                var       infoJson = await reader.ReadToEndAsync();
                JsonConvert.DeserializeObject<LevelInfo>(infoJson);
            }
            catch (Exception e)
            {
                throw new InvalidDataException("File information is corrupted.", e);
            }

            // Unzip and load
            m_guid = new Guid();
            var storeFolder = Path.Combine(m_setting.RootPath, m_guid.ToString());
            ZipFile.ExtractToDirectory(zipPath, storeFolder);
            await Open(storeFolder);
        }

        public void Export(string targetPath)
        {
        }
    }
}