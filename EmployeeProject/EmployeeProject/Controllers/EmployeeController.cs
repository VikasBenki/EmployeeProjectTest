using BussinessLayer.Interfaces;
using DatabaseLayer.Models.EmployeeModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace EmployeeProject.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeBL employeeBL;
        public EmployeeController(IEmployeeBL employeeBL)
        {
            this.employeeBL = employeeBL;
        }
        [HttpPost]
        [Authorize]
        public ActionResult Add(AddEmployee emp)
        {
            try
            {
                int UserId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var res =this.employeeBL.AddEmployee(emp, UserId);
                if (res!=null)
                {
                    return this.Ok(new {success = true, message ="Employee Added Successfully", Response = res});
                }
                else
                {
                    return this.BadRequest(new {success = false, message ="Failed to add employee"});

                }
            }
            catch (Exception Ex)
            {

                return NotFound(new {success=false, message=Ex.Message});
            }
        }
        [HttpDelete]
        public ActionResult DeleteEmployee(int EmpId)
        {
            try
            {
                int UserId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var res = this.employeeBL.DeleteEmployee( UserId, EmpId);
                if (res != null)
                {
                    return this.Ok(new { success = true, message = $"Employee deleted Successfully {EmpId}", Response = res });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Failed to Remove employee" });

                }
            }
            catch (Exception Ex)
            {
                return NotFound(new { Success = false, message = Ex.Message });
            }
        }
        [HttpGet]
        public ActionResult GetAllEmployee()
        {
            try
            {
                
                var res = this.employeeBL.GetallEmployes();
                if (res != null)
                {
                    return this.Ok(new { success = true, message = $"Employees got  Successfully", Response = res });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Failed to GetAll employee" });

                }
            }
            catch (Exception Ex)
            {
                return NotFound(new { Success = false, message = Ex.Message });
            }
        }
    }
}
