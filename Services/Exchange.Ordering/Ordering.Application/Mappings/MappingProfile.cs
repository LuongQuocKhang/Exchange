using AutoMapper;
using Ordering.Application.Features.Orders.Commands.CloseOrder;
using Ordering.Application.Features.Orders.Commands.PlaceOrder;
using Ordering.Application.Features.Orders.Commands.UpdateOrder;
using Ordering.Application.Models;
using Ordering.Domain.Entities;

namespace Ordering.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<TBL_ADM_ORDERS, OrdersViewModel>().ReverseMap();
            CreateMap<TBL_ADM_ORDERS, PlaceOrderCommand>().ReverseMap();
            CreateMap<TBL_ADM_ORDERS, UpdateOrderCommand>().ReverseMap();
        }
    }
}
