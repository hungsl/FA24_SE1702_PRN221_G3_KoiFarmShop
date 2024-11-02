using KoiFarmShop.Application.Common.Result;
using KoiFarmShop.Infrastructure.DTOs.ComboService.AddComboService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiFarmShop.Application.Interface.IService
{
    public interface IComboServiceService
    {
        public Task<Result> CreateComboServiceAsync(AddComboServiceRequest addComboService);
        public Task<Result> GetComboServiceByIdAsync(Guid id);
        public Task<Result> GetAllComboServicesAsync();
        public Task<Result> UpdateComboServiceAsync(Guid id, AddComboServiceRequest addComboService);
        public Task<Result> DeleteComboServiceAsync(Guid id);
    }
}
