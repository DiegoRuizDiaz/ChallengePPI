using Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface IOrdenesRepository
    {
        Task<List<Ordenes>> GetAll();
        Task<Ordenes?> GetByOrdenId(int ordenId);
        Task<Ordenes> Update(int ordenId, int estado);
        Task<Ordenes> Post(Ordenes orden);
        Task<Ordenes> Delete(Ordenes orden);
    }
}
