using Application.Services.Repositories;
using AutoMapper;
using Core.Security.Dtos;
using Core.Security.Entities;
using Core.Security.JWT;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Command.Login
{
    public class LoginCommand : IRequest<AccessToken>
    {
        public string Email { get; set; }
        public class LoginCommandHandler : IRequestHandler<LoginCommand, AccessToken>
        {
            private readonly IUserRepository _userRepository;
            private readonly IMapper _mapper;
            private readonly ITokenHelper _tokenHelper;
            public LoginCommandHandler(IUserRepository userRepository, IMapper mapper, ITokenHelper tokenHelper)
            {
                _userRepository = userRepository;
                _mapper = mapper;
                _tokenHelper = tokenHelper;
            }

            public async Task<AccessToken> Handle(LoginCommand request, CancellationToken cancellationToken)
            {
                User? userToCheck = await _userRepository.GetAsync(u => u.Email == request.Email);
                var claims = await _userRepository.GetClaims(userToCheck);
                AccessToken accessToken = _tokenHelper.CreateToken(userToCheck, claims);
                return accessToken;
            }
        }
    }
}
