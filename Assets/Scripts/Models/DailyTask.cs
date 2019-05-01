namespace HabitApp
{
    public enum DailyTaskStatus
    {
        Completed,
        Skipped,
        Failed
    }
    
    public class DailyTask
    {   
        public DailyTaskStatus? Status;

        public int Seq;

        public bool? IsToday;

        public bool? IsYesterday;

        public bool? IsSelected;

        public bool? IsFuture;
    }
}