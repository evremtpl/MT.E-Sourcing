using AutoMapper;
using MT.E_Sourcing.Common.Events.Concrete;
using MT.E_Sourcing.Sourcing.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MT.E_Sourcing.Sourcing.AutoMapper
{
    public class SourcingMapping : Profile
    {
        public SourcingMapping()
        {
            CreateMap<OrderCreateEvent, Bid>().ReverseMap();
        }
    }
}
