using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AxsisTest.Models
{
    public class Login
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Favor de introducir el nombre de usuario.")]
        [Display(Name = "Usuario:")]
        [StringLength(30, MinimumLength = 7, ErrorMessage = "El usuario debe constar de {2} caracteres mínimo.")]
        public string NombreUsuario { get; set; }

        [Required(ErrorMessage = "Favor de introducir la contraseña.")]
        [Display(Name = "Contraseña:")]
        [RegularExpression(@"(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*\W).{10,}", 
            ErrorMessage = "La contraseña debe tener por lo menos 1 letra mayúscula, 1 minúscula, 1 número, 1 alfanumérico y un mínimo de 10 caractéres.")]
        public string Password { get; set; }

        [NotMapped]
        [Required(ErrorMessage = "Favor de confirmar la contraseña.")]
        [Display(Name = "Confirmar contraseña:")]
        [Compare("Password", ErrorMessage = "Las contraseñas no coinciden.")]
        public string ConfirmPassword { get; set; }
    }
}