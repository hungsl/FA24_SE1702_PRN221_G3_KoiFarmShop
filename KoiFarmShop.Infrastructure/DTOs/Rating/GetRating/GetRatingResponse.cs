using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KVSC.Infrastructure.DTOs.Rating.GetRating
{
    public class GetRatingResponse
    {
        public Guid Id { get; set; }
        public Guid ServiceId { get; set; }
        public Guid CustomerId { get; set; }
        public int Score { get; set; }
        public string Feedback { get; set; }
        public string ServiceName { get; set; }
        public string CustomerName { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
