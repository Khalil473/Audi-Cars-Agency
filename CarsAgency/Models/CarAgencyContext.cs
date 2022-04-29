using Microsoft.EntityFrameworkCore;

namespace CarsAgency.Models
{
    public class CarAgencyContext:DbContext
    {
        public CarAgencyContext(DbContextOptions<CarAgencyContext> options) : base(options) { }
        public DbSet<Car> Cars { set; get; }
        public DbSet<Bought> boughtBy { set; get; }
        public DbSet<Client> Clients { set; get; }
        public DbSet<DeltWith> DeltWiths { set; get; }
        public DbSet<Department> Departments { set; get; }
        public DbSet<Employee> Employees { set; get; }
        public DbSet<EmployeesPhoneNum> EmployeesPhones { set; get; }
        public DbSet<ModificationPart> ModificationParts { set; get; }
        public DbSet<PartsForSale> PartsForSale { set; get; }
        public DbSet<SoldBy> SoldBy { set; get; }
        public DbSet<Specifications> Specifications { set; get; }

    }

}
