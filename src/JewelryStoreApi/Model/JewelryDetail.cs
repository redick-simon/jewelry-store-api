using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JewelryStoreApi.Model
{
    public class JewelryDetail
    {
        public double PricePerGram { get; set; }
        public double Weight { get; set; }
        public double? Discount { get; set; }
    }
}
