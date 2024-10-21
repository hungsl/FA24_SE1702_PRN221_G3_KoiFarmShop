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
        IFirebaseRepository FirebaseRepository { get; }
        IUserRepository UserRepository { get; }
        IPetRepository PetRepository { get; }
        IProductCategoryRepository ProductCategoryRepository { get; }
        IProductRepository ProductRepository { get; }
        IPetServiceRepository PetServiceRepository { get; }
        IPetServiceCategoryRepository PetServiceCategoryRepository { get; }
        IComboServiceRepository ComboServiceRepository { get; }
        IAppointmentRepository AppointmentRepository { get; }

        int Complete();
    }
}
