using Newtonsoft.Json;

namespace LevelEditor
{
    public class SubLevelData
    {
        [JsonProperty("Name", Order = 1)] public string Name;

        [JsonProperty("ItemAssets", Order = 2)]
        public ObservableList<ItemDataBase> ItemAssets = new();

        public SubLevelData(string name)
        {
            Name = name;
        }
    }
}