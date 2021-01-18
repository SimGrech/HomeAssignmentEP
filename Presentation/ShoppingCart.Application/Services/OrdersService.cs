using ShoppingCart.Application.Interfaces;
using ShoppingCart.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart.Application.Services
{
    public class OrdersService : IOrdersService
    {
        /* 
        Approach - storing items in cart table in db
        a) user must be logged in
        b) in the checkout method you need to fetch list of cart items from db
          
        */


        public void Checkout(string email)
        {
            //1. Get a list of products that have been added to the cart for the given email (from the db)

            //2. loop within the list of products to check qty from the stock
            //if you find a product with qty > stock - throw new exception not enough stock

            //3. Create order
            Guid orderId = Guid.NewGuid();
            Order newOrder = new Order();
            newOrder.Id = orderId;
            newOrder.UserEmail = email;
            //Contineu setting up other properties


            //4. loop with the list of products and create an OrderDetail Instance for each of the products
            //Start of loop
            OrderDetail detail = new OrderDetail();
            detail.OrderFK = orderId;

            //4.1
            //deduct qty from stock
            //detail.ProductFK = 


            //Continue setting up other properties

            //end of loop

            //3.1. Call the Addorder from inside the IOrdersRepository (this can be merged with step 3)

            //4.2 Loop Call the add orderDetail from inside the IOrderDetailsRepository (this can be merged with step 4)



            //throw new NotImplementedException();
        }
    }
}
