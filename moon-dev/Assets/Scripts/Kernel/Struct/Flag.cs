namespace Moon.Kernel.Struct
{
    /// <summary>
    ///    Custom value types to store special flag bits.
    /// </summary>
    public struct Flag
    {
        private bool m_flag;

        /// <summary>
        ///     Set the flag to <see langword="false" />.
        /// </summary>
        public bool SetFlag
        {
            set => m_flag = value;
        }

        /// <summary>
        ///     Gets the current flag bit and then sets it to <see langword="false" />.
        /// </summary>
        public bool GetFlag
        {
            get
            {
                var tempValue = m_flag;

                if (m_flag) m_flag = false;

                return tempValue;
            }
        }
    }
}