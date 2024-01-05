namespace LevelEditor
{
    public class SubLevelData
    {
        public string Name;
        
        public ObservableList<ItemData> ItemAssets = new ObservableList<ItemData>();

        public SubLevelData(string name)
        {
            Name = name;
        }
    }
}