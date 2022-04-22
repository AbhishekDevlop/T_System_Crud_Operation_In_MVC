using Microsoft.AspNetCore.Mvc;
using CRUD_Operation_In_MVC.Models;
using Microsoft.AspNetCore.Http;
using System;

namespace CRUD_Operation_In_MVC.Controllers
{
    public class EmployeesController : Controller
    {
        EmployeesDAL employeesDAL = new EmployeesDAL();
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult List()
        {
            ViewBag.EmployeeList = employeesDAL.GetAllEmployees();
            return View();
        }

        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Create(IFormCollection form)
        {
            Employees emp = new Employees();
            emp.EmpName = form["name"];
            emp.EmpSalary = Convert.ToDecimal(form["salary"]);
            int res = employeesDAL.Save(emp);
            if (res == 1)
                return RedirectToAction("List");

            return View();
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Employees employees = employeesDAL.GetEmployeeById(id);
            ViewBag.Name = employees.EmpName;
            ViewBag.Salary = employees.EmpSalary;
            ViewBag.Id = employees.EmpId;
            return View();
        }

        [HttpPost]
        public IActionResult Edit(IFormCollection form)
        {
            Employees employees = new Employees();
            employees.EmpName = form["name"];
            employees.EmpSalary = Convert.ToDecimal(form["salary"]);
            employees.EmpId = Convert.ToInt32(form["id"]);
            int res = employeesDAL.Update(employees);
            if (res == 1)
                return RedirectToAction("List");

            return View();
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            Employees employees = employeesDAL.GetEmployeeById(id);
            ViewBag.Name = employees.EmpName;
            ViewBag.salary = employees.EmpSalary;
            ViewBag.Id = employees.EmpId;
            return View();
        }
        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeleteConfirm(int id)
        {
            int res = employeesDAL.Delete(id);
            if (res == 1)
                return RedirectToAction("List");

            return View();
        }
    }
}
