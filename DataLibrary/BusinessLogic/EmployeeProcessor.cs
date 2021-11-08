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
        public static int CreateEmployee(string firstName,string lastName,string emailAddress,string phoneNo,DateTime dateOfBirth,long salary,string password,
            int leavesAvailable,int credits,string companyName)
        {
            Employee employee = new Employee()
            {
                firstName = firstName,
                lastName = lastName,
                emailAddress = emailAddress,
                phoneNo = phoneNo,
                dateOfBirth = dateOfBirth,
                salary = salary,
                password = password,
                leavesAvailable = leavesAvailable,
                credits = credits,
                companyName=companyName
            };
            string sql = @"insert into dbo.Employee values(@firstName,@lastName,@emailAddress,@phoneNo,@dateOfBirth,@salary,@password,@leavesAvailable,
                        @credits,@companyName);";
            return SqlDataAccess.SaveData(sql, employee);
        }
        public static int CreateCompany(string companyName, string motto, DateTime startDate)
        {
            Company company = new Company()
            {
                companyName = companyName,
                motto = motto,
                startDate = startDate
            };
            string sql = @"insert into dbo.Company values(@companyName, @motto, @startDate);";
            return SqlDataAccess.SaveData(sql, company);

        }
        public static int CreateManager(string firstName, string lastName, string emailAddress, string phoneNo, DateTime dateOfBirth, long salary, string password,
            int leavesAvailable, int credits, string companyName)
        {
            int forBr = DataLibrary.BusinessLogic.EmployeeProcessor.CreateEmployee(firstName, lastName, emailAddress,
                    phoneNo, dateOfBirth,salary, password, leavesAvailable, credits, companyName);
            string sql = " select * from dbo.Employee where dbo.Employee.EmailAddress = @emailAddress;";
            List<DataLibrary.Models.Employee> employeeList = DataLibrary.DataAccess.SqlDataAccess.Query<DataLibrary.Models.Employee,object>
                (sql, new { emailAddress = emailAddress});
            Employee employee2 = employeeList[0];
            string sql1 = "insert into dbo.CompanyManager values(@employeeId,@companyName);";
            return SqlDataAccess.SaveData(sql1, employee2);
        }
        public static List<EmployeeModel> LoadEmployees()
        {
            string sql = @"select  *  from dbo.Employee;";
            return SqlDataAccess.LoadData<EmployeeModel>(sql);
        }
        public static int CheckLogin(string emailAddress, string password)
        {
            string sql = "select * from dbo.Employee where dbo.Employee.EmailAddress = @emailAddress";
            List<Employee> employeeList = DataLibrary.DataAccess.SqlDataAccess.Query<Employee, object>(sql,
                new { emailAddress = emailAddress });
            if (employeeList.Count == 0)
            {
                return -1;
            }
            else if (employeeList[0].password != password)
            {
                return -2;
            }
            else
            {
                return employeeList[0].employeeId;
            }
        }
        public static bool IsManager( int employeeId)
        {
            string sql = "select * from dbo.CompanyManager where EmployeeId= @employeeId";
            List<object> List = SqlDataAccess.Query<object, object>(sql, new { employeeId = employeeId });
            if (List.Count > 0)
                return true;
            else return false;
        }
    }
}
