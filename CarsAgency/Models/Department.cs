using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarsAgency.Models
{
    public class Department
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int DepartmentId { get; set; }
        public string Name { get; set; }
        [ForeignKey("Employee")]
        public int ManagerId { set; get; }
        public Employee employee { get; set; }
    }
}
