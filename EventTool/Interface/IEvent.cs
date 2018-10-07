/*
 *  date:       2018-10-06
 *  author:     John-chen
 *  cn:         事件接口,包含注册、通知、移除
 *  en:         event interface
 */
namespace EventTool.Interface
{
    /// <summary>
    /// 事件接口
    /// </summary>
    public interface IEvent
    {
        /// <summary>
        /// 注册事件
        /// </summary>
        /// <param name="name"> 事件名 </param>
        /// <param name="e"> 注册的事件 </param>
        void RegistEvent(string name, MessageHandler e);

        /// <summary>
        /// 通知事件
        /// </summary>
        /// <param name="name"> 事件名 </param>
        /// <param name="args"> 事件参数 </param>
        void NotifyEvent(string name, object[] args);

        /// <summary>
        /// 移除事件
        /// </summary>
        /// <param name="name"> 事件名 </param>
        /// <param name="eventAction"> 已经注册的事件 </param>
        void RemoveEvent(string name, MessageHandler eventAction);
    }
}