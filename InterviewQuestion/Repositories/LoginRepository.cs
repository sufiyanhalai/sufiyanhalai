using DataAccessLayer;
using InterviewQuestion.DataContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InterviewQuestion.Repositories
{
    public class LoginRepository : ILoginRepository
    {

        private readonly ApplicationDbContext _DbContext;


        public LoginRepository(ApplicationDbContext DbContext)
        {
            _DbContext = DbContext;
        }

        public async Task<User> Login(Login login)
        {
            var getid = await _DbContext.User.Where(x => x.EmailId == login.EmailId && x.Password == login.Password).FirstOrDefaultAsync();

            return getid;
        }

        public async Task<User> Userregistration(User user)
        {
            var userdetails = await _DbContext.User.AddAsync(user);
            await _DbContext.SaveChangesAsync();
            return userdetails.Entity;
        }
    }
}
