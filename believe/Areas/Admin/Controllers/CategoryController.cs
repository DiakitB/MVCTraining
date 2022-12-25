
using believe.DataAccess.Repository;
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
		public IActionResult Create(Category obj)
		{
			if (obj.Name == null)
			{
				ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the Name.");
			}
			if (ModelState.IsValid)
			{
				_context.Category.Add(obj);
				_context.Save();
				TempData["success"] = "Category created successfully";
				return RedirectToAction("Index");
			}
			return View(obj);
		}
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            //var categoryFromDb = _db.Categories.Find(id);
            var categoryFromDbFirst = _context.Category.GetFirstOrDefault(u => u.Id == id);
            //var categoryFromDbSingle = _db.Categories.SingleOrDefault(u => u.Id == id);

            if (categoryFromDbFirst == null)
            {
                return NotFound();
            }

            return View(categoryFromDbFirst);
        }

        //POST
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

        }   //GET
        public IActionResult Edit(int id)
		{
			if (id == null || id == 0)
			{
				return NotFound();
			}
			//var categoryFromDb = _db.Categories.Find(id);
			var categoryFromDbFirst = _context.Category.GetFirstOrDefault(u => u.Id == id);
			//var categoryFromDbSingle = _db.Categories.SingleOrDefault(u => u.Id == id);

			if (categoryFromDbFirst == null)
			{
				return NotFound();
			}

			return View(categoryFromDbFirst);
		}
		//POST
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(Category obj)
		{
			if (obj.Name == null)
			{
				ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the Name.");
			}
			if (ModelState.IsValid)
			{
				_context.Category.Update(obj);
				_context.Save();
				TempData["success"] = "Category updated successfully";
				return RedirectToAction("Index");
			}
			return View(obj);
		}

	}
}
