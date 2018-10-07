
using System;
using NetworkTool;

namespace TestClient
{
    class Program
    {
        static void Main(string[] args)
        {
            ClientSocket client = new ClientSocket("127.0.0.1", 8080);
            Console.WriteLine("客户端启动成功！");

            client.ConnectServer();
            Console.WriteLine("连接服务器！");

            client.Send(new byte[] {1, 2, 3});

            Console.ReadKey();
        }
    }
}
