using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity
{
    public class AppIdentityDbContextSeed
    {
        public static async Task SeedUsersAsync(UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new AppUser
                {
                    DisplayName = "John",
                    Email = "john@doe.com",
                    UserName = "john@doe.com",
                    Address = new Address
                    {
                        FirstName = "John",
                        LastName = "Doe",
                        Street = "Fu Bar Street",
                        City = "Town",
                        Zipcode = "12345"
                    }
                };
                
                await userManager.CreateAsync(user, "Pa$$w0rd");
            }
        }
    }
}