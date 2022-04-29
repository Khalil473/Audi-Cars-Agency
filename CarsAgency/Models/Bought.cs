namespace CarsAgency.Models
{
    public class Bought
    {
        public int Id { set; get; }
        public int ClientId { set; get; }
        public Client Client { set; get; }
        public int CarId { set; get; }
        public Car Car { set; get; }
        public string PayMethod { set; get; }

    }
}
