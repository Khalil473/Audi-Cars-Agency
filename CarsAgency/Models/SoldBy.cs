namespace CarsAgency.Models
{
    public class SoldBy
    {
        public int Id { get; set; }
        public int PartsForSaleId { get; set; }
        public PartsForSale PartsForSale { get; set; }
        public int EmployeeId { set; get; }
        public Employee Employee { get; set; }
        public int QuantitySold { set; get; }

    }
}
