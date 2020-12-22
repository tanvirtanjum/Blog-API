using BlogAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogAPI.Repositories
{
    public class BlogRepository : Repository<blog>
    {
        public void UpdateBlog(blog bl)
        {
            using (BlogDBEntities blog = new BlogDBEntities())
            {
                var toUp = blog.blogs.Where(x => x.id == bl.id).First<blog>();

                toUp.text = bl.text;

                blog.SaveChanges();

            }
        }

        public List<blog> GetByOwner(string id)
        {
            return this.context.Set<blog>().Where(x => x.post_by.ToLower().Contains(id.ToLower())).ToList();
        }
    }
}