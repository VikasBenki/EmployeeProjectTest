using BussinessLayer.Interfaces;
using DatabaseLayer.Models.UserModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace EmployeeProject.Controllers
{
    [ApiController]
    [Route("[controller]")]   
    public class UserController : ControllerBase
    {
        private readonly IUserBL userBL;
        public UserController(IUserBL userBL)
        {
            this.userBL = userBL;
        }
        [HttpPost("AddUser")]
        public ActionResult Add(AddUser user)
        {
            try
            {
                var result = this.userBL.Add(user);
                if (result != null)
                {
                    return this.Ok(new {Success = true, message ="User Added Successfully",Response = result});
                }
                else
                {
                    return this.BadRequest(new {success=false, message ="failed to Add User"});
                }

            }
            catch (Exception Ex)
            {

                return NotFound(new {success=false, message = Ex.Message });
            }
        }
        [HttpPost("UserLogin")]
        public ActionResult UserLogin(string username, string password)
        {
            try
            {
                var result = this.userBL.UserLogin(username, password);
                if (result ==null)
                {
                    return this.BadRequest(new { success = false, message = "Falied to login" });
                    
                }
                else
                {
                    return this.Ok(new { success = true, message = " UserLogin successfull", resposne = result });
                }
            }
            catch (Exception Ex)
            {

                return NotFound(new { success = false, message = Ex.Message });
            }
        }
    }
}
