namespace TaskManager.Controllers
{
    using DataAccess.Repositories;
    using Models;
    using DataAccess.Entities;
    using VIewModels.UserManager;
    using System.Collections.Generic;
    using Filter;

    [AdminAuthentiation]
    public class UsersManagerController : BaseController<User, ByIdUserVM, AllUserVM, UsersFilter>
    { 
        protected override AllUserVM CreateBaseAllVM()
        {
            return new AllUserVM();
        }

        protected override ByIdUserVM CreateBaseByIdVM()
        {
            return new ByIdUserVM();
        }

        protected override BaseRepo<User> CreateRepository()
        {
            return RepositoryFactory.GetUsersRepository();
        }
        
        protected override void PopulateViewModel(ByIdUserVM model, User entity)
        {
            model.Id = entity.Id;
            model.Username = entity.Username;
            model.Password = entity.Password;
            model.Name = entity.Name;
            model.Email = entity.Email;
            model.IsAdmin = entity.IsAdmin;
        }

        protected override void PopulateEntity(User entity, ByIdUserVM model)
        {
            entity.Id = model.Id;
            entity.Username = model.Username;
            entity.Password = model.Password;
            entity.Name = model.Name;
            entity.Email = model.Email;
            entity.IsAdmin = model.IsAdmin;
        }

        protected override void DeleteFilter(int id)
        {
            TaskRepo taskRepository = RepositoryFactory.GetTasksRepository();
            List<Task> tasks = taskRepository.GetAll().FindAll(task => task.UserId == id && task.ResponsibleUser == id);
            foreach (var task in tasks)
            {
                taskRepository.Delete(task);
            }

            List<Task> tasksRespU = taskRepository.GetAll();
            foreach (var task in tasksRespU)
            {
                if (task.ResponsibleUser == AuthenticationManager.LoggedUser.Id)
                {
                    task.UserId = AuthenticationManager.LoggedUser.Id;

                    taskRepository.Save(task);
                }
            }

            CommentRepo comRepository = RepositoryFactory.GetCommentsRepository();
            List<Comment> comments = comRepository.GetAll().FindAll(com => com.CreatedBy == id);
            foreach (var com in comments)
            {
                comRepository.Delete(com);
            }

            LoggedWorkRepo loggedWorkRepo = RepositoryFactory.GetLoggedWorksRepository();
            List<LoggedWork> loggedWorks = loggedWorkRepo.GetAll().FindAll(lw => lw.UserId == id);
            foreach (var lw in loggedWorks)
            {
                loggedWorkRepo.Delete(lw);
            }
        }
    }
}