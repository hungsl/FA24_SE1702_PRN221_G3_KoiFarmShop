using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiFarmShop.WorkerService
{
    public class TaskEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid(); // ID duy nhất cho mỗi task
        public string Description { get; set; }        // Mô tả task
        public bool IsCompleted { get; set; }          // Trạng thái đã hoàn thành chưa
    }
}
