using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class ViewOrder
    {
        public List<Order> Orders { get; set; }
        public Customer Customer { get; set; }
    }
}
