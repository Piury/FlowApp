using FlowApi.Models.UserModels;
using FlowAPI.Services.Context;
using Microsoft.EntityFrameworkCore;

namespace FlowAPI.FlowDb.UserDb;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;
    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(UserModel entity)
    {
        await _context.User.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(UserModel entity)
    {
        _context.User.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<UserModel>> GetAllAsync()
    {
        return await _context.User.ToListAsync();
    }

    public async Task<UserModel> GetByIdAsync(string id)
    {
        return await _context.User.Where(user => user.Id.ToString() == id).FirstOrDefaultAsync();
    }
    public async Task<UserModel> GetByEmailAsync(string email)
    {
        return await _context.User.Where(user => user.Email.ToLower() == email.ToLower()).FirstOrDefaultAsync();
    }

    public async Task UpdateAsync(UserModel entity)
    {
        _context.User.Update(entity);
        await _context.SaveChangesAsync();
    }
}