using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Server
{
    class Program
    {
        const int port = 11000;
        static TcpListener listener;

        static void Main(string[] args)
        {
            try
            {
                int numC = 0;
                Server server = new Server(1);

                listener = new TcpListener(IPAddress.Any, port);//IPAddress.Parse("127.0.0.1")
                listener.Start();
                Console.WriteLine("Ожидание подключений...");

                while (true)
                {
                    TcpClient client = listener.AcceptTcpClient();
                    Client clientC = new Client(client, ++numC, server);
                    server.AddClient(clientC);
                    // создаем поток для обслуживания нового клиента
                    Thread clientThread = new Thread(new ThreadStart(clientC.Process));
                    clientThread.Start();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (listener != null)
                    listener.Stop();
            }
        }
    }
}
