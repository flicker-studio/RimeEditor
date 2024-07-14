using System;
using JetBrains.Annotations;

namespace Moon.Kernel.Struct
{
    /// <summary>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public struct Input<T> where T : struct
    {
        private T m_input;

        private bool m_inputDown;

        private bool m_inputUp;

        /// <summary>
        /// </summary>
        public event Action DownAction;

        /// <summary>
        /// </summary>
        [UsedImplicitly]
        public event Action UpAction;

        /// <summary>
        /// </summary>
        public T ResetInput
        {
            set => m_input = value;
        }

        /// <summary>
        /// </summary>
        public T SetInput
        {
            set
            {
                m_input = value;

                if (m_input.Equals(default(T)))
                {
                    UpAction?.Invoke();
                    m_inputDown = false;
                    m_inputUp   = true;
                }
                else
                {
                    DownAction?.Invoke();
                    m_inputDown = true;
                    m_inputUp   = false;
                }
            }
        }

        /// <summary>
        /// </summary>
        public T GetInput => m_input;

        /// <summary>
        /// </summary>
        public bool GetInputDown
        {
            get
            {
                var temp = m_inputDown;
                m_inputDown = false;
                return temp;
            }
        }

        /// <summary>
        /// </summary>
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