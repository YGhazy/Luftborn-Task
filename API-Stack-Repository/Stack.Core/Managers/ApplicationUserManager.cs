using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Stack.DAL;
using Stack.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stack.Core.Managers
{
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public DbSet<ApplicationUser> dbSet;
        public ApplicationDbContext context;
        public ApplicationUserManager(ApplicationDbContext _context, IUserStore<ApplicationUser> store, IOptions<IdentityOptions> optionsAccessor, IPasswordHasher<ApplicationUser> passwordHasher, IEnumerable<IUserValidator<ApplicationUser>> userValidators, IEnumerable<IPasswordValidator<ApplicationUser>> passwordValidators, ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManager<ApplicationUser>> logger) : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {

            dbSet = _context.Set<ApplicationUser>();
            context = _context;

        }


        public async Task<ApplicationUser> GetCustomerById(string userId)
        {
            return await Task.Run(() =>
            {
                var usersResult = dbSet.Where(a => a.Id == userId).Include(a=>a.Vehicles);
                if (usersResult.ToList().Count != 0)
                {
                    var applicationUserToReturn = usersResult.FirstOrDefault();
                    return applicationUserToReturn;
                }
                else
                {
                    return null;
                }
            });
        }

        public async Task<ApplicationUser> GetCustomerByUserName(string UserName)
        {
            return await Task.Run(() =>
            {
                var usersResult = dbSet.Where(a => a.UserName == UserName);
                if (usersResult.ToList().Count != 0)
                {

                    var applicationUserToReturn = usersResult.FirstOrDefault();

                    return applicationUserToReturn;

                }
                else
                {
                    return null;
                }
            });
        }


        public async Task<List<ApplicationUser>> GetAllCustomersExcept(string userId)
        {
            return await Task.Run(() =>
            {
                var applicationUser = dbSet.Where(a => a.Id != userId);
                if (applicationUser.ToList().Count != 0)
                    return applicationUser.ToList();
                else
                    return null;

            });
        }


        public async Task<List<ApplicationUser>> GetAllCustomers()
        {
            List<ApplicationUser> users = new List<ApplicationUser>();

            users = (from user in context.Users
                     join userRoles in context.UserRoles on user.Id equals userRoles.UserId
                     join role in context.Roles on userRoles.RoleId equals role.Id
                     where role.Name == "Customer"
                     select new ApplicationUser
                     {
                         Id = user.Id,
                         Email = user.Email,
                         PasswordHash = user.PasswordHash,
                         UserName = user.UserName,
                  
                     }).ToList();

            return await Task.Run(() =>
            {
                if (users != null && users.Count > 0)
                    return users.ToList();
                else
                    return null;
            });
        }
    }
}
