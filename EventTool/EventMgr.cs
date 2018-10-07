/*
 * date:        2018-10-06
 * author:      John-chen
 * cn:          事件管理器,管理所有的事件
 * en:          Event manager
 */

using UtilityTool;
using EventTool.Interface;
using System.Collections.Generic;

namespace EventTool
{
    /// <summary>
    /// 事件管理器
    /// </summary>
    public class EventMgr : Singleton<EventMgr>, IEvent
    {
        /// <summary>
        /// 注册事件
        /// </summary>
        /// <param name="name"></param>
        /// <param name="e"></param>
        public void RegistEvent(string name, MessageHandler e)
        {
            if (e == null) return;

            if (_events.ContainsKey(name))
            {
                _events[name].Add(e);
            }
            else
            {
                _events.Add(name, new List<MessageHandler>() { e });
            }
        }

        /// <summary>
        /// 通知事件
        /// </summary>
        /// <param name="name"></param>
        /// <param name="args"></param>
        public void NotifyEvent(string name, object[] args)
        {
            if(!_events.ContainsKey(name)) return;

            for (int i = 0; i < _events[name].Count; i++)
            {
                _events[name][i](args);
            }

        }

        /// <summary>
        /// 移除事件
        /// </summary>
        /// <param name="name"></param>
        /// <param name="e"></param>
        public void RemoveEvent(string name, MessageHandler e)
        {
            if(!_events.ContainsKey(name)) return;

            if (_events[name].Contains(e))
            {
                _events[name].Remove(e);
            }
        }

        /// <summary>
        /// 添加事件控制器
        /// </summary>
        /// <param name="eCtrl"> 事件控制器 </param>
        public void AddEventCtrl(EventController eCtrl)
        {
            if (_controllers.Contains(eCtrl) || eCtrl == null) return;
            
            _controllers.Add(eCtrl);
            eCtrl.OnInit();

            // todo: 会被通知两遍
            //eCtrl.OnNotifyEvent = NotifyEvent;
            eCtrl.OnRegistMsgEvent = RegistEvent;
            eCtrl.OnRemoveMsgEvent = RemoveEvent;
        }

        /// <summary>
        /// 移除事件控制器
        /// </summary>
        /// <param name="eCtrl"> 事件控制器 </param>
        public void RemoveEventCtrl(EventController eCtrl)
        {
            if (eCtrl == null) return;
            if (!_controllers.Contains(eCtrl))
            {
                eCtrl.OnRemove();
                return;
            }

            _controllers.Remove(eCtrl);
            eCtrl.OnRemove();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        protected override void Init()
        {
            _controllers = new List<EventController>();
            _events = new Dictionary<string, List<MessageHandler>>();
        }

        private List<EventController> _controllers; 
        private Dictionary<string, List<MessageHandler>> _events;
    }
}