using System;
using UnityEngine;

namespace LevelEditor
{
    public interface IAutoCreateSingleton
    {
    }

    public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
    {
        public static T SingletonNullable { get; private set; }

        public static T Singleton
        {
            get
            {
                if (Application.isPlaying == false) return null;

                if (SingletonNullable != null) return SingletonNullable; //第一次访问

                SingletonNullable = FindObjectOfType<T>(); // 从场景中查找

                if (SingletonNullable != null)
                {
                    SingletonNullable.OnSingletonInit(); //手动初始化
                    return SingletonNullable;
                }

                if (typeof(T).GetInterface(nameof(IAutoCreateSingleton)) != null)
                {
                    var go = new GameObject(typeof(T).Name);
                    SingletonNullable = go.AddComponent<T>();
                    SingletonNullable.OnSingletonInit();
                    return SingletonNullable;
                }

                throw new NullReferenceException(
                                                 $"Singleton<{typeof(T).Name}>.Singleton -> {typeof(T).Name} is null");
            }
        }

        protected virtual void Awake()
        {
            //Awake时如果还没有被访问 则将自己赋值给_singleton
            if (SingletonNullable == null)
            {
                SingletonNullable = this as T;

                // Debug.Log($"Singleton<{typeof(T).Name}>.Awake() -> {gameObject.name}");
                SingletonNullable.OnSingletonInit();
                return;
            }

            //如果已经被访问过 则销毁自己
            if (SingletonNullable != this) Destroy(gameObject);
        }

        private void OnDestroy()
        {
            if (SingletonNullable == this)
            {
                SingletonNullable.OnDispose();
                SingletonNullable = null;
            }
        }

        // Unity 2022 后 生命周期变更 OnApplicationQuit -> OnDisable -> OnDestroy
        private void OnApplicationQuit()
        {
            if (SingletonNullable == this)
            {
                SingletonNullable.OnDispose();
                SingletonNullable = null;
            }
        }

        protected virtual bool dontDestroyOnLoad()
        {
            return false;
        }

        private void OnSingletonInit()
        {
            // Debug.Log($"Singleton<{typeof(T).Name}>.OnInit() -> {gameObject.name}");
            transform.SetParent(null);

            if (dontDestroyOnLoad()) DontDestroyOnLoad(gameObject);

            OnInit();
        }

        protected virtual void OnInit()
        {
            gameObject.hideFlags = HideFlags.HideInHierarchy;
        }

        protected virtual void OnDispose()
        {
        }

        #if UNITY_EDITOR

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSplashScreen)]
        private static void ResetStatic()
        {
            SingletonNullable = null;
        }
        #endif
    }
}