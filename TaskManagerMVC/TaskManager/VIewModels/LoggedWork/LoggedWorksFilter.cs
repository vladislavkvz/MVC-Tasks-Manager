namespace TaskManager.VIewModels.TaskManager
{
    using DataAccess.Entities;
    using System;
    using Tools;

    public class LoggedWorksFilter : BaseFilterVM<LoggedWork>
    {
        [FilterProperty(DisplayName = "Time Spent")]
        public int TimeSpent { get; set; }
        public int TaskId { get; set; }

        public override Predicate<LoggedWork> BuildFilter()
        {
            if (TimeSpent == 0)
            {
                return (lw => lw.TaskId == TaskId);
            }
            return (lw => lw.TaskId == TaskId && (lw.TimeSpent == TimeSpent));
        }
    }
}