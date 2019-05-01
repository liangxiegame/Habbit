using System.Collections.Generic;
using QFramework.UIWidgets.ReduxPersist;

namespace HabitApp
{
    public class AppState : AbstractPersistState<AppState>
    {
        public List<Habit>  Habits = new List<Habit>();

        public Habit SelectedHabit = null;

        public List<DailyTask> Tasks  = new List<DailyTask>();
    }
}