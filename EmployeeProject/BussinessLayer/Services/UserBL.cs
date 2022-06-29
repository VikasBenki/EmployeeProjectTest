using BussinessLayer.Interfaces;
using DatabaseLayer.Models.UserModel;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinessLayer.Services
{
    public class UserBL : IUserBL
    {
        private readonly IUserRL userRL;
        public UserBL(IUserRL userRL)
        {
            this.userRL = userRL;
        }
        
        public AddUser Add(AddUser user)
        {
            try
            {
                return this.userRL.Add(user);
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }

        public LoginModel UserLogin(string username, string password)
        {
            try
            {
                return this.userRL.UserLogin(username, password);
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }

        
    }
}
