using System;
using System.IO;
using System.Net.Sockets;
using Newtonsoft.Json;
using SharedInformation;

namespace Client
{
    public class ClientImplementation : IDisposable
    {
        private const int PORT = 7777;
        private readonly TcpClient _client;
        private readonly string _hostname;

        public ClientImplementation(string hostname)
        {
            _client = new TcpClient();
            _hostname = hostname;
        }

        public void SendMessage(string username, string message)
        {
            try
            {
                var packet = new MessageDataContract
                {
                    Message = message,
                    Username = username
                };
                {
                    var stream = _client.GetStream();
                    var writer = new StreamWriter(stream){AutoFlush = true};
                    writer.WriteLine(Serializer.Serialize(packet));
                }
            }
            catch (SocketException)
            {
                Dispose();
            }
        }

        public bool Connect()
        {
            try
            {
                _client.Connect(_hostname, PORT);
            }
            catch (SocketException)
            {
                return false;
            }
            
            return true;
        }

        public void Dispose()
        {
            _client?.Dispose();
        }
    }
}