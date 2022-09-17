using Application.Features.Auth.Command.Login;
using Application.Features.Auth.Command.Register;
using Application.Features.Users.Commands.CreateUser;
using AutoMapper;
using Core.Security.Dtos;
using Core.Security.Entities;
using Core.Security.JWT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AccessToken, LoginCommand>().ReverseMap();
            CreateMap<AccessToken, RegisterCommand>().ReverseMap();
            CreateMap<AccessToken,UserForRegisterDto>().ReverseMap();
            CreateMap<AccessToken, UserForLoginDto>().ReverseMap();
        }
    }
}
