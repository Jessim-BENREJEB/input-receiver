using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace input_receiver
{
    internal class Client
    {
        int port;
        TcpClient client;
        public Client(int port)
        {
            this.port = port;
            this.client = new TcpClient("127.0.0.1", port);

        }

        public void StartCommunication()
        {
            NetworkStream stream = client.GetStream();
            while (true)
            {
                Console.Write("Message : ");
                string message = Console.ReadLine();

                byte[] data = Encoding.UTF8.GetBytes(message);
                stream.Write(data, 0, data.Length);

                byte[] buffer = new byte[1024];
                int bytesRead = stream.Read(buffer, 0, buffer.Length);

                string response = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                Console.WriteLine("Réponse serveur : " + response);
            }
        }


    }
}
