/*
 * date:        2018-10-06
 * author:      John-chen
 * cn:          单例模版
 * en:          singleton template
 */

using System;

namespace UtilityTool
{
    /// <summary>
    /// 单件,只有通过有效的 CreateSingleton才能使用
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class Singleton<T> where T : new()
    {
        /// <summary>
        /// 单件实例
        /// </summary>
        private static T _singleton = default(T);

        /// <summary>
        /// 安全锁
        /// </summary>
        private static object _safelock = new object();

        /// <summary>
        /// 保护构造
        /// </summary>
        protected Singleton() { Init(); }

        /// <summary>
        /// 初始化
        /// </summary>
        protected virtual void Init() { }

        /// <summary>
        /// 移除
        /// </summary>
        public virtual void Remove() { }

        /// <summary>
        /// 创建单例
        /// </summary>
        /// <returns></returns>
        public static T CreateSingleton()
        {
            lock (_safelock)
            {
                if (_singleton == null)
                {
                    _singleton = new T();
                }
            }
            return _singleton;
        }

        /// <summary>
        /// 单件对象
        /// </summary>
        public static T Ins { get { return _singleton; } }
    }

    /// <summary>
    /// 单件,直接食用
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BaseSingleton<T> where T : new()
    {
        private static T _instance = default(T);
        private static object _lockHelper = new object();

        protected BaseSingleton()
        {
            Init();
        }

        public static T Ins
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lockHelper)
                    {
                        if (_instance == null) _instance = new T();
                    }
                }
                return _instance;
            }
        }

        protected virtual void Init()
        {

        }
    }

    /// <summary>
    /// 单件,需要创建才能食用
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class SafeSingleton<T> where T : new()
    {
        /// <summary>
        /// 单件实例
        /// </summary>
        private static T _singleton = default(T);

        /// <summary>
        /// 安全锁
        /// </summary>
        private static object _safelock = new object();

        /// <summary>
        /// 保护构造
        /// </summary>
        protected SafeSingleton() { }

        /// <summary>
        /// 创建单例
        /// </summary>
        /// <returns></returns>
        public static T CreateSingleton()
        {
            lock (_safelock)
            {
                if (_singleton == null)
                {
                    _singleton = new T();
                }
            }
            return _singleton;
        }

        /// <summary>
        /// 单件对象
        /// </summary>
        public static T Ins { get { return _singleton != null ? _singleton : CreateSingleton(); } }
    }

    /// <summary>
    /// 简单单件，通过单次new T(...)来实现，不需要额外调用
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class SampleSingleton<T> where T : SampleSingleton<T>
    {
        /// <summary>
        /// 单件实例
        /// </summary>
        private static T _singleton = default(T);

        /// <summary>
        /// 安全锁
        /// </summary>
        private static object _safelock = new object();

        /// <summary>
        /// 保护构造，多次构造抛错
        /// </summary>
        protected SampleSingleton()
        {
            lock (_safelock)
            {
                if (_singleton != null)
                {
                    throw new Exception("Singleton repeated, " + typeof(T).ToString());
                }
                else
                {
                    _singleton = this as T;
                }
            }
        }

        /// <summary>
        /// 单件对象
        /// </summary>
        public static T Ins { get { return _singleton; } }
    }

    #region 暂时不用
    /*
    /// <summary>
    /// 管理器单件,只有通过有效的 CreateSingleton才能使用
    /// module 中 Manager 常用单例模式
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class SingletonMgr<T> : IEvent where T : new()
    {
        /// <summary>
        /// 管理器名字
        /// </summary>
        public string Name { get { return _name; } }

        /// <summary>
        /// 移除
        /// </summary>
        public virtual void Remove()
        {
            RemoveEventCtrl();
        }

        /// <summary>
        /// 移除事件控制器
        /// </summary>
        public virtual void RemoveEventCtrl()
        {
            MessageCtrl.Ins.RemoveCtrl(_eventCtrl);
        }

        /// <summary>
        /// 保护构造
        /// </summary>
        protected SingletonMgr()
        {
            _name = this.GetType().ToString();
            AddEventCtrl();
            Init();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        protected virtual void Init()
        {

        }

        /// <summary>
        /// 添加事件控制器
        /// </summary>
        protected virtual void AddEventCtrl()
        {
            _eventCtrl = new EventController(_name);
            MessageCtrl.Ins.AddEventCtrl(_eventCtrl);
        }

        /// <summary>
        /// 注册事件
        /// </summary>
        /// <param name="name"></param>
        /// <param name="eHandler"></param>
        public void RegistEvent(string name, MessageHandler eHandler)
        {
            _eventCtrl.RegistEvent(name, eHandler);
        }

        /// <summary>
        /// 移除事件
        /// </summary>
        /// <param name="name"></param>
        /// <param name="eHandler"></param>
        public void RemoveEvent(string name, MessageHandler eHandler)
        {
            _eventCtrl.RemoveEvent(name, eHandler);
        }

        /// <summary>
        /// 通知事件
        /// </summary>
        /// <param name="name"></param>
        /// <param name="args"></param>
        public void NotifyEvent(string name, params object[] args)
        {
            _eventCtrl.NotifyEvent(name, args);
        }

        #region singleton
        /// <summary>
        /// 单件实例
        /// </summary>
        private static T _singleton = default(T);

        /// <summary>
        /// 安全锁
        /// </summary>
        private static object _safelock = new object();

        /// <summary>
        /// 创建单例
        /// </summary>
        /// <returns></returns>
        public static T CreateSingleton()
        {
            lock (_safelock)
            {
                if (_singleton == null)
                {
                    _singleton = new T();
                }
            }
            return _singleton;
        }

        /// <summary>
        /// 单件对象
        /// </summary>
        public static T Ins { get { return _singleton; } }
        #endregion

        protected EventController _eventCtrl;
        protected string _name;
    }
    */
    #endregion
}