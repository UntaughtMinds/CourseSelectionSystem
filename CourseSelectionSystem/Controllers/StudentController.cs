using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CourseSelectionSystem.Models;
using CsvHelper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

namespace CourseSelectionSystem.Controllers
{
    public class StudentController : Controller
    {
        StudentInformation students = new StudentInformation();
        public IActionResult search([FromQuery]int id=0)
        {
            if (id == 0)
                this.ViewData["studentInf"] = students.GetAllStudent();
            else
                this.ViewData["studentInf"] = students.Search(id);
            return View();
        }
        public IActionResult newStudent([FromForm]string Name=null,string Class=null)
        {
            bool flag = false;
            this.ViewData["name"] = Name;
            if (Name != null)
                flag = students.NewStudent(Name,Class);
            this.ViewData["flag"] = flag;
            return View();
        }
        public IActionResult change([FromForm]int id, string Name = null, string Class = null)
        {
            bool flag = false;
            this.ViewData["name"] = Name;
            if (Name != null)
                flag = students.Change(id,Name,Class);
            else if (id != 0)
                this.ViewData["oldInf"] = students.Search(id).FirstOrDefault();
            this.ViewData["flag"] = flag;
            return View();
        }
        public IActionResult delete([FromQuery]int id = 0)
        {
            bool flag = false;
            this.ViewData["id"] = id;
            if (id != 0)
                flag = students.Delete(id);
            this.ViewData["flag"] = flag;
            return View();
        }
        public IActionResult upload([FromForm] IFormFile csvfile)
        {
            return View();
        }
    }
}