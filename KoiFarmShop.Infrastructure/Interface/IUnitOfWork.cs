using KoiFarmShop.Infrastructure.Interface.IRepositories;
using KVSC.Infrastructure.Interface.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiFarmShop.Infrastructure.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository UserRepository { get; }
        IPetRepository PetRepository { get; }
        IPetServiceRepository PetServiceRepository { get; }
        IPetServiceCategoryRepository PetServiceCategoryRepository { get; }
        IComboServiceRepository ComboServiceRepository { get; }
        IAppointmentRepository AppointmentRepository { get; }
        IRatingRepository RatingRepository { get; }
        IProductCategoryRepository ProductCategoryRepository { get; }
        IProductRepository ProductRepository { get; }
        int Complete();
    }
}
