using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Online_Courses.Models;

namespace Online_Courses.Controllers
{
    public class StudentsController : Controller
    {
        db_EducationEntities db = new db_EducationEntities();

        #region لیست دانشجویان

        public ActionResult Index()
        {
            var student= db.T_Students;
            return View(student);
        }
        #endregion

        #region ثبت نام کاربر
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StudentName,StudentFamily,StudentAge,StudentPhoneNumber,StudentPassword,StudentEmail,StudentGender,StudentMarital")]T_Students student,HttpPostedFileBase StudentImageName,string repeatPassword)
        {
            if(ModelState.IsValid)
            {
                if (student.StudentPassword != repeatPassword)
                {
                    ModelState.AddModelError("StudentPassword", "رمز عبور با تکرار آن مطابقت ندارد!");

                    return View(student);
                }

                string newNameImage = "user.jpg";

                if(StudentImageName != null)
                {
                    if (StudentImageName.ContentType != "image/jpeg" && StudentImageName.ContentType != "image/png")
                    {
                        ModelState.AddModelError("StudentImageName", "فرمت عکس باید فقط png یا jpg یا jpeg باشد!");
                        return View();
                    }

                    if (StudentImageName.ContentLength > 500000)
                    {
                        ModelState.AddModelError("imageName", " سایز عکس باید کوچکتر از 500 کیلوبایت باشد!");
                        return View();
                    }

                    newNameImage=Guid.NewGuid().ToString().Replace("-","")+Path.GetExtension(StudentImageName.FileName);
                    StudentImageName.SaveAs(Server.MapPath("/Images/profile/") + newNameImage);
                }

                student.StudentImageName = newNameImage;
                student.StudentRegisterDate = DateTime.Now;
                student.StudentIsActive = true;
                db.T_Students.Add(student);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
        }

        #endregion

        #region جزییات
        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var student=db.T_Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }


            return View(student);
        }
        #endregion

        #region ویرایش
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var student = db.T_Students.Find(id);
            if (student==null)
            {
                return HttpNotFound();
            }


            return View(student);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StudentId,StudentName,StudentFamily,StudentAge,StudentPhoneNumber,StudentPassword,StudentEmail,StudentGender,StudentMarital,StudentImageName,StudentRegisterDate,StudentIsActive")] T_Students student, HttpPostedFileBase StudentImageUpload)
        {
            if (ModelState.IsValid)
            {
                if(StudentImageUpload != null)
                {
                    if (StudentImageUpload.ContentType != "image/jpeg" && StudentImageUpload.ContentType != "image/png")
                    {
                        ModelState.AddModelError("StudentImageName", "فرمت عکس باید فقط png یا jpg یا jpeg باشد!");
                        return View(student);
                    }
                    if (StudentImageUpload.ContentLength > 500000)
                    {
                        ModelState.AddModelError("imageName", " سایز عکس باید کوچکتر از 500 کیلوبایت باشد!");
                        return View(student);
                    }

                    string newNameImage = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(StudentImageUpload.FileName);
                    StudentImageUpload.SaveAs(Server.MapPath("/Images/profile/") + newNameImage);
                    student.StudentImageName=newNameImage;
                }

                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }


            return View(student);
        }

        #endregion

        #region حذف
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var student=db.T_Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }

            return View(student);
        }

        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteStudent(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var student = db.T_Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            if(student!=null)
            {
                db.T_Students.Remove(student);
                db.SaveChanges();
                
                if (student.StudentImageName != "user.jpg")
                {
                    if (System.IO.File.Exists(Server.MapPath("/Images/profile/") + student.StudentImageName))
                    {
                        System.IO.File.Delete(Server.MapPath("/Images/profile/") + student.StudentImageName);
                    }
                }
                return RedirectToAction("Index");
            }
            

            return View(student);
        }
        #endregion

        #region Dispose
        protected override void Dispose(bool disposing)
        {
            if (disposing == true)
            {
                db.Dispose();
            }

            base.Dispose(disposing);
        }
        #endregion
    }
}