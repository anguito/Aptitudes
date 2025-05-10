using System.Data.Entity;
using System.Data.Entity.Core.Common;
using System.Data.SQLite;
using System.Data.SQLite.EF6;

namespace Aptitudes
{
    public class SQLiteConfiguration : DbConfiguration
    {
        public SQLiteConfiguration()
        {
            SetProviderFactory("System.Data.SQLite", SQLiteFactory.Instance);
            SetProviderFactory("System.Data.SQLite.EF6", SQLiteProviderFactory.Instance);
            SetProviderServices("System.Data.SQLite", (DbProviderServices)SQLiteProviderFactory.Instance.GetService(typeof(DbProviderServices)));
        }
    }
    public class AptitudesContext : DbContext
    {
        private static AptitudesContext _instancia;
        public AptitudesContext() : base("name=AptitudesConnection") { }

        public static AptitudesContext Instancia()
        {
            if (_instancia == null)
                _instancia = new AptitudesContext();
            return _instancia;
        }

        public DbSet<Aptitud> Aptitudes { get; set; }
        public DbSet<Factores> Factores { get; set; }
        public DbSet<Medidas> Medidas { get; set; }
    }
}

