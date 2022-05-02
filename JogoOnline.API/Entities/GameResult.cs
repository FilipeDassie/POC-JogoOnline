using JogoOnline.API.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Naylah;

namespace JogoOnline.API.Entities
{
    public class GameResult : Entity
    {
        public string PlayerId { get; set; }

        public Player Player { get; set; }

        public string GameId { get; set; }

        public Game Game { get; set; }

        public long Win { get; set; }

        internal static void EntityConfigure(ModelBuilder modelBuilder)
        {
            InfraExtensions.DefaultModelConfiguration<GameResult>(modelBuilder);

            #region Fields

            modelBuilder.Entity<GameResult>()
                .Property(x => x.PlayerId)
                .IsRequired();

            modelBuilder.Entity<GameResult>()
                .Property(x => x.GameId)
                .IsRequired();

            modelBuilder.Entity<GameResult>()
                .Property(x => x.Win)
                .IsRequired();

            #endregion
        }
    }
}