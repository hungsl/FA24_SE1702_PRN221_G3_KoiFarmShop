using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Collections.Concurrent;

namespace KoiFarmShop.RazorWebApp.Pages.Client
{
    public class ServerModel : PageModel
    {
        private static TcpListener server;
        private static bool isRunning = false;
        public static ConcurrentQueue<string> ReceivedMessages = new ConcurrentQueue<string>(); // Lưu trữ các tin nhắn nhận được

        public void OnGet()
        {
            if (!isRunning)
            {
                // Chạy server trong một background task không chặn trang Razor
                Task.Run(() => StartServerAsync());
            }
        }

        private async Task StartServerAsync()
        {
            if (isRunning)
            {
                return; // Nếu server đang chạy, không làm gì cả
            }

            isRunning = true;
            server = new TcpListener(IPAddress.Any, 5000);
            server.Start();

            while (true)
            {
                var client = await server.AcceptTcpClientAsync();
                _ = HandleClientAsync(client);
            }
        }

        private async Task HandleClientAsync(TcpClient client)
        {
            using (var stream = client.GetStream())
            {
                byte[] buffer = new byte[1024];
                int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                // Thêm tin nhắn nhận được vào danh sách
                ReceivedMessages.Enqueue($"Server received: {message}");

                string response = $"Server received: {message}";
                byte[] responseBuffer = Encoding.UTF8.GetBytes(response);
                await stream.WriteAsync(responseBuffer, 0, responseBuffer.Length);
            }
        }
    }
}
