
using believe.DataAccess.Repository;
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

      
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var obj = _context.Category.GetFirstOrDefault(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }

            _context.Category.Remove(obj);
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
                productVM.Product = _context.Product.GetFirstOrDefault(u=>u.Id==id);
				return View(productVM);
			}
          ;
          
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
                    if(obj.Product.ImageUrl!= null)
                    {
                        var oldImagePath = Path.Combine(wwwRootPath, obj.Product.ImageUrl.TrimStart('\\'));
                        if(System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }
                    using(var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        file.CopyTo(fileStreams);
                    }
                    obj.Product.ImageUrl = @"\images\products\" + fileName + extension;
                }

                if(obj.Product.Id == 0)
                {
					_context.Product.Add(obj.Product);
				}
                else
                {
                    _context.Product.Update(obj.Product);
                }
                
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
            var productList = _context.Product.GetAll(includeProperties: "Category,CoverType");
            var objlis =  Json(new {data = productList});
            return (objlis);
        }
        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var obj = _context.Product.GetFirstOrDefault(u => u.Id == id);
            if (obj == null)
            {
                var objlisd = Json(new { success = false, message="Error while deleting" });
                return (objlisd);
            }
            var oldImagePath = Path.Combine(_hostEnvironment.WebRootPath, obj.ImageUrl.TrimStart('\\'));
            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }

            _context.Product.Remove(obj);
            _context.Save();

            var objlisdr = Json(new { success = true, message = "delesuccefully" });
            return (objlisdr);

        }

        #endregion
    }

}
