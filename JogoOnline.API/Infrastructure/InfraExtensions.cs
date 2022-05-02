using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using Microsoft.Extensions.DependencyInjection;
using Naylah;
using System;

namespace JogoOnline.API.Infrastructure
{
    public static class InfraExtensions
    {
        public static void DefaultModelConfiguration<TEntity>(ModelBuilder modelBuilder, Action<EntityTypeBuilder<TEntity>> aditionalConfiguration = null) where TEntity : class, IEntity<string>, IModifiable
        {
            modelBuilder.Entity<TEntity>().HasKey(x => x.Id);
            modelBuilder.Entity<TEntity>().Property(x => x.Id).HasValueGenerator(typeof(IdValueGenerator));

            modelBuilder.Entity<TEntity>().Property(x => x.CreatedAt);
            modelBuilder.Entity<TEntity>().Property(x => x.UpdatedAt);
            modelBuilder.Entity<TEntity>().Property(x => x.Version).IsRowVersion();

            modelBuilder.Entity<TEntity>().HasQueryFilter(p => !p.Deleted);

            aditionalConfiguration?.Invoke(modelBuilder.Entity<TEntity>());
        }

        public static IWebHost MigrateDatabase<T>(this IWebHost webHost) where T : DbContext
        {
            using (var scope = webHost.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<T>();
                context.Database.Migrate();
            }

            return webHost;
        }

        public static void Truncate<TDbContext, TEntity>(this TDbContext ctx)
            where TDbContext : DbContext
            where TEntity : class, IEntity<string>, IModifiable, new()
        {
            var efType = ctx.Model.FindEntityType(typeof(TEntity));

            if (efType == null)
            {
                throw new Exception(efType.Name + " was not found in context model");
            }

        }
    }

    public class IdValueGenerator : ValueGenerator<string>
    {
        public override bool GeneratesTemporaryValues => true;

        public override string Next(EntityEntry entry)
        {
            return Guid.NewGuid().ToString("N");
        }
    }
}