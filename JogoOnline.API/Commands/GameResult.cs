using System;

namespace JogoOnline.API.Commands
{
    public class GameResult
    {
        public string PlayerId { get; set; }

        public string GameId { get; set; }

        public long Win { get; set; }

        public DateTimeOffset Timestamp { get; set; }
    }
}