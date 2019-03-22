namespace AxsisTest.DB
{
    using System.Data.Entity;
    using AxsisTest.Models;

    public class AxsisTestDB : DbContext
    {
        public AxsisTestDB()
            : base("name=AxsisTestDB")
        {
            Configuration.ValidateOnSaveEnabled = false;
        }

        public virtual DbSet<Usuario> Usuarios { get; set; }
    }
}