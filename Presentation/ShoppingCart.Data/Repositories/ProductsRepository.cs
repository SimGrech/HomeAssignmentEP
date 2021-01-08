using Microsoft.EntityFrameworkCore;
using ShoppingCart.Data.Context;
using ShoppingCart.Domain.Interfaces;
using ShoppingCart.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShoppingCart.Data.Repositories
{
    public class ProductsRepository : IProductsRepository
    {
        ShoppingCartDbContext _context;

        public ProductsRepository(ShoppingCartDbContext context) {
            _context = context;
        }


        public Guid AddProduct(Product p)
        {
            //Shopping
            _context.Products.Add(p);
            _context.SaveChanges(); //Saves permanently into the database
            return p.ID;
        }

        public void DeleteProduct(Product p)
        {
            _context.Products.Remove(p);
            _context.SaveChanges();
        }

        public void DisableProduct(Guid id) {
            var p = GetProduct(id);
            p.Disable = true;
            _context.SaveChanges();
        }

        public Product GetProduct(Guid id)
        {
            //Single or default will return ONE product. or null
            //return _context.Products.Include(x=>x.Category).SingleOrDefault(x => x.ID == id);
            return _context.Products.SingleOrDefault(x => x.ID == id);
        }

        public IQueryable<Product> GetProducts()
        {
            return _context.Products.Where(x => x.Disable == false);
        }
    }
}
