using KoiFarmShop.Infrastructure.Interface.IRepositories;
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
        IPetTypeRepository PetTypeRepository { get; }
        IPetHabitatRepository PetHabitatRepository { get; }

        IPetServiceRepository PetServiceRepository { get; }
        IPetServiceCategoryRepository PetServiceCategoryRepository { get; }
        IComboServiceRepository ComboServiceRepository { get; }
        IAppointmentRepository AppointmentRepository { get; }

        int Complete();
    }
}
