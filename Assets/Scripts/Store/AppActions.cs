namespace HabitApp
{
    public class SelectHabitAction
    {
        public int HabitIndex;

        public SelectHabitAction(int habitIndex)
        {
            HabitIndex = habitIndex;
        }
    }


    public class ChangeTaskStatusAction
    {
        public TaskData TaskData;

        public ChangeTaskStatusAction(TaskData taskData)
        {
            TaskData = taskData;
        }
    }
    
    
    public class DeleteHabitAction
    {
        public HabitData Habit;

        public DeleteHabitAction(HabitData habit)
        {
            Habit = habit;
        }
    }

    public class UpdateHabitAction
    {
        public HabitData Habit;

        public string NewTitle;

        public UpdateHabitAction(HabitData habit, string newTitle)
        {
            Habit = habit;
            NewTitle = newTitle;
        }
    }

    public class AddHabitAction
    {
        public HabitData Habit;

        public AddHabitAction(HabitData habit)
        {
            Habit = habit;
        }
    }
}