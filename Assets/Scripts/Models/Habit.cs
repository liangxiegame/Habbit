using System;
using System.Collections.Generic;
using System.Linq;

namespace HabitApp
{
    public class Habit
    {
        public string Id { get; } = Guid.NewGuid().ToString();

        public string Title { get; set; }

        public DateTime CreateAt { get; set; } = DateTime.Now;

        public bool Selected { get; set; } = false;

        public List<DailyTask> Tasks { get; set; } = Enumerable.Range(1, 81)
            .Select(index => new DailyTask()
            {
                Seq = index
            })
            .ToList();

        public override string ToString()
        {
            return CreateAt.ToString("MM/dd/yyyy");
        }
    }
}