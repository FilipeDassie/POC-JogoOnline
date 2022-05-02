using JogoOnline.API.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Naylah;
using System.Collections.Generic;

namespace JogoOnline.API.Entities
{
    public class Player : Entity
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public bool IsActive { get; set; } = true;

        public List<GameResult> GameResults { get; set; } = new List<GameResult>();

        internal static void EntityConfigure(ModelBuilder modelBuilder)
        {
            InfraExtensions.DefaultModelConfiguration<Player>(modelBuilder);

            #region Fields

            modelBuilder.Entity<Player>()
                .Property(x => x.Name)
                .HasColumnType("varchar(50)")
                .IsRequired();

            modelBuilder.Entity<Player>()
                .Property(x => x.IsActive)
                .IsRequired();

            #endregion

            #region Relationships

            modelBuilder.Entity<Player>()
                .HasMany(x => x.GameResults)
                .WithOne(x => x.Player)
                .HasForeignKey(x => x.PlayerId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

            #endregion
        }
    }
}