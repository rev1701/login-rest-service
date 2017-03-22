using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS_1701LoginAPI.Models
{
    public class User
    {
        public User()
        {

        }

        public User(int uid, string fn, string ln, int? ut, string e, string p)
        {
            this.UserId = uid;
            this.Email = e;
            this.Password = p;
            this.FirstName = fn;
            this.LastName = ln;
            this.UserType = ut;
            
        }
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? UserType { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

    }
}