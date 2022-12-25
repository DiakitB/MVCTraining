using believe.DataAccess.Repository.IRepository;
using believe.Models;
using believe.Views.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Diagnostics;

namespace believe.Areas.Customer.Controllers
{
    public class HomeController : Controller
        
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _context;
       
        public HomeController(ILogger<HomeController> logger , IUnitOfWork cont)
        {
            _logger = logger;
            _context = cont;
        }

        public IActionResult Index()
        {
            IEnumerable<Product> productListes = _context.Product.GetAll(includeProperties: "Category,CoverType");
            return View(productListes);
        }
        public IActionResult Details(int id)
        {
            ShoppingCart cartObj = new()
            {
                Count = 1,
                Product = _context.Product.GetFirstOrDefault(u=>u.Id==id, includeProperties: "Category,CoverType"),
               
            };


            return View(cartObj);
   
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}