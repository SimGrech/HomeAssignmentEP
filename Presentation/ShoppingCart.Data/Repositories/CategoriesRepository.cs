using ShoppingCart.Data.Context;
using ShoppingCart.Domain.Interfaces;
using ShoppingCart.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShoppingCart.Data.Repositories
{
    public class CategoriesRepository : ICategoriesRepository
    {

        ShoppingCartDbContext _context;

        public CategoriesRepository(ShoppingCartDbContext context) {
            _context = context;
        }

        public IQueryable<Category> GetCategories()
        {
            return _context.Categories;
        }

        public Category GetCategory(int id)
        {
            return _context.Categories.SingleOrDefault(x => x.Id == id);
        }

        public int AddCategory(Category category)
        {
            //Shopping
            _context.Categories.Add(category);
            _context.SaveChanges(); //Saves permanently into the database
            return category.Id;
        }
    }
}
