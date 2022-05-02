namespace JogoOnline.API.Commands
{
    public class Game
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public int Year { get; set; }

        public bool IsActive { get; set; } = true;
    }
}