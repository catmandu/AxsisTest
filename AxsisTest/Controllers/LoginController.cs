using System.Web.Mvc;
using AxsisTest.Models;
using AxsisTest.DB;
using AxsisTest.Helpers.Authentication;

namespace AxsisTest.Controllers
{
    public class LoginController : Controller
    {
        private DBManager _dbManager;

        public LoginController()
        {
            _dbManager = new DBManager();
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(Login model)
        {
            if (ModelState.IsValid)
            {
                if(_dbManager.ValidateUsuario(model))
                {
                    AuthManager.SignInUser(model);
                    return RedirectToAction("Index", "Home");
                }
            }
            return View();
        }

        public ActionResult LogOut()
        {
            AuthManager.SignOutUser();
            return RedirectToAction("Index");
        }
    }
}