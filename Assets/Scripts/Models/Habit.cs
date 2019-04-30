using System;

namespace HabitApp
{
    public class Habit
    {
        public string Id { get; } = Guid.NewGuid().ToString();
        
        public string Title { get; set; }

        public DateTime CreateAt { get; } = DateTime.Now;

        public bool Selected { get; } = false;
        
//        BuiltList<DailyTask> get tasks;
    }
}