using DataAccessLayer;
using DataAccessLayer.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InterviewQuestion.DataContext
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
        public DbSet<Employee> Employee { get; set; }

        public DbSet<User> User { get; set; }
        public DbSet<TechnologyMaster> TechnologyMaster { get; set; }

        public DbSet<Questions> Questions { get; set; }
        public DbSet<Answers> Answers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

    }
}
