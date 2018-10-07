/*
 * date:        2018-10-06
 * author:      John-chen
 * cn:          客户端socket (基类,分为： protobuf 二进制 json 等socket通信)
 * en:          Client Socket
 */

using System;
using System.Net;
using System.Net.Sockets;

namespace NetworkTool
{
    /// <summary>
    /// 客户端socket
    /// </summary>
    public class ClientSocket
    {
        /// <summary>
        /// 带参构造
        /// </summary>
        /// <param name="ip"> 服务器IP地址 </param>
        /// <param name="port"> 服务器端口号 </param>
        public ClientSocket(string ip, int port)
        {
            _ip = ip;
            _port = port;

            _msgBuffer = new byte[1024*1024];
            _clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        /// <summary>
        /// 连接服务器
        /// </summary>
        public void ConnectServer()
        {
            ConnetServer(_ip, _port);
        }

        /// <summary>
        /// 连接服务器
        /// </summary>
        /// <param name="ip"> 服务器Ip地址 </param>
        /// <param name="port"> 服务器端口号 </param>
        public void ConnetServer(string ip, int port)
        {
            IPAddress ipAds = IPAddress.Parse(ip);
            IPEndPoint ipEpt = new IPEndPoint(ipAds, port);
            _clientSocket.BeginConnect(ipEpt, OnBeginConnect, _clientSocket); 
        }

        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="datas"></param>
        public void Send(byte[] datas)
        {
            _clientSocket.BeginSend(datas, 0, datas.Length, 0, OnBeginSend, _clientSocket);
        }

        /// <summary>
        /// 连接后的回调
        /// </summary>
        /// <param name="ar"></param>
        private void OnBeginConnect(IAsyncResult ar)
        {
            try
            {
                Socket handler = (Socket)ar.AsyncState;
                handler.EndConnect(ar);
                _clientSocket.BeginReceive(_msgBuffer, 0, _msgBuffer.Length, 0, new AsyncCallback(OnRecive), null);
            }
            catch (SocketException e)
            {
                
            }
        }

        /// <summary>
        /// 发送后的回调
        /// </summary>
        /// <param name="ar"></param>
        private void OnBeginSend(IAsyncResult ar)
        {
            try
            {
                Socket handler = (Socket) ar.AsyncState;
                handler.EndSend(ar);
            }
            catch (SocketException e)
            {
                
            }
        }

        /// <summary>
        /// 接收数据
        /// </summary>
        /// <param name="ar"></param>
        private void OnRecive(IAsyncResult ar)
        {
            try
            {
                int rend = _clientSocket.EndReceive(ar);
                if (rend > 0)
                {
                    byte[] data = new byte[rend];
                    Array.Copy(_msgBuffer, 0, data, 0, rend);

                    // 这里对data进行处理

                    _clientSocket.BeginReceive(_msgBuffer, 0, _msgBuffer.Length, 0, new AsyncCallback(OnRecive), null);
                }
                else
                {
                    Dispose();
                }
            }
            catch (SocketException e)
            {
                
            }
        }

        /// <summary>
        /// 处理数据(解决粘包问题)
        /// </summary>
        /// <param name="data"></param>
        private void ReadData(byte[] data)
        {
            // 定义：包头由两部分组成
                // 包头：1.整体包的大小 2.协议号
                // 包体：整包大小 - 包头大小
            
        }

        /// <summary>
        /// 关闭连接
        /// </summary>
        private void Dispose()
        {
            try
            {
                _clientSocket.Shutdown(SocketShutdown.Both);
                _clientSocket.Close();
            }
            catch (SocketException e)
            {
                
            }
        }

        private int _port;
        private string _ip;
        private byte[] _msgBuffer;
        private Socket _clientSocket;
    }
}