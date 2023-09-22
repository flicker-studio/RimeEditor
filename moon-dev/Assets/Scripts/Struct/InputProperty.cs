using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct InputProperty<T> where T : struct
{
    private T m_input;
    
    private bool m_inputDown;

    private bool m_inputUp;

    public T SetInput
    {
        set
        {
            m_input = value;
            if (m_input.Equals(default(T)))
            {
                m_inputDown = false;
                m_inputUp = true;
            }
            else
            {
                m_inputDown = true;
                m_inputUp = false;
            }
        }
    }
    
    public T GetInput
    {
        get
        {
            return m_input;
        }
    }
    
    public bool GetInputDown
    {
        get
        {
            bool temp = m_inputDown;
            m_inputDown = false;
            return temp;
        }
    }
    
    public bool GetInputUp
    {
        get
        {
            bool temp = m_inputUp;
            m_inputUp = false;
            return temp;
        }
    }
}
