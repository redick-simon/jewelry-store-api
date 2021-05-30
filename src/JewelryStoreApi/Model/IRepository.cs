using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JewelryStoreApi.Model
{
    public interface IRepository
    {
        Task<IEnumerable<User>> GetUsers();
    }
}
