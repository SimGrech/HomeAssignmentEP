using AutoMapper;
using ShoppingCart.Application.ViewModels;
using ShoppingCart.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart.Application.AutoMapper
{
    //Convert
    //Domain (classes) >>>>>>> Application (model)
    public class ViewModelToDomainProfile : Profile
    {
        public ViewModelToDomainProfile() {
            CreateMap<ProductViewModel, Product>();
            CreateMap<CategoryViewModel, Category>();
            CreateMap<MemberViewModel, Member>();
        }


    }
}
