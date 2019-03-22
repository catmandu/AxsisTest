using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Web;
using AxsisTest.Models;
using AxsisTest.DB;

namespace AxsisTest.Helpers
{
    public class AuthManager
    {
        public bool CreateUser(Login userLogin)
        {
            var userStore = new UserStore<IdentityUser>();
            var manager = new UserManager<IdentityUser>(userStore);
            try
            {
                var user = manager.FindByName(userLogin.NombreUsuario);

                if(user != null)
                {
                    return true;
                }
                else
                {
                    user = new IdentityUser()
                    {
                        UserName = userLogin.NombreUsuario
                    };

                    userLogin.Password = PassEncryption.Encrypt(userLogin.Password);
                    IdentityResult result = manager.Create(user, userLogin.Password);
                    if (result.Succeeded)
                    {
                        var authenticationManager = HttpContext.Current.GetOwinContext().Authentication;
                        var userIdentity = manager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                        authenticationManager.SignIn(userIdentity);

                        return true;
                    }
                }

                return false;
            }
            catch
            {
                return false;
            }
        }

        public void SignInUser(Login userLogin)
        {
            var userStore = new UserStore<IdentityUser>();
            var manager = new UserManager<IdentityUser>(userStore);
            var user = manager.FindByName(userLogin.NombreUsuario);
            
            if (user != null)
            {
                var authenticationManager = HttpContext.Current.GetOwinContext().Authentication;
                var userIdentity = manager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                authenticationManager.SignIn(userIdentity);
            }
        }

        public void SignOutUser()
        {
            var authenticationManager = HttpContext.Current.GetOwinContext().Authentication;
            authenticationManager.SignOut();
        }
    }
}