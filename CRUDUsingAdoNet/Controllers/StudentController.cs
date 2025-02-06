using CRUDUsingAdoNet.DAL;
using CRUDUsingAdoNet.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRUDUsingAdoNet.Controllers
{
    public class StudentController : Controller
    {
        private readonly IConfiguration configuration;
        StudentDAL studentdal;
        public StudentController(IConfiguration configuration)
        {
            this.configuration = configuration;
            studentdal = new StudentDAL(configuration);
        }


        // GET: StudentController
        public ActionResult List()
        {
            var model = studentdal.GetAllStudents();
            return View(model);
            
        }

        // GET: StudentController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: StudentController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StudentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Student stud)
        {
            try
            {
                int res = studentdal.AddStudent(stud);
                if (res == 1)
                    return RedirectToAction(nameof(List));
                else
                    return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: StudentController/Edit/5
        public ActionResult Edit(int id)
        {
            var student = studentdal.GetStudentById(id);
            return View(student);
            
        }

        // POST: StudentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Student stud)
        {
            try
            {
                int res = studentdal.UpdateStudent(stud);
                if (res == 1)
                    return RedirectToAction(nameof(List));
                else
                    return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: StudentController/Delete/5
        public ActionResult Delete(int id)
        {
            var student = studentdal.GetStudentById(id);
            return View(student);
            //return View();
        }

        // POST: StudentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public ActionResult DeleteConfirm(int id)
        {
            try
            {
                int res = studentdal.DeleteStudent(id);
                if (res == 1)
                    return RedirectToAction(nameof(List));
                else
                    return View();
            }
            catch
            {
                return View();
            }
        }
    }
}
