using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity.Migrations;
using AxsisTest.Models;
using AxsisTest.Helpers.Encryption;

namespace AxsisTest.DB
{
    public class DBManager
    {
        public bool ValidateUsuario(Login user)
        {
            using (var dbContext = new AxsisTestDB())
            {
                string encodedPass = PassEncryption.Encrypt(user.Password);

                return dbContext.Usuarios.Any(u => 
                user.NombreUsuario == u.NombreUsuario
                && encodedPass == u.Password);
            }
        }

        public string GetUsuarioId(Login user)
        {
            using (var dbContext = new AxsisTestDB())
            {
                string encodedPass = PassEncryption.Encrypt(user.Password);

                return dbContext.Usuarios.FirstOrDefault(u =>
                user.NombreUsuario == u.NombreUsuario
                && encodedPass == u.Password).Id;
            }
        }

        public IEnumerable<Usuario> GetUsuarios()
        {
            using (var dbContext = new AxsisTestDB())
            {
                return dbContext.Usuarios.ToList();
            }
        }

        public Usuario GetUsuario(string id)
        {
            Usuario user = new Usuario();

            if (!string.IsNullOrEmpty(id))
            {
                using (var dbContext = new AxsisTestDB())
                {
                    user = dbContext.Usuarios.FirstOrDefault(u => u.Id == id);
                }
            }

            return user;
        }

        public void SaveOrUpdateUsuario(Usuario user)
        {
            using (var dbContext = new AxsisTestDB())
            {
                var existingUser = dbContext.Usuarios.FirstOrDefault(u => u.Id == user.Id);
                user.Password = existingUser != null ? existingUser.Password : PassEncryption.Encrypt(user.Password);

                dbContext.Usuarios.AddOrUpdate(user);
                dbContext.SaveChanges();
            }
        }
    }
}