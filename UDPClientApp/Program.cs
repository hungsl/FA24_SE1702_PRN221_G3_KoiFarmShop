using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace KoiFarmShop.UDPClientApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create a UDP client for listening on port 11000
            UdpClient udpServer = new UdpClient(11000);
            IPEndPoint remoteEP = new IPEndPoint(IPAddress.Any, 0);

            Console.WriteLine("Server is listening on port 11000...");

            try
            {
                while (true)
                {
                    // Receive the data sent by the client
                    byte[] data = udpServer.Receive(ref remoteEP);
                    string message = Encoding.ASCII.GetString(data);
                    Console.WriteLine($"Received from client: {message}");

                    // Echo the message back to the client
                    string response = $"Server received: {message}";
                    byte[] responseData = Encoding.ASCII.GetBytes(response);
                    udpServer.Send(responseData, responseData.Length, remoteEP);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                udpServer.Close();
            }
        }
    }
}
