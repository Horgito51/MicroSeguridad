using System.Collections.Generic;
using global::Seguridad.Business.DTOs.Seguridad;
using global::Seguridad.Business.Exceptions;

namespace Seguridad.Business.Validators.Seguridad
{
    public static class LoginValidator
    {
        public static void Validate(LoginRequestDTO loginRequest)
        {
            if (loginRequest == null)
                throw new ValidationException("LOG-001", "La solicitud de login no puede ser nula.");

            var errors = new Dictionary<string, string[]>();

            if (string.IsNullOrWhiteSpace(loginRequest.Username))
                errors["Username"] = new[] { "El nombre de usuario o correo es obligatorio." };
            else if (loginRequest.Username.Length > 50)
                errors["Username"] = new[] { "El nombre de usuario o correo no puede exceder 50 caracteres." };

            if (string.IsNullOrWhiteSpace(loginRequest.Password))
                errors["Password"] = new[] { "La contraseña es obligatoria." };
            else if (loginRequest.Password.Length < 6)
                errors["Password"] = new[] { "La contraseña debe tener al menos 6 caracteres." };

            if (errors.Count > 0)
                throw new ValidationException("LOG-002", errors);
        }
    }
}