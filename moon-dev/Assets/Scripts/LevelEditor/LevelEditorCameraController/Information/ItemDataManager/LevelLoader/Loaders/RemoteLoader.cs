using System.Collections.Generic;
using Cysharp.Threading.Tasks;

namespace LevelEditor
{
    public class RemoteLoader : LevelLoader
    {
        public override UniTask LoadLevelFiles(List<LevelData> levelDatas)
        {
            return new UniTask();
        }
    }
}
