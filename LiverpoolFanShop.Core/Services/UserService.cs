using LiverpoolFanShop.Core.Contracts;
using LiverpoolFanShop.Core.Models.Admin.User;
using LiverpoolFanShop.Infrastructure.Data.Common;
using LiverpoolFanShop.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiverpoolFanShop.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository repository;

        public UserService(IRepository _repository)
        {
            repository = _repository;
        }
        public async Task<IEnumerable<UserServiceModel>> AllAsync()
        {
            var users = await repository.AllReadOnly<ApplicationUser>()
                .ToListAsync();

            return users.Select(u => new UserServiceModel
            {
                Email = u.Email ?? string.Empty,
                FullName = $"{u.FirstName} {u.LastName}"
            });
        }

        public async Task<string> UserFullNameAsync(string userId)
        {
            var result = string.Empty;
            var user = await repository.GetByIdAsync<ApplicationUser>(userId);

            if(user != null)
            {
                result = $"{user.FirstName} {user.LastName}";
            }

            return result;
        }
    }
}
