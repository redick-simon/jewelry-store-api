using JewelryStoreApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JewelryStoreApi.Data
{
    public static class DbInitializer
    {
        public static void Initialize(JewelryDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.Users.Any())
            {
                return;
            }

            var users = new List<User>
            {
                new User{Username="norm", Password="Pasword2", UserType = UserType.NormalUser},
                new User{Username="special", Password="PrePass!!", UserType = UserType.PrivilegedUser}
            };

            foreach(var user in users)
            {
                context.Users.Add(user);
            }

            context.SaveChanges();

        }
    }
}
