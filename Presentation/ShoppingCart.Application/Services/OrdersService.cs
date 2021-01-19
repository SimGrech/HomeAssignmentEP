using AutoMapper;
using AutoMapper.QueryableExtensions;
using ShoppingCart.Application.Interfaces;
using ShoppingCart.Application.ViewModels;
using ShoppingCart.Domain.Interfaces;
using ShoppingCart.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShoppingCart.Application.Services
{
    public class OrdersService : IOrdersService
    {
        private ICartsRepository _cartsRepo;
        private IOrdersRepository _orderRepo;
        private IMapper _mapper;

        public OrdersService(ICartsRepository cartsRepository, IOrdersRepository ordersRepository, IMapper mapper)
        {
            _cartsRepo = cartsRepository;
            _orderRepo = ordersRepository;
            _mapper = mapper;
        }

        /* 
        Approach - storing items in cart table in db
        a) user must be logged in
        b) in the checkout method you need to fetch list of cart items from db
          
        */


        public void Checkout(string email)
        {
            //https://stackoverflow.com/a/2180920 Thread fix
            //1. Get a list of products that have been added to the cart for the given email (from the db)
            IList<CartViewModel> cartEntries = _cartsRepo.GetCartEntries(email).ProjectTo<CartViewModel>(_mapper.ConfigurationProvider).ToList<CartViewModel>();
            //2. loop within the list of products to check qty from the stock
            //Stock not showing up on the intellisense so I can't test with it
            foreach (var cart in cartEntries){
                //if(cart.Product.Stock < cart.Quantity){break;}
                //if you find a product with qty > stock - throw new exception not enough stock
            }

            //3. Create order
            Guid orderId = Guid.NewGuid();
            Order newOrder = new Order();
            newOrder.Id = orderId;
            newOrder.DatePlaced = DateTime.Today;
            newOrder.UserEmail = email;
            //Contineu setting up other properties

            //Might need to comment to test
            _orderRepo.AddOrder(newOrder);
            //3.1. Call the Addorder from inside the IOrdersRepository (this can be merged with step 3)

            //4. loop with the list of products and create an OrderDetail Instance for each of the products
            //Start of loop
            foreach (var cart in cartEntries)
            {
                OrderDetail newOrderDetail = new OrderDetail();
                newOrderDetail.OrderFK = orderId;
                newOrderDetail.ProductFK = cart.Product.Id;
                newOrderDetail.Price = (cart.Product.Price);
                newOrderDetail.Quantity = (cart.Quantity);
                //4.1
                //deduct qty from stock
                //detail.ProductFK = 

                //4.2 Loop Call the add orderDetail from inside the IOrderDetailsRepository (this can be merged with step 4)
                //Might need to comment for testing
                _orderRepo.AddOrderDetail(newOrderDetail);

                //end of loop
            }
            cartEntries = null;
            IList<Cart> cartEntriesToDelete = _cartsRepo.GetCartEntries(email).ToList<Cart>();
            //Remove Cart Entry
            foreach (var cart in cartEntriesToDelete)
            {
                _cartsRepo.DeleteFromCart(cart);
            }
            //throw new NotImplementedException();
        }
    }
}
