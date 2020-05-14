namespace MaskAutoCleaner.Dal
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Reflection;
    using System.Data.Common;
    using MaskAutoCleaner.Dal.Entities;

    public partial class MacDbContext : DbContext
    {

        public MacDbContext(DbConnection conn)
            : base(conn, true)
        {



        }


        public virtual DbSet<mac_state_machine_history> mac_state_machine_history { get; set; }
        public virtual DbSet<mac_recipe_param> mac_recipe_param { get; set; }
        public virtual DbSet<mac_log> mac_log { get; set; }



        public virtual DbSet<test_table> test_table { get; set; }




        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
        }



    }
}
