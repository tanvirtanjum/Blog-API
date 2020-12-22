﻿using BlogAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BlogAPI.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected BlogDBEntities context = new BlogDBEntities();

        public void Delete(int id)
        {
            this.context.Set<T>().Remove(GetID(id));
            this.context.SaveChanges();
        }

        public T Get(string username)
        {
            return this.context.Set<T>().Find(username);
        }

        public T GetID(int id)
        {
            return this.context.Set<T>().Find(id);
        }

        public List<T> GetAll()
        {
            return this.context.Set<T>().ToList();
        }

        public void Insert(T entity)
        {
            this.context.Set<T>().Add(entity);
            this.context.SaveChanges();
        }

        public void Update(T entity)
        {
            this.context.Entry(entity).State = EntityState.Modified;
            this.context.SaveChanges();
        }
    }
}