using System;
using System.Collections;
using System.Collections.Generic;
using Moon.Extension.Csharp.Type;
using UnityEngine;

/// <summary>
/// <see cref="FlagProperty"/> has been deprecated and <see cref="Flag"/> is being considered instead.
/// </summary>
[Obsolete("Struct.FlagProperty has been deprecated and Moon.Extension.Type.Flag is being considered instead.")]
public struct FlagProperty
{
    private bool m_flag;

    public bool SetFlag
    {
        set { m_flag = value; }
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