using JogoOnline.API.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JogoOnline.API.Infrastructure
{
    public class DbInitializer
    {
        public static void Seed(JogoOnlineDbContext context)
        {
            context.Database.Migrate();

            DateTimeOffset now = DateTimeOffset.Now;

            #region Players

            if (!context.Set<Player>().Any())
            {
                List<Player> objPlayers = new List<Player>()
                {
                    new Player() { Id = "7CCA4FA1-33FD-4792-A166-3133159A1435", Name = "Jess", Email = "player.jess@gmail.com", CreatedAt = now },
                    new Player() { Id = "06A8B294-494C-4282-9332-3AE6B7915307", Name = "Sunny", Email = "player.sunny@gmail.com", CreatedAt = now },
                    new Player() { Id = "4F0731C4-3BA0-4F6B-B9DA-BD5CD232AA8E", Name = "Leo", Email = "player.leo@gmail.com", CreatedAt = now },
                    new Player() { Id = "6CBE2098-7CEA-4C51-BE94-CC6C00EF84AE", Name = "Mia", Email = "player.mia@gmail.com", CreatedAt = now },
                    new Player() { Id = "6EAE63F4-A6B2-41A1-A0EE-987D68991DA5", Name = "Petit", Email = "player.petit@gmail.com", CreatedAt = now },
                    new Player() { Id = "6EAE63F4-A6B2-41A1-A0EE-987D68991DA5", Name = "Lucy", Email = "player.lucy@gmail.com", CreatedAt = now },
                    new Player() { Id = "6EAE63F4-A6B2-41A1-A0EE-987D68991DA5", Name = "Calvin", Email = "player.calvin@gmail.com", CreatedAt = now },
                    new Player() { Id = "6EAE63F4-A6B2-41A1-A0EE-987D68991DA5", Name = "Joy", Email = "player.joy@gmail.com", CreatedAt = now }
                };

                context.Set<Player>().AddRange(objPlayers);
            }

            #endregion

            #region Games

            if (!context.Set<Game>().Any())
            {
                List<Game> objGames = new List<Game>()
                {
                    new Game() { Id = "65A0B190-6914-4BE4-964F-948FBD707FE0", Name = "Campeonato XPTO " + now.Year.ToString("0000"),Description = "Maior jogo online XPTO...",  Year = now.Year, CreatedAt = now }
                };

                context.Set<Game>().AddRange(objGames);
            }

            #endregion

            context.SaveChanges();
        }
    }
}