using Newtonsoft.Json;
using ShoppingStore.DAL;
using ShoppingStore.Models;
using ShoppingStore.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShoppingStore.Controllers
{
    public class AdminController : Controller
    {
        private GenericUnitOfWork _unitOfWork = new GenericUnitOfWork();

        // GET: Admin
        public ActionResult Dashboard()
        {
            return View();
        }

        // -------------------- Category -------------------------------------------------------------
        public ActionResult GetCategories()
        {
            List<Tbl_Category> _allCategories = _unitOfWork.GetRepositoryInstance<Tbl_Category>()
                .GetAllRecordsQuerable().Where(cat => cat.IsDelete == false)
                .ToList();
            return View(_allCategories);
        }

        public ActionResult AddCategory()
        {
            return UpdateCategory(0);
        }

        public ActionResult UpdateCategory(int categoryID)
        {
            CategoryDetails catDetails; 

            if(categoryID != null)
            {
                catDetails = JsonConvert.DeserializeObject<CategoryDetails>(
                    JsonConvert.SerializeObject(
                        _unitOfWork.GetRepositoryInstance<Tbl_Category>().GetFirstOrDefault(categoryID)
                        )
                    );
            }
            else
            {
                catDetails = new CategoryDetails();
            }

            return View("UpdateCategory", catDetails);
        }

        //Edit method
        public ActionResult EditCategory(int categoryID)
        {
            ViewBag.Categories = GetMapGategories();
            if (categoryID != null)
            {
                return View(_unitOfWork.GetRepositoryInstance<Tbl_Category>().GetFirstOrDefault(categoryID));
            }
            else
            {
                return RedirectToAction("GetCategories");
            }

        }
        //Update method
        [HttpPost]
        public ActionResult EditCategory(Tbl_Category category)
        {
            _unitOfWork.GetRepositoryInstance<Tbl_Category>().Updtae(category);

            return RedirectToAction("GetCategories");
        }



        // -------------------- Product -------------------------------------------------------------
        public ActionResult GetProducts()
        {
            return View(_unitOfWork.GetRepositoryInstance<Tbl_Product>().GetElements().ToList());
        }

        //Edit method
        public ActionResult EditProduct(int productID)
        {
            ViewBag.Categories = GetMapGategories();
            if (productID != null)
            {
                return View(_unitOfWork.GetRepositoryInstance<Tbl_Product>().GetFirstOrDefault(productID));
            }
            else
            {
                return RedirectToAction("GetProducts");
            }
            
        }
        //Update method
        [HttpPost]
        public ActionResult EditProduct(Tbl_Product product, HttpPostedFileBase file)
        {
            string filename = "";
            if (file != null)
            {
                filename = System.IO.Path.GetFileName(file.FileName);
                string path = System.IO.Path.Combine(Server.MapPath("~/Content/images/Products/"), filename);
                file.SaveAs(path);
            }
            product.ProductImage = file != null ? filename : product.ProductImage;
            product.ModifiedDate = DateTime.Now;
            _unitOfWork.GetRepositoryInstance<Tbl_Product>().Updtae(product);

            return RedirectToAction("GetProducts");
        }

        // Add View Method
        public ActionResult AddProduct()
        {
            ViewBag.Categories = GetMapGategories();
            return View();
        }
        //Save method
        [HttpPost]
        public ActionResult AddProduct(Tbl_Product product, HttpPostedFileBase file)
        {
            string filename=""; 
            if (file != null)
            {
                filename = System.IO.Path.GetFileName(file.FileName);
                string path = System.IO.Path.Combine(Server.MapPath("~/Content/images/Products/"), filename);
                file.SaveAs(path);
            }
            product.ProductImage = filename;
            product.CreatedDate = DateTime.Now;
            _unitOfWork.GetRepositoryInstance<Tbl_Product>().Add(product);
            return RedirectToAction("GetProducts");
        }

        //---------------------- Util ----------------------------------------

        public IEnumerable<SelectListItem> GetMapGategories()
        {
            List<SelectListItem> selectList = new List<SelectListItem>();
            var categories = _unitOfWork.GetRepositoryInstance<Tbl_Category>().GetAllRecords();

            foreach (var cat in categories)
            {
                selectList.Add(new SelectListItem { Value = cat.CategoryId.ToString(), Text = cat.CategoryName });
            }
            return selectList;
        }
    }
}