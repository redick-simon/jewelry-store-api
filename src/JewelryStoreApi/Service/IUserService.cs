using JewelryStoreApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JewelryStoreApi.Service
{
    public interface IUserService
    {
        ValidateResult Validate(string basicAuth);
    }
}
