using SB.Core.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SB.Credito.Domain.Repositories
{
    public interface ICreditoRepository : IRepository<Entities.Credito>
    {
        void AddCredito(Entities.Credito movimentation);
        void RemoveCredito(Entities.Credito movimentation);
        Task<IList<Entities.Credito>> GetCreditos();
        Entities.Credito GetCreditoById(Guid id);
        void UpdateCredito(Entities.Credito movimentation);
    }
}
