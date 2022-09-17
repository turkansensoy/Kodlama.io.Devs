using Application.Features.Users.Commands.CreateUser;
using AutoMapper;
using Core.Security.Dtos;
using Core.Security.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, CreateUserCommand>().ReverseMap();
            CreateMap<User, UserForRegisterDto>().ReverseMap();
        }
    }
}
