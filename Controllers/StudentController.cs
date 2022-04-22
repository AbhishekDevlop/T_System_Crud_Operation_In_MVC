using Microsoft.AspNetCore.Mvc;
using CRUD_Operation_In_MVC.Models;
using Microsoft.AspNetCore.Http;
using System;

namespace CRUD_Operation_In_MVC.Controllers
{
    public class StudentController : Controller
    {
        StudentDAL studentDAL = new StudentDAL();
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult List()
        {
            ViewBag.StudentList = studentDAL.GetAllStudent();
            return View();
        }

        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Create(IFormCollection form)
        {
            Student student = new Student();
            student.Name = form["name"];
            student.CourseName = form["CourseName"];
            int result = studentDAL.Save(student);
            if (result == 1)
                return RedirectToAction("List");

            return View();
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Student student = studentDAL.GetStudentById(id);
            ViewBag.Name = student.Name;
            ViewBag.CourseName = student.CourseName;
            ViewBag.Id = student.Id;
            return View();
        }

        [HttpPost]
        public IActionResult Edit(IFormCollection form)
        {
            Student student = new Student();
            student.Name = form["name"];
            student.CourseName = form["CourseName"];
            student.Id = Convert.ToInt32(form["id"]);
            int res = studentDAL.Update(student);
            if (res == 1)
                return RedirectToAction("List");

            return View();
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            Student student = studentDAL.GetStudentById(id);
            ViewBag.Name = student.Name;
            ViewBag.CourseName = student.CourseName;
            ViewBag.Id = student.Id;
            return View();
        }
        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeleteConfirm(int id)
        {
            int res = studentDAL.Delete(id);
            if (res == 1)
                return RedirectToAction("List");

            return View();
        }
    }
}
