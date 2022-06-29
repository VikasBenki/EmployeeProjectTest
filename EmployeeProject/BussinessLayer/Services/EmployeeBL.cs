using BussinessLayer.Interfaces;
using DatabaseLayer.Models.EmployeeModel;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinessLayer.Services
{
    public class EmployeeBL : IEmployeeBL
    {
        private readonly IEmployeeRL employeeRL;
        public EmployeeBL(IEmployeeRL employeeRL)
        {
            this.employeeRL = employeeRL;
        }
        public AddEmployee AddEmployee(AddEmployee emp, int UserId)
        {
            try
            {
                return this.employeeRL.AddEmployee(emp, UserId);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public string DeleteEmployee(int UserId, int EmpId)
        {
            try
            {
                return this.employeeRL.DeleteEmployee(UserId, EmpId);

            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }
        public List<EmpResponse> GetallEmployes()
        {
            try
            {
                return this.employeeRL.GetallEmployes();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
