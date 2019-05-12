using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace HabitApp
{
    public class AppReducer
    {
        private const int MONTH_DAYS  = 25;
        private const int SEASON_DAYS = 81;

        public static List<TaskData> FormatTasks(HabitData selectedHabit)
        {
            var retTasks = new List<TaskData>();

            var now = DateTime.Now;
            var currentDate = new DateTime(now.Year, now.Month, now.Day, 23, 59, 59);
            var passedDays = (currentDate - selectedHabit.CreateAt).Days;

            var seq = passedDays + 1;

            var presentDays = MONTH_DAYS;

            if (passedDays > MONTH_DAYS)
            {
                presentDays = SEASON_DAYS;
            }

            for (var i = 0; i < presentDays; i++)
            {
                var selectedTask = selectedHabit.Tasks[i];

                if (selectedTask.Status == TaskStatus.Completed)
                {
                    Debug.LogError(selectedTask.Status);
                }
                else
                {
                    Debug.Log(selectedTask.Status);
                }

                if (selectedTask.Seq > seq)
                {
                    selectedTask.IsFuture = true;
                    selectedTask.IsToday = false;
                    selectedTask.IsYesterday = false;
                    selectedTask.Selected = false;
                }
                else if (selectedTask.Seq == seq)
                {
                    selectedTask.IsFuture = false;
                    selectedTask.IsToday = true;
                    selectedTask.IsYesterday = false;
                    selectedTask.Selected = true;
                }
                else if (selectedTask.Seq == seq - 1)
                {
                    selectedTask.IsFuture = false;
                    selectedTask.IsToday = false;
                    selectedTask.IsYesterday = true;
                    selectedTask.Selected = false;
                }
                else if (selectedTask.Seq == seq - 1)
                {
                    selectedTask.IsFuture = false;
                    selectedTask.IsToday = false;
                    selectedTask.IsYesterday = false;
                    selectedTask.Selected = false;
                }

                retTasks.Add(selectedTask);
            }

            return retTasks;
        }

        public static AppState Reduce(AppState previousState, object action)
        {
            switch (action)
            {
                case SelectHabitAction selectHabitAction:
                {
                    if (previousState.SelectedHabit != null)
                    {
                        var habitId = previousState.SelectedHabit.Id;

                        var previousSelectedHabit = previousState.Habits.Find(habit => habit.Id == habitId);

                        if (previousSelectedHabit != null)
                        {
                            previousSelectedHabit.Selected = false;
                        }
                    }

                    previousState.SelectedHabit = selectHabitAction.Habit;
                    previousState.SelectedHabit.Selected = true;

                    previousState.Tasks.Clear();

                    previousState.Tasks.AddRange(FormatTasks(previousState.SelectedHabit));


                    return previousState;
                }

                case UpdateHabitAction updateHabitAction:
                    updateHabitAction.Habit.Title = updateHabitAction.NewTitle;
                    return previousState;
                case DeleteHabitAction deleteHabitAction:
                    previousState.Habits.Remove(deleteHabitAction.Habit);
                    return previousState;
                case AddHabitAction addHabitAction:
                    previousState.Habits.Add(addHabitAction.Habit);

                    if (previousState.Habits.Count == 1)
                    {
                        Reduce(previousState, new SelectHabitAction(addHabitAction.Habit));
                    }

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
                    else if (status == TaskStatus.Skipped || status == TaskStatus.UnSetted)
                    {
                        task.Status = TaskStatus.Completed;
                    }

                    var selectedHabit = previousState.SelectedHabit;

                    var searchedTask = selectedHabit.Tasks.Find(searchTask => searchTask.Id == task.Id);

                    if (searchedTask != null)
                    {
                        searchedTask.Status = task.Status;
                    }
                    
                    searchedTask = previousState.Tasks.Find(searchTask => searchTask.Id == task.Id);
                    
                    if (searchedTask != null)
                    {
                        searchedTask.Status = task.Status;
                    }

                    var searchedHabit = previousState.Habits.Find(searchHabit => searchHabit.Id == selectedHabit.Id);

                    if (searchedHabit != null)
                    {
                        searchedTask = searchedHabit.Tasks.Find(searchTask => searchTask.Id == task.Id);
                    
                        if (searchedTask != null)
                        {
                            searchedTask.Status = task.Status;
                        }
                    }
                    
                    return previousState;
            }

            return previousState;
        }
    }
}