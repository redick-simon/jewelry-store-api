using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JewelryStoreApi.Model
{
    public interface IJewelryService
    {
        double Calculate(JewelryDetail jewelryDetail);

        byte[] CreateByteArray(JewelryDetail jewelryDetail);
    }
}
