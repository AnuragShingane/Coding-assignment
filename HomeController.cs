using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;

namespace WebApplication2.Controllers
{
    public class HomeController : Controller
    {
        EcomShopEntities db = new EcomShopEntities();
        public ActionResult Index()
        {
            var data = db.tblInventories.ToList();

            return View(data);
        }


        public ActionResult Details(int id)
        {
            using (EcomShopEntities db=new EcomShopEntities())
            {
                return View(db.tblInventories.Where(x => x.ProductID == id).FirstOrDefault());
            }
            
        }
        [HttpGet]
        public ActionResult Edit (int id)
        {
            using (EcomShopEntities db = new EcomShopEntities())
            {
                return View(db.tblInventories.Where(x => x.ProductID == id).FirstOrDefault());
            }

        }

        [HttpPost]
        public ActionResult Edit(int id, tblInventory invent)
        {
            try
            {
                using (EcomShopEntities db=new EcomShopEntities())
                {
                    db.Entry(invent).State = EntityState.Modified;
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }

        }
         
        [HttpGet]
        public ActionResult Delete (int id)
        {
            using (EcomShopEntities db = new EcomShopEntities())
            {
                return View(db.tblInventories.Where(x => x.ProductID == id).FirstOrDefault());
            }

        }

        [HttpPost]
        public ActionResult Delete (int id, tblInventory invent)
        {
            try
            {
                using (EcomShopEntities db = new EcomShopEntities())
                {
                    invent = db.tblInventories.Where(x => x.ProductID == id).FirstOrDefault();
                    db.tblInventories.Remove(invent);
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
            

        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        [HttpGet]
        public ActionResult Create()
        {
            

            return View();
        }

        [HttpPost]
        public ActionResult Create(tblInventory t)
        {
            if(ModelState.IsValid == true)
            {
                db.tblInventories.Add(t);
                int a = db.SaveChanges();
                if (a > 0)
                {
                    TempData["InsertMessage"] = "<script>alert('Inserted !!')</script>";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["InsertMessage"] = "<script>alert('Not Inserted !!')</script>";

                }
            }
            
            return View();
        }
    }
}