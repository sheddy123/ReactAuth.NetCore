using ReactAuth.NetCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReactAuth.NetCore.Repository.IRepository
{
    public interface IUserRepository
    {
        User Create(User user);
        User GetByEmail(string email);
    }
}
