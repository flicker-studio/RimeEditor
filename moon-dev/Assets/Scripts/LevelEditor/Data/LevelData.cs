namespace LevelEditor
{
    public class LevelData
    {
        public string Name;
        
        public ObservableList<ItemData> ItemAssets = new ObservableList<ItemData>();

        public LevelData(string name)
        {
            Name = name;
        }
    }
}