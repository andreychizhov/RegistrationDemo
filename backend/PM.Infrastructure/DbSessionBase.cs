using System.Threading.Tasks;

namespace PM.Infrastructure
{
    public abstract class DbSessionBase : IDbSession
    {
        public void CommitChanges()
        {
            CommitChangesCore();
        }

        public async Task CommitChangesAsync()
        {
            await CommitChangesAsyncCore();
        }

        protected abstract void CommitChangesCore();
        protected abstract Task CommitChangesAsyncCore();
    }
}