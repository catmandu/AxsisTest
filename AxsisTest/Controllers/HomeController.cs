using System.Web.Mvc;
using AxsisTest.Models;
using AxsisTest.DB;
using AxsisTest.Helpers;

namespace AxsisTest.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private DBManager _dbManager;
        private AuthManager _authManager;

        public HomeController()
        {
            _dbManager = new DBManager();
            _authManager = new AuthManager();
        }

        
        public ActionResult Index()
        {
            var usuarios = _dbManager.GetUsuarios();
      
            return View(usuarios);
        }
        
        [AllowAnonymous]
        public ActionResult Save()
        {
            return View(new Usuario());
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Usuario model)
        {
            if (ModelState.IsValid)
            {
                if (_authManager.CreateUser(model)
                    && _dbManager.SaveUsuario(model))
                {
                    return RedirectToAction("Index");
                }

                ModelState.AddModelError("NombreUsuario", "Ya existe un usuario con la información proporcionada");
            }
            return View(model);
        }

        public ActionResult Update(int id)
        {
            var usuario = _dbManager.GetUsuario(id);

            return View(usuario);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(Usuario model)
        {
            if (ModelState.IsValid)
            {
                if (_authManager.UpdateUser(model)
                    &&_dbManager.UpdateUsuario(model))
                {
                    return RedirectToAction("Index");
                }

                ModelState.AddModelError("NombreUsuario", "Ya existe un usuario con la información proporcionada");
            }
            return View(model);
        }

        public ActionResult Delete(int id = 0)
        {
            _dbManager.DeleteUsuario(id);

            return RedirectToAction("Index");
        }
    }
}