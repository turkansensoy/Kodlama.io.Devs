using Application.Services.Repositories;
using AutoMapper;
using Core.Security.Dtos;
using Core.Security.Entities;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Commands.CreateUser
{
    public class CreateUserCommand : IRequest<UserForRegisterDto>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserForRegisterDto>
        {
            private readonly IUserRepository _userRepository;
            private readonly IMapper _mapper;
            public CreateUserCommandHandler(IUserRepository userRepository, IMapper mapper)
            {
                _userRepository = userRepository;
                _mapper = mapper;
            }

            public async Task<UserForRegisterDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
            {
                User mappedUser = _mapper.Map<User>(request);
                User createdUser = await _userRepository.AddAsync(mappedUser);
                UserForRegisterDto userForRegisterDto = _mapper.Map<UserForRegisterDto>(createdUser);
                return userForRegisterDto;
            }
        }
    }
}
