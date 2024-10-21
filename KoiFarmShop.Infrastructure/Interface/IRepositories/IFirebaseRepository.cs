using KoiFarmShop.Infrastructure.DTOs.Firebase.AddImage;
using KoiFarmShop.Infrastructure.DTOs.Firebase.GetImage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiFarmShop.Infrastructure.Interface.IRepositories
{
    public interface IFirebaseRepository
    {
        Task<AddImageResponse> UploadImageAsync(AddImageRequest request);
        Task<GetImageResponse> GetImageAsync(GetImageRequest request);
    }
}
