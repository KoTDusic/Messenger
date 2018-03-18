using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Server;

namespace Server
{
    class Program
    {
        private const int PORT = 7777;
        private static Thread _connector;
        private static TcpListener _server;

        static void Main(string[] args)
        {
            _server = new TcpListener(IPAddress.Any, PORT);
            _connector = new Thread(AcceptConnection);
            _connector.Start();
            while (true)
            {
                Console.ReadKey();
            }
        }

        private static void AcceptConnection()
        {
            _server.Start();
            while (true)
            {
                var newClient = _server.AcceptTcpClient();
                var connection = new ClientConnetion(newClient);
                connection.StartHandlind();
            }
        }
    }
}