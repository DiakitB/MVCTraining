
using believe.DataAccess.Repository;
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
		public IActionResult Create(CoverType obj)
        {
            if (ModelState.IsValid)
            {
				_context.CoverType.Add(obj);
				_context.Save();
				TempData["success"] = "CoverType created successfully";
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
			var CoverTypeFromDbFirst = _context.CoverType.GetFirstOrDefault(u => u.Id == id);

			if (CoverTypeFromDbFirst == null)
			{
				return NotFound();
			}

			return View(CoverTypeFromDbFirst);
		}
		//POST
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public IActionResult DeletePOST(int? id)
		{
			var obj = _context.CoverType.GetFirstOrDefault(u => u.Id == id);
			if (obj == null)
			{
				return NotFound();
			}

			_context.CoverType.Remove(obj);
			_context.Save();
			TempData["success"] = "CoverType deleted successfully";
			return RedirectToAction("Index");

		}
		//GET
		public IActionResult Edit(int? id)
		{
			if (id == null || id == 0)
			{
				return NotFound();
			}
			var CoverTypeFromDbFirst = _context.CoverType.GetFirstOrDefault(u => u.Id == id);

			if (CoverTypeFromDbFirst == null)
			{
				return NotFound();
			}

			return View(CoverTypeFromDbFirst);
		}

		[HttpPost]
		//POST
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(CoverType obj)
		{

			if (ModelState.IsValid)
			{
				_context.CoverType.Update(obj);
				_context.Save();
				TempData["success"] = "CoverType updated successfully";
				return RedirectToAction("Index");
			}
			return View(obj);
		}
	}
}
