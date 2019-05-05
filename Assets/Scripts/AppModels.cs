using System;
using System.Collections.Generic;

namespace HabitApp
{
    public class HabitData
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string Title { get; set; } = string.Empty;

        public List<TaskStatus> Tasks = new List<TaskStatus>();
    }

    /// <summary>
    /// 值类型  int float char struct
    /// 引用类型 class object 
    /// </summary>
    public enum TaskStatus
    {           
        /// <summary>
        /// 打卡成功
        /// </summary>
        Completed,
        /// <summary>
        /// 失败或者未打卡
        /// </summary>
        Failed,
        /// <summary>
        /// 忽略，有急事
        /// </summary>
        Skipped,
    }

    /// <summary>
    /// 打卡模型、任务模型
    /// </summary>
    public class TaskData
    {        
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public TaskStatus? Status { get; set; } = null;
    }
}