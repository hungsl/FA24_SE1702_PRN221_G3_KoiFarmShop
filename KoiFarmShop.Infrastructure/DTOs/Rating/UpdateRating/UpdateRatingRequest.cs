using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KVSC.Infrastructure.DTOs.Rating.UpdateRating
{
    public class UpdateRatingRequest
    {
        public Guid Id { get; set; }
        public int Score { get; set; }
        public string Feedback { get; set; }
        public Guid CustomerId { get; set; }
    }
}
