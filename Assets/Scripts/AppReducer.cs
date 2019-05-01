using System;
using System.Collections.Generic;
using System.Linq;
using QFramework;

namespace HabitApp
{
    public class AppReducer
    {

        public static AppState Reduce(AppState previousState, object action)
        {
            switch (action)
            {
                case AddHabitAction addHabitAction:

                    previousState.Habits.Add(addHabitAction.NewHabit);

                    if (previousState.Habits.Count == 1)
                    {
                        return Reduce(previousState, new SelectHabitAction(previousState.Habits.First()));
                    }

                    return previousState;

                case SelectHabitAction selectHabitAction:

                    if (previousState.SelectedHabit.IsNotNull())
                    {
                        previousState.SelectedHabit.Selected = false;
                    }

                    previousState.SelectedHabit = selectHabitAction.Habit;

                    previousState.SelectedHabit.Selected = true;

                    var tasks = selectHabitAction.Habit.Tasks;
                    var formatedTasks = FormatTasks(selectHabitAction.Habit, tasks);
                    previousState.Tasks.Clear();
                    previousState.Tasks.AddRange(formatedTasks);

                    return previousState;
                case SelectTaskAction selectTaskAction:

                    var selectTask = selectTaskAction.Task;

                    selectTask.IsSelected = true;

                    var status = DailyTaskStatus.Completed;
                    if (selectTask.Status == DailyTaskStatus.Completed)
                    {
                        status = DailyTaskStatus.Failed;
                    }
                    else if (selectTask.Status == DailyTaskStatus.Failed)
                    {
                        status = DailyTaskStatus.Skipped;
                    }

                    selectTask.Status = status;

                    return previousState;
                case UpdateHabitAction updateHabitAction:

                    return previousState;
                
                case DeleteHabitAction deleteHabitAction:
                    previousState.Habits.Remove(deleteHabitAction.Habit);
                    return previousState;
            }

            return previousState;
        }

        const int _monthDays  = 25;
        const int _quaterDays = 81;

        static List<DailyTask> FormatTasks(Habit habit, List<DailyTask> tasks)
        {
            var now = DateTime.Now;
            var currentDate = new DateTime(now.Year, now.Month, now.Day, 23, 59, 59);
            var passedDays = (currentDate - habit.CreateAt).Days;

            var seq = passedDays + 1;

            var presentedDays = _monthDays;
            if (passedDays >= _monthDays)
            {
                presentedDays = _quaterDays;
            }

            var _tasks = new List<DailyTask>();
            for (var i = 0; i < presentedDays; i++)
            {
                var task = tasks[i];
                if (task.Seq > seq)
                {
                    task.IsFuture = true;
                    task.IsToday = false;
                    task.IsYesterday = false;
                    task.IsSelected = false;
                }
                else if (task.Seq == seq)
                {
                    task.IsSelected = true;
                    task.IsFuture = false;
                    task.IsYesterday = false;
                    task.IsToday = true;
                }
                else if (task.Seq == seq - 1)
                {
                    task.IsYesterday = true;
                    task.IsToday = false;
                    task.IsFuture = false;
                    task.IsSelected = false;
                }
                else
                {
                    task.IsFuture = false;
                    task.IsYesterday = false;
                    task.IsSelected = false;
                    task.IsToday = false;
                }

                _tasks.Add(task);
            }

            return _tasks;
        }
    }
}