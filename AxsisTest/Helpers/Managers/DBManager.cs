using System.Collections.Generic;
using System.Linq;
using System.Data.Entity.Migrations;
using AxsisTest.Models;
using AxsisTest.Helpers;
using System;

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

        public IEnumerable<Usuario> GetUsuarios()
        {
            using (var dbContext = new AxsisTestDB())
            {
                return dbContext.Usuarios.ToList();
            }
        }

        public Usuario GetUsuario(int id)
        {
            Usuario user = new Usuario();

            if (id != 0)
            {
                using (var dbContext = new AxsisTestDB())
                {
                    user = dbContext.Usuarios.FirstOrDefault(u => u.Id == id);
                }
            }

            return user;
        }

        public bool SaveUsuario(Usuario user)
        {
            using (var dbContext = new AxsisTestDB())
            {
                try
                {
                    user.Password = PassEncryption.Encrypt(user.Password);

                    dbContext.Usuarios.Add(user);
                    dbContext.SaveChanges();
                }
                catch(Exception ex)
                {
                    return false;
                }

                return true;
            }
        }

        public bool UpdateUsuario(Usuario user)
        {
            using (var dbContext = new AxsisTestDB())
            {
                try
                {
                    user.Password = PassEncryption.Encrypt(user.Password);

                    var userToUpdate = dbContext.Usuarios.FirstOrDefault(u => u.Id == user.Id);
                    userToUpdate = user;

                    dbContext.Usuarios.AddOrUpdate(userToUpdate);
                    dbContext.SaveChanges();
                }
                catch(Exception ex)
                {
                    return false;
                }

                return true;
            }
        }

        public bool DeleteUsuario(int id)
        {
            using (var dbContext = new AxsisTestDB())
            {
                try
                {
                    var user = dbContext.Usuarios.FirstOrDefault(u => u.Id == id);
                    user.Estatus = false;

                    dbContext.Usuarios.AddOrUpdate(user);
                    dbContext.SaveChanges();
                }
                catch(Exception ex)
                {
                    return false;
                }

                return true;
            }
        }
    }
}