using Microsoft.EntityFrameworkCore;
using SB.Core.Data;
using SB.Credito.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SB.Credito.Infra.Repositories
{
    public class CreditoRepository : ICreditoRepository
    {
        private readonly CreditoContext _context;

        public CreditoRepository(CreditoContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task<IList<Domain.Entities.Credito>> GetCreditos()
        {
            return await _context.Creditos.AsNoTracking().ToListAsync();
        }

        public void AddCredito(Domain.Entities.Credito movimentation)
        {
            _context.Creditos.Add(movimentation);
        }

        public void RemoveCredito(Domain.Entities.Credito movimentation)
        {
            _context.Remove(movimentation);
        }

        public Domain.Entities.Credito GetCreditoById(Guid id)
        {
            var movimentation = _context.Creditos.Where(a => a.Id == id).Include(x => x.Cpf).AsNoTracking().FirstOrDefault();
            return movimentation;
        }

        public void UpdateCredito(Domain.Entities.Credito movimentation)
        {
            _context.Update(movimentation);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
