/*
 * date:        2018-10-06
 * author:      John-chen
 * cn:          事件控制器,实现了事件接口
 * en:          Event controller
 */

using System.Collections.Generic;
using EventTool.Interface;

namespace EventTool
{
    /// <summary>
    /// 事件控制器
    /// </summary>
    public class EventController : IEvent
    {
        /// <summary>
        /// 带参构造
        /// </summary>
        /// <param name="name"></param>
        public EventController(string name)
        {
            Name = name;
            Ctor();
        }

        /// <summary>
        /// 控制器名
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// 注册回调
        /// </summary>
        public HandlerMsg OnRegistMsgEvent { get; set; }

        /// <summary>
        /// 移除回调
        /// </summary>
        public HandlerMsg OnRemoveMsgEvent { get; set; }

        /// <summary>
        /// 通知回调
        /// </summary>
        public MessageNotify OnNotifyEvent { get; set; }

        /// <summary>
        /// 注册事件
        /// </summary>
        /// <param name="name"> 事件名 </param>
        /// <param name="e"> 注册的事件 </param>
        public void RegistEvent(string name, MessageHandler e)
        {
            if (_events.ContainsKey(name))
            {
                _events[name].Add(e);
            }
            else
            {
                _events.Add(name, new List<MessageHandler>() {e});
            }
            OnRegistMsgEvent?.Invoke(name, e);
        }

        /// <summary>
        /// 通知事件
        /// </summary>
        /// <param name="name"> 事件名 </param>
        /// <param name="args"> 事件参数 </param>
        public void NotifyEvent(string name, object[] args)
        {
            if(!_events.ContainsKey(name)) return;

            var es = _events[name];
            for (int i = 0; i < es.Count; i++)
            {
                es[i](args);
            }

            // todo: 会被通知两遍
            // OnNotifyEvent?.Invoke(name, args);
        }

        /// <summary>
        /// 移除事件
        /// </summary>
        /// <param name="name"> 事件名 </param>
        /// <param name="eventAction"> 已经注册的事件 </param>
        public void RemoveEvent(string name, MessageHandler eventAction)
        {
            if (!_events.ContainsKey(name)) return;

            var es = _events[name];
            if (es.Contains(eventAction))
            {
                es.Remove(eventAction);
            }

            OnRemoveMsgEvent?.Invoke(name, eventAction);
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public void OnInit()
        {
            
        }

        /// <summary>
        /// 被移除
        /// </summary>
        public void OnRemove()
        {
            RemoveAllEvents();
        }

        /// <summary>
        /// 移除所有注册事件
        /// </summary>
        private void RemoveAllEvents()
        {
            _events.Clear();
        }

        /// <summary>
        /// 构造初始化
        /// </summary>
        private void Ctor()
        {
            _events = new Dictionary<string, List<MessageHandler>>();
        }

        private Dictionary<string, List<MessageHandler>> _events;
    }
}