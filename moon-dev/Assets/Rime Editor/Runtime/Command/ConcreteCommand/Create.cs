using LevelEditor.Data;
using LevelEditor.Item;

namespace LevelEditor.Command
{
    /// <summary>
    ///     The command to create items
    /// </summary>
    public sealed class Create : ICommand
    {
        private readonly ItemBase _itemBase;

        /// <summary>
        ///     Default constructor
        /// </summary>
        public Create(ItemType type)
        {
            _itemBase = ItemFactory.Create(type);
        }

        /// <inheritdoc />
        public void Execute()
        {
            _itemBase.Active();
        }

        /// <inheritdoc />
        public void Undo()
        {
            _itemBase.Inactive();
        }
    }
}