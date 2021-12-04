using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Rocky.Data;
using Rocky.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rocky.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ProductController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Product> objList = _db.Product;

            foreach(var obj in objList)
            {
                obj.Category = _db.Category.FirstOrDefault(u => u.Id == obj.CategoryId);
            }

            return View(objList);
        }

        // Get for Upsert 
        public IActionResult Upsert(int? id)
        {
            Product product = new Product();

            if(id == null)
            {
                //___________ this is for create___________________

                //we are getting name and id of category and storting it on CategoryDropDown varibale using projections and passing the values using viewbag

                IEnumerable<SelectListItem> CategoryDropDown = _db.Category.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                    
                });
                
                ViewBag.CategoryDropDown = CategoryDropDown; 


                return View(product);
            }
            else
            {
                // this is for Update
                product = _db.Product.Find(id);
                if(product == null)
                {
                    return NotFound();
                }
                else
                {
                    return View(product);
                }
            }
            
        }

        // POST for create 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            if (ModelState.IsValid)
            {
                _db.Category.Add(obj);
                _db.SaveChanges();

                return RedirectToAction("Index");

            }
            return View(obj);

        }

        // Get for Delete
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var obj = _db.Category.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        // POST for Delete 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var obj = _db.Category.Find(id);

            if (obj == null)
            {
                return NotFound();
            }

            _db.Category.Remove(obj);
            _db.SaveChanges();

            return RedirectToAction("Index");



        }
    }
}
