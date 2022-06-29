using DatabaseLayer.Models.EmployeeModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
    public interface IEmployeeRL
    {
        AddEmployee AddEmployee(AddEmployee emp, int UserId);
        public string DeleteEmployee(int UserId, int EmpId);
        public List<EmpResponse> GetallEmployes();
    }
}
