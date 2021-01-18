using ShoppingCart.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShoppingCart.Domain.Interfaces
{
    public interface ICartsRepository
    {
        IQueryable<Cart> GetCartEntries(string email);

        Cart GetCartEntry(int id);

        void DeleteFromCart(Cart cart);

        int AddProductToCart(string email, Product product, int quantity);
    }
}
