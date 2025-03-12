using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebApplicationWithSP.Data;
using WebApplicationWithSP.Models;

namespace WebApplicationWithSP.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly EmployeeRepository _repository;

        public EmployeeController(EmployeeRepository repository)
        {
            _repository = repository;
        }

        public IActionResult Index()
        {
            List<Employee> employees = _repository.GetEmployees();
            return View(employees);
        }

        public IActionResult Details(int id)
        {
            var employee = _repository.GetEmployeeById(id);
            if (employee == null)
                return NotFound();
            return View(employee);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                _repository.AddEmployee(employee);
                return RedirectToAction("Index");
            }
            return View(employee);
        }

        public IActionResult Edit(int id)
        {
            var employee = _repository.GetEmployeeById(id);
            if (employee == null)
                return NotFound();
            return View(employee);
        }

        [HttpPost]
        public IActionResult Edit(Employee employee)
        {
            if (ModelState.IsValid)
            {
                _repository.UpdateEmployee(employee);
                return RedirectToAction("Index");
            }
            return View(employee);
        }

        public IActionResult Delete(int id)
        {
            var employee = _repository.GetEmployeeById(id);
            if (employee == null)
                return NotFound();
            return View(employee);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _repository.DeleteEmployee(id);
            return RedirectToAction("Index");
        }
    }


}
