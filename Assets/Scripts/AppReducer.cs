using System.Linq;

namespace HabitApp
{
    public class AppReducer
    {
        public static AppState Reduce(AppState previousState, object action)
        {
            switch (action)
            {
                case SelectHabitAction selectHabitAction:

                    previousState.SelectedHabitIndex = selectHabitAction.HabitIndex;

                    return previousState;
                case UpdateHabitAction updateHabitAction:
                    updateHabitAction.Habit.Title = updateHabitAction.NewTitle;
                    return previousState;
                case DeleteHabitAction deleteHabitAction:
                    previousState.Habits.Remove(deleteHabitAction.Habit);
                    return previousState;
                case AddHabitAction addHabitAction:
                    previousState.Habits.Add(addHabitAction.Habit);
                    return previousState;
                case ChangeTaskStatusAction changeTaskStatusAction:

                    var status = changeTaskStatusAction.TaskData.Status;
                    var task = changeTaskStatusAction.TaskData;

                    if (status == TaskStatus.Completed)
                    {
                        task.Status = TaskStatus.Failed;
                    }
                    else if (status == TaskStatus.Failed)
                    {
                        task.Status = TaskStatus.Skipped;
                    }
                    else if (status == TaskStatus.Skipped || status == null)
                    {
                        task.Status = TaskStatus.Completed;
                    }

                    return previousState;
            }

            return previousState;
        }
    }
}