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
            UdpClient udpClient = new UdpClient();
            try
            {
                // Send a message to the server
                udpClient.Connect("127.0.0.1", 11000);
                string message = "Hello from UDP Client!";
                byte[] sendBytes = Encoding.UTF8.GetBytes(message);

                udpClient.Send(sendBytes, sendBytes.Length);

                Console.WriteLine("Message sent to the server.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                udpClient.Close();
            }
        }
    }
}
