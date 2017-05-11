namespace TaskManager.Controllers
{
    using DataAccess.Entities;
    using DataAccess.Repositories;
    using System.Web.Mvc;
    using Models;
    using VIewModels.TaskManager;
    using System;

    public class CommentsController : BaseController<Comment, ByIdCommentVM, AllCommentsVM, CommentsFilter>
    {
        protected override AllCommentsVM CreateBaseAllVM()
        {
            return new AllCommentsVM();
        }

        protected override ByIdCommentVM CreateBaseByIdVM()
        {
            return new ByIdCommentVM();
        }

        protected override BaseRepo<Comment> CreateRepository()
        {
            return RepositoryFactory.GetCommentsRepository();
        }

        protected override void PopulateViewModel(ByIdCommentVM model, Comment entity)
        {
            int taskId = Convert.ToInt32(this.Request["taskID"]);
            if (entity.Id <= 0)
            {
                model.Id = entity.Id;
                model.TaskId = taskId;
                model.CreatedBy = AuthenticationManager.LoggedUser.Id;
                model.Comments = entity.Comments;
            }
            else
            {
                model.Id = entity.Id;
                model.TaskId = entity.TaskId;
                model.CreatedBy = AuthenticationManager.LoggedUser.Id;
                model.Comments = entity.Comments;
            }
        }

        protected override void PopulateEntity(Comment entity, ByIdCommentVM model)
        {
            entity.Id = model.Id;
            entity.TaskId = model.TaskId;
            entity.CreatedBy = model.CreatedBy;
            entity.Comments = model.Comments;
        }

        protected override ActionResult Redirect(Comment entity)
        {
           return RedirectToAction("Details","TasksManager", new { id=entity.TaskId }); 
        }
    }
}