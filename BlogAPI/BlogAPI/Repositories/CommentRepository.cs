using BlogAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogAPI.Repositories
{
    public class CommentRepository : Repository<comment>
    {
        public List<comment> GetAllByPostID(int id)
        {
            return this.GetAll().Where(x => x.post_id == id).ToList();
        }

        public List<comment> GetAllByPostID_CommentID(int id1, int id2)
        {
            return this.GetAll().Where(x => x.post_id == id1).Where(x => x.id == id2).ToList();
        }

        public void UpdateComment(comment comment)
        {
            using (BlogDBEntities blog = new BlogDBEntities())
            {
                var toUp = blog.comments.Where(x => x.id == comment.id).First<comment>();

                toUp.comment1 = comment.comment1;

                blog.SaveChanges();

            }
        }

        public void DeleteAllCommentFromPost(int id)
        {
            using (var blog = new BlogDBEntities())
            {
                int noOfRowDeleted = blog.Database.ExecuteSqlCommand("delete from comments where post_id = '"+id+"';");
            }
        }

        public List<comment> GetByPostIdCommentBy(int id, string commet_by)
        {
            return this.context.Set<comment>().Where(x => x.post_id == id && x.commet_by.ToLower().Contains(commet_by.ToLower())).ToList();
        }
    }
}