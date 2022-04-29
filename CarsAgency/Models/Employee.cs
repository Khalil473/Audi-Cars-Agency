using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarsAgency.Models
{
    public enum Gender
    {
        Male,Female
    }
    public class Employee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int EmployeeId { set; get; }
        public string Name { set; get; }
        public double Salary { set; get; }
        public string? Adress { set; get; }
        public Gender gender { set; get; }
        public int? DepartmentId { set; get; }
        public Department? department { set; get; }
        public string Email { set; get; }
        public string Password { set; get; }
        public List<EmployeesPhoneNum> PhoneNumbers { set; get; }
    }
}
