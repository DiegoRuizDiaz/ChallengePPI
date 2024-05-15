using Domain.Models;

namespace Services.Interfaces
{
    public interface IActivosService
    {
        Task<ActivosDTO?> GetById(int id);
    }
}
