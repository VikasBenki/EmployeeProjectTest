using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseLayer.Models.UserModel
{
    public class LoginModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string token { get; set; }

    }
}
