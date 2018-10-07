/*
 * date:        2018-10-06
 * author:      John-chen
 * cn:          日志信息
 * en:          Log info
 */
namespace LogTool
{
    /// <summary>
    /// 日志等级
    /// </summary>
    public enum LogLevel
    {
        /// <summary>
        /// 普通信息
        /// </summary>
        Info,

        /// <summary>
        /// 错误信息
        /// </summary>
        Error,

        /// <summary>
        /// 警告信息
        /// </summary>
        Warning
    }

    /// <summary>
    /// 日志打印委托
    /// </summary>
    /// <param name="arg"> 内容 </param>
    /// <param name="lv"> 打印级别 </param>
    public delegate void LogDelegate(object arg, LogLevel lv);
}