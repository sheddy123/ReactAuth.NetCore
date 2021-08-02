using Microsoft.EntityFrameworkCore;
using ReactAuth.NetCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReactAuth.NetCore.Data
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {

        }

        DbSet<User> Users { get; set; }
    }
}
