using Moon.Runtime.DesignPattern;

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