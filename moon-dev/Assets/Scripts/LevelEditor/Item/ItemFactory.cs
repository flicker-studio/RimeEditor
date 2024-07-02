using JetBrains.Annotations;
using LevelEditor.Data;

namespace LevelEditor.Item
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
        public static ItemBase Create(ItemType type)
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
        /// <param name="itemBase">
        /// </param>
        /// <returns>
        ///     A new instance
        /// </returns>
        [UsedImplicitly]
        public static ItemBase Copy(ItemBase itemBase)
        {
            return itemBase.Type switch
                   {
                       ItemType.PLATFORM  => new Platform(),
                       ItemType.MECHANISM => new Platform(),
                       ItemType.ENTRANCE  => new Entrance(),
                       ItemType.EXIT      => new Exit(itemBase as Exit),
                       _                  => null
                   };
        }
    }
}