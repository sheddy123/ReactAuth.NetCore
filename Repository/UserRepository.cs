using ReactAuth.NetCore.Data;
using ReactAuth.NetCore.Models;
using ReactAuth.NetCore.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReactAuth.NetCore.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly UserContext _userContexts;

        public UserRepository(UserContext userContexts) => _userContexts = userContexts;


        public User Create(User user, UserContext _userContext)
        {
            try
            {
                _userContext.Add(user);
                user.Id = _userContext.SaveChanges();
                return user;
            }
            catch (Exception ex)
            {
                user.ErrorMessage = ex.InnerException.Message;
                return user;
            }
        }

        public User GetByEmail(string email, UserContext _userContext) => _userContext.Users.FirstOrDefault(a => a.Email.Equals(email));


        public User GetById(int id, UserContext _userContext) => _userContext.Users.FirstOrDefault(a => a.Id.Equals(id));

    }
}
