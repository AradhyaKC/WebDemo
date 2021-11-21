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
                employeeModels.Add(new EmployeeModel
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
            var data = DataLibrary.BusinessLogic.EmployeeProcessor.ViewProjects();
            List<ProjectModel> list = new List<ProjectModel>();
            foreach(var project in data)
            {
                list.Add(new ProjectModel()
                {
                    projectName = project.projectName,
                    projectId = project.projectId,
                    projectLeaderId = project.projectLeaderId,
                    projectLeaderName = DataLibrary.BusinessLogic.EmployeeProcessor.GetName(project.projectLeaderId, true),
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
                DataLibrary.BusinessLogic.EmployeeProcessor.CreateProject(model.projectName, model.projectLeaderId, model.description,
                    DataLibrary.BusinessLogic.EmployeeProcessor.GetCompanyName(Convert.ToInt32(cookie["employeeId"]), true));
                return RedirectToAction("Index");
            }
            return View();
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
                int noOfRecordsInserted = DataLibrary.BusinessLogic.EmployeeProcessor.CreateEmployee(
                    employee.firstName, employee.lastName, employee.emailAddress,
                        employee.phoneNo, employee.dateOfBirth, employee.salary, employee.password,
                        employee.leavesAvailable, employee.credits, "SomeCompany10");
                return RedirectToAction("Employees","Home");
            }
            return View();
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