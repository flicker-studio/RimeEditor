using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 单例工具类
/// </summary>
/// <typeparam name="T"></typeparam>
public class Singleton<T> where T : class, new()
{
    private static T m_instance;

    public static T Instance
    {
        get { return m_instance ??= new T(); }
    }
    
}