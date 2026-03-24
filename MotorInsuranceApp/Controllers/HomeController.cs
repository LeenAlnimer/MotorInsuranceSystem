using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MotorInsuranceApp.Data;
using MotorInsuranceApp.Models;
using System.Linq;

namespace MotorInsuranceApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        // Cars

        public IActionResult Cars()
        {
            var cars = _context.Cars.ToList();
            return View(cars);
        }

        [HttpPost]
        public IActionResult AddCar(Car car)
        {
            _context.Cars.Add(car);
            _context.SaveChanges();
            return RedirectToAction("Cars");
        }

        public IActionResult DeleteCar(int id)
        {
            var car = _context.Cars.Find(id);

            if (car != null)
            {
                _context.Cars.Remove(car);
                _context.SaveChanges();
            }

            return RedirectToAction("Cars");
        }

        [HttpPost]
        public IActionResult EditCar(Car updatedCar)
        {
            var car = _context.Cars.Find(updatedCar.Id);

            if (car != null)
            {
                car.Brand = updatedCar.Brand;
                car.Model = updatedCar.Model;
                car.Year = updatedCar.Year;
                _context.SaveChanges();
            }

            return RedirectToAction("Cars");
        }

        //  Customers 

        public IActionResult Customers()
        {
            var customers = _context.Customers.ToList();
            return View(customers);
        }

        [HttpPost]
        public IActionResult AddCustomer(Customer customer)
        {
            _context.Customers.Add(customer);
            _context.SaveChanges();
            return RedirectToAction("Customers");
        }

        public IActionResult DeleteCustomer(int id)
        {
            var customer = _context.Customers.Find(id);

            if (customer != null)
            {
                _context.Customers.Remove(customer);
                _context.SaveChanges();
            }

            return RedirectToAction("Customers");
        }

        [HttpPost]
        public IActionResult EditCustomer(Customer updatedCustomer)
        {
            var customer = _context.Customers.Find(updatedCustomer.Id);

            if (customer != null)
            {
                customer.Name = updatedCustomer.Name;
                customer.Email = updatedCustomer.Email;
                customer.Phone = updatedCustomer.Phone;
                _context.SaveChanges();
            }

            return RedirectToAction("Customers");
        }

        //  Policies

        public IActionResult Policies()
        {
            var policies = _context.Policies
                .Include(p => p.Customer)
                .Include(p => p.Car)
                .ToList();

            ViewBag.Customers = _context.Customers.ToList();
            ViewBag.Cars = _context.Cars.ToList();

            return View(policies);
        }

        [HttpPost]
        public IActionResult AddPolicy(Policy policy)
        {
            _context.Policies.Add(policy);
            _context.SaveChanges();
            return RedirectToAction("Policies");
        }

        public IActionResult DeletePolicy(int id)
        {
            var policy = _context.Policies.Find(id);

            if (policy != null)
            {
                _context.Policies.Remove(policy);
                _context.SaveChanges();
            }

            return RedirectToAction("Policies");
        }

        //  Dashboard
        public IActionResult Index()
        {
            var totalCars = _context.Cars.Count();
            var totalCustomers = _context.Customers.Count();
            var totalPolicies = _context.Policies.Count();

            var recentPolicies = _context.Policies
                .Include(p => p.Customer)
                .Include(p => p.Car)
                .OrderByDescending(p => p.Id)
                .Take(5)
                .ToList();

            ViewBag.TotalCars = totalCars;
            ViewBag.TotalCustomers = totalCustomers;
            ViewBag.TotalPolicies = totalPolicies;

            return View(recentPolicies);
        }

    }
}