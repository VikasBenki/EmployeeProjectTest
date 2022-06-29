using DatabaseLayer.Models.UserModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
    public interface IUserRL
    {
        AddUser Add(AddUser user);
        LoginModel UserLogin(string username, string password);
    }
}
