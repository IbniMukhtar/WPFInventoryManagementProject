using System.Data.Entity;
using WpfApp3.Models;

namespace WpfApp3.Data
{
    public partial class AppDbContext : DbContext
    {
        public AppDbContext() : base("name=AppDbContext")
        {
        }

        public virtual DbSet<MAITEM> MAITEMS { get; set; } = null!;
        public virtual DbSet<MAPARTY> MAPARTIES { get; set; } = null!;
        public virtual DbSet<MAUSER> MAUSERS { get; set; } = null!;
        public virtual DbSet<TB_ORDER> TB_ORDER { get; set; } = null!;
        public virtual DbSet<TB_ORDERDTL> TB_ORDERDTL { get; set; } = null!;

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
