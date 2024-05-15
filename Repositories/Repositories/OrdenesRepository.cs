using Microsoft.EntityFrameworkCore;
using Repositories.Data;
using Repositories.Entities;
using Repositories.Interfaces;
using System.Data;

namespace Repositories.Repositories
{
    public class OrdenesRepository : IOrdenesRepository
    {
        private readonly AppDbContext _context;
        public OrdenesRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Ordenes>> GetAll()
        {
            return await _context.Ordenes.OrderByDescending(a => a.IdOrden).ToListAsync();
        }

        public async Task<Ordenes?> GetByOrdenId(int ordenId)
        {
            return await _context.Ordenes.FindAsync(ordenId);
        }

        public async Task<Ordenes> Update(int ordenId, int estado)
        {
            var ordenToUpdate = await _context.Ordenes.FindAsync(ordenId);
            ordenToUpdate.Estado = estado;

            await _context.SaveChangesAsync();
            return ordenToUpdate;
        }

        public async Task<Ordenes> Post(Ordenes orden)
        {
            await _context.Ordenes.AddAsync(orden);
            await _context.SaveChangesAsync();
            return orden;
        }

        public async Task<Ordenes> Delete(Ordenes orden)
        {
            _context.Remove(orden);
            await _context.SaveChangesAsync();
            return orden;
        }
    }
}
