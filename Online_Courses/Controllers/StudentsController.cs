using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Online_Courses.Models;

namespace Online_Courses.Controllers
{
    public class StudentsController : Controller
    {
        db_EducationEntities db = new db_EducationEntities();

        public ActionResult Index()
        {
            var student= db.T_Students;
            return View(student);
        }
    }
}