
using believe.DataAccess.Repository.IRepository;
using believe.DataOD;
using believe.Models;
using believe.Views.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace believe.Controllers
{
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        public ProductController(IUnitOfWork context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment; 
        }

        public IActionResult Index()
        {
           
            return View();
        }
        //GET
       
      
        public IActionResult Delete(int? id)
        {
            if (id is null or 0)
            {
                return NotFound();
            }
            var obj = _context.Prodcut.GetFirstOrDefault(c => c.Id == id);
            return View(obj);
        }
        [HttpPost]
        public IActionResult DeletePOST(int? id)
        {
            var obj = _context.Prodcut.GetFirstOrDefault(c => c.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            _context.Prodcut.Remove(obj);
           _context.Save();
            TempData["success"] = "Category deleted successfully";
            return RedirectToAction("Index");   
        }
        public IActionResult UpSert(int? id)
        {

            //Product product = new();
            //IEnumerable < SelectListItem > CategoryList = _context.Category.GetAll().Select(u=>new SelectListItem { Text = u.Name, Value = u.Id.ToString() });
            // IEnumerable<SelectListItem> CoverTypeList = _context.CoverType.GetAll().Select(u => new SelectListItem { Text = u.Name, Value = u.Id.ToString() });
            ProductVM productVM = new()
            {
                Product = new(),
             CategoryList =    _context.Category.GetAll().Select(u => new SelectListItem { Text = u.Name, Value = u.Id.ToString() }),
             CoverTypeList =  _context.CoverType.GetAll().Select(u => new SelectListItem { Text = u.Name, Value = u.Id.ToString() })


            };
            
            if (id is null or 0)
            {
                //create product

                //ViewBag.CategoryList = CategoryList;
                //ViewBag.CoverTypeList = CoverTypeList;  
                return View(productVM);
            }
            else
            {
                //update product
            }
          ;
            return View(productVM);
        }
        [HttpPost]
        public IActionResult UpSert(ProductVM obj, IFormFile? file)
        {
            

            if(ModelState.IsValid)
            {
                string wwwRootPath = _hostEnvironment.WebRootPath;
                if(file !=null)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(wwwRootPath, @"images\products");
                    var extension = Path.GetExtension(file.FileName);
                    using(var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        file.CopyTo(fileStreams);
                    }
                    obj.Product.ImageUrl = @"\images\products\" + fileName + extension;
                }


                _context.Prodcut.Add(obj.Product);
                _context.Save();
                TempData["success"] = "Product  update successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
            
        }
        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            var productList = _context.Prodcut.GetAll();
            return Json(new {data = productList});
        }
            

        #endregion
    }

}
