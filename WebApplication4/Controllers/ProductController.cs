using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using WebApplication4.Models;

namespace WebApplication4.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            using(ProductDBModelEntities db = new ProductDBModelEntities())
            {
                List<tblProduct> ProductList = (from data in db.tblProducts
                                                select data).ToList();
                return View(ProductList);
            }
        }

        // GET: Product/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            return View(new tblProduct()); // To redirect in the route Create
        }

        // POST: Product/Create
        [HttpPost]
        public ActionResult Create(tblProduct product, HttpPostedFileBase postedFile)
        {
            // Store data
            try
            {
                if (postedFile != null)
                {
                    string extension = Path.GetExtension(postedFile.FileName);
                    if (extension.Equals(".jpg") || extension.Equals(".png"))
                    {
                        string filename = "IMG-" + DateTime.Now.ToString("yyyyMMddhhmmssffff") + extension;
                        string savepath = Server.MapPath("~/Content/Images/");
                        postedFile.SaveAs(savepath + filename);
                        product.prod_pic = filename;
                    }
                    else
                    {
                        ModelState.AddModelError("", "You can only upload JPG or PNG files."); // Return an error
                        return View(product);
                    }
                }

                using (ProductDBModelEntities db = new ProductDBModelEntities()) // save in the database
                {
                    db.tblProducts.Add(product);
                    db.SaveChanges();
                }

                TempData["SuccessMessage"] = "Product created successfully.";  // if sccessfull return a message

                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "An error occurred while saving the product. Please try again.");
                return View(product);
            }
        }

        // GET: Product/Edit/5
        public ActionResult Edit(int id)
        {
            using (ProductDBModelEntities db = new ProductDBModelEntities())
            {

                tblProduct product = db.tblProducts.Find(id);
                return View(product);

            }
        }

        // POST: Product/Edit/5
        [HttpPost]
        public ActionResult Edit(tblProduct product, HttpPostedFileBase postedFile)
        {
            try
            {
                // TODO: Add update logic here
                string filename = "";
                if(postedFile != null)
                {
                    string extension = Path.GetExtension(postedFile.FileName);
                    if (extension.Equals(".jpg") || extension.Equals(".png"))
                    {
                        filename = "IMG-" + DateTime.Now.ToString("yyyyMMddhhmmssffff") + extension;
                        string savepath = Server.MapPath("~/Content/Images/");
                        postedFile.SaveAs(savepath + filename);
                    }

                }
                using (ProductDBModelEntities db = new ProductDBModelEntities())
                {
                    tblProduct tbl = db.tblProducts.Find(product.prod_id);
                    tbl.prod_name = product.prod_name;
                    tbl.prod_price = product.prod_price;
                    tbl.prod_qty = product.prod_qty;
                    if(!filename.Equals(""))
                    {
                        tbl.prod_pic = filename;
                    }
                    db.SaveChanges();

                }
                // Update the product
                // Add success message to TempData
                TempData["SuccessMessage"] = "Product updated successfully.";


                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Product/Delete/5
        public ActionResult Delete(int id)
        {
            using (ProductDBModelEntities db = new ProductDBModelEntities())
            {
                var product = db.tblProducts.Find(id);
                if (product == null)
                {
                    return HttpNotFound();
                }

                db.tblProducts.Remove(product);
                db.SaveChanges();

                TempData["SuccessMessage"] = "Product deleted successfully.";

                return RedirectToAction("Index");
            }
        }


        // POST: Product/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
