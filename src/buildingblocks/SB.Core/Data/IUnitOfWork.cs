using System.Threading.Tasks;

namespace SB.Core.Data
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}
