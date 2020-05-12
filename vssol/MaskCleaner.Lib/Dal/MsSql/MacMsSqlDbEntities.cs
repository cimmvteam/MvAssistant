namespace MaskAutoCleaner.Dal
{
    using MaskAutoCleaner.Dal.Entities;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.SqlClient;

    public partial class MsSqlDbEntities : MacDbContext
    {

        public MsSqlDbEntities(SqlConnection conn)
            : base(conn)
        {
            //Database.SetInitializer<MariaStockDbEntities>(new MigrateDatabaseToLatestVersion<MariaStockDbEntities,>());
            //Database.SetInitializer<MariaStockDbEntities>(new CreateDatabaseIfNotExists<MariaStockDbEntities>());

            ((IObjectContextAdapter)this).ObjectContext.CommandTimeout = 180;

        }



        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {


            modelBuilder.Entity<test_table>()
           .Property(e => e.varchar50)
           .IsUnicode(false);

            modelBuilder.Entity<test_table>()
                .Property(e => e.Text)
                .IsUnicode(false);

            modelBuilder.Entity<test_table>()
                .Property(e => e.varchar_max)
                .IsUnicode(false);

            modelBuilder.Entity<test_table>()
                .Property(e => e.decimal_18_0)
                .HasPrecision(18, 0);

            modelBuilder.Entity<test_table>()
                .Property(e => e.nchar10)
                .IsFixedLength();

        }



    }
}
