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
        //public ActionResult CreateEmployee()
        //{
            
        //}
        //[HttpPost]
        //public ActionResult CreateEmployee()
        //{

        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult SignUp(EmployeeModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        int forBr =DataLibrary.BusinessLogic.EmployeeProcessor.CreateEmployee(model.EmployeeID, 
        //            model.FirstName,model.LastName, model.EmailAddress);
        //        return RedirectToAction("Index");
        //    }
        //    return View();
        //}
    }
}