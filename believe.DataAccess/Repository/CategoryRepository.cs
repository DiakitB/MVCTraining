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
    public class CategoryRepository : Repository<Category> , ICategoryRepository
    {
        private readonly ApplicationContext _context;
        public CategoryRepository(ApplicationContext context) : base(context) 
        {
            _context= context;
        }

        //public void Save()
        //{
        //   _context.SaveChanges();
        //}

        public void Update(Category? category)
        {
            _context.categories.Update(category); ;
        }
    }
}
