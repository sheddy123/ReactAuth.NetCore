using System;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ReactAuth.NetCore.Models;
using ReactAuth.NetCore.Data.Dtos;

namespace ReactAuth.NetCore.Mappings
{
    public class Usermappings : Profile
    {
        public Usermappings()
        {
            CreateMap<User, RegisterDto>().ReverseMap();
        }
    }
}
