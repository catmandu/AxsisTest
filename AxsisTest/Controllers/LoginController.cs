using System;
using System.Web.Mvc;
using AxsisTest.Models;

namespace AxsisTest.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(Usuario model)
        {
            if (ModelState.IsValid)
            {
                if(model.NombreUsuario.Equals("usuario")
                    && model.Password.Equals("mipass"))
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            return View();
        }
    }
}