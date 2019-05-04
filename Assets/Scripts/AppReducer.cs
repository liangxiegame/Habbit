using System.Linq;

namespace HabitApp
{
    public class AppReducer
    {
        public static AppState Reduce(AppState previousState, object action)
        {
            switch (action)
            {
                case UpdateHabitAction updateHabitAction:
                    updateHabitAction.Habit.Title = updateHabitAction.NewTitle;
                    return previousState;
                case DeleteHabitAction deleteHabitAction:
                    previousState.Habits.Remove(deleteHabitAction.Habit);
                    return previousState;
                case AddHabitAction addHabitAction:
                    previousState.Habits.Add(addHabitAction.Habit);
                    return previousState;  
            }
            
            return previousState;
        }
    }
}