using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CourseSelectionSystem.Models;

namespace CourseSelectionSystem.Controllers
{
    public class SelectionController : Controller
    {
        CourseSelection selection = new CourseSelection();
        public IActionResult select([FromForm] int CourseId = 0,int StudentId = 0)
        {
            this.ViewData["courseInf"] = new CourseInformation().GetAllCourse();
            this.ViewData["studentInf"] = new StudentInformation().GetAllStudent();
            bool flag = false;
            this.ViewData["id"] = CourseId;
            if (CourseId != 0)
                flag = selection.select(CourseId, StudentId);
            this.ViewData["flag"] = flag;
            return View();
        }
        public IActionResult delete([FromForm] int CourseId = 0, int StudentId = 0)
        {
            bool flag = false;
            this.ViewData["id"] = CourseId;
            if (CourseId != 0)
                flag = selection.Delete(CourseId, StudentId);
            this.ViewData["flag"] = flag;
            return View();
        }
    }
}