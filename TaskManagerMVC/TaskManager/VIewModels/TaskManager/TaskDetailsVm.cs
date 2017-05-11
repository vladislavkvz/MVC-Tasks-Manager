namespace TaskManager.VIewModels.TaskManager
{
    using DataAccess.Entities;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class TaskDetailsVm:BaseByIdVM
    {
        public int UserId { get; set; }
        [Required(ErrorMessage = "Please add title")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Please add description")]
        public string Description { get; set; }
        public int ResponsibleUser { get; set; }
        public string Creator { get; set; }
        [Required(ErrorMessage = "Please choose status")]
        public string Status { get; set; }
        public DateTime LastChange { get; set; }
        public DateTime CreateTime { get; set; }
        public Pager PagerComment { get; set; }
        public Pager PagerLogWork { get; set; }
        public CommentsFilter FilterComment { get; set; }
        public LoggedWorksFilter FilterLoggedWorks { get; set; }
        public List<Comment> CommentsList { get; set; }
        public List<LoggedWork> LoggedWorkList { get; set; }
    }
}