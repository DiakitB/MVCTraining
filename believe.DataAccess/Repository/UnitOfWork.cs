using believe.DataAccess.Repository.IRepository;
using believe.DataOD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace believe.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationContext _context;
        public UnitOfWork(ApplicationContext context)
        {
            _context = context;
            Category = new CategoryRepository(_context);
            CoverType= new CoverTypeRepository(_context);
            Prodcut = new ProductRepository(_context);
        }
        public ICategoryRepository Category { get; private set; }
        public ICoverTypeRepository CoverType { get; private set; } 
        public IProductRepository Prodcut { get; private set; }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
