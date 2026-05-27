using System.ComponentModel.DataAnnotations;

namespace Seguridad.Business.DTOs.Seguridad
{
    public class LoginRequestDTO
    {
        [Required(ErrorMessage = "El nombre de usuario o correo es requerido")]
        public string Username { get; set; }

        [Required(ErrorMessage = "La contraseña es requerida")]
        public string Password { get; set; }
    }
}