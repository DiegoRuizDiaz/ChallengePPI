using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IActivosService
    {
        Task<ActivosDTO?> GetById(int id);
    }
}
