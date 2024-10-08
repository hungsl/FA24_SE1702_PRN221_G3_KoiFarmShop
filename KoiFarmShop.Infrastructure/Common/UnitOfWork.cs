using KoiFarmShop.Infrastructure.DB;
using KoiFarmShop.Infrastructure.Implement.Repositories;
using KoiFarmShop.Infrastructure.Interface.IRepositories;
using KoiFarmShop.Infrastructure.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Cloud.Storage.V1;

namespace KoiFarmShop.Infrastructure.Common
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly KVSCContext _context;

        public IUserRepository UserRepository { get; private set; }
        public IPetRepository PetRepository { get; private set; }
        public IPetTypeRepository PetTypeRepository { get; private set; }
        public IPetHabitatRepository PetHabitatRepository { get; private set; }
        public IPetServiceRepository PetServiceRepository { get; private set; }
        public IPetServiceCategoryRepository PetServiceCategoryRepository { get; private set; }
        public IComboServiceRepository ComboServiceRepository { get; private set; }
        public IAppointmentRepository AppointmentRepository { get; private set; }
        public UnitOfWork(KVSCContext context)
        {
            _context = context;
            UserRepository = new UserRepository(_context);
            PetRepository = new PetRepository(_context);
            PetServiceRepository = new PetServiceRepository(_context);
            PetServiceCategoryRepository = new PetServiceCategoryRepository(_context);
            ComboServiceRepository = new ComboServiceRepository(_context);
            AppointmentRepository = new AppointmentRepository(_context);
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
