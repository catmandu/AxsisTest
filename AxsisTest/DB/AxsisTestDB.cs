namespace AxsisTest.DB
{
    using System.Data.Entity;
    using AxsisTest.Models;

    public class AxsisTestDB : DbContext
    {
        // Your context has been configured to use a 'AxsisTestDB' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'AxsisTest.DB.AxsisTestDB' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'AxsisTestDB' 
        // connection string in the application configuration file.
        public AxsisTestDB()
            : base("name=AxsisTestDB")
        {
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        public virtual DbSet<Usuario> Usuarios { get; set; }
    }
}