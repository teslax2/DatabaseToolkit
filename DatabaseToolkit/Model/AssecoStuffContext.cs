using System.Data.Entity;

namespace DatabaseToolkit.Model
{
    public class AssecoStuffContext : DbContext
    {
        public DbSet<AssecoStuff> AssecoStuffs { get; set; }
        public AssecoStuffContext(string connectionString)
        {
            this.Database.Connection.ConnectionString = connectionString;
        }
    }
}
