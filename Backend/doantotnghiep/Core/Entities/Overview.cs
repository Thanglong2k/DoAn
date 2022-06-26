using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Overview
    {
    }
    public class MonthlyTurnover { 
        public int One { get; set; }
        public int Two { get; set; }
        public int Three { get; set; }
        public int Four { get; set; }
        public int Five { get; set; }
        public int Six { get; set; }
        public int Seven { get; set; }
        public int Eight { get; set; }
        public int Nine { get; set; }
        public int Ten { get; set; }
        public int Eleven { get; set; }
        public int Twelve { get; set; }
    }

    public class QuantitySoldOfManufacturer
    {
        public Manufacturer Manufacturer { get; set; }
        public int Quantity { get; set; }
    }
    public class ProductBestSelling
    {
        public Product Product { get; set; }
        public ProductAttribute ProductAttribute { get; set; }
        public int SellingQuantity { get; set; }

    }
}
