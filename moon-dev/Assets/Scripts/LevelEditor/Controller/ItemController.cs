using System;
using LevelEditor.Data;

namespace LevelEditor.Controller
{
    /// <summary>
    ///     This class will control the behavior of all objects in the edited state.
    /// </summary>
    public class ItemController
    {
        /// <summary>
        ///     Removes the selected item and updates the status
        /// </summary>
        public static void RemoveItem(Item targetItem)
        {
            throw new NotImplementedException();
        }
        
        /// <summary>
        ///     Removes the selected item and updates the status
        /// </summary>
        public static void RemoveItem(ItemView targetItemView)
        {
            RemoveItem(targetItemView.Item);
        }
    }
}