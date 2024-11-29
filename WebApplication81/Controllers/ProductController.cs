using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication81.Models;

namespace WebApplication81.Controllers
{
    public class ProductController : Controller
    {
       private AppDbContext db = new AppDbContext();

        // GET: Products
        public ActionResult Index(int page = 1, int pageSize = 10)
        {
            var totalCount = db.Products.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

            var products = db.Products.Include("Category")
                                      .OrderBy(p => p.ProductId)
                                      .Skip((page - 1) * pageSize)
                                      .Take(pageSize)
                                      .ToList();

            ViewBag.TotalCount = totalCount;
            ViewBag.CurrentPage = page;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalPages = totalPages; 

            return View(products);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName");
            return View();
        }

        // POST: Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName", product.CategoryId);
            return View(product);
        }



         public ActionResult Edit(int id)
    {
        var product = db.Products.Include("Category").FirstOrDefault(p => p.ProductId == id);
        if (product == null)
        {
            return HttpNotFound();
        }
        ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName", product.CategoryId);
        return View(product);
    }

    // POST: Product/Edit
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit(Product product)
    {
        if (ModelState.IsValid)
        {
            db.Entry(product).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName", product.CategoryId);
        return View(product);
    }

        public ActionResult Delete(int id)
        {
            var d = db.Products.Find(id);
            db.Products.Remove(d);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


    }
}




    
