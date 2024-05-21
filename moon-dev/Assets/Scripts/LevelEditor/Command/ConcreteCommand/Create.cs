using LevelEditor.Data;

namespace LevelEditor.Command
{
    /// <summary>
    ///     The command to create items
    /// </summary>
    public sealed class Create : ICommand
    {
        private readonly Item _item;

        /// <summary>
        ///     Default constructor
        /// </summary>
        public Create(ItemType type)
        {
            _item = ItemFactory.Create(type);
        }

        /// <inheritdoc />
        public void Execute()
        {
            _item.Active();
        }

        /// <inheritdoc />
        public void Undo()
        {
            _item.Inactive();
        }
    }
}