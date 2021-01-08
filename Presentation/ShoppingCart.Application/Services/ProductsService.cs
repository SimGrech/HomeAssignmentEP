using AutoMapper;
using AutoMapper.QueryableExtensions;
using ShoppingCart.Application.Interfaces;
using ShoppingCart.Application.ViewModels;
using ShoppingCart.Domain.Interfaces;
using ShoppingCart.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace ShoppingCart.Application.Services
{
    public class ProductService : IProductsService
    {
        private IProductsRepository _productsRepo;
        private IMapper _mapper;

        public ProductService(IProductsRepository productsRepository, IMapper mapper)
        {
            _productsRepo = productsRepository;
            _mapper = mapper;
        }

        public void AddProduct(ProductViewModel product)
        {
            //Changing this using automapper later on

            //Ctrl + .

            //Old Method
            /*
            Product newProduct = new Product()
            {
                Description = product.Description,
                Name = product.Name,
                Price = product.Price,
                CategoryId = product.Category.Id,
                ImageUrl = product.ImageUrl

            };
            */
            var myProduct = _mapper.Map<Product>(product);

            //This is done to avoid an error from the automapper, which is trying to insert a category
            myProduct.Category = null;
            _productsRepo.AddProduct(myProduct);
        }

        public ProductViewModel GetProduct(Guid id)
        {
            var myProduct = _productsRepo.GetProduct(id);
            ProductViewModel myModel = new ProductViewModel();
            myModel.ImageUrl = myProduct.ImageUrl;
            myModel.Name = myProduct.Name;
            myModel.Price = myProduct.Price;
            myModel.Id = myProduct.ID;
            myModel.Category = new CategoryViewModel() {
                Id = myProduct.Category.Id,
                Name = myProduct.Category.Name
            };
            return myModel;
        }

        public IQueryable<ProductViewModel> GetProducts()
        {
            var products = _productsRepo.GetProducts().ProjectTo<ProductViewModel>(_mapper.ConfigurationProvider);
            return products;
            //To check wether this works
            //Demonstrate alternative way with ProjectTo
            //var products = _productsRepo.GetProducts();
            //var result = _mapper.Map<IQueryable<Product>, IQueryable<ProductViewModel>>(products);

            //return result;

            //Different Method for mapping
            //var products2 = _productsRepo.GetProducts().ProjectTo<ProductViewModel>(_mapper.ConfigurationProvider);

            //Domain >> ViewModels
            //Old Method used
            /*
            var list = from p in _productsRepo.GetProducts()
                       select new ProductViewModel()
                       {
                           Id = p.ID,
                           Description = p.Description,
                           Name = p.Name,
                           Price = p.Price,
                           Category = new CategoryViewModel() { Id = p.Category.Id, Name = p.Category.Name },
                           ImageUrl = p.ImageUrl
                       };
            return list;
            */
            
        }

        public IQueryable<ProductViewModel> GetProducts(string keyword)
        {
            var products = _productsRepo.GetProducts().Where(x=>x.Description.Contains(keyword) || x.Name.Contains(keyword))
                .ProjectTo<ProductViewModel>(_mapper.ConfigurationProvider);

            return products;
        }

        public IQueryable<ProductViewModel> GetProducts(int category)
        {
            var list = from p in _productsRepo.GetProducts().Where(x => x.Category.Id == category)
                       select new ProductViewModel()
                       {
                           Id = p.ID,
                           Description = p.Description,
                           Name = p.Name,
                           Price = p.Price,
                           Category = new CategoryViewModel() { Id = p.Category.Id, Name = p.Category.Name },
                           ImageUrl = p.ImageUrl
                       };
            return list;
        }

        public void DeleteProduct(Guid id)
        {
            var pToDelete = _productsRepo.GetProduct(id);

            if (pToDelete != null) {
                _productsRepo.DeleteProduct(pToDelete);
            } 

        }

        public void DisableProduct(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
