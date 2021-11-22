using System;
using System.Collections.Generic;
using System.Text;

namespace DataLibrary.Models
{
    public class EmployeeModel
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string  EmailAddress { get; set; }
    }
    public class Employee
    {
        public int employeeId { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string emailAddress { get; set; }
        public string phoneNo { get; set; }
        public DateTime dateOfBirth { get; set; }
        public long salary { get; set; }
        public string password { get; set; }
        public int leavesAvailable { get; set; }
        public int credits { get; set; }
        public string companyName { get; set; }
        public string department { get; set; }

    }
        
}
