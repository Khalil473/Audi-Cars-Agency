using Microsoft.AspNetCore.Mvc;
using CarsAgency.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace CarsAgency.Controllers
{
    public class LoginController : Controller
    {
        private readonly CarAgencyContext carAgencyContext;
        public LoginController(CarAgencyContext context)
        {
            carAgencyContext = context;
        }
        public IActionResult Login()
        {
            return View("Index");
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return View("Index");
        }
        public IActionResult MainScreen()
        {
            if (isLoggedIn())
            {
                int uId = (int)HttpContext.Session.GetInt32("UserId");
                Employee user = carAgencyContext.Employees.Where(e => e.EmployeeId == uId).ToList().First();
                if (user == null) return Logout();
                ViewBag.User = user;
                return View("MainScreen");
            }
            return Logout();
        }
        [HttpPost]
        public IActionResult MainScreen(Employee emp)
        {
            if (isLoggedIn())
            {
                return View();
            }
            var employees = carAgencyContext.Employees.Where(e => e.Email.Equals(emp.Email)).ToList();
            foreach (Employee e in employees)
            {
                if (e.Password.Equals(emp.Password))
                {
                    HttpContext.Session.SetInt32("UserId", e.EmployeeId);
                    ViewBag.User = e;
                    return View("MainScreen");
                }
            }
            ViewBag.ErrorMsg = "Invalid Email/Password please try again";
            return Logout();
        }
        private bool isLoggedIn()
        {
            return HttpContext.Session.GetInt32("UserId") != null;
        }

        public IActionResult Cars()
        {
            if (isLoggedIn())
            {
                List<Car> cars = carAgencyContext.Cars.ToList();
                ViewBag.CarsInfo = cars;
                return View();
            }
             return Logout();
        }
        public IActionResult CarDetails(int Id)
        {
            if (!isLoggedIn()) return Logout();

            var carDetail = carAgencyContext.Cars.Where(e => e.Id == Id).Join(
                carAgencyContext.Specifications,
                car => car.Id,
                Specs => Specs.CarId,
                (car, specs) => new
                {
                    Id = car.Id,
                    Model = car.Model,
                    Price = car.StockPrice,
                    Color = specs.Color,
                    Trans = specs.Transmission,
                    FuelType = specs.FuelType
                }).ToList().First();
            List<ModificationPart> parts = carAgencyContext.ModificationParts.Where(e => e.CarId == Id).ToList();
            ViewBag.ModParts = parts;
            ViewBag.Price = carDetail.Price;
            double totalPrice = carDetail.Price;
            foreach (ModificationPart m in parts)
            {
                totalPrice += m.Price;
            }
            ViewBag.TotalPrice = totalPrice;
            ViewBag.Color = carDetail.Color;
            ViewBag.PhotoPath = Url.Content("~/lib/" + carDetail.Id.ToString() + ".jpg");
            ViewBag.Trans = carDetail.Trans;
            ViewBag.FuelType = ((FuelType)carDetail.FuelType).ToString();
            ViewBag.Model = carDetail.Model;
            return View();
        }
        public IActionResult AvalibaleCars()
        {
            if (isLoggedIn())
            {

                List<Bought> bought = carAgencyContext.boughtBy.ToList();
                List<Car> cars = new List<Car>();
                List<Car> AllCars = carAgencyContext.Cars.ToList();
                foreach (Car car in AllCars)
                {
                    bool isBought = false;
                    foreach (Bought b in bought)
                    {

                        if (car.Id == b.CarId)
                        {
                            isBought = true;
                            break;
                        }

                    }
                    if (!isBought) cars.Add(car);
                }
                ViewBag.CarsInfo = cars;
                return View("Cars");
            }
            return Logout();
        }
        public IActionResult SoldCars()
        {
            if (isLoggedIn())
            {
                List<Bought> bought = carAgencyContext.boughtBy.ToList();
                List<Car> cars = new List<Car>();
                List<Car> AllCars = carAgencyContext.Cars.ToList();
                foreach (Car car in AllCars)
                {
                    bool isBought = false;
                    foreach (Bought b in bought)
                    {

                        if (car.Id == b.CarId)
                        {
                            isBought = true;
                            break;
                        }

                    }
                    if (isBought) cars.Add(car);
                }
                ViewBag.CarsInfo = cars;
                return View("Cars");
            }
            return Logout();
        }
        public IActionResult AddCar()
        {
            if (isLoggedIn()) return View();
            return Logout();
        }
        [HttpPost]
        public IActionResult AddSpecs(Car c)
        {
            if (!isLoggedIn()) return Logout();
            int lastcarId = carAgencyContext.Cars.ToList().Last().Id;
            HttpContext.Session.SetInt32("CarId", lastcarId + 1);
            Car newCar = new Car
            {
                Id = lastcarId + 1,
                Model = c.Model,
                StockPrice = c.StockPrice
            };
            carAgencyContext.Cars.Add(newCar);
            carAgencyContext.SaveChanges();
            return View();
        }
        [HttpPost]
        public IActionResult AddMods(Specifications s)
        {
            if (!isLoggedIn()) return Logout();
            int carId = (int)HttpContext.Session.GetInt32("CarId");
            Specifications newSpecs = new Specifications
            {
                Color = s.Color,
                CarId = carId,
                FuelType = s.FuelType,
                Transmission = s.Transmission
            };
            carAgencyContext.Specifications.Add(newSpecs);
            carAgencyContext.SaveChanges();
            return View("AddMods");
        }
        [HttpPost]
        public IActionResult AddOneMod(ModificationPart p)
        {
            if (!isLoggedIn()) return Logout();
            int carId = (int)HttpContext.Session.GetInt32("CarId");
            int lastModId = carAgencyContext.ModificationParts.ToList().Last().Id;
            ModificationPart newMod = new ModificationPart
            {
                Id = lastModId + 1,
                Name = p.Name,
                Price = p.Price,
                CarId = carId
            };
            carAgencyContext.ModificationParts.Add(newMod);
            carAgencyContext.SaveChanges();
            return View("AddMods");
        }
        public IActionResult SellCar()
        {

            if (isLoggedIn())
            {
                List<Bought> bought = carAgencyContext.boughtBy.ToList();
                List<Car> cars = new List<Car>();
                List<Car> AllCars = carAgencyContext.Cars.ToList();
                foreach (Car car in AllCars)
                {
                    bool isBought = false;
                    foreach (Bought b in bought)
                    {

                        if (car.Id == b.CarId)
                        {
                            isBought = true;
                            break;
                        }

                    }
                    if (!isBought) cars.Add(car);
                }
                ViewBag.CarsInfo = cars;
                return View();
            }
            return Logout();
        }
        public IActionResult Sell(int Id)
        {
            if (!isLoggedIn()) return Logout();
            HttpContext.Session.SetInt32("CarId", Id);
            ViewBag.Clients = carAgencyContext.Clients.ToList();
            ViewBag.CarId = Id;
            return View("Sell");
        }
        [HttpPost]
        public IActionResult FinalSale(Bought b)
        {
            if (!isLoggedIn()) return Logout();
            b.CarId = (int)HttpContext.Session.GetInt32("CarId");
            carAgencyContext.boughtBy.Add(b);
            carAgencyContext.SaveChanges();
            return MainScreen();
        }
        public IActionResult ShowParts()
        {
            if (!isLoggedIn()) return Logout();
            List<PartsForSale> parts = carAgencyContext.PartsForSale.ToList();
            return View("ShowParts",parts);
        }

        public IActionResult PartQuantity(int Id)
        {
            if (!isLoggedIn()) return Logout();

            HttpContext.Session.SetInt32("PartId", Id);

            return View("PartQuantity");
        }
        [HttpPost]
        public IActionResult AddToPart(Quantity q)
        {
            if (!isLoggedIn()) return Logout();
            int Id = (int)HttpContext.Session.GetInt32("PartId");
            PartsForSale partTobeUpdated = carAgencyContext.PartsForSale.Where(e => e.Id == Id).ToList().First();
            partTobeUpdated.Quantity = partTobeUpdated.Quantity + q.quantity;
            carAgencyContext.PartsForSale.Update(partTobeUpdated);
            carAgencyContext.SaveChanges();
            return ShowParts();
        }
        public IActionResult AddPartItem()
        {
            if (!isLoggedIn()) return Logout();
            return View();
        }
        [HttpPost]
        public IActionResult AddNewPart(PartsForSale p)
        {
            if (!isLoggedIn()) return Logout();
            int PartId = carAgencyContext.PartsForSale.ToList().Last().Id;
            p.Id = PartId + 1;
            carAgencyContext.PartsForSale.Add(p);
            carAgencyContext.SaveChanges();
            return ShowParts();
        }
        [HttpPost]
        public IActionResult CarSearch(Car c)
        {
            if (!isLoggedIn()) return Logout();
            List<Car> cars = new List<Car>();
            List<Car> AllCars = carAgencyContext.Cars.ToList();
            foreach (Car ca in AllCars)
            {
                if (ca.Model.Contains(c.Model))
                {
                    cars.Add(ca);
                }
            }
            ViewBag.CarsInfo = cars;
            return View("Cars");
        }
        public IActionResult ShowEmployees()
        {
            if (!isLoggedIn()) return Logout();
            if (HttpContext.Session.GetInt32("UserId") != 0) return Logout();
            List<Employee> employees = carAgencyContext.Employees.ToList();
            return View("ShowEmployees",employees);
        }
        public IActionResult EditEmployee(int Id)
        {
            if (!isLoggedIn()) return Logout();
            if (HttpContext.Session.GetInt32("UserId") != 0) return Logout();
            HttpContext.Session.SetInt32("EmployeeId", Id);
            Employee e = carAgencyContext.Employees.Where(i => i.EmployeeId == Id).ToList().First();
            return View(e);
        }
        [HttpPost]
        public IActionResult EditEmployee1(Employee employee)
        {
            if (!isLoggedIn()) return Logout();
            int Id = (int)HttpContext.Session.GetInt32("EmployeeId");
            if (HttpContext.Session.GetInt32("UserId") != 0) return Logout();
            Employee employeeToBeUpdated = carAgencyContext.Employees.Where(i => i.EmployeeId == Id).ToList().First();
            employeeToBeUpdated.EmployeeId = Id;
            employeeToBeUpdated.Email = employee.Email;
            employeeToBeUpdated.Adress = employee.Adress;
            employeeToBeUpdated.Salary = employee.Salary;
            employeeToBeUpdated.Name = employee.Name;
            carAgencyContext.Employees.Update(employeeToBeUpdated);
            carAgencyContext.SaveChanges();
            return ShowEmployees();
        }
        public IActionResult AddEmployee()
        {
            if (!isLoggedIn()) return Logout();
            if (HttpContext.Session.GetInt32("UserId") != 0) return Logout();
            return View();
        }
        [HttpPost]
        public IActionResult AddEmployee1(Employee employee)
        {
            if (!isLoggedIn()) return Logout();
            if (HttpContext.Session.GetInt32("UserId") != 0) return Logout();
            int LastEmployeeId = carAgencyContext.Employees.ToList().Last().EmployeeId;
            Employee employeeToBeAdded = new Employee
            {
                EmployeeId = LastEmployeeId + 1,
                Name = employee.Name,
                Email = employee.Email,
                Password = employee.Password,
                Salary = employee.Salary,
                Adress = employee.Adress
            };

            carAgencyContext.Employees.Add(employeeToBeAdded);
            carAgencyContext.SaveChanges();
            return ShowEmployees();
        }
        public IActionResult AddClient()
        {
            if (!isLoggedIn()) return Logout();
            if (HttpContext.Session.GetInt32("UserId") != 0) return Logout();
            return View("AddClient");
        }
        [HttpPost]
        public IActionResult AddClient1(Client client)
        {
            if (!isLoggedIn()) return Logout();
            if (HttpContext.Session.GetInt32("UserId") != 0) return Logout();
            int LastClientId = carAgencyContext.Clients.ToList().Last().Id;
            Client ClientToAdd = new Client()
            {
                Id = LastClientId + 1,
                Name = client.Name,
                Adress = client.Adress,
                PhoneNumber = client.PhoneNumber
            };
            carAgencyContext.Clients.Add(ClientToAdd);
            carAgencyContext.SaveChanges();
            return ShowClients();
        }
        public IActionResult ShowClients()
        {
            if (!isLoggedIn()) return Logout();
            if (HttpContext.Session.GetInt32("UserId") != 0) return Logout();
            List<Client> clients = carAgencyContext.Clients.ToList();
            return View("ShowClients",clients);
        }
        public IActionResult EditClient(int Id)
        {
            if (!isLoggedIn()) return Logout();
            if (HttpContext.Session.GetInt32("UserId") != 0) return Logout();
            HttpContext.Session.SetInt32("ClientId", Id);
            Client ToBeEdited = carAgencyContext.Clients.Where(e => e.Id == Id).ToList().First();
            return View("EditClient", ToBeEdited);
        }
        public IActionResult EditClient1(Client client)
        {
            if (!isLoggedIn()) return Logout();
            int Id = (int)HttpContext.Session.GetInt32("ClientId");
            if (HttpContext.Session.GetInt32("UserId") != 0) return Logout();
            Client tt = new Client()
            {
                Id = Id,
                Name = client.Name,
                Adress = client.Adress,
                PhoneNumber = client.PhoneNumber
            };
            carAgencyContext.Clients.Update(tt);
            carAgencyContext.SaveChanges();
            return ShowClients();
        }
        public IActionResult About()
        {
            return View();
        }
        public IActionResult ContactUs()
        {
            return View();
        }
    }

}
