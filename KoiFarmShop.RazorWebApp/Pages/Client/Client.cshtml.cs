using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Sockets;
using System.Text;

namespace KoiFarmShop.RazorWebApp.Pages.Client
{
    public class ClientModel : PageModel
    {
        public string Message { get; set; }

        public async Task OnPostAsync(string message)
        {
            Message = await SendMessageAsync(message);
        }

        private async Task<string> SendMessageAsync(string message)
        {
            using (TcpClient client = new TcpClient("localhost", 5000))
            {
                byte[] messageBuffer = Encoding.UTF8.GetBytes(message);
                await client.GetStream().WriteAsync(messageBuffer, 0, messageBuffer.Length);

                byte[] responseBuffer = new byte[1024];
                int bytesRead = await client.GetStream().ReadAsync(responseBuffer, 0, responseBuffer.Length);
                return Encoding.UTF8.GetString(responseBuffer, 0, bytesRead);
            }
        }
    }
}
