namespace DataAccess.Repositories
{
    using Entities;
    using System;
    using System.Globalization;
    using System.IO;

    public class LoggedWorkRepo : BaseRepo<LoggedWork>
    {
        public LoggedWorkRepo(string filePath) : base(filePath)
        {
        }

        protected override void ReadFromStream(StreamReader sr, LoggedWork entity)
        {
            entity.UserId = int.Parse(sr.ReadLine());
            entity.TaskId = int.Parse(sr.ReadLine());
            entity.TimeSpent = int.Parse(sr.ReadLine());
            entity.LoggedOn = DateTime.ParseExact(sr.ReadLine(), "dd/MM/yyyy hh:mm tt", CultureInfo.InvariantCulture);
        }

        protected override void WriteToStream(StreamWriter sw, LoggedWork entity)
        {
            sw.WriteLine(entity.UserId);
            sw.WriteLine(entity.TaskId);
            sw.WriteLine(entity.TimeSpent);
            sw.WriteLine(entity.LoggedOn.ToString("dd/MM/yyyy hh:mm tt", CultureInfo.InvariantCulture));
        }
    }
}