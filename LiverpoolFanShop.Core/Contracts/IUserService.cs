using LiverpoolFanShop.Core.Models.Admin.User;

namespace LiverpoolFanShop.Core.Contracts
{
    public interface IUserService
    {
        Task<string> UserFullNameAsync(string userId);

        Task<IEnumerable<UserServiceModel>> AllAsync();
    }
}
