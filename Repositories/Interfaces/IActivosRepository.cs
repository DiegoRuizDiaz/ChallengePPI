using Domains.Entities;

namespace Repositories.Interfaces
{
    public interface IActivosRepository
    {
        Task<Activos?> GetById(int id);
    }
}
