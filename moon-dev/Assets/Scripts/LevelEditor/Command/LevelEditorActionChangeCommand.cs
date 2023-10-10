public class LevelEditorActionChangeCommand : LevelEditorCommand
{
    private LevelEditorAction m_levelEditorAction;
    
    private LEVELEDITORACTIONTYPE m_lastActionType;

    private LEVELEDITORACTIONTYPE m_nextActionType;

    public LevelEditorActionChangeCommand(LevelEditorAction levelEditorAction,LEVELEDITORACTIONTYPE nextActionType)
    {
        m_levelEditorAction = levelEditorAction;
        m_lastActionType = m_levelEditorAction.LevelEditorActionType;
        m_nextActionType = nextActionType;
    }
    public override void Execute()
    {
        m_levelEditorAction.LevelEditorActionType = m_nextActionType;
    }

    public override void Undo()
    {
        m_levelEditorAction.LevelEditorActionType = m_lastActionType;
    }
}
