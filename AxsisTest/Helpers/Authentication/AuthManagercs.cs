using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Web;
using AxsisTest.Models;
using AxsisTest.DB;

namespace AxsisTest.Helpers.Authentication
{
    public static class AuthManager
    {
        public static void CreateUser(Login userLogin)
        {
            var _dbManager = new DBManager();
            var userStore = new UserStore<IdentityUser>();
            var manager = new UserManager<IdentityUser>(userStore);

            var user = new IdentityUser()
            {
                UserName = userLogin.NombreUsuario
            };

            IdentityResult result = manager.Create(user, userLogin.Password);
            if (result.Succeeded)
            {
                var authenticationManager = HttpContext.Current.GetOwinContext().Authentication;
                var userIdentity = manager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                authenticationManager.SignIn(userIdentity);
            }
        }

        public static void SignInUser(Login userLogin)
        {
            var _dbManager = new DBManager();
            var userStore = new UserStore<IdentityUser>();
            var manager = new UserManager<IdentityUser>(userStore);
            var user = manager.Find(userLogin.NombreUsuario, userLogin.Password);
            
            if (user != null)
            {
                var authenticationManager = HttpContext.Current.GetOwinContext().Authentication;
                var userIdentity = manager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                authenticationManager.SignIn(userIdentity);
                authenticationManager.SignOut();
            }
        }

        public static void SignOutUser()
        {
            var authenticationManager = HttpContext.Current.GetOwinContext().Authentication;
            authenticationManager.SignOut();
        }
    }
}