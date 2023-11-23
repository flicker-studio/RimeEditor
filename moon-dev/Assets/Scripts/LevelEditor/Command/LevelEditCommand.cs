using Frame.Tool;

namespace LevelEditor
{
    public abstract class LevelEditCommand : ICommand
    {
        public abstract void Execute();

        public abstract void Undo();
    }
}
