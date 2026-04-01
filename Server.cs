using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace input_receiver
{
    internal class Server
    {
        private int port;
        private TcpListener server;

        public Server(int port)
        {
            this.port = port;
            this.server = new TcpListener(IPAddress.Loopback, port);
        }

        public async void StartServer()
        {
            
            this.server.Start();
            Console.WriteLine("Serveur prêt à écouter sur l'adresse " + IPAddress.Loopback + " sur le port " +  port);

            while (true)
            {
                TcpClient client = this.server.AcceptTcpClient();
                Thread clientThread = new Thread(() => HandleClient(client));
                clientThread.Start();
            }
        }

        private void HandleClient(TcpClient client)
        {
            NetworkStream stream = client.GetStream();
            byte[] buffer = new byte[1024];
            int bytesRead;

            try
            {
                while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) != 0)
                {
                    string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    Console.WriteLine("Reçu : " + message);

                    string response = "Message reçu !";
                    byte[] responseBytes = Encoding.UTF8.GetBytes(response);
                    stream.Write(responseBytes, 0, responseBytes.Length);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur : " + ex.Message);
            }

            client.Close();
            Console.WriteLine("Client déconnecté.");
        }
    }
}
