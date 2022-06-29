using DatabaseLayer.Models.UserModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinessLayer.Interfaces
{
    public interface IUserBL
    {
        AddUser Add(AddUser user);
        LoginModel UserLogin(string username, string password);
    }
}
