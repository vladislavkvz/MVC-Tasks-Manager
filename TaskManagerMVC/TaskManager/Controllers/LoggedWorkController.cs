namespace TaskManager.Controllers
{
    using DataAccess.Entities;
    using DataAccess.Repositories;
    using System;
    using System.Web.Mvc;
    using Models;
    using VIewModels.TaskManager;

    public class LoggedWorkController : BaseController<LoggedWork, ByIdLoggedWorkVM, AllLoggedWorkVM, LoggedWorksFilter>
    {
        protected override AllLoggedWorkVM CreateBaseAllVM()
        {
            return new AllLoggedWorkVM();
        }

        protected override ByIdLoggedWorkVM CreateBaseByIdVM()
        {
            return new ByIdLoggedWorkVM();
        }

        protected override BaseRepo<LoggedWork> CreateRepository()
        {
            return RepositoryFactory.GetLoggedWorksRepository();
        }

        protected override void PopulateViewModel(ByIdLoggedWorkVM model, LoggedWork entity)
        {
            int taskId = Convert.ToInt32(this.Request["taskID"]);

            if (entity.Id <= 0)
            {
                model.Id = entity.Id;
                model.TaskId = taskId;
                model.UserId = AuthenticationManager.LoggedUser.Id;
                model.TimeSpent = entity.TimeSpent;
                model.LoggedOn = DateTime.Now;
            }
            else
            {
                model.Id = entity.Id;
                model.TaskId = entity.TaskId;
                model.UserId = AuthenticationManager.LoggedUser.Id;
                model.TimeSpent = entity.TimeSpent;
                model.LoggedOn = DateTime.Now;
            }
        }

        protected override void PopulateEntity(LoggedWork entity, ByIdLoggedWorkVM model)
        {
            entity.Id = model.Id;
            entity.TaskId = model.TaskId;
            entity.UserId = model.UserId;
            entity.TimeSpent = model.TimeSpent;
            entity.LoggedOn = DateTime.Now;
        }

        protected override ActionResult Redirect(LoggedWork entity)
        {
            return RedirectToAction("Details", "TasksManager", new { id = entity.TaskId });
        }
    }
}