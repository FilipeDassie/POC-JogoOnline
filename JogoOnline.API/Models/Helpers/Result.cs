using System.Collections.Generic;

namespace JogoOnline.API.Models.Helpers
{
    public class Result<T>
    {
        public bool Success { get; set; }

        public List<Error> Errors { get; set; } = new List<Error>();

        public T Data { get; set; }
    }
}