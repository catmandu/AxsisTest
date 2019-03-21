using System;
using System.Linq;
using System.Web.Mvc;
using AxsisTest.Models;
using AxsisTest.DB;
using AxsisTest.Helpers.Encryption;

namespace AxsisTest.Controllers
{
    
    public class HomeController : Controller
    {
        private DBManager _dbManager;

        public HomeController()
        {
            _dbManager = new DBManager();
        }

        [Authorize]
        public ActionResult Index()
        {
            var usuarios = _dbManager.GetUsuarios();
      
            return View(usuarios);
        }
        
        public ActionResult Save(string id = null)
        {
            var usuario = _dbManager.GetUsuario(id);

            return View(usuario);
        }

        [HttpPost]
        public ActionResult Save(Usuario model)
        {
            _dbManager.SaveOrUpdateUsuario(model);

            return RedirectToAction("Index");
        }
    }
}