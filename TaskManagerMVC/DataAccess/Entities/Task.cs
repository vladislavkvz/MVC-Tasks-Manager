namespace DataAccess.Entities
{
    using System;

    public class Task : BaseEntity
    {
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int ResponsibleUser { get; set; }
        public string Creator { get; set; }
        public string Status { get; set; }
        public DateTime LastChange { get; set; }
        public DateTime CreateTime { get; set; }
    }
}