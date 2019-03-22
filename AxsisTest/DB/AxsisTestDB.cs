namespace AxsisTest.DB
{
    using System.Data.Entity;
    using AxsisTest.Models;

    public partial class AxsisTestDB : DbContext
    {
        public AxsisTestDB()
            : base("name=AxsisTestDB")
        {
            Configuration.ValidateOnSaveEnabled = false;
            Database.SetInitializer<AxsisTestDB>(null);
        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
    }
}
