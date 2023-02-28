using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InterviewQuestion.Repositories
{
    public interface ILoginRepository
    {
        Task<User> Login(Login login);

        Task<User> Userregistration(User user);
    }
}
