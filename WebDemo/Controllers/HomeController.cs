using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDemo.Models;
using DataLibrary;

namespace WebDemo.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            HttpCookie cookie = Request.Cookies.Get("UserInfo");
            if(cookie != null && cookie["employeeId"] !="")
            {
                ViewBag.Title = "Logged in";
            }
            else
            {
                ViewBag.Title = "Home Page";
            }
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Hello WOrld.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Changed";

            return View();
        }
        public ActionResult SignUp()
        {
            ViewBag.Message = "Message comes here";
            return View();
        }

        //public ActionResult ViewEmployees()
        //{
        //    var data = DataLibrary.BusinessLogic.EmployeeProcessor.LoadEmployees();
        //    List<WebDemo.Models.EmployeeModel> models = new List<EmployeeModel>();
        //    foreach(var row in data)
        //    {
        //        models.Add(new EmployeeModel()
        //        {
        //            EmployeeID = row.EmployeeId,
        //            FirstName= row.FirstName,
        //            LastName = row.LastName,
        //            EmailAddress= row.EmailAddress,
        //            ConfirmEmailAddress= row.EmailAddress
        //        });
        //    }
        //    return View(models);
        //}

        public ActionResult Employees()
        {
            var data = DataLibrary.BusinessLogic.EmployeeProcessor.LoadEmployees();
            List<WebDemo.Models.EmployeeModel> employeeModels = new List<EmployeeModel>();
            foreach(var employee in data)
            {
                employeeModels.Add(new WebDemo.Models.EmployeeModel
                {
                    employeeId = employee.employeeId,
                    firstName = employee.firstName,
                    lastName = employee.lastName,
                    emailAddress= employee.emailAddress,
                    confirmEmailAddress = employee.emailAddress,
                    phoneNo= employee.phoneNo,
                    dateOfBirth= employee.dateOfBirth,
                    salary= employee.salary,
                    password= employee.password,
                    confirmPassword=employee.password,
                    leavesAvailable= employee.leavesAvailable,
                    credits= employee.credits
                });
            }
            return View(employeeModels);
        }


        public ActionResult CreateCompany()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCompany(CompanyModel company)
        {
            if (ModelState.IsValid)
            {
                int forBr = DataLibrary.BusinessLogic.EmployeeProcessor.CreateCompany(company.companyName,company.motto,company.startDate);
                HttpCookie cookie = new HttpCookie("UserInfo");
                cookie["companyName"] = company.companyName;
                Response.Cookies.Add(cookie);
                return RedirectToAction("CreateManager");
            }
            return View();
        }
        
        public ActionResult CreateManager()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateManager(EmployeeModel employee)
        {
            if (ModelState.IsValid)
            {
                var httpCookie= Request.Cookies.Get("UserInfo");
                if (httpCookie == null) throw new Exception();
                var companyName = httpCookie["companyName"];
                if(companyName == "" || companyName==null)
                {
                    var Manager =DataLibrary.BusinessLogic.EmployeeProcessor.GetEmployee
                        (Convert.ToInt32(httpCookie["employeeId"]));
                    companyName = Manager.companyName;
                }
                
                int forBr = DataLibrary.BusinessLogic.EmployeeProcessor.CreateManager(employee.firstName,employee.lastName,employee.emailAddress,
                    employee.phoneNo,employee.dateOfBirth, employee.salary, employee.password,employee.leavesAvailable,employee.credits,companyName);
                return RedirectToAction("Index");
            }
            return View();
        }

        
        public ActionResult LogIn()
        {
            ViewBag.Title = "";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogIn(LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                int employeeId = DataLibrary.BusinessLogic.EmployeeProcessor.CheckLogin(loginModel.emailAddress, loginModel.password);
                if(employeeId == -1)
                {
                    ViewBag.Title = "An account with this email address is not registered";
                }
                else if(employeeId == -2)
                {
                    ViewBag.Title = "Incorrect password";
                }
                else if(employeeId >= 0)
                {
                    HttpCookie cookie = Request.Cookies.Get("UserInfo");
                    if (cookie == null)
                    {
                        cookie = new HttpCookie("UserInfo");
                    }
                    cookie["employeeId"] = employeeId.ToString();
                    Response.Cookies.Add(cookie);

                    return RedirectToAction("Index");
                }
            }
            return View();
        }

        public ActionResult LogOut()
        {
            HttpCookie cookie = Request.Cookies.Get("UserInfo");
            if((cookie == null))
            {
                throw new Exception();
            }
            cookie["employeeId"] = "";
            cookie["companyName"] = "";
            Response.Cookies.Add(cookie);
            return RedirectToAction("Index");
        }
        public ActionResult ViewProjects()
        {
            HttpCookie cookie = Request.Cookies.Get("UserInfo");
            if (cookie == null || cookie["employeeId"] == null) throw new Exception();
            var data = DataLibrary.BusinessLogic.EmployeeProcessor.ViewProjects(Convert.ToInt32(cookie["employeeId"]));
            List<ProjectModel> list = new List<ProjectModel>();
            foreach(var project in data)
            {
                list.Add(new ProjectModel()
                {
                    projectName = project.projectName,
                    projectId = project.projectId,
                    projectLeaderId = project.projectLeaderId,
                    projectLeaderName = DataLibrary.BusinessLogic.EmployeeProcessor.GetEmployee(project.projectLeaderId).firstName,
                    companyName = project.companyName,
                    description = project.description
                });
            }
            return View(list);
        }
        public ActionResult CreateProject()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateProject(ProjectModel model)
        {
            if (ModelState.IsValid)
            {
                HttpCookie cookie = Request.Cookies.Get("UserInfo");
                if (cookie == null || cookie["employeeId"] == "") throw new Exception();
                if (DataLibrary.BusinessLogic.EmployeeProcessor.GetEmployee(model.projectLeaderId) == null)
                    throw new Exception("Employee of this id does not exist");
                string companyName = DataLibrary.BusinessLogic.EmployeeProcessor.GetEmployee(Convert.ToInt32(cookie["employeeId"])).companyName;
                DataLibrary.BusinessLogic.EmployeeProcessor.CreateProject(model.projectName, model.projectLeaderId, model.description,
                    companyName);
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult PromoteEmployee(int employeeId)
        {
            if (!DataLibrary.BusinessLogic.EmployeeProcessor.EmployeeExists(employeeId)) throw new Exception("unknown Employee");

            DataLibrary.BusinessLogic.EmployeeProcessor.PromoteEmployeeToManager(employeeId);
            return RedirectToAction("Employees");
            
        }
        public ActionResult ViewProjectTeam(int projectId)
        {
            var data = DataLibrary.BusinessLogic.EmployeeProcessor.ViewProjectTeam(projectId);
            List<WorksOnModel> list = new List<WorksOnModel>();
            foreach (var employee in data)
            {
                list.Add(new WorksOnModel()
                {
                    employeeId = employee.employeeId,
                    projectId = employee.projectId,
                    firstName = DataLibrary.BusinessLogic.EmployeeProcessor.GetEmployee(employee.employeeId).firstName,
                    role = employee.role,
                    shiftStartTime = employee.shiftStartTime,
                    shiftEndTime = employee.shiftEndTime
                });
            }
            return View(list);
        }
        public ActionResult AddProjectEmployee()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddProjectEmployee(WorksOnModel model)
        {
            if (ModelState.IsValid)
            {
                if(DataLibrary.BusinessLogic.EmployeeProcessor.GetEmployee(model.employeeId) == null)
                {
                    ViewBag.Title = "The employeeId passed does not exist";
                    return View();
                }
                else if(DataLibrary.BusinessLogic.EmployeeProcessor.GetProject(model.projectId) == null)
                {
                    ViewBag.Title = "The projectId passed does not exist";
                    return View();
                }
                int noOfRecordsInserted = DataLibrary.BusinessLogic.EmployeeProcessor.AddProjectEmployee(model.projectId, model.employeeId, model.role,
                    model.shiftStartTime, model.shiftEndTime);
            }
            return RedirectToAction("ViewProjects");
        }

        public ActionResult ViewAttendance(int employeeId)
        {
            List<DataLibrary.Models.Attendance> attendances = 
                DataLibrary.BusinessLogic.EmployeeProcessor.ViewAttendance(employeeId);
            List<WebDemo.Models.AttendanceModel> attendanceModels = new List<AttendanceModel>();
            foreach(var attendance in attendances)
            {
                attendanceModels.Add(new AttendanceModel
                {
                    date = attendance.Date,
                    CheckInTime=attendance.CheckInTime,
                    CheckOutTime=attendance.CheckOutTime
                }) ;
            }
            return View(attendanceModels);
        }

        public ActionResult CreateEmployee()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateEmployee(EmployeeModel employee)
        {
            if (ModelState.IsValid)
            {
                HttpCookie cookie = Request.Cookies.Get("UserInfo");
                if (cookie == null) throw new Exception("cookie not found ");
                var manager = DataLibrary.BusinessLogic.EmployeeProcessor.
                    GetEmployee(Convert.ToInt32(cookie["employeeId"]));
                int noOfRecordsInserted = DataLibrary.BusinessLogic.EmployeeProcessor.CreateEmployee(
                    employee.firstName, employee.lastName, employee.emailAddress,
                        employee.phoneNo, employee.dateOfBirth, employee.salary, employee.password,
                        employee.leavesAvailable, employee.credits, manager.companyName);
                return RedirectToAction("Employees","Home");
            }
            return View();
        }

        public ActionResult EditEmployee(int employeeId)
        {
            var employee = DataLibrary.BusinessLogic.EmployeeProcessor.GetEmployee(employeeId);
            var Webemployee = new WebDemo.Models.EmployeeModel
            {
                employeeId = employee.employeeId,
                firstName = employee.firstName,
                lastName = employee.lastName,
                emailAddress = employee.emailAddress,
                phoneNo = employee.phoneNo,
                dateOfBirth = employee.dateOfBirth,
                salary = employee.salary,
                password = employee.password,
                leavesAvailable = employee.leavesAvailable,
                credits = employee.credits
            };
            return View(Webemployee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditEmployee(EmployeeModel employeeModel)
        {
            HttpCookie cookie = Request.Cookies.Get("UserInfo");
            if (cookie == null) throw new Exception("cookie not found ");

            DataLibrary.Models.Employee employee = new DataLibrary.Models.Employee
            {
                employeeId = employeeModel.employeeId,
                firstName = employeeModel.firstName,
                lastName = employeeModel.lastName,
                emailAddress = employeeModel.emailAddress,
                phoneNo = employeeModel.phoneNo,
                dateOfBirth = employeeModel.dateOfBirth,
                salary = employeeModel.salary,
                password = employeeModel.password,
                leavesAvailable = employeeModel.leavesAvailable,
                credits = employeeModel.credits,
                companyName = ""
            };
           
             if (DataLibrary.BusinessLogic.EmployeeProcessor.EditEmployee(employee))
                return RedirectToAction("Employees", "Home");
             else
                throw new Exception(" cannot Edit this employee");

            return RedirectToAction("Index");
        }
        public ActionResult EditProject(int projectId)
        {
            var project = DataLibrary.BusinessLogic.EmployeeProcessor.GetProject(projectId);
            var projectModel = new ProjectModel()
            {
                projectId = project.projectId,
                companyName = project.companyName,
                description = project.description,
                projectLeaderId = project.projectLeaderId,
                projectLeaderName = DataLibrary.BusinessLogic.EmployeeProcessor.GetEmployee(project.projectLeaderId).firstName,
                projectName = project.projectName
            };
            return View(projectModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProject(ProjectModel projectModel)
        {
            if (DataLibrary.BusinessLogic.EmployeeProcessor.EditProject(projectModel.projectId, projectModel.projectName, projectModel.projectLeaderId,
                projectModel.description, projectModel.companyName))
            {
                return RedirectToAction("ViewProjects");
            }
            else
            {
                throw new Exception();
            }
        }
        public ActionResult EditProjectEmployee(int employeeId,int projectId)
        {
            var projectEmployee = DataLibrary.BusinessLogic.EmployeeProcessor.GetProjectEmployee(employeeId,projectId);
            var worksOnModel = new WorksOnModel()
            {
                employeeId = projectEmployee.employeeId,
                projectId = projectEmployee.projectId,
                firstName = DataLibrary.BusinessLogic.EmployeeProcessor.GetEmployee(projectEmployee.employeeId).firstName,
                role = projectEmployee.role,
                shiftEndTime = projectEmployee.shiftEndTime,
                shiftStartTime = projectEmployee.shiftStartTime
            };
            return View(worksOnModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProjectEmployee(WorksOnModel worksOnModel)
        {
            if (DataLibrary.BusinessLogic.EmployeeProcessor.EditProjectEmployee(worksOnModel.projectId, worksOnModel.employeeId, worksOnModel.role,
                worksOnModel.shiftStartTime, worksOnModel.shiftEndTime))
            {
                return RedirectToAction("ViewProjectTeam",new { projectId = worksOnModel.projectId });
            }
            else
            {
                throw new Exception();
            }
        }
        public ActionResult FireEmployee(int employeeId)
        {
            DataLibrary.BusinessLogic.EmployeeProcessor.FireEmployee(employeeId);
            return RedirectToAction("Employees");
        }
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult SignUp(EmployeeModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        int forBr =DataLibrary.BusinessLogic.EmployeeProcessor.CreateEmployee(model.EmployeeID, 
        //            model.FirstName,model.LastNameod, mel.EmailAddress);
        //        return RedirectToAct"Indeion(x");
        //    }
        //    return View();
        //}
    }
}