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
    public class CommentController : ApiController
    {
        CommentRepository commentRepo = new CommentRepository();

        [Route("{id}/comment", Name = "GetCommentByPost")]
        public IHttpActionResult Get(int id)
        {
            var blog_comments = commentRepo.GetAllByPostID(id);

            if (blog_comments == null)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            return Ok(blog_comments);
        }

        [Route("{id1}/comment/{id2}", Name = "GetCommentByPostAndId")]
        public IHttpActionResult Get(int id1, int id2)
        {
            var blog_comment = commentRepo.GetAllByPostID_CommentID(id1, id2);

            if (blog_comment == null)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            return Ok(blog_comment);
        }

        [Route("{id}/comment/comment_by/{commet_by}", Name = "GetCommentByPostIdCommentBy")]
        public IHttpActionResult Get(int id, string commet_by)
        {
            var comments = commentRepo.GetByPostIdCommentBy(id, commet_by);

            if (comments == null)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            return Ok(comments);
        }

        [Route("{id}/comment")]
        public IHttpActionResult Post(comment comment)
        {
            commentRepo.Insert(comment);
            string uri = Url.Link("GetCommentByPost", new { id = comment.id });
            return Created(uri, comment);
        }

        [Route("{id1}/comment/{id2}")]
        public IHttpActionResult Put([FromUri] int id1, [FromUri] int id2, [FromBody] comment comment)
        {
            var com = commentRepo.GetID(id2);
            comment.post_id = id1;
            comment.id = id2;
            comment.commet_by = com.commet_by;
            commentRepo.UpdateComment(comment);
            return Ok(comment);
        }

        [Route("{id1}/comment/{id2}")]
        public IHttpActionResult Delete(int id1, int id2)
        {
            commentRepo.Delete(id2);
            return StatusCode(HttpStatusCode.NoContent);
        }

    }
}
