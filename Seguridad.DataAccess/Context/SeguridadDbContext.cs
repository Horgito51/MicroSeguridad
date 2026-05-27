using Microsoft.EntityFrameworkCore;
using Seguridad.DataAccess.Entities.Seguridad;

namespace Seguridad.DataAccess.Context
{
    public class SeguridadDbContext : DbContext
    {
        public SeguridadDbContext(DbContextOptions<SeguridadDbContext> options) : base(options)
        {
        }

        public DbSet<UsuarioAppEntity> UsuariosApp => Set<UsuarioAppEntity>();
        public DbSet<RolEntity> Roles => Set<RolEntity>();
        public DbSet<UsuarioRolEntity> UsuariosRoles => Set<UsuarioRolEntity>();
        public DbSet<AuditoriaEntity> Auditorias => Set<AuditoriaEntity>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SeguridadDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
