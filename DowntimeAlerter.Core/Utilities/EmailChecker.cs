using System.Net.Mail;
using System.Text.RegularExpressions;

namespace DowntimeAlerter.Core.Utilities
{
    public static class EmailChecker
    {
        public static bool IsValidEmail(string email)
        {
            try
            {
                const string emailRegex =
                    @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
                var isValidEmail = Regex.IsMatch(email, emailRegex);
                if (isValidEmail)
                {
                    var addr = new MailAddress(email);
                    return addr.Address == email;
                }

                return isValidEmail;
            }
            catch
            {
                return false;
            }
        }
    }
}