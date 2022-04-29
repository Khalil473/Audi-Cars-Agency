namespace CarsAgency.Models
{
    public class DeltWith
    {
        public int Id { set; get; }
        public int EmployeeId { set; get; }
        public Employee Employee { set; get; }
        public int ClientId { set; get; }
        public Client Client { set; get; }

    }
}
