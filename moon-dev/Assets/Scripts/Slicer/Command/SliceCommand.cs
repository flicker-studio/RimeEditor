using Frame.Tool;

namespace Slicer
{
    public abstract class SliceCommand : ICommand
    {
        public abstract void Execute();

        void Undo()
        {
        }
    }
}