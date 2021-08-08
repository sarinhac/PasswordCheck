using System;


namespace PasswordCheck.Models
{
    public class UserModelApi
    {
        public string User { get; set; }
        public string Token { get; set; }
        public bool Authenticated { get; set; }
        public DateTime TokenExpirationDateTime { get; set; }
    }
}
