﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using LMS_1701LoginAPI.DAL;
using DM = LMS_1701LoginAPI.Models;
using AutoMapper;
using NLog;
using System.Web.Http.Cors;

namespace LMS_1701LoginAPI.Controllers
{
    
    public class LoginController : ApiController
    {
        private UserScoresLoginEntities1 db = new UserScoresLoginEntities1();
        private static Logger logger = LogManager.GetCurrentClassLogger();

        // GET: api/Login
        public IEnumerable<DM.User> GetUsers()
        {

            logger.Trace("http request to get users from database");
            var temp = db.Users.ToList();
            List<DM.User> users = new List<Models.User>();
            foreach (var item in temp)
            {
                users.Add(DM.ConvertEntityToModel.UserToModel(item));

            }
            return users;
        }

        // GET: api/Login/5
        [ResponseType(typeof(User))]
        public async Task<IHttpActionResult> GetUser(int id)
        {
            User user = await db.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        //GET
        [Route("api/Users/GetUser")]
        [ResponseType(typeof(User))]
        public async Task<IHttpActionResult> GetUser(string email)
        {
            List<User> users = await db.Users.ToListAsync();

            User user = null;

            foreach (User usrObj in users)
            {
                if (usrObj.email == email)
                {
                    user = usrObj;
                    break;
                }

            }

            if (user == null)
            {
                return NotFound();
            }

            Models.User usr = DM.ConvertEntityToModel.UserToModel(user);

            return Ok(usr);
        }

        // PUT: api/Login/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutUser(int id, User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != user.UserPK)
            {
                return BadRequest();
            }

            db.Entry(user).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Login
        [ResponseType(typeof(User))]
        public async Task<IHttpActionResult> PostUser(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Users.Add(user);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = user.UserPK }, user);
        }

        // DELETE: api/Login/5
        [ResponseType(typeof(User))]
        public async Task<IHttpActionResult> DeleteUser(int id)
        {
            User user = await db.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            db.Users.Remove(user);
            await db.SaveChangesAsync();

            return Ok(user);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserExists(int id)
        {
            return db.Users.Count(e => e.UserPK == id) > 0;
        }
    }
}