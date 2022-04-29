using System.Linq;

namespace CarsAgency.Models
{
    public class DbInitializer
    {
        public static void Initialize(CarAgencyContext context)
        {
            context.Database.EnsureCreated();
            if (context.Employees.ToList().Any())
            {
                return;
            }
            var departments = new Department[]
            {
                new Department{DepartmentId=0,Name="Adminstrator",ManagerId=0},
                new Department{DepartmentId=1,Name="Sales",ManagerId=1}
            };
            foreach (Department s in departments)
            {
                context.Departments.Add(s);
            }
            context.SaveChanges();
            var employees = new Employee[]
            {
                new Employee{EmployeeId=0,
                    Name="Khalil Abdallah",
                    Email="Khalil@gmail.com",
                    Adress="DeirBallout-Salfeet",
                    Password="123456",
                    DepartmentId=0,
                    gender=0,
                    Salary=4000},
                new Employee{EmployeeId=1,
                    Name="Ashraf Abd Alkhaliq",
                    Email="Ashraf@gmail.com",
                    Adress="Arrabeh-Jenin",
                    Password="123456",
                    DepartmentId=1,
                    gender=0,
                    Salary=3500},
                new Employee{EmployeeId=2,
                    Name="Mohammed Khanfar",
                    Email="Hamood@gmail.com",
                    Adress="Al Rameh-Jenin",
                    Password="123456",
                    DepartmentId=1,
                    gender=0,
                    Salary=3500}
            };
            foreach (Employee s in employees)
            {
                context.Employees.Add(s);
            }
            context.SaveChanges();
            var cars = new Car[]
            {
                new Car{Id=1,
                        Model="Q3",
                        StockPrice=84000},
                new Car{Id=2,
                        Model="Q4",
                        StockPrice=102500},
                new Car{Id=3,
                        Model="A4",
                        StockPrice=88200},
                new Car{Id=4,
                        Model="A3",
                        StockPrice=75000}
            };
            foreach (Car s in cars)
            {
                context.Cars.Add(s);
            }
            context.SaveChanges();
            var specs = new Specifications[] { 
                new Specifications{CarId=1,
                                   Color="Red",
                                   Transmission="Automatic",
                                   FuelType=FuelType.Diesel},
                new Specifications{CarId=2,
                                   Color="Black",
                                   Transmission="Manual",
                                   FuelType=FuelType.Gasoline},
                new Specifications{CarId=3,
                                   Color="White",
                                   Transmission="Automatic",
                                   FuelType=FuelType.Gasoline},
                new Specifications{CarId=4,
                                   Color="Blue",
                                   Transmission="Manual",
                                   FuelType=FuelType.Ethanol}
            };
            foreach (Specifications s in specs)
            {
                context.Specifications.Add(s);
            }
            context.SaveChanges();
            var parts = new PartsForSale[]
            {
                new PartsForSale{Id=1,
                                 Name="Seat Cover",
                                 Price=400,
                                 Quantity=130},
                new PartsForSale{Id=2,
                                 Name="Exhaust system",
                                 Price=590,
                                 Quantity=205},
                new PartsForSale{Id=3,
                                 Name="Fog Lights",
                                 Price=1300,
                                 Quantity=320}
            };
            foreach (PartsForSale s in parts)
            {
                context.PartsForSale.Add(s);
            }
            context.SaveChanges();
            var mods = new ModificationPart[]
            {
                new ModificationPart{Id=1,
                                     CarId=1,
                                     Name="Sun roof",
                                     Price=4000},

                new ModificationPart{Id=2,
                                     CarId=1,
                                     Name="Back Camera",
                                     Price=1230},

                new ModificationPart{Id=3,
                                     CarId=1,
                                     Name="Touch screen",
                                     Price=700},
                new ModificationPart{Id=4,
                                     CarId=2,
                                     Name="XENON HEAD LIGHTS",
                                     Price=3500},
                new ModificationPart{Id=5,
                                     CarId=2,
                                     Name="Touch screen",
                                     Price=900},
                new ModificationPart{Id=6,
                                     CarId=3,
                                     Name="Panorama",
                                     Price=2400}
            };
            foreach (ModificationPart s in mods)
            {
                context.ModificationParts.Add(s);
            }
            context.SaveChanges();
            var phones = new EmployeesPhoneNum[]
            {
                new EmployeesPhoneNum{EmployeeId=0,
                                     PhoneNumber="0599673099"},
                new EmployeesPhoneNum{EmployeeId=0,
                                     PhoneNumber="0569673099"},
                new EmployeesPhoneNum{EmployeeId=1,
                                     PhoneNumber="0569988745"},
                new EmployeesPhoneNum{EmployeeId=2,
                                     PhoneNumber="0569481425"}
            };
            foreach (EmployeesPhoneNum s in phones)
            {
                context.EmployeesPhones.Add(s);
            }
            context.SaveChanges();

            var clients = new Client[]
            {
                new Client{Id=1,
                           Name="Ahmad",
                           PhoneNumber="0569782544",
                           Adress="Nablus" },
                new Client{Id=2,
                           Name="Khaled",
                           PhoneNumber="0548796482",
                           Adress="Ramallah" },
                new Client{Id=3,
                           Name="Jafar",
                           PhoneNumber="0598785419",
                           Adress="Salfeet" }
            };
            foreach (Client s in clients)
            {
                context.Clients.Add(s);
            }
            context.SaveChanges();
            var PartSales = new SoldBy[]
            {
                new SoldBy{PartsForSaleId=1,
                           EmployeeId=1,
                            QuantitySold=40},

                new SoldBy{PartsForSaleId=1,
                           EmployeeId=1,
                            QuantitySold=10},

                new SoldBy{PartsForSaleId=2,
                           EmployeeId=2,
                            QuantitySold=20},

                new SoldBy{PartsForSaleId=3,
                           EmployeeId=2,
                            QuantitySold=70}
            };
            foreach (SoldBy s in PartSales)
            {
                context.SoldBy.Add(s);
            }
            context.SaveChanges();
            var deals = new DeltWith[]
            {
                new DeltWith{EmployeeId=1,
                             ClientId=1},

                new DeltWith{EmployeeId=1,
                             ClientId=2},

                new DeltWith{EmployeeId=2,
                             ClientId=3}
            };
            foreach (DeltWith s in deals)
            {
                context.DeltWiths.Add(s);
            }
            context.SaveChanges();
            var buys = new Bought[]
            {
                new Bought{ClientId=1,
                           CarId=1,
                           PayMethod="Cash" },

                new Bought{ClientId=2,
                           CarId=2,
                           PayMethod="Visa" }
            };
            foreach (Bought s in buys)
            {
                context.boughtBy.Add(s);
            }
            context.SaveChanges();
        }

        
    }
}
