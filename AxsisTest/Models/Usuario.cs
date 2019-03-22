using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AxsisTest.Helpers;

namespace AxsisTest.Models
{
    [Table("Usuario")]
    public class Usuario : Login
    {
        public Usuario()
        {
            this.FechaCreacion = DateTime.Now;
        }

        [Required(ErrorMessage = "Favor de introducir el correo")]
        [RegularExpression(@"^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)([a-zA-Z]{2,4})(\.[a-zA-Z]{2,4})?$"
        , ErrorMessage = "Formato de correo inválido.")]
        [Display(Name = "Correo:")]
        public string Correo { get; set; }

        public bool Estatus { get; set; }

        public Sexo Sexo { get; set; }

        public DateTime FechaCreacion { get; set; }
    }
}