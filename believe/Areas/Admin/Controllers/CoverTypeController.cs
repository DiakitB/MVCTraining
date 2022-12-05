
using believe.DataAccess.Repository.IRepository;
using believe.DataOD;
using believe.Models;
using Microsoft.AspNetCore.Mvc;

namespace believe.Controllers
{
    public class CoverTypeController : Controller
    {
        private readonly IUnitOfWork _context;
        public CoverTypeController(IUnitOfWork context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            IEnumerable<CoverType> categoryFromDb = _context.CoverType.GetAll();
            return View(categoryFromDb);
        }
        //GET
       
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CoverType? coverType)
        {
            if(coverType == null)

            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                _context.CoverType.Add(coverType);
                _context.Save();
                TempData["success"] = "Category created successfully";
                return RedirectToAction("Index");
            }
            return View(coverType);
          
            
            
        }
        public IActionResult Delete(int? id)
        {
            if (id is null or 0)
            {
                return NotFound();
            }
            var obj = _context.CoverType.GetFirstOrDefault(c => c.Id == id);
            return View(obj);
        }
        [HttpPost]
        public IActionResult DeletePOST(int? id)
        {
            var obj = _context.CoverType.GetFirstOrDefault(c => c.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            _context.CoverType.Remove(obj);
           // _context.Save();
            TempData["success"] = "Category deleted successfully";
            return RedirectToAction("Index");   
        }
        public IActionResult Edit(int? id)
        {
            if(id is null or 0)
            {
                return NotFound();
            }
            var obj = _context.CoverType.GetFirstOrDefault(c => c.Id == id);
            return View(obj);
        }
        [HttpPost]
        public IActionResult Edit(CoverType? obj)
        {
            if(obj == null )
            {
                return NotFound();
            }

            if(ModelState.IsValid)
            {
                _context.CoverType.Update(obj);
               // _context.Save();
                TempData["success"] = "Category update successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
            
        }
    }
}
