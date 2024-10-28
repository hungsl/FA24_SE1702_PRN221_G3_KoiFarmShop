using KoiFarmShop.Application.Common.Result;
using KoiFarmShop.Infrastructure.DTOs.Firebase.GetImage;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiFarmShop.Application.Interface.IService
{
    public interface IFirebaseService
    {
        Task<Result> UploadImageAsync(IFormFile file, string folder);
        Task<Result> GetImageAsync(GetImageRequest request);
    }
}
