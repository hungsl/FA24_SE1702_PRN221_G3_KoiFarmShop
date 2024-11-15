using KoiFarmShop.Infrastructure.Interface.IRepositories;
<<<<<<< HEAD
using KVSC.Infrastructure.Interface.IRepositories;
=======
>>>>>>> Dev_Danh_skibidi
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
<<<<<<< HEAD
=======

>>>>>>> Dev_Danh_skibidi
        IPetServiceRepository PetServiceRepository { get; }
        IPetServiceCategoryRepository PetServiceCategoryRepository { get; }
        IComboServiceRepository ComboServiceRepository { get; }
        IAppointmentRepository AppointmentRepository { get; }
<<<<<<< HEAD
        IRatingRepository RatingRepository { get; }
=======
>>>>>>> Dev_Danh_skibidi

        int Complete();
    }
}
