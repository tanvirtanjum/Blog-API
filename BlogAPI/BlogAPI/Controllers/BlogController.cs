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
    [RoutePrefix("api/blog")]
    public class BlogController : ApiController
    {
        BlogRepository blogRepo = new BlogRepository();
        CommentRepository commentRepo = new CommentRepository();

        [Route("")]
        public IHttpActionResult Get()
        {
            return Ok(blogRepo.GetAll());
        }

        [Route("{id}", Name = "GetblogByID")]
        public IHttpActionResult Get(int id)
        {
            var blog = blogRepo.GetID(id);

            if (blog == null)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            return Ok(blog);
        }

        [Route("post_by/{post_by}", Name = "GetPostBy")]
        public IHttpActionResult Get(string post_by)
        {
            var blog = blogRepo.GetByOwner(post_by);

            if (blog == null)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            return Ok(blog);
        }

        [Route("")]
        public IHttpActionResult Post(blog blog)
        {
            blogRepo.Insert(blog);
            string uri = Url.Link("GetblogByID", new { id = blog.id });
            return Created(uri, blog);
        }

        [Route("{id}")]
        public IHttpActionResult Put([FromUri] int id, [FromBody] blog blog)
        {
            var com = blogRepo.GetID(id);
            blog.id = id;
            blog.post_by = com.post_by;
            blogRepo.UpdateBlog(blog);
            return Ok(blog);
        }

        [Route("{id}")]
        public IHttpActionResult Delete(int id)
        {
            commentRepo.DeleteAllCommentFromPost(id);
            blogRepo.Delete(id);
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
