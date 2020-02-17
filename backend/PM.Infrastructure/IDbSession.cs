using System.Text;
using System.Threading.Tasks;

namespace PM.Infrastructure
{
    public interface IDbSession
    {
        void CommitChanges();

        Task CommitChangesAsync();
    }
}
