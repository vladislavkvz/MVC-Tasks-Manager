namespace TaskManager.Controllers
{
    using DataAccess.Entities;
    using DataAccess.Repositories;
    using Models;
    using System.Web.Mvc;
    using Filter;
    using VIewModels.UserManager;

    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            AuthenticationManager.Authenticate(username, password);

            if (AuthenticationManager.LoggedUser == null)
            {
                ModelState.AddModelError("authenticationFailed", "Wrong username or password!");
                ViewData["username"] = username;

                return View();
            }
            return RedirectToAction("Index", "Home");
        }

        [UserAuthentication]
        public ActionResult Logout()
        {
            AuthenticationManager.Logout();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult Register()
        {
            if (AuthenticationManager.LoggedUser != null)
                return RedirectToAction("Index", "Home");

            User u = new User();
            ByIdUserVM model = new ByIdUserVM();
            model.Id = u.Id;
            model.Username = u.Username;
            model.Password = u.Password;
            model.Name = u.Name;
            model.IsAdmin = false;
            return View(model);
        }

        [HttpPost]
        public ActionResult Register(ByIdUserVM userVm)
        {
            if (AuthenticationManager.LoggedUser != null)
                return RedirectToAction("Index", "Home");

            if (ModelState.IsValid)
            {
                UserRepo usersRepository = RepositoryFactory.GetUsersRepository();
                User user = new User();
                user.Id = userVm.Id;
                user.Username = userVm.Username;
                user.Password = userVm.Password;
                user.Name = userVm.Name;
                user.IsAdmin = false;
                user.Email = userVm.Email;
                usersRepository.Save(user);

                return RedirectToAction("Login");
            }
            return View(userVm);
        }
    }
}