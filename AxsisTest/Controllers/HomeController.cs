using System.Web.Mvc;
using AxsisTest.Models;
using AxsisTest.DB;
using AxsisTest.Helpers;

namespace AxsisTest.Controllers
{
    
    public class HomeController : Controller
    {
        private DBManager _dbManager;
        private AuthManager _authManager;

        public HomeController()
        {
            _dbManager = new DBManager();
            _authManager = new AuthManager();
        }

        [Authorize]
        public ActionResult Index()
        {
            var usuarios = _dbManager.GetUsuarios();
      
            return View(usuarios);
        }
        
        public ActionResult Save(int id = 0)
        {
            ViewBag.Title = id == 0 ? "Crear" : "Editar";

            var usuario = _dbManager.GetUsuario(id);

            return View(usuario);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Usuario model)
        {
            if (ModelState.IsValid)
            {
                if (_authManager.CreateUser(model)
                    && _dbManager.SaveOrUpdateUsuario(model))
                {
                    return RedirectToAction("Index");
                }
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