using JetBrains.Annotations;
using LevelEditor.Data;

namespace LevelEditor
{
    public static class ItemFactory
    {
        /// <summary>
        ///     Create the corresponding Item
        /// </summary>
        /// <param name="type"></param>
        /// <returns>A new instance</returns>
        [UsedImplicitly]
        public static AbstractItem Create(ItemType type)
        {
            return type switch
                   {
                       ItemType.PLATFORM  => new Platform(),
                       ItemType.MECHANISM => new Platform(),
                       ItemType.ENTRANCE  => new Entrance(null),
                       ItemType.EXIT      => new Exit(null),
                       _                  => null
                   };
        }

        /// <summary>
        ///     Create the corresponding Item
        /// </summary>
        /// <param name="item"></param>
        /// <returns>A new instance</returns>
        [UsedImplicitly]
        public static AbstractItem Copy(AbstractItem item)
        {
            return item.Type switch
                   {
                       ItemType.PLATFORM  => new Platform(),
                       ItemType.MECHANISM => new Platform(),
                       ItemType.ENTRANCE  => new Entrance(null),
                       ItemType.EXIT      => new Exit(null),
                       _                  => null
                   };
        }
    }
}