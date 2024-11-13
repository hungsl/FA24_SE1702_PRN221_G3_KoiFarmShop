using KoiFarmShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KVSC.Domain.Entities
{
    public class Rating : BaseEntity
    {
        public Guid ServiceId { get; set; }
        public Guid CustomerId { get; set; }
        public int Score { get; set; }
        public string Feedback { get; set; }
         
        public PetService Service { get; set; }
        public User Customer { get; set; }
    }
}
