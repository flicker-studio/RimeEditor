using Frame.Tool;

public abstract class SliceCommand : ICommand
{
    public abstract void Execute();

    void ICommand.Undo(){}
}
