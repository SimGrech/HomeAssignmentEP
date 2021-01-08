using AutoMapper;
using ShoppingCart.Application.ViewModels;
using ShoppingCart.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart.Application.AutoMapper
{
    //Convert
    //Application (model) >> Domain (Classes)
    public class DomainToViewModelProfile: Profile
    {
        public DomainToViewModelProfile() {
            CreateMap<Product, ProductViewModel>();//Since names are the same .ForMember(x=>x.Name, opt=>opt.MapFrom(src=>src.Name));
            //Informing the automapper library that we are mapping (linking) Product Class onto the ProductViewModel
            
            CreateMap<Category, CategoryViewModel>();

            CreateMap<Member, MemberViewModel>();
        }
    }
}
