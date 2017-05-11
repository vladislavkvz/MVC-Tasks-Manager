namespace TaskManager.VIewModels.TaskManager
{
    using System.ComponentModel.DataAnnotations;

    public class ByIdCommentVM:BaseByIdVM
    {
        public int TaskId { get; set; }
        [Required(ErrorMessage = "Please Enter A Comment")]
        public string Comments { get; set; }
        public int CreatedBy { get; set; }
    }
}