using DataLibrary.DataAccess;
using DataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLibrary.BusinessLogic
{
    public static class EmployeeProcessor
    {
        
        public static int CreateEmployee(int employeeId, string firstName, string lastName, string email)
        { 
            EmployeeModel model = new EmployeeModel()
            {
                EmployeeId = employeeId,
                FirstName = firstName,
                LastName = lastName,
                EmailAddress = email
            };

            string sql = @" insert into dbo.Employee
                            values (1,@EmployeeId,@FirstName,@LastName,@EmailAddress);";

            return SqlDataAccess.SaveData(sql, model);
        }
        public static List<EmployeeModel> LoadEmployees()
        {
            string sql = @"select  *  from dbo.Employee;";
            return SqlDataAccess.LoadData<EmployeeModel>(sql);
        }
    }
}
