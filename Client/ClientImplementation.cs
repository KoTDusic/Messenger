using System;
using System.IO;
using System.Net.Sockets;
using System.Windows;
using SharedInformation;
using SharedInformation.DataContracts;

namespace Client
{
    public class ClientImplementation : IDisposable
    {
        private const int PORT = 7777;
        private readonly TcpClient _client;
        private readonly string _hostname;
        private string _key;
        private StreamWriter _writer;
        private StreamReader _reader;

        public ClientImplementation(string hostname)
        {
            _client = new TcpClient();
            _hostname = hostname;
            var stream = _client.GetStream();
            _writer = new StreamWriter(stream) { AutoFlush = true };
            _reader = new StreamReader(stream);
        }

        public void Login(string username, string password)
        {
            var json = new LoginRequest
            {
                Login = username,
                Password = password
            };

            var request = RequestBuilder.BuildRequest(RequestHeaders.Login, json, _key);
            _writer.Write(request);

            var response = _reader.ReadLine();
            var data = RequestBuilder.ParseResponse(response);
            var result = Serializer.Deserialize<LoginResponse>(data.Response);
            if (string.IsNullOrEmpty(result.Error))
            {
                _key = result.Key;
            }
            else
            {
                MessageBox.Show(result.Error);
            }
        }

        public void SendMessage(string username, string message)
        {
            try
            {
                var safeMessage = message?.Replace(Constants.RequestDelimiter, '$');
                var packet = new MessageDataContract
                {
                    Message = safeMessage,
                    Username = username
                };

                var json = Serializer.Serialize(packet);
                var requestString = RequestBuilder.BuildRequest(RequestHeaders.SendMessage, json, _key);
                _writer.WriteLine(requestString);
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