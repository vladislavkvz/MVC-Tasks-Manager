namespace TaskManager.VIewModels.TaskManager
{
    using DataAccess.Entities;
    using System;
    using Tools;

    public class CommentsFilter : BaseFilterVM<Comment>
    {
        [FilterProperty(DisplayName = "Comment")]
        public string Comment { get; set; }
        public int TaskId { get; set; }

        public override Predicate<Comment> BuildFilter()
        {
            return (c => (String.IsNullOrEmpty(Comment) || c.Comments.Contains(Comment)) && c.TaskId == TaskId);
        }
    }
}