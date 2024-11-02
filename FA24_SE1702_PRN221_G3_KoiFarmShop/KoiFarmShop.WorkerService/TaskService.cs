using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiFarmShop.WorkerService
{
    public class TaskService
    {
        private static ConcurrentQueue<TaskEntity> TaskQueue = new(); 

        public void AddTask(string description)
        {
            TaskQueue.Enqueue(new TaskEntity { Description = description });
        }

        public bool TryGetNextTask(out TaskEntity task) => TaskQueue.TryDequeue(out task);

        public List<TaskEntity> GetAllTasks()
        {
            return TaskQueue.ToList();
        }
    }
}
