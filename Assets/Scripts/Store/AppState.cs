using System;
using System.Collections.Generic;
using UnityEngine;

namespace HabitApp
{
    public class AppState
    {
        public List<HabitData> Habits { get; set; } =  new List<HabitData>()
        {
//             new HabitData() { Title = "创建习惯",CreateAt = DateTime.Now - TimeSpan.FromDays(1)},
//             new HabitData() { Title = "早起",CreateAt = DateTime.Now - TimeSpan.FromDays(7)},
//             new HabitData() { Title = "早睡",CreateAt = DateTime.Now - TimeSpan.FromDays(30)},
//             new HabitData() { Title = "运动",CreateAt = DateTime.Now}
        };

        public int SelectedHabitIndex = 0;

        public HabitData SelectedHabit
        {
            get
            {
                if (Habits.Count > 0 
                    && SelectedHabitIndex < Habits.Count)
                {
                    return Habits[SelectedHabitIndex];
                }

                SelectedHabitIndex = 0;

                return null;
            }
        }
    }
}