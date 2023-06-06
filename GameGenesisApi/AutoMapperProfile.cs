using AutoMapper;
using GameGenesisApi.Dtos.Account;
using GameGenesisApi.Dtos.Basket;
using GameGenesisApi.Dtos.Category;
using GameGenesisApi.Dtos.Image;
using GameGenesisApi.Dtos.Library;
using GameGenesisApi.Dtos.Product;
using GameGenesisApi.Dtos.Shop;
using GameGenesisApi.Models;

namespace GameGenesisApi
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() 
        {
            CreateMap<Account, GetAccountDto>();
            CreateMap<AddAccountDto, Account>();
            CreateMap<Basket, GetBasketDto>();
            CreateMap<AddbasketDto, Basket>();
            CreateMap<ProductCategory, GetCategoryDto>();
            CreateMap<AddCategoryDto, ProductCategory>();
            CreateMap<Library, GetLibraryDto>();
            CreateMap<AddLibraryDto, Library>();
            CreateMap<Product, GetProductDto>();
            CreateMap<AddProductDto, Product>();
            CreateMap<Shop, GetShopDto>();
            CreateMap<AddShopDto, Shop>();
            CreateMap<AddImageDto, Image>();
            CreateMap<Image, GetImageDto>();
        }

    }
}
