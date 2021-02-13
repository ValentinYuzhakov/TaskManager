using System;
using System.Threading.Tasks;
using TaskManager.Data.Repositories.Interfaces;
using TaskManager.Domain.Models;

namespace TaskManager.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        readonly DatabaseContext context;


        public UserRepository(DatabaseContext context)
        {
            this.context = context;
        }


        public async Task CreateAsync(User entity)
        {
            await context.AddAsync(entity);
        }

        public async Task DeleteAsync(User entity)
        {
            await Task.Run(() => context.Remove(entity));
        }

        public async Task<User> GetAsync(Guid entityId)
        {
            return await context.Users.FindAsync(entityId);
        }

        public async Task UpdateAsync(User entity)
        {
            await Task.Run(() => context.Update(entity));
        }

        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}
