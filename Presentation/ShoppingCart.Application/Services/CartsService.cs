using AutoMapper;
using ShoppingCart.Application.Interfaces;
using ShoppingCart.Application.ViewModels;
using ShoppingCart.Domain.Interfaces;
using System;
using ShoppingCart.Domain.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper.QueryableExtensions;

namespace ShoppingCart.Application.Services
{

    public class CartsService : ICartsService
    {

        private ICartsRepository _cartsRepo;
        private IMapper _mapper;

        public CartsService(ICartsRepository cartsRepository, IMapper mapper)
        {
            _cartsRepo = cartsRepository;
            _mapper = mapper;
        }


        public void AddCartEntry(CartViewModel cartEntry)
        {
            var myCartEntry = _mapper.Map<Cart>(cartEntry);
            //This is done to avoid an error from the automapper, which is trying to insert a product
            myCartEntry.Product_FK = myCartEntry.Product.ID;
            myCartEntry.Product = null;
            _cartsRepo.AddProductToCart(myCartEntry);
        }

        public CartViewModel GetCartEntry(int id)
        {
            
                var cartEntry = _cartsRepo.GetCartEntry(id);
                CartViewModel myCartEntry = _mapper.Map<CartViewModel>(cartEntry);
                
                return myCartEntry;
            
        }
        
        public void DeleteCartEntry(int id)
        {
            var cartEntryToDelete = _cartsRepo.GetCartEntry(id);
            
            if (cartEntryToDelete != null)
            {
                _cartsRepo.DeleteFromCart(cartEntryToDelete);
            }            
        }

        public IQueryable<CartViewModel> GetCarts(string email)
        {
            var cartEntries = _cartsRepo.GetCartEntries(email).ProjectTo<CartViewModel>(_mapper.ConfigurationProvider);

            return cartEntries;
            
        }
    }
}
