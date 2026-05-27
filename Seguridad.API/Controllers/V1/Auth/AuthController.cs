using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using global::Seguridad.API.Models.Common;
using global::Seguridad.API.Models.Requests.Internal;
using global::Seguridad.Business.DTOs.Seguridad;
using global::Seguridad.Business.Interfaces.Seguridad;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Seguridad.API.Controllers.V1.Auth
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/internal/auth")]
    [Route("api/v{version:apiVersion}/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginRequest)
        {
            var response = await _authService.LoginAsync(loginRequest);
            return Ok(new ApiSuccessResponse<AuthTokenResponse>(ToAuthTokenResponse(response), "Autenticación exitosa"));
        }

        [HttpPost("register-cliente")]
        public async Task<IActionResult> RegisterCliente([FromBody] RegisterClienteDTO registerRequest)
        {
            var response = await _authService.RegisterClienteAsync(registerRequest);
            return CreatedAtAction(
                nameof(Login),
                routeValues: null,
                value: new ApiSuccessResponse<AuthTokenResponse>(ToAuthTokenResponse(response), "Registro exitoso"));
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] RefreshTokenRequest request)
        {
            var response = await _authService.RefreshTokenAsync(request.RefreshToken);
            return Ok(new ApiSuccessResponse<AuthTokenResponse>(ToAuthTokenResponse(response), "Token actualizado exitosamente"));
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout([FromBody] LogoutRequest request)
        {
            await _authService.LogoutAsync(request.RefreshToken);
            return NoContent();
        }

        [Authorize]
        [HttpPost("cambiar-password")]
        public async Task<IActionResult> CambiarPassword([FromBody] CambiarPasswordRequest request)
        {
            var idValue = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!int.TryParse(idValue, out var idUsuario))
                throw new UnauthorizedAccessException();

            var usuario = User.Identity?.Name ?? "Sistema";
            await _authService.CambiarPasswordAsync(idUsuario, request.PasswordActual, request.PasswordNuevo, usuario);
            return NoContent();
        }

        private static AuthTokenResponse ToAuthTokenResponse(LoginResponseDTO response)
        {
            return new AuthTokenResponse
            {
                Token = response.Token,
                RefreshToken = response.RefreshToken,
                Expiration = response.Expiration,
                UsuarioId = response.UsuarioId,
                UsuarioGuid = response.UsuarioGuid,
                Username = response.Username,
                Email = response.Correo,
                Roles = response.Roles
            };
        }
    }
}
