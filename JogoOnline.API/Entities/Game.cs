using JogoOnline.API.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Naylah;
using System.Collections.Generic;

namespace JogoOnline.API.Entities
{
    public class Game : Entity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public int Year { get; set; }

        public bool IsActive { get; set; } = true;

        public List<GameResult> GameResults { get; set; } = new List<GameResult>();

        internal static void EntityConfigure(ModelBuilder modelBuilder)
        {
            InfraExtensions.DefaultModelConfiguration<Game>(modelBuilder);

            #region Fields

            modelBuilder.Entity<Game>()
                .Property(x => x.Name)
                .HasColumnType("varchar(50)")
                .IsRequired();

            modelBuilder.Entity<Game>()
                .Property(x => x.Description)
                .HasColumnType("varchar(max)")
                .IsRequired();

            modelBuilder.Entity<Game>()
                .Property(x => x.Year)
                .IsRequired();

            modelBuilder.Entity<Game>()
                .Property(x => x.IsActive)
                .IsRequired();

            #endregion

            #region Relationships

            modelBuilder.Entity<Game>()
                .HasMany(x => x.GameResults)
                .WithOne(x => x.Game)
                .HasForeignKey(x => x.GameId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

            #endregion
        }
    }
}