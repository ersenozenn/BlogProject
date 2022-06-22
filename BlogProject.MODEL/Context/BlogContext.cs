using BlogProject.MODEL.Entities;
using BlogProject.MODEL.Maps;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.MODEL.Context
{
    public class BlogContext : DbContext
    {
        public BlogContext(DbContextOptions<BlogContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CategoryMap());
            modelBuilder.ApplyConfiguration(new PostMap());
            modelBuilder.ApplyConfiguration(new UserMap());

            //Do not delete
            base.OnModelCreating(modelBuilder);
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("SQL SERVER CONNECTION STRING")
        //    base.OnConfiguring(optionsBuilder);
        //}
        public DbSet<Category>Categories { get; set; }
        public DbSet<Post> Posts { get; set; }  
        public DbSet<User> Users { get; set; }  
                

    }
}
