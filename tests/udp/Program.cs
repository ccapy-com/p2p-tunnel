﻿using System.Net;
using System.Net.Sockets;
using System.Text;

namespace udp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"client or server、请输入:");
            string type = Console.ReadLine();

            switch (type)
            {
                case "client":
                    Client();
                    break;
                case "server":
                    Server();
                    break;
                default:
                    break;
            }

            Console.ReadLine();


        }

        static void Client()
        {
            Console.Write("绑定ip+端口:");
            IPEndPoint ep0 = IPEndPoint.Parse(Console.ReadLine());
            Console.Write("目标ip+端口:");
            IPEndPoint ep = IPEndPoint.Parse(Console.ReadLine());

            var udp = new UdpClient(ep0);
            udp.Client.EnableBroadcast = true;
            Console.WriteLine("====================================================\n");
            while (true)
            {
                Console.Write("输入消息:");
                string msg = Console.ReadLine();
                udp.Send(Encoding.UTF8.GetBytes(msg), ep);
            }
        }

        static void Server()
        {
            Console.Write("监听ip+端口:");
            IPEndPoint ep = IPEndPoint.Parse(Console.ReadLine());
            Console.Write("加入组播(空不加入):");
            string group = Console.ReadLine();

            var udp = new UdpClient(ep);
            if (string.IsNullOrWhiteSpace(group) == false)
            {
                udp.JoinMulticastGroup(IPAddress.Parse(group));
            }
            Console.WriteLine($"已监听:{ep}、开始接收消息:\n====================================================\n");

            IPEndPoint ep1 = new IPEndPoint(IPAddress.Any, 0);
            while (true)
            {
                var bytes = udp.Receive(ref ep1);
                Console.WriteLine($"收到消息:{Encoding.UTF8.GetString(bytes)}");
            }
        }
    }
}