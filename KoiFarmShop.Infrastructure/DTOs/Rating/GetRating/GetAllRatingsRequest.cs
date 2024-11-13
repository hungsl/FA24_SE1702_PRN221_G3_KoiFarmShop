using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KVSC.Infrastructure.DTOs.Rating.GetRating
{
    public class GetAllRatingsRequest
    {
        public Guid? ServiceId { get; set; }
        public int? Score { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int PageNumber { get; set; } = 1; 
        public int PageSize { get; set; } = 10;
    }
}
