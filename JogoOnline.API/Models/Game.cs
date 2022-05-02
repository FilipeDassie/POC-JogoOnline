namespace JogoOnline.API.Models
{
    public class Game
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int Year { get; set; }

        public bool IsActive { get; set; }
    }
}