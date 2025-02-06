using CRUDUsingAdoNet.DAL;
using CRUDUsingAdoNet.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRUDUsingAdoNet.Controllers
{
    public class ProductController : Controller
    {
        private readonly IConfiguration configuration;
        ProductDAL productdal;
        public ProductController(IConfiguration configuration)
        {
            this.configuration = configuration;
            productdal = new ProductDAL(configuration);
          
        }

        public ActionResult List()
        {
            var model=productdal.GetAllProducts();
            return View(model);
        }

        // GET: ProductController/Details/5
        public ActionResult Details(int id)
        {
            var product = productdal.GetProductById(id);
            return View(product);
            
        }

        // GET: ProductController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product prod)
        {
            try
            {
                int res=productdal.AddProduct(prod);
                if(res==1)
                return RedirectToAction(nameof(List));
                else
                    return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductController/Edit/5
        public ActionResult Edit(int id)
        {
            var product= productdal.GetProductById(id);
            return View(product);
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product prod)
        {
            try
            {
                int res = productdal.UpdateProduct(prod);
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

        // GET: ProductController/Delete/5
        public ActionResult Delete(int id)
        {
            var product = productdal.GetProductById(id);
            return View(product);
            
        }

        // POST: ProductController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public ActionResult DeleteConfirm(int id)
        {
            try
            {
                int res=productdal.DeleteProduct(id);
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
