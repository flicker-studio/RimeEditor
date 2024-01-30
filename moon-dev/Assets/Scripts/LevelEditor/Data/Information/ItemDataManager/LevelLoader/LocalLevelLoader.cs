using System.Collections.Generic;
using System.IO;
using System.Linq;
using Cysharp.Threading.Tasks;
using Frame.Static.Global;

namespace LevelEditor
{
    public class LocalLevelLoader : LevelLoader
    {
        override public async UniTask LoadLevelFiles(List<LevelData> levelDatas)
        {
            levelDatas.Clear();

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
                    levelDatas.Add(levelData);

                    if (File.Exists(imagePath))
                    {
                        await UpdateImage(levelData, imagePath);
                    }
                }
            }
        }
    }
}