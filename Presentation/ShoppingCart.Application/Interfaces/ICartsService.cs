using ShoppingCart.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShoppingCart.Application.Interfaces
{
    public interface ICartsService
    {
        //Gets all cart entries associated to the email supplied
        IQueryable<CartViewModel> GetCarts(string email);

        //Gets cart instance 
        CartViewModel GetCartEntry(int id);

        void AddCartEntry(CartViewModel cartEntry);

        void DeleteCartEntry(int id);

    }
}
