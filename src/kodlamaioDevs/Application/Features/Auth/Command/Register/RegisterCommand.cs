using Application.Features.Auth.Rules;
using Application.Features.Users.Commands.CreateUser;
using Application.Services.Repositories;
using AutoMapper;
using Core.Security.Dtos;
using Core.Security.Entities;
using Core.Security.Hashing;
using Core.Security.JWT;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Command.Register
{
    public class RegisterCommand : IRequest<AccessToken>
    {
        public string Password { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public class RegisterCommandHandler : IRequestHandler<RegisterCommand, AccessToken>
        {
            private readonly IUserRepository _userRepository;
            private readonly IMapper _mapper;
            private readonly AuthBusinessRules _authBusinessRules;
            private readonly ITokenHelper _tokenHelper;
            public RegisterCommandHandler(IUserRepository userRepository, IMapper mapper,AuthBusinessRules authBusinessRules,ITokenHelper tokenHelper)
            {
                _userRepository = userRepository;
                _mapper = mapper;
                _authBusinessRules = authBusinessRules;
                _tokenHelper = tokenHelper;
            }

            public async Task<AccessToken> Handle(RegisterCommand request, CancellationToken cancellationToken)
            {
                byte[] passwordHash, passwordSalt;
                HashingHelper.CreatePasswordHash(password: request.Password, out passwordHash, out passwordSalt);
                var user = new User
                {
                    Email = request.Email,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                    Status = true
                };
                await _authBusinessRules.UserMailCanNotDuplicatedWhenInserted(request.Email);
                User createduser = await _userRepository.AddAsync(user);
                UserForRegisterDto userForRegisterDto = _mapper.Map<UserForRegisterDto>(createduser);
               var claims= await _userRepository.GetClaims(user);
                AccessToken accessToken = _tokenHelper.CreateToken(user,claims);
                return accessToken;
            }
        }
    }
}
