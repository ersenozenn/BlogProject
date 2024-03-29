﻿using BlogProject.CORE.Entity;
using BlogProject.CORE.Service;
using BlogProject.MODEL.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using BlogProject.CORE.Entity.Enums;

namespace BlogProject.SERVICE.Base
{
    public class BaseService<T> : ICoreService<T> where T : CoreEntity
    {
        private readonly BlogContext context;
        public BaseService(BlogContext _context)
        {
            this.context = _context;
        }
    
        public bool Activate(Guid id)
        {
            T activated = GetByID(id);
            activated.Status = Status.Active;
            return Update(activated);
        }

        public bool Add(T item)
        {
            try
            {
                context.Set<T>().Add(item);
                return Save() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Add(List<T> item)
        {
            try
            {
                using (TransactionScope ts= new TransactionScope() )
                {
                    context.Set<T>().AddRange(item);
                    ts.Complete();
                    return Save() > 0;  
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        //public bool Any(Expression<Func<T, bool>> exp)
        //{
        //    return context.Set<T>().Any(exp);
        //}
        public bool Any(Expression<Func<T, bool>> exp)=>context.Set<T>().Any(exp);

        public List<T> GetActive()=>context.Set<T>().Where(x=>x.Status==Status.Active || x.Status ==Status.Updated).ToList();
        

        public List<T> GetAll() => context.Set<T>().ToList();

        public T GetbyDefault(Expression<Func<T, bool>> exp) => context.Set<T>().FirstOrDefault(exp);


        public T GetByID(Guid id)
        {
            return context.Set<T>().Find(id);
        }

        public List<T> GetDefault(Expression<Func<T, bool>> exp) => context.Set<T>().Where(exp).ToList();
        

        public bool Remove(T item)
        {
            item.Status = Status.Deleted;
            return Update(item);
        }

        public bool Remove(Guid item)
        {
            try
            {
                using (TransactionScope ts=new TransactionScope())
                {
                    T removedItem = GetByID(item);
                    removedItem.Status = Status.Deleted;
                    ts.Complete();return Update(removedItem);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool RemoveAll(Expression<Func<T, bool>> exp)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    var collection = GetDefault(exp);
                    int count = 0;

                    foreach (var item in collection)
                    {
                        item.Status = Status.Deleted;
                        bool opResult=Update(item);

                        if (opResult) count++;
                    }
                    if (collection.Count == count)
                        ts.Complete();
                    else
                        return false;
                }
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public int Save()
        {
            return context.SaveChanges();
        }

        public bool Update(T item)
        {
            try
            {
                context.Set<T>().Update(item);
                return Save() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
