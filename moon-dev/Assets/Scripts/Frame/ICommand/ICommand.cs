namespace Frame.Tool
{
    public interface ICommand
    {
        public void Execute();

        public void Undo();
    }
}
