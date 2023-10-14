using System.Collections.Generic;

public delegate void LevelEditorCommandExcute(LevelEditorCommand command);
public class LevelEditorCommandManager
{
    private Stack<LevelEditorCommand> m_undoCommands = new Stack<LevelEditorCommand>();

    private Stack<LevelEditorCommand> m_redoCommands = new Stack<LevelEditorCommand>();

    public void Excute(LevelEditorCommand command)
    {
        command.Execute();
        m_undoCommands.Push(command);
        m_redoCommands.Clear();
    }

    public void Undo()
    {
        if (m_undoCommands.Count <= 0) return;
        LevelEditorCommand command = m_undoCommands.Pop();
        m_redoCommands.Push(command);
        command.Undo();
    }

    public void Redo()
    {
        if (m_redoCommands.Count <= 0) return;
        LevelEditorCommand command = m_redoCommands.Pop();
        m_undoCommands.Push(command);
        command.Execute();
    }
}
