using System;

namespace Struct
{
    public struct InputProperty<T> where T : struct
    {
        private T m_input;

        private bool m_inputDown;

        private bool m_inputUp;

        public event Action DownAction;

        public event Action UpAction;

        public T ResetInput
        {
            set => m_input = value;
        }

        public T SetInput
        {
            set
            {
                m_input = value;

                if (m_input.Equals(default(T)))
                {
                    UpAction?.Invoke();
                    m_inputDown = false;
                    m_inputUp = true;
                }
                else
                {
                    DownAction?.Invoke();
                    m_inputDown = true;
                    m_inputUp = false;
                }
            }
        }

        public T GetInput => m_input;

        public bool GetInputDown
        {
            get
            {
                var temp = m_inputDown;
                m_inputDown = false;
                return temp;
            }
        }

        public bool GetInputUp
        {
            get
            {
                var temp = m_inputUp;
                m_inputUp = false;
                return temp;
            }
        }
    }
}