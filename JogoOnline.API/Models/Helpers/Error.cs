using static JogoOnline.API.Helpers.Validation;

namespace JogoOnline.API.Models.Helpers
{
    public class Error
    {
        public string Field { get; set; }

        public ValidationMessage Message { get; set; }

        public Error(string field, ValidationMessage message)
        {
            Field = field;
            Message = message;
        }
    }
}