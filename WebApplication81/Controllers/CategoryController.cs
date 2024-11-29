using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication81.Models;

namespace WebApplication81.Controllers
{
    public class CategoryController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: Categories
        public ActionResult Index()
        {
            var categories = db.Categories.ToList(); // Fetching all categories
            return View(categories); // Passing the list to the view
        }

  
        // GET: Categories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                db.Categories.Add(category);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(category);
        }

        public ActionResult Edit(int id)
        {
            var category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Category/Update
        [HttpPost]
        [ValidateAntiForgeryToken] 
        public ActionResult Update(Category cr)
        {
            if (ModelState.IsValid) // Check if the model state is valid
            {
                // Find the existing category in the database
                var existingCategory = db.Categories.Find(cr.CategoryId);
                if (existingCategory != null)
                {
                    // Update the properties
                    existingCategory.CategoryName = cr.CategoryName;

                    // Save changes to the database
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    return HttpNotFound(); 
                }
            }

            // If model state is not valid, return the same view with the current model
            return View(cr);
        }

        public ActionResult Delete(int id)
        {
            var x= db.Categories.Find(id);
            db.Categories.Remove(x);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
 
    

}


