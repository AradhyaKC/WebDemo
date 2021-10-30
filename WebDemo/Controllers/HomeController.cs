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

        public ActionResult ViewEmployees()
        {
            var data = DataLibrary.BusinessLogic.EmployeeProcessor.LoadEmployees();
            List<WebDemo.Models.EmployeeModel> models = new List<EmployeeModel>();
            foreach(var row in data)
            {
                models.Add(new EmployeeModel()
                {
                    EmployeeID = row.EmployeeId,
                    FirstName= row.FirstName,
                    LastName = row.LastName,
                    EmailAddress= row.EmailAddress,
                    ConfirmEmailAddress= row.EmailAddress
                });
            }
            return View(models);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignUp(EmployeeModel model)
        {
            if (ModelState.IsValid)
            {
                int forBr =DataLibrary.BusinessLogic.EmployeeProcessor.CreateEmployee(model.EmployeeID, 
                    model.FirstName,model.LastName, model.EmailAddress);
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}