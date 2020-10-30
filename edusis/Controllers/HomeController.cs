using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using edusis.Models;
using System.Data.SqlClient;
using System.Configuration;
using Dapper;

namespace edusis.Models
{
   
    public class HomeController : Controller
    {
        IDbConnection connect = new SqlConnection(Properties.Settings.Default.conStr);
        List<student> studentList = new List<student>();
        List<lesson> lessonList = new List<lesson>();
        List<classroom> classroomList = new List<classroom>();

        student _student = new student();
        lesson _lesson = new lesson();
        classroom _classroom = new classroom();

        public ActionResult Index()
        {

            using (connect)
            {

                studentList = connect.Query<student>("Select * From students").ToList();
                lessonList = connect.Query<lesson>("Select * From Lessons").ToList();
                classroomList = connect.Query<classroom>("Select * From classrooms").ToList();
                ViewBag.LessonList = lessonList;
                ViewBag.ClassroomList = classroomList;
                ViewBag.studentList = studentList;
                ViewBag.classCount = classroomList.Count();
                ViewBag.totalList = studentList.Count();
                ViewBag.lessons = lessonList;
                ViewBag.lessonCount = lessonList.Count();
                ViewBag.isBus = connect.Query<student>("Select * From students where ogrServis='var'").Count();
                ViewBag.isActive = connect.Query<lesson>("SELECT * FROM lessons where lessonActivite=1").Count();
                ViewBag.Male = connect.Query<lesson>("SELECT * from students where ogrCins='erkek'").Count();
                ViewBag.Female = connect.Query<lesson>("SELECT * from students where ogrCins='bayan'").Count();
            }
            return View(studentList);
        }

        public ActionResult Details(int id)
        {
            using (connect)
            {
                _student = connect.Query<student>("Select * From students " +
                                       "WHERE ogrID =" + id, new { id }).SingleOrDefault();
            }
            return View(_student);
        }


        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(student _student)
        {
            
                using (connect)

                {

                string sqlQuery = $@"Insert Into students (ogrName,ogrSurname,OgrTc,ogrTel,ogrServis,ogrSehir,classID,ogrMail,ogrCins) Values('{ _student.ogrName}',
                            '{_student.ogrSurname}',
                            '{_student.OgrTc}',
                            '{_student.ogrTel}',
                            '{_student.ogrServis}',
                            '{_student.ogrSehir}',
                            '{_student.classID}',
                            '{_student.ogrMail}',
                            '{_student.ogrCins}')";
                     int rowsAffected = connect.Execute(sqlQuery);
                }

          
                return RedirectToAction("Index");
            
        }

        public ActionResult Edit(int id)
        {
           
            using (connect)
            {
                _student = connect.Query<student>("Select * From students " +
                                       "WHERE ogrID =" + id, new { id }).SingleOrDefault();
            }
            return View(_student);
        }

        [HttpPost]
        public ActionResult Edit(student _student)
        {
            using (connect)
            {
                string sqlQuery = "update students set ogrName='" + _student.ogrName + "',ogrSurname='" + _student.ogrSurname + "',ogrTc='" + _student.OgrTc +"',ogrTel'"+_student.ogrTel+ "',ogrServis'"+_student.ogrServis+ "',ogrSehir'"+_student.ogrSehir+ "',classID'"+ _student.classID+ "',ogrMail'"+ _student.ogrMail+ "'ogrCins'"+ _student.ogrCins +"'where ogrID=" + _student.ogrID;
                int rowsAffected = connect.Execute(sqlQuery);
            }
            return RedirectToAction("Index");
        }


        [HttpPost]
        public JsonResult Delete(int id)
        {
            using (connect)
            {
                string sorgum = "Delete From students WHERE ogrID = " + id;

                int rowsAffected = connect.Execute(sorgum);


            }
            return Json("İlgili öğrenci başarıyla silinmiştir.", JsonRequestBehavior.AllowGet);

        }

    }
}
