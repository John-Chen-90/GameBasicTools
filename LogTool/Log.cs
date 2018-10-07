/*
 * date:        2018-10-06
 * author:      John-chen
 * cn:          日志打印类
 * en:          Log print class
 */
namespace LogTool
{
    /// <summary>
    /// 日志打印
    /// </summary>
    public class Log
    {
        /// <summary>
        /// 是否保存到客户端
        /// </summary>
        public static bool IsSaveToClient;

        /// <summary>
        /// 是否上传到服务器
        /// </summary>
        public static bool IsSendToServer;
        

        /// <summary>
        /// 设置打印委托
        /// </summary>
        /// <param name="logDelegage"></param>
        public static void SetLogDelegate(LogDelegate logDelegage)
        {
            if(_ins == null) _ins = new Log();
            _ins._logDelegate = logDelegage ?? null;
        }

        /// <summary>
        /// info 打印
        /// </summary>
        /// <param name="logInfo"> 打印内容 </param>
        public static void Info(object logInfo)
        {
            var lv = LogLevel.Info;
            _ins?.LogPrint(logInfo, lv);
            _ins?.SaveLogInfo(logInfo, lv);
        }

        /// <summary>
        /// 警告信息 打印
        /// </summary>
        /// <param name="logInfo"></param>
        public static void Warning(object logInfo)
        {
            var lv = LogLevel.Warning;
            _ins?.LogPrint(logInfo, lv);
            _ins?.SaveLogInfo(logInfo, lv);
        }

        /// <summary>
        /// 错误信息 打印
        /// </summary>
        /// <param name="logInfo"></param>
        public static void Error(object logInfo)
        {
            var lv = LogLevel.Error;
            _ins?.LogPrint(logInfo, lv);
            _ins?.SaveLogInfo(logInfo, lv);
        }

        /// <summary>
        /// 日志打印
        /// </summary>
        /// <param name="logInfo"></param>
        /// <param name="lv"></param>
        private void LogPrint(object logInfo, LogLevel lv)
        {
            _ins?._logDelegate?.Invoke(logInfo, lv);
        }

        /// <summary>
        /// 保存log 信息
        /// </summary>
        /// <param name="logInfo"></param>
        /// <param name="lv"></param>
        private void SaveLogInfo(object logInfo, LogLevel lv)
        {
            SaveToClient(logInfo, lv);
            SendToServer(logInfo, lv);
        }

        /// <summary>
        /// 保存到客户端
        /// </summary>
        /// <param name="logInfo"></param>
        /// <param name="lv"></param>
        private void SaveToClient(object logInfo, LogLevel lv)
        {
            if(!IsSaveToClient) return;
            // 按 <[时间] 代码行数：打印内容> 存储
        }

        /// <summary>
        /// 发送到服务器
        /// </summary>
        /// <param name="logInfo"></param>
        /// <param name="lv"></param>
        private void SendToServer(object logInfo, LogLevel lv)
        {
            if(!IsSendToServer) return;
            // 按 <[时间] 代码行数：打印内容> 发送
        }

        /// <summary>
        /// 静态单例
        /// </summary>
        private static Log _ins;

        /// <summary>
        /// 打印委托
        /// </summary>
        private LogDelegate _logDelegate;

    }
}