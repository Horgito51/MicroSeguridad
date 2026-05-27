namespace Seguridad.API.Models.Common
{
    public class AuthTokenResponse
    {
        public string Token { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
        public DateTime Expiration { get; set; }
        public int UsuarioId { get; set; }
        public Guid UsuarioGuid { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public IReadOnlyCollection<string> Roles { get; set; } = [];
    }
}
