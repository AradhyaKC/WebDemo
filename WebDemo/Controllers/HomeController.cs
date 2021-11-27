using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDemo.Models;
using DataLibrary;
using System.Net.Mail;

namespace WebDemo.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "EMS";
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Hello WOrld.";

            return View();
        }

        public ActionResult Contact()
        {
            HttpCookie cookie = Request.Cookies.Get("UserInfo");
            if (cookie == null || cookie["employeeId"] == null) throw new Exception();
            var company = DataLibrary.BusinessLogic.EmployeeProcessor.GetCompany(Convert.ToInt32(cookie["employeeId"]));
            CompanyModel companyModel = new CompanyModel()
            {
                companyName = company.companyName,
                address = company.address,
                emailAddress = company.emailAddress,
                motto = company.motto,
                phoneNo = company.phoneNo,
                startDate = company.startDate
            };
            return View("ContactInfo",companyModel);
        }
        public ActionResult SignUp()
        {
            ViewBag.Message = "Message comes here";
            return View();
        }
        public ActionResult PersonalInfo()
        {
            HttpCookie cookie = Request.Cookies.Get("UserInfo");
            if (cookie == null || cookie["employeeId"] == null) throw new Exception();
            var employee = DataLibrary.BusinessLogic.EmployeeProcessor.GetEmployee(Convert.ToInt32(cookie["employeeId"]));
            EmployeeModel employeeModel = new EmployeeModel()
            {
                employeeId = employee.employeeId,
                firstName = employee.firstName,
                lastName = employee.lastName,
                emailAddress = employee.emailAddress,
                confirmEmailAddress = employee.emailAddress,
                phoneNo = employee.phoneNo,
                dateOfBirth = employee.dateOfBirth,
                salary = employee.salary,
                password = employee.password,
                confirmPassword = employee.password,
                leavesAvailable = employee.leavesAvailable,
                credits = employee.credits,
                department = employee.department
            };
            return View(employeeModel);
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
                    credits= employee.credits,
                    department = employee.department
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
                int forBr = DataLibrary.BusinessLogic.EmployeeProcessor.CreateCompany(company.companyName,company.motto,company.startDate,
                    company.address,company.emailAddress,company.phoneNo);
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
                bool firstManager = true;
                if(companyName == "" || companyName==null)
                {
                    var Manager =DataLibrary.BusinessLogic.EmployeeProcessor.GetEmployee
                        (Convert.ToInt32(httpCookie["employeeId"]));
                    companyName = Manager.companyName;
                    firstManager = false;
                }
                int forBr = DataLibrary.BusinessLogic.EmployeeProcessor.CreateManager(employee.firstName,employee.lastName,employee.emailAddress,
                    employee.phoneNo,employee.dateOfBirth, employee.salary, employee.password,employee.leavesAvailable,employee.credits,companyName,
                    employee.department);
                if (firstManager)
                {
                    int employeeId = DataLibrary.BusinessLogic.EmployeeProcessor.CheckLogin(employee.emailAddress, employee.password);
                    httpCookie["employeeId"] = employeeId.ToString();
                    httpCookie["companyName"] = "";
                    Response.Cookies.Add(httpCookie);
                }
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

        //public ActionResult ViewAttendance()
        //{
        //    HttpCookie cookie = Request.Cookies.Get("UserInfo");
        //    if (cookie == null) throw new Exception("Cookie not found");
        //    int employeeId = Convert.ToInt32(cookie["employeeId"]);
        //    return ViewAttendance(employeeId);
        //}
        public ActionResult ViewAttendance(int employeeId)
        {
            List<DataLibrary.Models.Attendance> attendances = 
                DataLibrary.BusinessLogic.EmployeeProcessor.ViewAttendance(employeeId);
            List<WebDemo.Models.AttendanceModel> attendanceModels = new List<AttendanceModel>();
            ViewBag.employeeId = Convert.ToString(employeeId);
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
        public ActionResult RecordAttendance()
        {
            HttpCookie cookie = Request.Cookies.Get("UserInfo");
            if (cookie == null) throw new Exception("null cookie");
            int employeeId = Convert.ToInt32(cookie["employeeId"]);
            DataLibrary.BusinessLogic.EmployeeProcessor.RecordAttendance(employeeId);
            return RedirectToAction("ViewAttendance",new { employeeId = employeeId });
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
                        employee.leavesAvailable, employee.credits, manager.companyName,employee.department);

                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress("MinorProject587@gmail.com");
                mailMessage.To.Add(new MailAddress(employee.emailAddress));
                mailMessage.IsBodyHtml = true;
                mailMessage.Body = @" <h4> Hello " + employee.firstName + " " + employee.lastName + " , Your Login credentials for you work account are :"
                                    + " Email : " + employee.emailAddress + "Password : " + employee.password + " </h4>";
                mailMessage.Subject = "Login Credentials ";

                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
                smtpClient.Credentials = new System.Net.NetworkCredential("MinorProject587@gmail.com", "19BCA41051587");
                smtpClient.EnableSsl = true;
                smtpClient.Send(mailMessage);

                return RedirectToAction("Employees","Home");
            }
            return View();
        }

        public ActionResult EditEmployee(int employeeId)
        {
            if (ModelState.IsValid)
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
                    credits = employee.credits,
                    department = employee.department
                };
                return View(Webemployee);
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditEmployee(EmployeeModel employeeModel)
        {
            if (ModelState.IsValidField("salary") && ModelState.IsValidField("leavesAvailable") && ModelState.IsValidField("credits") &&
                ModelState.IsValidField("department"))
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
                    companyName = "",
                    department = employeeModel.department
                };

                if (DataLibrary.BusinessLogic.EmployeeProcessor.EditEmployee(employee))
                    return RedirectToAction("Employees", "Home");
                else
                    throw new Exception(" cannot Edit this employee");
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult ChangePassword()
        {
            ViewBag.Error = "";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {
                HttpCookie cookie = Request.Cookies.Get("UserInfo");
                int employeeId = Convert.ToInt32(cookie["employeeId"]);
                DataLibrary.Models.Employee employee =
                    DataLibrary.BusinessLogic.EmployeeProcessor.GetEmployee(employeeId);
                if(employee.password != model.oldpassword)
                {
                    ViewBag.Error = " Incorrect Old password";
                    return View();
                }
                DataLibrary.BusinessLogic.EmployeeProcessor.ChangePassword(employeeId, model.password);
                return RedirectToAction("PersonalInfo");
            }
            return View();
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
            if (ModelState.IsValidField("projectLeaderId") && ModelState.IsValidField("description"))
            {
                if (DataLibrary.BusinessLogic.EmployeeProcessor.GetEmployee(projectModel.projectLeaderId) == null)
                {
                    ViewBag.Title = "The Id for the projectLeader passes does not exist";
                    return View(projectModel);
                }
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
            return View();
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
            if (ModelState.IsValidField("role") && ModelState.IsValidField("ShiftStartTime") && ModelState.IsValidField("shiftEndTime"))
            {
                if (DataLibrary.BusinessLogic.EmployeeProcessor.EditProjectEmployee(worksOnModel.projectId, worksOnModel.employeeId, worksOnModel.role,
                worksOnModel.shiftStartTime, worksOnModel.shiftEndTime))
                {
                    return RedirectToAction("ViewProjectTeam", new { projectId = worksOnModel.projectId });
                }
                else
                {
                    throw new Exception();
                }
            }
            return View();
        }
        public ActionResult FireEmployee(int employeeId)
        {
            DataLibrary.BusinessLogic.EmployeeProcessor.FireEmployee(employeeId);
            return RedirectToAction("Employees");
        }
        public ActionResult CreateLeave()
        {
            ViewBag.Title = "";
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateLeave(LeaveModel leaveModel)
        {
            if (ModelState.IsValid)
            {
                HttpCookie cookie = Request.Cookies.Get("UserInfo");
                if (cookie == null || cookie["employeeId"] == null) throw new Exception();
                leaveModel.employeeId = Convert.ToInt32(cookie["employeeId"]);
                int noOfDays = (int)(leaveModel.endDate - leaveModel.startDate).TotalSeconds / 86400 + 1;
                if (DataLibrary.BusinessLogic.EmployeeProcessor.GetEmployee(leaveModel.employeeId).leavesAvailable < noOfDays)
                {
                    ViewBag.Title = "Not enough leaves available.";
                    return View();
                }
                if ((leaveModel.endDate.Ticks - leaveModel.startDate.Ticks) < 0)
                {
                    ViewBag.Title = "End date should be later than start date.";
                    return View();
                }
                if ((leaveModel.startDate - DateTime.Today).TotalSeconds / 86400.0f < 1.0f ||
                    (leaveModel.endDate - DateTime.Today).TotalSeconds / 86400.0f < 1.0f)
                {
                    ViewBag.Title = "Start Date or End Date cannot be today.";
                    return View();
                }
                DataLibrary.BusinessLogic.EmployeeProcessor.CreateLeave(leaveModel.employeeId, leaveModel.startDate, leaveModel.endDate, leaveModel.reason);
                return RedirectToAction("ViewAttendance", new { employeeId = leaveModel.employeeId });  
            }
            return View();
        }
        public ActionResult ViewLeave(int employeeId)
        {
            var data = DataLibrary.BusinessLogic.EmployeeProcessor.ViewLeave(employeeId);
            List<LeaveModel> list = new List<LeaveModel>();
            foreach(var item in data)
            {
                list.Add(new LeaveModel()
                {
                    employeeId = item.employeeId,
                    startDate = item.startDate,
                    endDate = item.endDate,
                    reason = item.reason
                });
            }
            return View(list);
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