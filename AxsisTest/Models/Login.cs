using System;
using System.ComponentModel.DataAnnotations;

namespace AxsisTest.Models
{
    public class Login
    {
        [Key]
        public string Id { get; set; }

        [Required(ErrorMessage = "Favor de introducir el nombre de usuario")]
        [Display(Name = "Usuario:")]
        public string NombreUsuario { get; set; }

        [Required(ErrorMessage = "Favor de introducir la contraseña")]
        [Display(Name = "Contraseña:")]
        public string Password { get; set; }
    }
}