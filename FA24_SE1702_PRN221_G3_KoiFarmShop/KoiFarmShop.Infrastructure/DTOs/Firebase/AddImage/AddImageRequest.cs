using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiFarmShop.Infrastructure.DTOs.Firebase.AddImage
{
    public class AddImageRequest
    {
        public IFormFile ImageFile { get; set; }
        public string Folder { get; set; }

        public AddImageRequest(IFormFile imageFile, string folder)
        {
            ImageFile = imageFile;
            Folder = folder;
        }
    }
}
