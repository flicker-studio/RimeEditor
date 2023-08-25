using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//事件列表
public enum GameEvent
{
    ChangeMoveState
}

/// <summary>
/// 事件中心管理器
/// </summary>
public class EventCenterManager : Singleton<EventCenterManager>
{
    private readonly Dictionary<GameEvent, UnityEventBase> m_eventDict = new Dictionary<GameEvent, UnityEventBase>();

    #region 参数的

    /// <summary>
    /// 添加事件监听
    /// </summary>
    /// <param name="eventName">事件名</param>
    /// <param name="action">绑定action</param>
    public void AddEventListener<T>(GameEvent eventName, UnityAction<T> action)
    {
        UnityEventBase unityEvent = null;
        if (!m_eventDict.TryGetValue(eventName, out unityEvent))
        {
            unityEvent = new UnityEvent<T>();
            m_eventDict.Add(eventName, unityEvent);
        }
        (unityEvent as UnityEvent<T>)?.AddListener(action);
    }
    
    /// <summary>
    /// 添加事件监听
    /// </summary>
    /// <param name="eventName">事件名</param>
    /// <param name="action">绑定action</param>
    public void AddEventListener<T,K>(GameEvent eventName, UnityAction<T,K> action)
    {
        UnityEventBase unityEvent = null;
        if (!m_eventDict.TryGetValue(eventName, out unityEvent))
        {
            unityEvent = new UnityEvent<T,K>();
            m_eventDict.Add(eventName, unityEvent);
        }
        (unityEvent as UnityEvent<T,K>)?.AddListener(action);
    }

    /// <summary>
    /// 移除事件监听
    /// </summary>
    /// <param name="eventName">事件名</param>
    /// <param name="action">绑定action</param>
    public void RemoveEventListener<T>(GameEvent eventName, UnityAction<T> action)
    {
        UnityEventBase unityEvent = null;
        if (m_eventDict.TryGetValue(eventName, out unityEvent))
        {
            (unityEvent as UnityEvent<T>)?.RemoveListener(action);
        }
    }
    
    /// <summary>
    /// 移除事件监听
    /// </summary>
    /// <param name="eventName">事件名</param>
    /// <param name="action">绑定action</param>
    public void RemoveEventListener<T,K>(GameEvent eventName, UnityAction<T,K> action)
    {
        UnityEventBase unityEvent = null;
        if (m_eventDict.TryGetValue(eventName, out unityEvent))
        {
            (unityEvent as UnityEvent<T,K>)?.RemoveListener(action);
        }
    }

    /// <summary>
    /// 触发事件
    /// </summary>
    /// <param name="eventName">事件名</param>
    /// <param name="parameter">参数</param>
    public void EventTrigger<T>(GameEvent eventName, T parameter)
    {
        UnityEventBase unityEvent = null;
        if (m_eventDict.TryGetValue(eventName, out unityEvent))
        {
            (unityEvent as UnityEvent<T>)?.Invoke(parameter);
        }
        else 
        {
            Debug.LogWarning($"事件：{eventName} 不存在！");
        }
    }

    /// <summary>
    /// 触发事件
    /// </summary>
    /// <param name="eventName">事件名</param>
    /// <param name="parameter">参数</param>
    public void EventTrigger<T,K>(GameEvent eventName, T parameter,K parameterExtra)
    {
        UnityEventBase unityEvent = null;
        if (m_eventDict.TryGetValue(eventName, out unityEvent))
        {
            (unityEvent as UnityEvent<T,K>)?.Invoke(parameter,parameterExtra);
        }
        else 
        {
            Debug.LogWarning($"事件：{eventName} 不存在！");
        }
    }

    #endregion


    #region 无参数的

    /// <summary>
    /// 添加事件监听【无参数】
    /// </summary>
    /// <param name="eventName">事件名</param>
    /// <param name="action"></param>
    public void AddEventListener(GameEvent eventName, UnityAction action)
    {
        UnityEventBase unityEvent = null;
        if (!m_eventDict.TryGetValue(eventName, out unityEvent))
        {
            unityEvent = new UnityEvent();
            m_eventDict.Add(eventName, unityEvent);
        }
        (unityEvent as UnityEvent)?.AddListener(action);
    }

    /// <summary>
    /// 移除事件监听【无参数】
    /// </summary>
    /// <param name="eventName">事件名</param>
    /// <param name="action">绑定action</param>
    public void RemoveEventListener(GameEvent eventName, UnityAction action)
    {
        UnityEventBase unityEvent = null;
        if (m_eventDict.TryGetValue(eventName, out unityEvent))
        {
            (unityEvent as UnityEvent)?.RemoveListener(action);
        }
    }

    /// <summary>
    /// 触发事件【无参数】
    /// </summary>
    /// <param name="eventName">事件名</param>
    public void EventTrigger(GameEvent eventName)
    {
        UnityEventBase unityEvent = null;
        if (m_eventDict.TryGetValue(eventName, out unityEvent))
        {
            (unityEvent as UnityEvent)?.Invoke();
        }
        else 
        {
            Debug.LogWarning($"事件：{eventName} 不存在！");
        }
    }

    #endregion


    /// <summary>
    /// 清空（场景切换时）
    /// </summary>
    public void Clear()
    {
        m_eventDict.Clear();
    }
}