namespace AxsisTest.DB
{
    using System.Data.Entity;
    using AxsisTest.Models;

    public class AxsisTestDB : DbContext
    {
        public AxsisTestDB()
            : base("name=AxsisTestDB")
        {
        }

        public virtual DbSet<Usuario> Usuarios { get; set; }
    }
}