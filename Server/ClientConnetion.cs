using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Threading;
using DatabaseController;
using SharedInformation;
using SharedInformation.DataContracts;

namespace Server
{
    public class ClientConnetion : IDisposable
    {
        private readonly TcpClient _client;
        private readonly string _key;
        private StreamWriter _streamWritter;
        public ClientConnetion(TcpClient client)
        {
            _client = client;
            _key = Randomizer.Next().ToString();
        }

        public void StartHandlind()
        {
            var th = new Thread(Processing);
            th.Start();
        }

        private void Processing()
        {
            try
            {
                using (var stream = _client.GetStream())
                {
                    using (var readStream = new StreamReader(stream))
                    {
                        _streamWritter = new StreamWriter(stream) {AutoFlush = true};
                        while (true)
                        {
                            var res = readStream.ReadLine();
                            var data = RequestBuilder.ParseResponse(res);
                            ProcessMessage(data.Header, data.Key, data.Response);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Dispose();
            }
        }

        private void ProcessLogin(string message)
        {
            var user = Serializer.Deserialize<LoginRequest>(message);
            var findUsers = AccountRepozitory.GetAll().
                Count(account => account.Username.Equals(user.Login) &&
                account.Password.Equals(user.Password));
            var json = new LoginResponse();
            if (findUsers != 0)
            {
                json.Key = _key;
                json.Error = string.Empty;
            }
            else
            {
                json.Key = string.Empty;
                json.Error = "Имя пользователя или пароль не найдены";
            }
            string request = RequestBuilder.BuildRequest(RequestHeaders.Login, json);
            _streamWritter.WriteLine(request);
            _streamWritter.Flush();
        }


        private void ProcessMessage(RequestHeaders header, string key, string message)
        {
            if (header == RequestHeaders.Login)
            {
                ProcessLogin(message);
            }
            switch (header)
            {
                case RequestHeaders.SendMessage:
                {
                    var response = Serializer.Deserialize<MessageDataContract>(message);
                    Console.WriteLine($"{response.Username}:{response.Message}");
                        break;
                }
                case RequestHeaders.GetMessages:
                {
                    break;
                }
            }
        }
        public void Dispose()
        {
            _client.Close();
            _client.Dispose();
        }
    }
}