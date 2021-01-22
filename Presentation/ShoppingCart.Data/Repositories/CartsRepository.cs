using ShoppingCart.Data.Context;
using ShoppingCart.Domain.Interfaces;
using ShoppingCart.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShoppingCart.Data.Repositories
{
    public class CartsRepository : ICartsRepository
    {

        ShoppingCartDbContext _context;

        public CartsRepository(ShoppingCartDbContext context)
        {
            _context = context;
        }

        public int AddProductToCart(Cart newCartEntry)
        {
            //Creates a new cart entry to add under the current user
            
            _context.Carts.Add(newCartEntry);
            _context.SaveChanges(); //Saves permanently into the database
            return newCartEntry.Id;
        }

        public void DeleteFromCart(Cart cartEntry)
        {
            cartEntry.Product_FK = cartEntry.Product.ID;
            cartEntry.Product = null;
            _context.Carts.Remove(cartEntry);

            _context.SaveChanges();
            //throw new NotImplementedException();
        }

        public Cart GetCartEntry(int id)
        {
            //Gets a singular cart entry
            return _context.Carts.SingleOrDefault(x => x.Id == id);
            //throw new NotImplementedException();
        }

        public IQueryable<Cart> GetCartEntries(string email)
        {
            //Retrieves whole cart owned by a user
            return _context.Carts.Where(x => x.Email == email);
        }
    }
}
