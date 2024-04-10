

using System.Text.RegularExpressions;

namespace BARBERSHOP_V2.Service.Validator
{
    public class PasswordValidator
    {
        public bool IsStrongPassword(string password)
        {
            string pattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*\W).{8,}$";
            return Regex.IsMatch(password, pattern);
        }
    }
}
