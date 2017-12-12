using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShBazmool.Models;

namespace ShBazmool.Models
{
    public class UserManager
    {
        private readonly ShBazmoolDbContext dbContext;

        public UserManager(ShBazmoolDbContext context)
        {
            dbContext = context;
        }

        public bool IsValid(string username, string password)
        {
            return dbContext.Users.Any(u => u.UserName == username && u.Password == password);
        }
    }
}
