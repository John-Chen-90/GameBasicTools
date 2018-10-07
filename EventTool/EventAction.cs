/*
*  date:        2018-10-06
*  author:      John-chen
*  cn:          事件委托
*  en:          Event delegate
*/

namespace EventTool
{
    /// <summary>
    /// 事件委托
    /// </summary>
    /// <param name="args"></param>
    public delegate void MessageHandler(object[] args);
    
    /// <summary>
    /// 注册/移除事件委托
    /// </summary>
    /// <param name="name"></param>
    /// <param name="msg"></param>
    public delegate void HandlerMsg(string name, MessageHandler msg);
    
    /// <summary>
    /// 通知事件委托
    /// </summary>
    /// <param name="name"></param>
    /// <param name="args"></param>
    public delegate void MessageNotify(string name, object[] args);
}