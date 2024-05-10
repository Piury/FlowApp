using FlowApi.Models.UserModels;

namespace FlowAPI.FlowDb.UserDb;

public interface IUserRepository : IRepository<UserModel>
{
    Task<UserModel> GetByEmailAsync(string email);
}