using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DM = LMS_1701LoginAPI.Models;

namespace LMS_1701LoginAPI.Models
{
    public class ConvertEntityToModel
    {
        public static DM.User UserToModel(LMS_1701LoginAPI.DAL.User entity)
        {
            DM.User result = new Models.User(entity.UserPK, entity.fname, entity.lname, entity.UserType, entity.email, entity.password);
            return result;
        }

    }
}