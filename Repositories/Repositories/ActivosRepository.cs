using Domains.Entities;
using Repositories.Data;
using Repositories.Interfaces;

namespace Repositories.Repositories
{
    public class ActivosRepository : IActivosRepository
    {
        private readonly AppDbContext _context;
        public ActivosRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Activos?> GetById(int id)
        {
            return await _context.Activos.FindAsync(id);
        }

    }
}
