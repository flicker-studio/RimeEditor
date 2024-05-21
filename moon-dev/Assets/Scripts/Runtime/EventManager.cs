using System.Collections.Generic;
using Moon.Runtime.DesignPattern;
using UnityEngine;
using UnityEngine.Events;

namespace Moon.Runtime
{
    /// <summary>
    ///     事件列表
    /// </summary>
    public enum GameEvent
    {
        /// <summary>
        /// </summary>
        UNDO_AND_REDO
    }
    
    /// <summary>
    ///     事件中心管理器
    /// </summary>
    public class EventManager : Singleton<EventManager>
    {
        private readonly Dictionary<GameEvent, UnityEventBase> _eventDict = new();
        
        #region 参数的
        
        /// <summary>
        ///     添加事件监听
        /// </summary>
        /// <param name="eventName">事件名</param>
        /// <param name="action">绑定action</param>
        public void AddEventListener<T>(GameEvent eventName, UnityAction<T> action)
        {
            if (!_eventDict.TryGetValue(eventName, out var unityEvent))
            {
                unityEvent = new UnityEvent<T>();
                _eventDict.Add(eventName, unityEvent);
            }
            
            (unityEvent as UnityEvent<T>)?.AddListener(action);
        }
        
        /// <summary>
        ///     添加事件监听
        /// </summary>
        /// <param name="eventName">事件名</param>
        /// <param name="action">绑定action</param>
        public void AddEventListener<T, TK>(GameEvent eventName, UnityAction<T, TK> action)
        {
            if (!_eventDict.TryGetValue(eventName, out var unityEvent))
            {
                unityEvent = new UnityEvent<T, TK>();
                _eventDict.Add(eventName, unityEvent);
            }
            
            (unityEvent as UnityEvent<T, TK>)?.AddListener(action);
        }
        
        /// <summary>
        ///     移除事件监听
        /// </summary>
        /// <param name="eventName">事件名</param>
        /// <param name="action">绑定action</param>
        public void RemoveEventListener<T>(GameEvent eventName, UnityAction<T> action)
        {
            if (_eventDict.TryGetValue(eventName, out var unityEvent)) (unityEvent as UnityEvent<T>)?.RemoveListener(action);
        }
        
        /// <summary>
        ///     移除事件监听
        /// </summary>
        /// <param name="eventName">事件名</param>
        /// <param name="action">绑定action</param>
        public void RemoveEventListener<T, TK>(GameEvent eventName, UnityAction<T, TK> action)
        {
            if (_eventDict.TryGetValue(eventName, out var unityEvent)) (unityEvent as UnityEvent<T, TK>)?.RemoveListener(action);
        }
        
        /// <summary>
        ///     触发事件
        /// </summary>
        /// <param name="eventName">事件名</param>
        /// <param name="parameter">参数</param>
        public void EventTrigger<T>(GameEvent eventName, T parameter)
        {
            if (_eventDict.TryGetValue(eventName, out var unityEvent))
                (unityEvent as UnityEvent<T>)?.Invoke(parameter);
            else
                Debug.LogWarning($"事件：{eventName} 不存在！");
        }
        
        /// <summary>
        ///     触发事件
        /// </summary>
        /// <param name="eventName">事件名</param>
        /// <param name="parameter">参数</param>
        /// <param name="parameterExtra"></param>
        public void EventTrigger<T, TK>(GameEvent eventName, T parameter, TK parameterExtra)
        {
            if (_eventDict.TryGetValue(eventName, out var unityEvent))
                (unityEvent as UnityEvent<T, TK>)?.Invoke(parameter, parameterExtra);
            else
                Debug.LogWarning($"事件：{eventName} 不存在！");
        }
        
        #endregion
        
        #region 无参数的
        
        /// <summary>
        ///     添加事件监听【无参数】
        /// </summary>
        /// <param name="eventName">事件名</param>
        /// <param name="action"></param>
        public void AddEventListener(GameEvent eventName, UnityAction action)
        {
            if (!_eventDict.TryGetValue(eventName, out var unityEvent))
            {
                unityEvent = new UnityEvent();
                _eventDict.Add(eventName, unityEvent);
            }
            
            (unityEvent as UnityEvent)?.AddListener(action);
        }
        
        /// <summary>
        ///     移除事件监听【无参数】
        /// </summary>
        /// <param name="eventName">事件名</param>
        /// <param name="action">绑定action</param>
        public void RemoveEventListener(GameEvent eventName, UnityAction action)
        {
            if (_eventDict.TryGetValue(eventName, out var unityEvent)) (unityEvent as UnityEvent)?.RemoveListener(action);
        }
        
        /// <summary>
        ///     触发事件【无参数】
        /// </summary>
        /// <param name="eventName">事件名</param>
        public void EventTrigger(GameEvent eventName)
        {
            if (_eventDict.TryGetValue(eventName, out var unityEvent))
                (unityEvent as UnityEvent)?.Invoke();
            else
                Debug.LogWarning($"事件：{eventName} 不存在！");
        }
        
        #endregion
        
        /// <summary>
        ///     清空（场景切换时）
        /// </summary>
        public void Clear()
        {
            _eventDict.Clear();
        }
    }
}