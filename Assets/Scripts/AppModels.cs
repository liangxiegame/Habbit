using System;

namespace HabitApp
{
    public class HabitData
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string Title { get; set; } = string.Empty;
    }
}