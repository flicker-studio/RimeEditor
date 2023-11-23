using System;

namespace Frame.Tool
{
    public interface ICommand
    {
        public void Execute();

        public void Undo();
    }

    public readonly struct BuildInCommand : ICommand
    {
        private readonly Action m_execute;
        private readonly Action m_undo;

        public BuildInCommand(Action execute = null, Action undo = null)
        {
            m_execute = execute;
            m_undo = undo;
        }

        public void Execute()
        {
            m_execute?.Invoke();
        }

        public void Undo()
        {
            m_undo?.Invoke();
        }
    }
}