namespace HabitApp
{
    public class AddHabitAction
    {
        public Habit NewHabit { get; }

        public AddHabitAction(Habit newHabit)
        {
            NewHabit = newHabit;
        }
    }

    public class SelectHabitAction
    {
        public SelectHabitAction(Habit habit)
        {
            Habit = habit;
        }

        public Habit Habit { get; }
    }

    public class SelectTaskAction
    {
        public DailyTask Task { get; }

        public SelectTaskAction(DailyTask task)
        {
            Task = task;
        }
    }

    public class UpdateHabitAction
    {
        public Habit UpdatedHabit { get; }

        public UpdateHabitAction(Habit updatedHabit)
        {
            UpdatedHabit = updatedHabit;
        }
    }

    public class DeleteHabitAction
    {
        public Habit Habit { get; }

        public DeleteHabitAction(Habit habit)
        {
            Habit = habit;
        }
    }
}