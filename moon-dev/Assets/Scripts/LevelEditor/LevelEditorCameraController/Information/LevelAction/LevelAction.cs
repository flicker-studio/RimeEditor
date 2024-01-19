using System;

namespace LevelEditor
{
    public class LevelAction
    {
        public event Action ExitEditorView;

        public void InvokeExitEditor()
        {
            ExitEditorView?.Invoke();
        }
    }

}
