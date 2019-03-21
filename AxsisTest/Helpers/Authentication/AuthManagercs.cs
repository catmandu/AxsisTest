using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using System.Web;
using AxsisTest.Models;
using AxsisTest.DB;

namespace AxsisTest.Helpers.Authentication
{
    public static class AuthManager
    {
        public static void SignInUser(Login userLogin)
        {
            var _dbManager = new DBManager();
            var userStore = new UserStore<IdentityUser>();
            var manager = new UserManager<IdentityUser>(userStore);
            
            var user = new IdentityUser()
            {
                Id = _dbManager.GetUsuarioId(userLogin),
                UserName = userLogin.NombreUsuario
            };

            var authenticationManager = System.Web.HttpContext.Current.GetOwinContext().Authentication;
            var userIdentity = manager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
            authenticationManager.SignIn(userIdentity);
        }
    }
}