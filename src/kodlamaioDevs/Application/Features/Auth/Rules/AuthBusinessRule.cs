using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using Core.Security.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Rules
{
    public class AuthBusinessRules
    {
        private readonly IUserRepository _userRepository;
        public AuthBusinessRules(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task UserMailCanNotDuplicatedWhenInserted(string name)
        {
            IPaginate<User> result = await _userRepository.GetListAsync(b=>b.Email==name);
            if (result.Items.Any()) throw new BusinessException("User exists.");
        }
    }
}
