using Frame.Tool;

namespace LevelEditor
{
    public abstract class Command : ICommand
    {
        public abstract void Execute();

        public abstract void Undo();
    }
}
