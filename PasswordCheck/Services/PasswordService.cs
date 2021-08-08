
using System.Linq;
using System.Text.RegularExpressions;

namespace PasswordCheck.Services
{
    public class PasswordService
    {
        public static bool isValid(string password)
        {

            if (string.IsNullOrEmpty(password) || password.Length < 15)
                return false;

            if (!password.Any(char.IsUpper))
                return false;

            if(!password.Any(char.IsLower))
                return false;

            if (!password.Contains("@") && !password.Contains("#") && !password.Contains("_") && !password.Contains("-") && !password.Contains("!"))
                return false;

            Match verifyIsRepeat = Regex.Match(password, @"(\w)\1+?"); //confere se há caracteres repetidos

            if (verifyIsRepeat.Success)
                return false;

            return true; // Senha atende a todas as exigencias

        }

        public static string GenerateSalt()
        {
            return BCrypt.Net.BCrypt.GenerateSalt();
        }
        public static string CreatePassword(string password)
        {

            return BCrypt.Net.BCrypt.HashPassword(password, GenerateSalt()); ;
        }

        public static bool ValidatePassword(string password, string correctHash)
        {
            return BCrypt.Net.BCrypt.Verify(password, correctHash);
        }
    }
}
