using System;
using System.Collections.Generic;
using global::Seguridad.Business.DTOs.Seguridad;

namespace Seguridad.API.Models.Requests.Internal
{
    public class RefreshTokenRequest
    {
        public string RefreshToken { get; set; } = string.Empty;
    }

    public class LogoutRequest
    {
        public string RefreshToken { get; set; } = string.Empty;
    }

    public class CambiarPasswordRequest
    {
        public string PasswordActual { get; set; } = string.Empty;
        public string PasswordNuevo { get; set; } = string.Empty;
    }

    public class InhabilitarRequest
    {
        public string Motivo { get; set; } = string.Empty;
    }

    public class RolUpsertRequest
    {
        public string NombreRol { get; set; } = string.Empty;
        public string DescripcionRol { get; set; } = string.Empty;
        public string EstadoRol { get; set; } = "ACT";
        public bool Activo { get; set; } = true;

        public RolCreateDTO ToCreateDto() => new()
        {
            NombreRol = NombreRol,
            DescripcionRol = DescripcionRol,
            EstadoRol = EstadoRol,
            Activo = Activo
        };

        public RolUpdateDTO ToUpdateDto(int id) => new()
        {
            IdRol = id,
            NombreRol = NombreRol,
            DescripcionRol = DescripcionRol,
            EstadoRol = EstadoRol,
            Activo = Activo
        };
    }

    public class RolPermisosUpsertRequest
    {
        public List<string> Permisos { get; set; } = new();
    }

    public class UsuarioCreateRequest
    {
        public int? IdCliente { get; set; }
        public Guid? ClienteGuid { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Nombres { get; set; } = string.Empty;
        public string Apellidos { get; set; } = string.Empty;
        public string EstadoUsuario { get; set; } = "ACT";
        public bool Activo { get; set; } = true;
        public int? IdRol { get; set; }
        public Guid? RolGuid { get; set; }
        public List<RolDTO> Roles { get; set; } = new();

        public UsuarioCreateDTO ToCreateDto() => new()
        {
            IdCliente = IdCliente,
            Username = Username,
            Correo = Correo,
            Password = Password,
            Nombres = Nombres,
            Apellidos = Apellidos,
            EstadoUsuario = EstadoUsuario,
            Activo = Activo,
            IdRol = IdRol,
            RolGuid = RolGuid,
            Roles = Roles
        };
    }

    public class UsuarioUpdateRequest
    {
        public string Correo { get; set; } = string.Empty;
        public string Nombres { get; set; } = string.Empty;
        public string Apellidos { get; set; } = string.Empty;
        public string EstadoUsuario { get; set; } = "ACT";
        public bool Activo { get; set; } = true;
        public List<RolDTO> Roles { get; set; } = new();
        public byte[] RowVersion { get; set; } = Array.Empty<byte>();

        public UsuarioUpdateDTO ToUpdateDto(int id) => new()
        {
            IdUsuario = id,
            Correo = Correo,
            Nombres = Nombres,
            Apellidos = Apellidos,
            EstadoUsuario = EstadoUsuario,
            Activo = Activo,
            Roles = Roles,
            RowVersion = RowVersion
        };
    }
}
