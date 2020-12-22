using BlogAPI.Repositories;
using BlogAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BlogAPI.Controllers
{
    [RoutePrefix("api/login")]
    public class LoginController : ApiController
    {
        LoginRepository loginRepo = new LoginRepository();

        [Route("")]
        public IHttpActionResult Get()
        {
            return Ok(loginRepo.GetAll());
        }

        [Route("{username}", Name = "GetLoginByUsername")]
        public IHttpActionResult Get(string username)
        {
            var log = loginRepo.Get(username);

            if (log == null)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            return Ok(log);
        }

        [Route("")]
        public IHttpActionResult Post(login login)
        {
            loginRepo.Insert(login);
            string uri = Url.Link("GetLoginByUsername", new { username = login.username });
            return Created(uri, login);
        }
    }
}
