using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace CarsAgency.Models
{
    
    public class EmployeesPhoneNum
    {
        public int Id { set; get; }
        public int EmployeeId { get; set;}
        public string PhoneNumber { get; set;}
        public Employee employee { set; get; }

    }
}
