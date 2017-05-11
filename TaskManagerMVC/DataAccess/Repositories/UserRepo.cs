namespace DataAccess.Repositories
{
    using System;
    using System.IO;
    using Entities;

    public class UserRepo : BaseRepo<User>
    {
        public UserRepo(string filePath)
            : base(filePath)
        {
        }

        protected override void ReadFromStream(StreamReader sr, User entity)
        {
            entity.Username = sr.ReadLine();
            entity.Password = sr.ReadLine();
            entity.Name = sr.ReadLine();
            entity.Email = sr.ReadLine();
            entity.IsAdmin = Convert.ToBoolean(sr.ReadLine());
        }

        protected override void WriteToStream(StreamWriter sw, User entity)
        {
            sw.WriteLine(entity.Username);
            sw.WriteLine(entity.Password);
            sw.WriteLine(entity.Name);
            sw.WriteLine(entity.Email);
            sw.WriteLine(entity.IsAdmin);
        }
    }
}