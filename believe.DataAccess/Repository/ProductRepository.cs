using believe.DataAccess.Repository.IRepository;
using believe.DataOD;
using believe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace believe.DataAccess.Repository
{
    public class ProductRepository : Repository<Product> , IProductRepository
    {
        private readonly ApplicationContext _context;
        public ProductRepository(ApplicationContext context) : base(context) 
        {
            _context= context;
        }

        //public void Save()
        //{
        //   _context.SaveChanges();
        //}

        public void Update(Product? obj)
        {
            _context.products.Update(obj); ;
        }
    }
}
