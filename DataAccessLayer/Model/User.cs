using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer
{
    public class User
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string EmailId { get; set; }

        public string Password { get; set; }
    }
}
