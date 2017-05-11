namespace TaskManager.VIewModels.TaskManager
{
    using DataAccess.Entities;
    using Models;
    using System;
    using Tools;

    public class TasksFilter : BaseFilterVM<Task>
    {
        [FilterProperty(DisplayName = "Title")]
        public string Title { get; set; }
        [FilterProperty(DisplayName = "Description")]
        public string Description { get; set; }

        public override Predicate<Task> BuildFilter()
        {
            return (t => (String.IsNullOrEmpty(Title) || t.Title.Contains(Title)) &&
                         (String.IsNullOrEmpty(Description) || t.Description.Contains(Description)) &&
                         (t.UserId == AuthenticationManager.LoggedUser.Id || t.ResponsibleUser == AuthenticationManager.LoggedUser.Id));
        }
    }
}