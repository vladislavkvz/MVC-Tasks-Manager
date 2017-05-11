namespace TaskManager.VIewModels.UserManager
{
    using DataAccess.Entities;
    using System;
    using Tools;

    public class UsersFilter : BaseFilterVM<User>
    {
        [FilterProperty(DisplayName = "Username")]
        public string Username { get; set; }
        [FilterProperty(DisplayName = "Name")]
        public string Name { get; set; }
        [FilterProperty(DisplayName = "Email")]
        public string Email { get; set; }

        public override Predicate<User> BuildFilter()
        {
            return (u => (String.IsNullOrEmpty(Username) || u.Username.Contains(Username)) &&
                            (String.IsNullOrEmpty(Name) || u.Name.Contains(Name)) &&
                            (String.IsNullOrEmpty(Email) || u.Email.Contains(Email)));
        }
    }
}