using System;

namespace JogoOnline.API.Models
{
    public class GameResultMemory
    {
        public Player Player { get; set; }

        public Game Game { get; set; }

        public long Win { get; set; }

        public DateTimeOffset? CreatedAt { get; set; }
    }

    public class GameResultBalance
    {
        public Player Player { get; set; }

        public long Balance { get; set; }

        public DateTimeOffset? LastUpdateDate { get; set; }
    }
}