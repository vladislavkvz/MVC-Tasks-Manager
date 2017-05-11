namespace DataAccess.Repositories
{
    using System.Configuration;

    public class RepositoryFactory
    {
        public static UserRepo GetUsersRepository()
        {
            string path = ConfigurationManager.AppSettings["dataPath"];
            return new UserRepo(path + @"\users.txt");
        }

        public static TaskRepo GetTasksRepository()
        {
            string path = ConfigurationManager.AppSettings["dataPath"];
            return new TaskRepo(path + @"\tasks.txt");
        }

        public static CommentRepo GetCommentsRepository()
        {
            string path = ConfigurationManager.AppSettings["dataPath"];
            return new CommentRepo(path + @"\comments.txt");
        }

        public static LoggedWorkRepo GetLoggedWorksRepository()
        {
            string path = ConfigurationManager.AppSettings["dataPath"];
            return new LoggedWorkRepo(path + @"\loggedwork.txt");
        }
    }
}