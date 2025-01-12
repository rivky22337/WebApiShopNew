using AutoMapper;
using Entities;
using DTO;
using Entities.Models;
using AutoMapper;

namespace MyShop
{
    public class Mapper : Profile
    {
        public Mapper()
        {

            CreateMap<User,FullUserDTO>();
            CreateMap< FullUserDTO,User>();

            CreateMap<User,ReturnUserDTO>();
            CreateMap<LoginUserDTO,User >();

            CreateMap<Order, GetOrderDTO>();

            CreateMap<PostOrderDTO, Order>();
            CreateMap<OrderItemDTO, OrderItem>();

            CreateMap<OrderItem, OrderItemDTO>();
            //CreateMap<OrderItemDTO, OrderItem>();





            CreateMap<Product, SingleProductDTO>();
            CreateMap<Product, ListProductDTO>().ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.ToString()));


            CreateMap<Category, CategoryDTO>();
        }
        //CreateMap<Product, ProductDto>();
        //    CreateMap<User, UserByIdDto>();
        //    CreateMap<addUserDto, User>();
        //    CreateMap<UserByIdDto, User>();
        //    CreateMap<addUserDto, returnPostUserDto>();
        //    CreateMap<User, returnLoginUserDto>();
        //    CreateMap<Category, getCategoryDto>();
        //    CreateMap<OrderItem, OrderItemDto>();
        //    CreateMap<OrderItemDto, OrderItem>();

        //    CreateMap<Order, OrderPostDto>();
        //    CreateMap<OrderPostDto, Order>();

        //    CreateMap<Order, returnOrderDto>();
        //    CreateMap<OrderPostDto, returnOrderDto>();

    }
}
