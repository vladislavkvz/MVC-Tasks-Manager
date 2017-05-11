namespace DataAccess.Entities
{
    using System;

    public class LoggedWork : BaseEntity
    {
        public int UserId { get; set; }
        public int TaskId { get; set; }
        public int TimeSpent { get; set; }
        public DateTime LoggedOn { get; set; }
    }
}