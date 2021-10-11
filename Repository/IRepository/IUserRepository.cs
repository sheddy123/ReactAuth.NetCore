using ReactAuth.NetCore.Data;
using ReactAuth.NetCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReactAuth.NetCore.Repository.IRepository
{
    public interface IUserRepository
    {
        User Create(User user, UserContext _userContext);
        User GetByEmail(string email, UserContext _userContext);
        User GetById(int Id, UserContext _userContext);
    }
}
