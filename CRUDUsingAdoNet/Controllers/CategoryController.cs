using CRUDUsingAdoNet.DAL;
using CRUDUsingAdoNet.Models;
using Microsoft.AspNetCore.Mvc;

namespace CRUDUsingAdoNet.Controllers
{
    [Route("Category")]
    public class CategoryController : Controller
    {
        private readonly Categorydal dal;
        public CategoryController(IConfiguration configuration)
        {
            string connectionstring = configuration.GetConnectionString("defaultConnection");
            dal = new Categorydal(connectionstring);

        }
        [Route("Index")]
        public IActionResult Index()
        {
            var model = dal.GetAllCategory();
            return View(model);
        }
        [HttpGet("Create")]
        public IActionResult Create()
        {

            return View();
        }
        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category cat)
        {
            int categoryinsert = dal.InsertCategory(cat);
            if (categoryinsert == 1)
            {
                TempData["success"] = "Category Created Successfully";
                return RedirectToAction("Index");
            }
            {
                return View();
            }



        }
       
        [HttpPost("LoadData")]
        public JsonResult LoadData()
        {
            try
            {
                int draw = Convert.ToInt32(Request.Form["draw"]);
                int start = Convert.ToInt32(Request.Form["start"]);
                int length = Convert.ToInt32(Request.Form["length"]);
                string searchValue = Request.Form["search[value]"];

                int totalRecords;
                List<Category> categories = dal.GetPagedCategories(length, start, searchValue, out totalRecords);

                return Json(new
                {
                    draw = draw,
                    recordsTotal = totalRecords,
                    recordsFiltered = totalRecords,
                    data = categories
                });
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }


        // [Route("Category/Edit/{id}")]
        [HttpGet("Edit/{id}")]
        public ActionResult Edit(int id)
        {
            var category = dal.GetCategoryById(id);
            return View(category);
        }

        // POST: ProductController/Edit/5
       
        //[Route("Category/Edit/{id}")]
        [HttpPost("Edit/{id}")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Category cat)
        {
            try
            {
                int res = dal.UpdateCategory(cat);
                if (res == 1)
                {
                    TempData["success"] = "Category Updated Successfully";
                    return RedirectToAction("Index");
                }
                   
               
                else
                    return View();
            }
            catch
            {
                return View();
            }
        }
        [Route("Detail/{id}")]
        
        public IActionResult Detail(int id)
        {
            var category = dal.GetCategoryById(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }
       

        [HttpGet("Delete/{id}")]
      
        public IActionResult Delete(int id)
        {
            var category = dal.GetCategoryById(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: Category/DeleteConfirmed/1 (Perform actual delete operation)
        
        [ValidateAntiForgeryToken]
        [HttpPost("Delete/{id}")]
        public IActionResult DeleteConfirmed(int id)
        {
            dal.DeleteCategory(id);
            TempData["success"] = "Category Deleted Successfully";
            return RedirectToAction("Index");
        }
    }

}

