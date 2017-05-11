namespace TaskManager.VIewModels.UserManager
{
    using System.ComponentModel.DataAnnotations;
    using ValidationAttributes;

    public class ByIdUserVM : BaseByIdVM
    {
        [Required(ErrorMessage = "Enter username")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Enter Password")]
        public string Password { get; set; }
        [IsEqual("Password")] 
        public string VerifyPass { get; set; }
        [Required(ErrorMessage = "Enter your Name")]
        public string Name { get; set; }
        [Unique("DataAccess.Entities.User")]
        [EmailAddress]
        public string Email { get; set; }
        public bool IsAdmin { get; set; }
    }
}