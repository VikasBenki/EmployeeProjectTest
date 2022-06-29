using DatabaseLayer.Models.UserModel;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RepositoryLayer.Services
{
    public class UserRL: IUserRL
    {    private readonly IConfiguration configuration;
        private readonly SqlConnection conect = new SqlConnection();
        private string ConnectionString;

        public UserRL(IConfiguration configuration)
        {
            this.configuration = configuration;
            ConnectionString = configuration.GetConnectionString("EmployeeProject");
            conect.ConnectionString = ConnectionString;
        }
        public AddUser Add(AddUser user)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SPAddUser", conect);
                cmd.CommandType =CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserName", user.UserName);
                cmd.Parameters.AddWithValue("@Email", user.Email);
                var encryptedPassword=EncryptPassword(user.Password);
                cmd.Parameters.AddWithValue("@Password",encryptedPassword);

                conect.Open();
                var result = cmd.ExecuteNonQuery();
                conect.Close();
                if(result != 0)
                {
                    return user;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }
        public static string EncryptPassword(string password)
        {
            try
            {
                if(string.IsNullOrEmpty(password))
                {
                    return null;
                }
                else
                {
                    byte[] b = Encoding.ASCII.GetBytes(password);
                    string encrypted = Convert.ToBase64String(b);
                    return encrypted;
                }
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
                LoginModel login = new LoginModel();
                SqlCommand cmd = new SqlCommand("SP_User_Login", conect)
                {
                    CommandType = CommandType.StoredProcedure
                };
                conect.Open();
                cmd.Parameters.AddWithValue("@UserName", username);
                var encryptedPassword= EncryptPassword(password);
                cmd.Parameters.AddWithValue("@Password", encryptedPassword);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while(reader.Read())
                    {
                        
                        login.UserId= Convert.ToInt32(reader["UserId"]);
                        login.UserName = Convert.ToString(reader["UserName"]);
                        login.Email = Convert.ToString(reader["Email"]);
                       

                    }
                    login.token = this.GenerateSecurityToken(username, login.UserId);
                    return login;

                }
                else
                {
                    return null;
                }
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }
        public string GenerateSecurityToken(string Email, int userId)
        {
            var SecurityKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("THIS_IS_MY_KEY_TO_GENERATE_TOKEN"));
            var credentials = new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
               new Claim(ClaimTypes.Role, "User"),
                new Claim(ClaimTypes.Email, Email),
                new Claim("UserId", userId.ToString())
            };
            var token = new JwtSecurityToken(
                this.configuration["Jwt:Issuer"],
                this.configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddHours(24),
                signingCredentials: credentials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
