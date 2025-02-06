using CRUDUsingAdoNet.DAL;
using CRUDUsingAdoNet.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRUDUsingAdoNet.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IConfiguration configuration;
        EmployeeDAL employeedal;
        public EmployeeController(IConfiguration configuration)
        {
            this.configuration = configuration;
            employeedal=new EmployeeDAL(configuration);
        }

        // GET: EmployeeController

        public ActionResult List()
        {
            var model = employeedal.GetAllEmployees();
            return View(model);
           
        }

        // GET: EmployeeController/Details/5
        public ActionResult Details(int id)
        {
           return View();

        }

        // GET: EmployeeController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EmployeeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Employee empl)
        {
            try
            {
                int res = employeedal.AddEmployee(empl);
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

        // GET: EmployeeController/Edit/5
        public ActionResult Edit(int id)
        {
            var employee = employeedal.GetEmployeeById(id);
            return View(employee);
        }

        // POST: EmployeeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Employee empl)
        {
            try
            {
                int res = employeedal.UpdateEmployee(empl);
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

        // GET: EmployeeController/Delete/5
        public ActionResult Delete(int id)
        {
            var employee = employeedal.GetEmployeeById(id);
            return View(employee);
        }

        // POST: EmployeeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public ActionResult DeleteConfirm(int id)
        {
            try
            {
                int res = employeedal.DeleteEmployee(id);
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
