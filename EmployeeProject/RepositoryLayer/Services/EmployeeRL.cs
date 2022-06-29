using DatabaseLayer.Models.EmployeeModel;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace RepositoryLayer.Services
{
    public class EmployeeRL: IEmployeeRL
    {
        private readonly IConfiguration configurarion;
        private readonly SqlConnection conect = new SqlConnection();
        private readonly string ConnectionString;
        public EmployeeRL(IConfiguration configurarion)
        {
            this.configurarion = configurarion;
            ConnectionString = configurarion.GetConnectionString("EmployeeProject");
            conect.ConnectionString = ConnectionString;
        }

        public AddEmployee AddEmployee(AddEmployee emp,int UserId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_Add_Employee", conect)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@EmployeeName", emp.EmployeeName);
                cmd.Parameters.AddWithValue("@Contact", emp.Contact);
                cmd.Parameters.AddWithValue("@Email", emp.Email);
                cmd.Parameters.AddWithValue("@Department", emp.Department);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                conect.Open();
                var result= cmd.ExecuteNonQuery();
                conect.Close();
                if(result != 0)
                {
                    return emp;
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
        public string DeleteEmployee(int UserId,int EmpId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_Delete_Employee", conect)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("UserId", UserId);
                cmd.Parameters.AddWithValue("EmpId", EmpId);
                conect.Open();
                var res = cmd.ExecuteNonQuery();
                conect.Close();
                if(res!= 0)
                {
                    return "Employee Removed successfully";
                }
                else
                {
                    return "Failed to remove the employee";
                }

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
                List<EmpResponse> EmpRes = new List<EmpResponse>();
                SqlCommand cmd = new SqlCommand("SP_GetAll_Employee", conect);
                cmd.CommandType = CommandType.StoredProcedure;
                conect.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if(reader.HasRows)
                {
                    while(reader.Read())
                    {
                        EmpResponse emp = new EmpResponse();
                        EmpResponse temp;
                        temp = ReadData(emp, reader);
                        EmpRes.Add(temp);
                    }
                    conect.Close();
                    return EmpRes;
                }
                else
                {conect.Close();
                    return null;
                }
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }

        public EmpResponse ReadData(EmpResponse emp, SqlDataReader reader)
        {
            emp.EmpId = Convert.ToInt32(reader["EmpId"]);
            emp.EmployeeName = Convert.ToString(reader["Employee_Name"]);
            emp.Contact = Convert.ToInt64(reader["Contact"]);
            emp.Email = Convert.ToString(reader["Email"]);
            emp.Department = Convert.ToString(reader["Department"]);
            return emp;
        }
    }
}
