
using believe.DataAccess.Repository.IRepository;
using believe.DataOD;
using believe.Models;
using Microsoft.AspNetCore.Mvc;

namespace believe.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _context;
        public CategoryController(IUnitOfWork context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            IEnumerable<Category> categoryFromDb = _context.Category.GetAll();
            return View(categoryFromDb);
        }
        //GET
       
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category? category)
        {
            if(category == null)

            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                _context.Category.Add(category);
               // _context.Save();
                TempData["success"] = "Category created successfully";
                return RedirectToAction("Index");
            }
            return View(category);
          
            
            
        }
        public IActionResult Delete(int? id)
        {
            if (id is null or 0)
            {
                return NotFound();
            }
            var obj = _context.Category.GetFirstOrDefault(c => c.Id == id);
            return View(obj);
        }
        [HttpPost]
        public IActionResult DeletePOST(int? id)
        {
            var obj = _context.Category.GetFirstOrDefault(c => c.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            _context.Category.Remove(obj);
          //  _context.Save();
            TempData["success"] = "Category deleted successfully";
            return RedirectToAction("Index");   
        }
        public IActionResult Edit(int? id)
        {
            if(id is null or 0)
            {
                return NotFound();
            }
            var obj = _context.Category.GetFirstOrDefault(c => c.Id == id);
            return View(obj);
        }
        [HttpPost]
        public IActionResult Edit(Category? obj)
        {
            if(obj == null )
            {
                return NotFound();
            }

            if(ModelState.IsValid)
            {
                _context.Category.Update(obj);
                _context.Save();
                TempData["success"] = "Category update successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
            
        }
    }
}
