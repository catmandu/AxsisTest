using System.Web.Mvc;
using AxsisTest.Models;
using AxsisTest.DB;
using AxsisTest.Helpers;

namespace AxsisTest.Controllers
{
    public class LoginController : Controller
    {
        private DBManager _dbManager;
        private AuthManager _authManager;

        public LoginController()
        {
            _dbManager = new DBManager();
            _authManager = new AuthManager();
        }

        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(Login model)
        {
            if (ModelState.IsValid)
            {
                if (_dbManager.ValidateUsuario(model)
                    && _authManager.SignInUser(model))
                {
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("Password", "Usuario o contraseña incorrectos");
            }

            return View();
        }

        public ActionResult LogOut()
        {
            _authManager.SignOutUser();

            return RedirectToAction("Index");
        }
    }
}