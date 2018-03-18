using System;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using SharedInformation;

namespace Server
{
    public class ClientConnetion : IDisposable
    {
        private readonly TcpClient _client;

        public ClientConnetion(TcpClient client)
        {
            _client = client;
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
                        while (true)
                        {
                            var res = readStream.ReadLine();
                            var response = Serializer.Deserialize<MessageDataContract>(res);
                            Console.WriteLine($"{response.Username}: {response.Message}");
                        }
                    }  
                }
            }
            catch (Exception e)
            {
                Dispose();
            }
        }

        public void Dispose()
        {
            _client.Close();
            _client.Dispose();
        }
    }
}