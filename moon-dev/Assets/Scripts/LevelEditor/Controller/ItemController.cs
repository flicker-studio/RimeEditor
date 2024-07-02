using System;
using System.Collections.Generic;
using LevelEditor.Item;
using LevelEditor.View.Element;

namespace LevelEditor.Controller
{
    /// <summary>
    ///     This class will control the behavior of all objects in the edited state.
    /// </summary>
    public class ItemController
    {
        private List<ItemView> ItemList = new List<ItemView>();
        
        public void AddItem(ItemBase item)
        {
            throw new NotImplementedException();
        }
        
        public void AddItem(ItemView item)
        {
            AddItem(item.ItemBase);
        }
        
        /// <summary>
        ///     Removes the selected item and updates the status
        /// </summary>
        public static void RemoveItem(ItemBase targetItemBase)
        {
            throw new NotImplementedException();
        }
        
        /// <summary>
        ///     Removes the selected item and updates the status
        /// </summary>
        public static void RemoveItem(ItemView targetItemView)
        {
            RemoveItem(targetItemView.ItemBase);
        }
    }
}