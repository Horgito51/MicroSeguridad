using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using global::Seguridad.DataAccess.Context;
using global::Seguridad.DataAccess.Entities.Seguridad;
using global::Seguridad.DataAccess.Repositories.Interfaces.Seguridad;

namespace Seguridad.DataAccess.Repositories.Seguridad
{
    public class AuditoriaRepository : IAuditoriaRepository
    {
        protected readonly SeguridadDbContext _context;
        protected readonly DbSet<AuditoriaEntity> _dbSet;

        public AuditoriaRepository(SeguridadDbContext context)
        {
            _context = context;
            _dbSet = context.Set<AuditoriaEntity>();
        }

        public async Task<AuditoriaEntity?> GetByIdAsync(long id, CancellationToken ct = default)
            => await _dbSet.FindAsync(new object[] { id }, ct);

        public async Task<IEnumerable<AuditoriaEntity>> GetAllAsync(CancellationToken ct = default)
            => await _dbSet.ToListAsync(ct);

        public async Task<AuditoriaEntity> AddAsync(AuditoriaEntity entity, CancellationToken ct = default)
        {
            await _dbSet.AddAsync(entity, ct);
            await _context.SaveChangesAsync(ct);
            return entity;
        }

        // Nota: Update y Delete no están en la interfaz, pero si los necesitas, se pueden agregar.

        public async Task<AuditoriaEntity?> GetByGuidAsync(Guid guid, CancellationToken ct = default)
            => await _dbSet.FirstOrDefaultAsync(a => a.AuditoriaGuid == guid, ct);
    }
}