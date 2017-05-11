namespace TaskManager.Controllers
{
    using DataAccess.Entities;
    using DataAccess.Repositories;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;
    using Filter;
    using VIewModels.TaskManager;
    using VIewModels;
    using System.Linq;

    public class TasksManagerController : BaseController<Task, ByIdTaskVM, AllTasksVM, TasksFilter>
    {
        protected override AllTasksVM CreateBaseAllVM()
        {
            return new AllTasksVM();
        }

        protected override ByIdTaskVM CreateBaseByIdVM()
        {
            return new ByIdTaskVM();
        }

        protected override BaseRepo<Task> CreateRepository()
        {
            return RepositoryFactory.GetTasksRepository();
        }

        protected override void PopulateViewModel(ByIdTaskVM model, Task entity)
        {
            model.Id = entity.Id;
            model.Title = entity.Title;
            model.Description = entity.Description;
            UserRepo uRepo = RepositoryFactory.GetUsersRepository();
            List<User> users = uRepo.GetAll();
            model.ResponsibleUser = entity.ResponsibleUser;
            model.Status = entity.Status;
            if (entity.Id <= 0)
            {
                model.UserId = AuthenticationManager.LoggedUser.Id;
                model.CreateTime = DateTime.Now;
                model.Creator = AuthenticationManager.LoggedUser.Name;
            }
            else
            {
                model.Creator = entity.Creator;
                model.UserId = entity.UserId;
                model.CreateTime = entity.CreateTime;
            }
            model.LastChange = DateTime.Now;
        }
        
        protected override void PopulateEntity(Task entity, ByIdTaskVM model)
        {
            entity.Id = model.Id;
            entity.UserId = model.UserId;
            entity.Title = model.Title;
            entity.Description = model.Description;
            entity.ResponsibleUser = model.ResponsibleUser;
            entity.Creator = model.Creator;
            entity.Status = model.Status;
            entity.LastChange = DateTime.Now;
            entity.CreateTime = model.CreateTime;
        }

        protected override void DeleteFilter(int id)
        {
            CommentRepo comRepository = RepositoryFactory.GetCommentsRepository();
            List<Comment> comments = comRepository.GetAll().FindAll(com => com.TaskId == id);
            foreach (var com in comments)
            {
                comRepository.Delete(com);
            }
            LoggedWorkRepo loggedWorkRepo = RepositoryFactory.GetLoggedWorksRepository();
            List<LoggedWork> loggedWorks = loggedWorkRepo.GetAll().FindAll(lw => lw.TaskId == id);
            foreach (var lw in loggedWorks)
            {
                loggedWorkRepo.Delete(lw);
            }
        }
        
        [UserAuthentication]
        public ActionResult Details(int? id)
        {
            TaskDetailsVm model = new TaskDetailsVm();
            model.PagerComment = model.PagerComment ?? new Pager();
            model.PagerLogWork = model.PagerLogWork ?? new Pager();
            model.FilterComment = model.FilterComment ?? new CommentsFilter();
            model.FilterLoggedWorks = model.FilterLoggedWorks ?? new LoggedWorksFilter();
            TryUpdateModel(model);

            TaskRepo repository = RepositoryFactory.GetTasksRepository();
            Task task = repository.GetById(Convert.ToInt32(id));
            model.Id = task.Id;
            model.UserId = task.UserId;
            model.Title = task.Title;
            model.Description = task.Description;
            model.ResponsibleUser = task.ResponsibleUser;
            model.Creator = task.Creator;
            model.Status = task.Status;
            model.LastChange = task.LastChange;
            model.CreateTime = task.CreateTime;

            CommentRepo comRepo = RepositoryFactory.GetCommentsRepository();
            LoggedWorkRepo logWorkRepo = RepositoryFactory.GetLoggedWorksRepository();

            string action = this.ControllerContext.RouteData.Values["action"].ToString();
            string controller = this.ControllerContext.RouteData.Values["controller"].ToString();

            //comments
            model.FilterComment.Prefix = "FilterComment.";
            string commentsPrefix = "PagerComment.";
            model.FilterComment.TaskId = task.Id;
            int commentsResultCount = comRepo.Count(model.FilterComment.BuildFilter());
            model.PagerComment = new Pager(commentsResultCount, model.PagerComment.CurrentPage, model.PagerComment.PageSize, commentsPrefix, action, controller);
            model.FilterComment.ParentPager = model.PagerComment;
            model.CommentsList = comRepo.GetAll(model.FilterComment.BuildFilter(), model.PagerComment.CurrentPage, model.PagerComment.PageSize.Value).ToList();

            //loggedwork
            model.FilterLoggedWorks.Prefix = "FilterLoggedWorks.";
            string loggedWorkPrefix = "PagerLogWork.";
            model.FilterLoggedWorks.TaskId = task.Id;
            int loggedWorkResultCount = logWorkRepo.Count(model.FilterLoggedWorks.BuildFilter());
            model.PagerLogWork = new Pager(loggedWorkResultCount, model.PagerLogWork.CurrentPage, model.PagerLogWork.PageSize, loggedWorkPrefix, action, controller);
            model.FilterLoggedWorks.ParentPager = model.PagerLogWork;
            model.LoggedWorkList = logWorkRepo.GetAll(model.FilterLoggedWorks.BuildFilter(), model.PagerLogWork.CurrentPage, model.PagerLogWork.PageSize.Value).ToList();

            return View(model);
        }
    }
}