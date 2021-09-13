using AutoMapper;
using stone.contabancaria.api.application.Models;
using stone.contabancaria.api.core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace stone.contabancaria.api.application.Mapper
{
    public class ContaCorrenteProfile : Profile
    {
        public ContaCorrenteProfile()
        {
            CreateMap<ContaCorrentePostModel, ContaCorrente>();
        }
        
    }
}
