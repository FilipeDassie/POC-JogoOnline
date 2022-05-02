using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace JogoOnline.API.Helpers
{
    public class Validation
    {
        public enum ValidationMessage
        {
            RequiredField = 1,
            InvalidValue = 2,
            NotFound = 3,
            Duplicated = 4,
            InvalidCredentials = 5,
            Inactive = 6
        }

        #region E-mail Validations

        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return false;
            }

            Regex regex = new Regex(@"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$");

            return regex.IsMatch(email);
        }

        #endregion

        #region List Validations

        public static bool IsEmptyList<TSource>(IEnumerable<TSource> list)
        {
            //return ((lista == null) || (lista.FirstOrDefault() == null));
            return ((list == null) || (!list.Any()));
        }

        #endregion
    }
}