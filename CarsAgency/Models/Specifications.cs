namespace CarsAgency.Models
{
    public enum FuelType
    {
        Gasoline, Diesel, Ethanol
    }
    public class Specifications
    {
        public int Id { set; get; }
        public int CarId { get; set; }
        public Car Car { set; get; }
        public string Color { set; get; }
        public string Transmission { set; get; }
        public FuelType FuelType { set; get; }

    }
}
