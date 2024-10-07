using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiFarmShop.Domain.Entities
{
    public class Payment : BaseEntity
    {
        public Guid SystemTransactionId { get; set; } // Foreign key to SystemTransaction
        public decimal TotalAmount { get; set; }
        public decimal Tax { get; set; }
        public DateTime TransactionDate { get; set; }
    }
}
