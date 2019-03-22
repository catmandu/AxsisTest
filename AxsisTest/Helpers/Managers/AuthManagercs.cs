using System;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using AxsisTest.Models;
using AxsisTest.DB;

namespace AxsisTest.Helpers
{
    public class AuthManager
    {
        public bool CreateUser(Usuario newUser)
        {
            string encodedPass = PassEncryption.Encrypt(newUser.Password);
            var userStore = new UserStore<IdentityUser>();
            var manager = new UserManager<IdentityUser>(userStore);

            try
            {
                var user = manager.Find(newUser.NombreUsuario, encodedPass);

                //User already exist
                if(user != null)
                {
                    return false;
                }
                user = new IdentityUser()
                {
                    UserName = newUser.NombreUsuario,
                    Email = newUser.Correo
                };

                IdentityResult result = manager.Create(user, encodedPass);
                if (result.Succeeded)
                {
                    var authenticationManager = HttpContext.Current.GetOwinContext().Authentication;
                    var userIdentity = manager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie );
                    authenticationManager.SignIn(userIdentity);

                    return true;
                }

                return false;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public bool UpdateUser(Usuario userNewData)
        {
            var existingUser = new DBManager().GetUsuario(userNewData.Id);
            var userStore = new UserStore<IdentityUser>();
            var manager = new UserManager<IdentityUser>(userStore);
            try
            {
                var user = manager.Find(existingUser.NombreUsuario, existingUser.Password);

                //User found
                if (user != null)
                {
                    manager.Delete(user);

                    user = new IdentityUser()
                    {
                        UserName = userNewData.NombreUsuario,
                        Email = userNewData.Correo
                    };

                    string newEncodedPass = PassEncryption.Encrypt(userNewData.Password);

                    IdentityResult result = manager.Create(user, newEncodedPass);

                    if (result.Succeeded)
                    {
                        SignOutUser();
                        SignInUser(userNewData);
                    }

                    return result.Succeeded;
                }

                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool SignInUser(Login userLogin)
        {
            string encodedPass = PassEncryption.Encrypt(userLogin.Password);
            var userStore = new UserStore<IdentityUser>();
            var manager = new UserManager<IdentityUser>(userStore);
            var user = manager.Find(userLogin.NombreUsuario, encodedPass);

            if (user != null)
            {
                var authenticationManager = HttpContext.Current.GetOwinContext().Authentication;
                var userIdentity = manager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                authenticationManager.SignIn(userIdentity);

                return true;
            }

            return false;
        }

        public void SignOutUser()
        {
            var authenticationManager = HttpContext.Current.GetOwinContext().Authentication;
            authenticationManager.SignOut();
        }
    }
}