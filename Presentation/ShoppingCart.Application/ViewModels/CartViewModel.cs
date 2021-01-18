using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart.Application.ViewModels
{
    public class CartViewModel
    {
        public int Id { get; set; }

        public ProductViewModel Product { get; set; }

        public string Email { get; set; }

        public int Quantity { get; set; }
    }
}
