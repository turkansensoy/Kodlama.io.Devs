using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Core.Security.Entities;
using Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class UserRepository : EfRepositoryBase<User, BaseDbContext>,IUserRepository
    {
        public UserRepository(BaseDbContext context) : base(context)
        {
        }
        public async Task<List<OperationClaim>> GetClaims(User user)
        {
            using (var context = new BaseDbContext())
            {
                var result = await (from operationClaim in context.operationClaims
                             join userOperationClaim in context.userOperationClaims
                             on operationClaim.Id equals userOperationClaim.OperationClaimId
                             where userOperationClaim.UserId == user.Id
                             select new OperationClaim { Id = operationClaim.Id, Name = operationClaim.Name }).ToListAsync();
                return result;
            }
        }
    }
}
