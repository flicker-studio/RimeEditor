using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct FlagProperty
{
    private bool m_flag;

    public bool SetFlag
    {
        set
        {
            m_flag = value;
        }
    }

    public bool GetFlag
    {
        get
        {
            bool tempValue = m_flag;
            if (m_flag)
            {
                m_flag = false;
            }

            return tempValue;
        }
    }
}
