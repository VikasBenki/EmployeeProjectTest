using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseLayer.Models.EmployeeModel
{
    public class AddEmployee
    {
        public string EmployeeName { get; set; }
        public long Contact { get; set; }
        public string Email { get; set; }
        public string Department { get; set; }
        
    }
}
