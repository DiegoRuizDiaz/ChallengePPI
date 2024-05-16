using Domains.Entities;

namespace Repositories.Interfaces
{
    public interface IActivosRepository
    {
        Task<Activos?> GetByTicker(string ticker);
    }
}
