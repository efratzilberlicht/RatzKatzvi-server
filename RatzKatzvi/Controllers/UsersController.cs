using BL;
using Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RatzKatzvi.Controllers
{
    [RoutePrefix("api/users")]
    public class UsersController : ApiController
    {
        //GetAllUsers    
        public IHttpActionResult GetAllKeyWords()
        {
            return Ok(UsersBL.GetAllUsers());
        }
        //GetUserById
        [Route("GetUserById/{userId}")]
        public IHttpActionResult GetById(int userId)
        {
            try
            {
                return Ok(UsersBL.GetUserById(userId));
            }
            catch
            {
                return NotFound();
            }
        }
        //GeByUserName
        [Route("GeByUserName/{userName}")]
        public IHttpActionResult GeByUserName(string userName)
        {
            try
            {
                return Ok(UsersBL.GeByUserName(userName));
            }
            catch
            {
                return NotFound();
            }
        }
        // POST: api/Users
        [HttpPost, Route("LoginByUserName/{userName}/{password}")]
        public IHttpActionResult LoginByUserName(string userName, string password)
        {
            try
            {
                Users1 user = UsersBL.Login1(userName, password);
                if (user == null)
                    throw new Exception();
                return Ok(user);
            }
            catch
            {
                return NotFound();
            }
        }
        [HttpPost, Route("LoginByEmail/{email}/{password}")]
        public IHttpActionResult LoginByEmail(string email, string password)
        {
            try
            {
                Users1 user = UsersBL.Login2(email, password);
                if (user == null)
                    throw new Exception();
                return Ok(user);
            }
            catch
            {
                return NotFound();
            }
        }

        
        public IHttpActionResult Post([FromBody] Users1 user)
        {
            try
            {
                UsersBL.AddUser(user);
                return Ok();
            }
            catch
            {
                return Conflict();
            }
        }

        // PUT: api/Users/5
        //UPDATE
        [HttpPut]
        [Route("Put")]
        public IHttpActionResult Put([FromBody] Users1 user)
        {
            try
            {
                UsersBL.UpdateUser(user);
                return Ok();
            }
            catch
            {
                return NotFound();
            }
        }

        // DELETE: api/Users/5
        public IHttpActionResult Delete(int id, [FromBody] Users1 user)
        {
            try
            {
                UsersBL.DeleteUser(user);
                return Ok();
            }
            catch
            {
                return Conflict();
            }
        }
    }
}
