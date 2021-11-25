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
            int leavesAvailable,int credits,string companyName,string department)
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
                companyName=companyName,
                department =department
            };
            string sql = @"insert into dbo.Employee values(@firstName,@lastName,@emailAddress,@phoneNo,@dateOfBirth,@salary,@password,@leavesAvailable,
                        @credits,@companyName,@department);";
            return SqlDataAccess.SaveData(sql, employee);
        }
        public static bool PromoteEmployeeToManager(int employeeId)
        {
            if (IsManager(employeeId))
            {
                throw new Exception("Already a Manager . id =" + employeeId);
                return false;
            }

            string sql = "insert into dbo.CompanyManager values(@employeeId, @companyName)";
            var employee = GetEmployee(employeeId);
            var sqlObject = new { employeeId = employeeId, companyName =employee.companyName};
            SqlDataAccess.Query<object, object>(sql, sqlObject);
            return true;
        }
        public static bool EditEmployee(Employee employee)
        {

            var tempEmployee = DataLibrary.BusinessLogic.EmployeeProcessor.GetEmployee(employee.employeeId);
            tempEmployee.salary = employee.salary;
            tempEmployee.credits = employee.credits;
            tempEmployee.department = employee.department;
            tempEmployee.leavesAvailable = employee.leavesAvailable;
            string sql = @"update dbo.Employee set Salary = @salary ,LeavesAvailable= @leavesAvailable,
                           Credits= @credits, Department =@department where EmployeeId = @employeeId";
            SqlDataAccess.Query<object, Employee>(sql, tempEmployee);
            //new
            //{
            //    salary = employee.salary,
            //    leavesAvailable = employee.leavesAvailable
            //,
            //    credits = employee.credits
            //}
            return true;
        }
        public static bool EditProject(int projectId, string projectName, int projectLeaderId, string description, string companyName)
        {
            Project project = new Project()
            {
                projectId = projectId,
                projectName = projectName,
                projectLeaderId = projectLeaderId,
                description = description,
                companyName = companyName
            };
            string sql = @"update dbo.Project set ProjectLeaderId = @projectLeaderId, Description = @description where ProjectId = @projectId";
            SqlDataAccess.Query<object, Project>(sql, project);
            return true;
        }

        public static Employee GetEmployee(int employeeId)
        {
            if (!EmployeeExists(employeeId)) throw new Exception("Employee of this id does not exist , id =" + employeeId);
            string sql = "select * from dbo.Employee where EmployeeId = @employeeId";
            var listOfEmployee = SqlDataAccess.Query<Employee, object>(sql, new { employeeId = employeeId });
            return listOfEmployee[0];
        }
        public static int CreateCompany(string companyName, string motto, DateTime startDate,string address,string emailAddress,string phoneNo)
        {
            Company company = new Company()
            {
                companyName = companyName,
                motto = motto,
                startDate = startDate,
                address = address,
                emailAddress = emailAddress,
                phoneNo = phoneNo,
                   
            };
            string sql = @"insert into dbo.Company values(@companyName, @motto, @startDate,@address,@phoneNo,@emailAddress);";
            return SqlDataAccess.SaveData(sql, company);

        }
        public static int CreateManager(string firstName, string lastName, string emailAddress, string phoneNo, DateTime dateOfBirth, long salary, string password,
            int leavesAvailable, int credits, string companyName,string department)
        {
            int forBr = DataLibrary.BusinessLogic.EmployeeProcessor.CreateEmployee(firstName, lastName, emailAddress,
                    phoneNo, dateOfBirth,salary, password, leavesAvailable, credits, companyName,department);
            string sql = " select * from dbo.Employee where dbo.Employee.EmailAddress = @emailAddress;";
            List<DataLibrary.Models.Employee> employeeList = DataLibrary.DataAccess.SqlDataAccess.Query<DataLibrary.Models.Employee,object>
                (sql, new { emailAddress = emailAddress});
            Employee employee2 = employeeList[0];
            string sql1 = "insert into dbo.CompanyManager values(@employeeId,@companyName);";
            return SqlDataAccess.SaveData(sql1, employee2);
        }
        public static int CreateProject(string projectName,int projectLeaderId,string description,string companyName)
        {
            Project project = new Project()
            {
                projectName = projectName,
                projectLeaderId = projectLeaderId,
                description = description,
                companyName = companyName
            };
            string sql = @"insert into dbo.Project values(@projectName, @projectLeaderId, @description, @companyName);";
            return SqlDataAccess.SaveData(sql, project);
        }
        public static List<Project> ViewProjects(int employeeId)
        {
            var employee = GetEmployee(employeeId);
            string sql;
            if (IsManager(employeeId))
            {
                sql = "select * from dbo.Project where CompanyName = @companyName;";
                return SqlDataAccess.Query<Project, object>(sql, new { companyName = employee.companyName });
            }
            else
            {
                sql = "select ProjectId from dbo.WorksOn where EmployeeId = @id;";
                var projectIds = SqlDataAccess.Query<int, object>(sql, new { id = employeeId });
                List<Project> projects = new List<Project>();
                foreach(var id in projectIds)
                {
                    projects.Add(GetProject(id));
                }
                return projects;
            }
        }
        public static List<WorksOn> ViewProjectTeam(int projectId)
        {
            string sql = "select * from dbo.WorksOn where ProjectId = @projectId;";
            return SqlDataAccess.Query<WorksOn,object>(sql ,new { projectId = projectId });
        }

        public static int AddProjectEmployee(int projectId,int employeeId, string role,DateTime shiftStartTime,DateTime shiftEndTime)
        {
            WorksOn worksOn = new WorksOn()
            {
                projectId = projectId,
                employeeId = employeeId,
                role = role,
                shiftEndTime = shiftEndTime,
                shiftStartTime = shiftStartTime
            };
            string sql = @"insert into dbo.WorksOn values(@employeeId,@projectId,@role,@shiftStartTime,@shiftEndTime);";
            return SqlDataAccess.SaveData(sql, worksOn);
        }
        public static bool EditProjectEmployee(int projectId, int employeeId, string role, DateTime shiftStartTime, DateTime shiftEndTime)
        {
            WorksOn worksOn = new WorksOn()
            {
                 employeeId = employeeId,
                 projectId = projectId,
                 role = role,
                 shiftStartTime = shiftStartTime,
                 shiftEndTime = shiftEndTime
            };
            string sql = @"update dbo.WorksOn set Role = @role, ShiftStartTime = @shiftStartTime, ShiftEndTime = @shiftEndTime where
                          ProjectId = @projectId and EmployeeId = @employeeId;";
            SqlDataAccess.Query<object, WorksOn>(sql, worksOn);
            return true;
        }

        public static void ChangePassword(int employeeId , string password)
        {
            string sql = " update dbo.Employee set Password = @password where EmployeeId= @employeeId";
            var sqlObject = new { employeeId = employeeId, password = password };
            SqlDataAccess.Query<object, object>(sql, sqlObject);
        }
        public static List<Employee> LoadEmployees()
        {
            string sql = @"select  *  from dbo.Employee;";
            return SqlDataAccess.LoadData<Employee>(sql);
        }
        public static bool CanFireEmployee(int employeeId)
        {
            if (!EmployeeExists(employeeId)) throw new Exception("no employee of this id exists. id= " + employeeId);
            if (IsProjectLeader(employeeId)) return false;
            if (IsManager(employeeId))
            {
                //if only manager 
                string sql1 = "select CompanyName from dbo.CompanyManager where EmployeeId =@managerId";
                List<string> companyName = SqlDataAccess.Query<string, object>(sql1, new { managerId = employeeId });
                sql1 = "select EmployeeId from dbo.CompanyManager where CompanyName=@companyName";
                List<int> managers = SqlDataAccess.Query<int, object>(sql1, new { companyName = companyName });
                if (managers.Count < 2)
                {
                    return false;
                }
            }
            return true;
        }
        public static void FireEmployee(int employeeId)
        {
            if (!CanFireEmployee(employeeId)) throw new Exception("Cannot fire this Employee id =" + employeeId);
            string sql = "delete from dbo.Attendence where EmployeeId= @employeeId";
            var sqlObject = new { employeeId = employeeId };
            SqlDataAccess.Query<object, object>(sql, sqlObject);

            sql = "delete from dbo.Leave where EmployeeId= @employeeId";
            SqlDataAccess.Query<object, object>(sql, sqlObject);

            sql = "delete from dbo.CompanyManager where EmployeeId =@employeeId";
            SqlDataAccess.Query<object, object>(sql, sqlObject);

            sql = "delete from dbo.WorksOn where EmployeeId =@employeeId";
            SqlDataAccess.Query<object, object>(sql, sqlObject);

            sql = "delete from dbo.Employee where EmployeeId= @employeeId";
            SqlDataAccess.Query<object, object>(sql, sqlObject);
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
        public static bool IsProjectLeader(int employeeId)
        {
            if (!EmployeeExists(employeeId)) throw new Exception("no employee of this id exist id= " + employeeId);
            string sql = "select * from dbo.Project where ProjectLeaderId = @employeeId";
            List<object> List = SqlDataAccess.Query<object, object>(sql, new { employeeId = employeeId });
            if (List.Count > 0)
                return true;
            else return false;
        }

        //public static Employee GetEmployeeModel(int id)
        //{
        //    string sql = "select * from dbo.Employee where EmployeeId = @id;";
        //    List<Employee> list = SqlDataAccess.Query<Employee, object>(sql, new { id = id });
        //    if (list.Count == 0)
        //        return null;
        //    else
        //        return list[0];
        //}
        public static Project GetProject(int id)
        {
            string sql = "select * from dbo.Project where ProjectId = @id;";
            List<Project> list = SqlDataAccess.Query<Project ,object>(sql, new { id = id });
            if (list.Count == 0)
                return null;
            else
                return list[0];
        }
        public static WorksOn GetProjectEmployee(int employeeId,int projectId)
        {
            string sql = "select * from dbo.WorksOn where ProjectId = @projectId and EmployeeId = @employeeId;";
            List<WorksOn> list = SqlDataAccess.Query<WorksOn, object>(sql, new { projectId = projectId, employeeId = employeeId });
            if (list.Count == 0)
                return null;
            else
                return list[0];
        }
        public static string GetName(int id, bool isEmployee)
        {
            if (isEmployee)
            {
                string sql = "select FirstName from dbo.Employee where EmployeeId = @id;";
                List<string> list = SqlDataAccess.Query<string, object>(sql, new { id = id });
                if (list.Count == 0)
                    throw new Exception("there is no Employee of the id =" + id);
                return list[0];
            }
            else
            {
                string sql = "select ProjectName from dbo.Project where ProjectId = @id";
                List<string> list = SqlDataAccess.Query<string, object>(sql, new { id = id });
                if (list.Count == 0)
                    throw new Exception("there is no Project of the id =" + id);
                return list[0];
            }
        }
        public static bool EmployeeExists(int employeeId)
        {
            string sql = "select * from dbo.Employee where employeeId= @employeeID";
            List<DataLibrary.Models.Employee> returnList = SqlDataAccess.Query<DataLibrary.Models.Employee, object>
                (sql,new { employeeID = employeeId });
            if (returnList.Count > 0)
                return true;
            else return false;
        }
        public static int CheckAttendance(int employeeId)
        {
            var sqlObject = new { employeeId = employeeId, date = DateTime.Today };
            string sql = "select * from dbo.Attendence where EmployeeId = @employeeId and Date=@date";
            List<Attendance> TodayAttendence = SqlDataAccess.Query<Attendance, object>(sql, sqlObject);
            if (TodayAttendence.Count == 0)
            {
                return 1;
            }
            else if (TodayAttendence[0].CheckOutTime.Year == 0001)
            {
                return 2;
            }
            else return 3;
        }
        public static List<Attendance> ViewAttendance(int employeeId)
        {
            string sql = " select Date,CheckInTime,CheckOutTime from dbo.Attendence where EmployeeId = @employeeId";
            var sqlObject = new { employeeId = employeeId };
            List<Attendance> attendances = SqlDataAccess.Query<Attendance, object>(sql, sqlObject);
            return attendances;
        }
        public static void RecordAttendance(int employeeId)
        {
            var sqlObject = new { employeeId = employeeId, date = DateTime.Today };
            string sql = "select * from dbo.Attendence where EmployeeId = @employeeId and Date=@date";
            List<Attendance> TodayAttendence = SqlDataAccess.Query<Attendance, object>(sql, sqlObject);
            if(TodayAttendence.Count== 0)
            {
                var sqlObject1 = new { employeeId = employeeId,today= DateTime.Today, time = DateTime.Now };
                string sql1 = "insert into dbo.Attendence values (@employeeId,@today , @time , null)";
                SqlDataAccess.Query<object, object>(sql1, sqlObject1);
            } 
            else if (TodayAttendence[0].CheckOutTime.Year == 0001)
            {
                var sqlObject1 = new { employeeId = employeeId, today = DateTime.Today, time = DateTime.Now };
                string sql1 = @"update dbo.Attendence set CheckOutTime = @time where EmployeeId=@employeeId
                                 and Date = @today";
                SqlDataAccess.Query<object, object>(sql1, sqlObject1);
            }
            else if (TodayAttendence[0].CheckOutTime!= null)
            {
                throw new Exception("A cal made to Record Time before checking . id = " + employeeId);
            }
        }
        public static Company GetCompany(int employeeId)
        {
            string sql = "select CompanyName from dbo.Employee where EmployeeId = @employeeId;";
            var companyName = SqlDataAccess.Query<string, object>(sql, new { employeeId = employeeId });
            sql = "select * from dbo.Company where CompanyName = @companyName;";
            return SqlDataAccess.Query<Company, object>(sql, new { companyName = companyName })[0];
        }
    }
}
