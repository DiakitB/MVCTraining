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
    public class CoverTypeRepository : Repository<CoverType> , ICoverTypeRepository
    {
        private readonly ApplicationContext _context;
        public CoverTypeRepository(ApplicationContext context) : base(context) 
        {
            _context= context;
        }

        //public void Save()
        //{
        //   _context.SaveChanges();
        //}

        public void Update(CoverType? coverType)
        {
            _context.coverType.Update(coverType); ;
        }
    }
}
