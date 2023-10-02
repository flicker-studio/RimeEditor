using Frame.Tool;

public abstract class LevelEditorCommand : ICommand
{
    public abstract void Execute();

    public abstract void Undo();
}
