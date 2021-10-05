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
        private readonly UserContext _userContext;

        public UserRepository(UserContext userContext) => _userContext = userContext;


        public User Create(User user)
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

        public User GetByEmail(string email) => _userContext.Users.FirstOrDefault(a => a.Email.Equals(email));


        public User GetById(int id) => _userContext.Users.FirstOrDefault(a => a.Id.Equals(id));

    }
}
