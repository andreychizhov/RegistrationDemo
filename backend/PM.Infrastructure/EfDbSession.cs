using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace PM.Infrastructure
{
    public class EFDbSession : DbSessionBase, IDisposable
    {
        private DbContext _context;

        public EFDbSession(DbContext context)
        {
            _context = context;
        }

        public DbContext Context
        {
            get { return _context; }
        }

        protected override void CommitChangesCore()
        {
            _context.SaveChanges();
        }

        protected override async Task CommitChangesAsyncCore()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
