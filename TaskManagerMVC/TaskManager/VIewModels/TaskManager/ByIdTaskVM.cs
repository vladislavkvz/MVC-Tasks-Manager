namespace TaskManager.VIewModels.TaskManager
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class ByIdTaskVM:BaseByIdVM
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
    }
}