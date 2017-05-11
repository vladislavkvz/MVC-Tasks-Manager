namespace DataAccess.Repositories
{
    using Entities;
    using System;
    using System.IO;

    public class CommentRepo : BaseRepo<Comment>
    {
        public CommentRepo(string filePath) : base(filePath)
        {
        }

        protected override void ReadFromStream(StreamReader sr, Comment entity)
        {
            entity.TaskId = Convert.ToInt32(sr.ReadLine());
            entity.Comments = sr.ReadLine();
            entity.CreatedBy = Convert.ToInt32(sr.ReadLine());
        }

        protected override void WriteToStream(StreamWriter sw, Comment entity)
        {
            sw.WriteLine(entity.TaskId);
            sw.WriteLine(entity.Comments);
            sw.WriteLine(entity.CreatedBy);
        }
    }
}