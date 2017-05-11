namespace TaskManager.Controllers
{
    using DataAccess.Entities;
    using DataAccess.Repositories;
    using System.Linq;
    using System.Web.Mvc;
    using Filter;
    using VIewModels;

    [UserAuthentication]
    public abstract class BaseController<T, IdVM, AllVM, FilterVM> : Controller
        where T : BaseEntity, new()
        where IdVM : BaseByIdVM
        where FilterVM : BaseFilterVM<T>, new()
        where AllVM : BaseAllVM<T, FilterVM>
    {
        public BaseController()
        {
            repo = CreateRepository();
        }

        private BaseRepo<T> repo;

        protected abstract BaseRepo<T> CreateRepository();
        protected abstract IdVM CreateBaseByIdVM();
        protected abstract AllVM CreateBaseAllVM();
        
        protected virtual void DeleteFilter(int id)
        {
        }

        protected virtual void PopulateViewModel(IdVM model, T entity)
        {
        }

        protected virtual void PopulateEntity(T entity, IdVM model)
        {
        }

        protected virtual ActionResult Redirect(T entity)
        {
            return RedirectToAction("Index");
        }
        
        public virtual ActionResult Index()
        {
            AllVM model = CreateBaseAllVM();
            model.Pager = new Pager();
            model.Filter = new FilterVM();
            TryUpdateModel(model);

            model.Filter.Prefix = "Filter.";
            int resultCount = repo.Count(model.Filter.BuildFilter());
            string prefix = "Pager.";
            string action = this.ControllerContext.RouteData.Values["action"].ToString();
            string controller = this.ControllerContext.RouteData.Values["controller"].ToString();
            model.Pager = new Pager(resultCount, model.Pager.CurrentPage, model.Pager.PageSize, prefix, action, controller);
            model.Filter.ParentPager = model.Pager;
            model.Items = repo.GetAll(model.Filter.BuildFilter(), model.Pager.CurrentPage, model.Pager.PageSize.Value).ToList();
            return View(model);
        }
        
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            T entity = (id == null || id <= 0) ? new T() : repo.GetById(id.Value);
            IdVM model = CreateBaseByIdVM();
            PopulateViewModel(model, entity);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(FormCollection collection)
        {
            IdVM model = CreateBaseByIdVM();
            TryUpdateModel(model);
            if (ModelState.IsValid)
            {
                T entity = new T();
                PopulateEntity(entity, model);
                repo.Save(entity);
                return Redirect(entity);
            }
            return View();
        }
        
        public ActionResult Delete(int id)
        {
            T entity = repo.GetById(id);
            repo.Delete(entity);
            DeleteFilter(id);
            return Redirect(entity);
        }
    }
}