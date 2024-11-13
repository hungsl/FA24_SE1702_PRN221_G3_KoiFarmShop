using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KVSC.Infrastructure.DTOs.Rating.AddRating
{
    public class AddRatingRequest
    {
        public Guid ServiceId { get; set; }
        public Guid CustomerId { get; set; }
        public int Score { get; set; }  // Giá trị từ 1 đến 5
        public string Feedback { get; set; }
    }
}
