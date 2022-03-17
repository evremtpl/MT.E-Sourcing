using AutoMapper;
using MT.E_Sourcing.Order.Application.Commands.OrderCreate;
using MT.E_Sourcing.Order.Application.Responses;

namespace MT.E_Sourcing.Order.Application.Mapper
{
   public class OrderMappingProfile : Profile
    {
        public OrderMappingProfile()
        {
            CreateMap<Domain.Entities.Order, OrderCreateCommand>().ReverseMap();
            CreateMap<Domain.Entities.Order, OrderResponse>().ReverseMap();
        }
    }
}
