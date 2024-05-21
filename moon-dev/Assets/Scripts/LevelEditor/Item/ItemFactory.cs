using JetBrains.Annotations;
using LevelEditor.Data;

namespace LevelEditor
{
    /// <summary>
    ///     Item's factory class, which can only be used to create new Item subclasses.
    /// </summary>
    public static class ItemFactory
    {
        /// <summary>
        ///     Create the corresponding Item
        /// </summary>
        /// <param name="type"></param>
        /// <returns>A new instance</returns>
        [UsedImplicitly]
        public static Item Create(ItemType type)
        {
            return type switch
                   {
                       ItemType.PLATFORM  => new Platform(),
                       ItemType.MECHANISM => new Platform(),
                       ItemType.ENTRANCE  => new Entrance(),
                       ItemType.EXIT      => new Exit(),
                       _                  => null
                   };
        }
        
        /// <summary>
        ///     Create the corresponding Item
        /// </summary>
        /// <param name="item">
        /// </param>
        /// <returns>
        ///     A new instance
        /// </returns>
        [UsedImplicitly]
        public static Item Copy(Item item)
        {
            return item.Type switch
                   {
                       ItemType.PLATFORM  => new Platform(),
                       ItemType.MECHANISM => new Platform(),
                       ItemType.ENTRANCE  => new Entrance(),
                       ItemType.EXIT      => new Exit(),
                       _                  => null
                   };
        }
    }
}