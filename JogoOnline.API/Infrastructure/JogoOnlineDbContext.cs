using JogoOnline.API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Naylah;
using Naylah.Data.Access;

namespace JogoOnline.API.Infrastructure
{
    public class JogoOnlineDbContext : DbContext, IUnitOfWork
    {
        public const string Schema = "JogoOnline";

        public JogoOnlineDbContext(DbContextOptions<JogoOnlineDbContext> options) : base(options)
        {
            Database.SetCommandTimeout(600);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Ignore<Notification>();

            modelBuilder.HasDefaultSchema(Schema);

            Player.EntityConfigure(modelBuilder);
            Game.EntityConfigure(modelBuilder);
            GameResult.EntityConfigure(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        internal static void ConfigureDBContext(SqlServerDbContextOptionsBuilder obj)
        {
            obj.MigrationsHistoryTable("__Migrations", Schema);
        }

        public int Commit()
        {
            return SaveChanges();
        }
    }

    public class GateContextDesignTimeFactory : IDesignTimeDbContextFactory<JogoOnlineDbContext>
    {
        public JogoOnlineDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<JogoOnlineDbContext>();

            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB; Database=JogoOnline; Trusted_Connection=True; MultipleActiveResultSets=false;", JogoOnlineDbContext.ConfigureDBContext);

            return new JogoOnlineDbContext(optionsBuilder.Options);
        }
    }
}