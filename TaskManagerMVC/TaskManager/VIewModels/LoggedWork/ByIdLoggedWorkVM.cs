namespace TaskManager.VIewModels.TaskManager
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class ByIdLoggedWorkVM:BaseByIdVM
    {
        public int UserId { get; set; }
        public int TaskId { get; set; }
        [Required(ErrorMessage = "Please Enter A Time")]
        public int TimeSpent { get; set; }
        public DateTime LoggedOn { get; set; }
    }
}