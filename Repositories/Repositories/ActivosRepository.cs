using Domains.Entities;
using Microsoft.EntityFrameworkCore;
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

        public async Task<Activos?> GetByTicker(string ticker)
        {
            return await _context.Activos.FirstOrDefaultAsync(a=> a.Ticker.ToUpper() == ticker.ToUpper());
        }

    }
}
