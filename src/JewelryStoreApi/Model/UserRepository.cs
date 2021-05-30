using JewelryStoreApi.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelryStoreApi.Model
{
    public class UserRepository : IRepository
    {
        private JewelryDbContext _context;
        public UserRepository(JewelryDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }
    }
}
