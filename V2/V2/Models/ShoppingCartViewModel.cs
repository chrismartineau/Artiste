using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace V2.Models
{
    public class ShoppingCartViewModel
    {
        public List<Achat> CartItems { get; set; }
        public decimal CartTotal { get; set; }
    }
}