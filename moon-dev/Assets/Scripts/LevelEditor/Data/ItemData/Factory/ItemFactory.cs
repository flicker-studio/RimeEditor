namespace LevelEditor
{
    public class ItemFactory
    {
        public ItemData CreateItem(ItemProduct itemProduct)
        {
            switch (itemProduct.ItemType)
            {
                case ITEMTYPEENUM.Platform:
                    return new PlatformData(itemProduct);
                case ITEMTYPEENUM.Mechanism:
                    return MechanismFactory(itemProduct);
                default:
                    return null;
            }
        }

        private ItemData MechanismFactory(ItemProduct itemProduct)
        {
            switch (itemProduct.Name)
            {
                case "Entrance":
                    return new EntranceData(itemProduct);
                case "Exit":
                    return new ExitData(itemProduct);
                default:
                    return null;
            }
        }
    }
}
