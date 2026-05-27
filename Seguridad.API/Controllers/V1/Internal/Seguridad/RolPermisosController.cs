using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using global::Seguridad.API.Models.Requests.Internal;
using global::Seguridad.Business.Interfaces.Seguridad;

namespace Seguridad.API.Controllers.V1.Internal.Seguridad
{
    [ApiController]
    [Authorize(Roles = "ADMINISTRADOR,ADMIN,RECEPCIONISTA,OPERATIVO,DESK_SERVICE")]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/internal/roles")]
    public class RolPermisosController : ControllerBase
    {
        private readonly IRolService _rolService;

        public RolPermisosController(IRolService rolService)
        {
            _rolService = rolService;
        }

        [HttpPost("{rolGuid:guid}/permisos")]
        public async Task<IActionResult> AsignarPermisos(Guid rolGuid, [FromBody] RolPermisosUpsertRequest request)
        {
            // Validar que el rol exista (el almacenamiento de permisos puede implementarse cuando exista el modelo en BD)
            _ = await _rolService.GetByGuidAsync(rolGuid);
            return NoContent();
        }
    }
}
