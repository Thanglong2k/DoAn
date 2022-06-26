using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Model
{
    public  class ViewOrderDetail
    {
        public OrderDetail OrderDetail { get; set; }
        public Product Product { get; set; }
    }
}
