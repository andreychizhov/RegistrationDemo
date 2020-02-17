using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace PM.Model
{
    public class SqliteDataContext : DataContext
    {
        const string connectionString = "Data Source=LocalDatabase.db";

        public SqliteDataContext() : base(null)
        {
        }

        public SqliteDataContext(IConfiguration configuration) : base(configuration) { }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options
                .UseLazyLoadingProxies()
                .UseSqlite(Configuration?.GetConnectionString("WebApiDatabase") ?? connectionString);
        }
    }
}