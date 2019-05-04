using System.Collections.Generic;

namespace HabitApp
{
    public class AppState
    {
        public List<HabitData> Habits = new List<HabitData>()
        {
            new HabitData() { Title = "创建习惯"},
             new HabitData() { Title = "创建习惯"},
             new HabitData() { Title = "创建习惯"},
             new HabitData() { Title = "创建习惯"},
             new HabitData() { Title = "创建习惯"},
             new HabitData() { Title = "创建习惯"},
             new HabitData() { Title = "早起"},
             new HabitData() { Title = "早睡"},
             new HabitData() { Title = "运动"}
        };
    }
}