using System;
using System.Collections.Generic;
using LevelEditor.Item;

namespace LevelEditor.Controller
{
    /// <summary>
    ///     This class will control the behavior of all objects in the edited state.
    /// </summary>
    public class ItemController
    {
        private List<ItemBase> ItemList = new();

        public void AddItem(ItemBase item)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Removes the selected item and updates the status
        /// </summary>
        public static void RemoveItem(ItemBase targetItemBase)
        {
            throw new NotImplementedException();
        }
    }
}