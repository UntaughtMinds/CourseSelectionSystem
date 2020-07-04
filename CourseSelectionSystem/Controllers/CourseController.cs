using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CourseSelectionSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace CourseSelectionSystem.Controllers
{
    public class CourseController : Controller
    {
        CourseInformation courses = new CourseInformation();
        public IActionResult search([FromQuery] int id=0)
        {
            if (id == 0)
                this.ViewData["courseInf"] = courses.GetAllCourse();
            else
                this.ViewData["courseInf"] = courses.Search(id);
            return View();
        }
        public IActionResult newCourse([FromForm] string Name = null, int IsElective = 0, int Credit = 0)
        {
            bool flag = false;
            this.ViewData["name"] = Name;
            if (Name != null)
                flag = courses.NewCourse(Name, IsElective, Credit);
            this.ViewData["flag"] = flag;
            return View();
        }
        public IActionResult change([FromForm]int id = 0, string Name = null, int IsElective = 0, int Credit = 0)
        {
            bool flag = false;
            this.ViewData["id"] = id;
            if (Name != null)
                flag = courses.Change(id, Name, IsElective, Credit);
            else if (id!=0)
                this.ViewData["oldInf"] = courses.Search(id).FirstOrDefault();
            this.ViewData["flag"] = flag;
            return View();
        }
        public IActionResult delete([FromQuery]int id = 0)
        {
            bool flag = false;
            this.ViewData["id"] = id;
            if (id != 0)
                flag = courses.Delete(id);
            this.ViewData["flag"] = flag;
            return View();
        }
    }
}