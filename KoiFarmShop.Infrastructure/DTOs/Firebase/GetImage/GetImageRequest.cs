using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiFarmShop.Infrastructure.DTOs.Firebase.GetImage
{
    public class GetImageRequest
    {
        public string FilePath { get; set; }

        public GetImageRequest(string filePath)
        {
            FilePath = filePath;
        }
    }
}
