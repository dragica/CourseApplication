using CourseApplication.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace CourseApplication.Persistance.Contexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Course> Course { get; set; }
        public DbSet<Company> Company { get; set; }
        public DbSet<Participant> Participant { get; set; }
        public DbSet<CourseDate> CourseDate { get; set; }
        public DbSet<Domain.Models.CourseApplication> CourseApplication { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>()
           .HasMany(p => p.Dates)
           .WithOne(p => p.Course)
           .HasForeignKey(p => p.CourseId)
           .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Company>()
           .HasMany(p => p.Participants)
           .WithOne(p => p.Company)
           .HasForeignKey(p => p.CompanyId)
           .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CourseDate>()
           .HasMany(p => p.CourseApplications)
           .WithOne(p => p.CourseDate)
           .HasForeignKey(p => p.CourseDateId)
           .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Domain.Models.CourseApplication>()
            .HasOne(c => c.Company)
            .WithOne(c => c.CourseApplication)
            .HasForeignKey<Domain.Models.CourseApplication>(c => c.CompanyId);



            modelBuilder.Entity<Participant>().Property(e => e.CompanyId).ValueGeneratedOnAdd();

            modelBuilder.Entity<Course>().HasData
            (
                new Course
                {
                    Id = 1,
                    Name = "Cutting trees, the ins and outs"

                },
                new Course
                {
                    Id = 2,
                    Name = "CSS and you - a love story"
                },
                new Course
                {
                    Id = 3,
                    Name = "Cutting trees, the ins and outs"
                },
                new Course
                {
                    Id = 4,
                    Name = "Christmas eve - myth or reality?"
                },
                new Course
                {
                    Id = 5,
                    Name = "LEGO colors through time"
                }
            );

            modelBuilder.Entity<CourseDate>().HasData
            (
                new CourseDate
                {
                    Id = 1,
                    CourseId = 1,
                    Date = DateTime.ParseExact("2017-01-01", "yyyy-MM-dd", CultureInfo.InvariantCulture)
                },
                new CourseDate
                {
                    Id = 2,
                    CourseId = 1,
                    Date = DateTime.ParseExact("2017-10-31", "yyyy-MM-dd", CultureInfo.InvariantCulture)
                },
                new CourseDate
                {
                    Id = 3,
                    CourseId = 2,
                    Date = DateTime.ParseExact("2017-05-25", "yyyy-MM-dd", CultureInfo.InvariantCulture)
                },
                new CourseDate
                {
                    Id = 4,
                    CourseId = 2,
                    Date = DateTime.ParseExact("2017-05-26", "yyyy-MM-dd", CultureInfo.InvariantCulture)
                },
                new CourseDate
                {
                    Id = 5,
                    CourseId = 2,
                    Date = DateTime.ParseExact("2017-05-27", "yyyy-MM-dd", CultureInfo.InvariantCulture)
                },
                new CourseDate
                {
                    Id = 6,
                    CourseId = 3,
                    Date = DateTime.ParseExact("2017-01-01", "yyyy-MM-dd", CultureInfo.InvariantCulture)
                },
                new CourseDate
                {
                    Id = 7,
                    CourseId = 3,
                    Date = DateTime.ParseExact("2018-12-10", "yyyy-MM-dd", CultureInfo.InvariantCulture),
                },
                new CourseDate
                {
                    Id = 8,
                    CourseId = 3,
                    Date = DateTime.ParseExact("2017-04-01", "yyyy-MM-dd", CultureInfo.InvariantCulture)
                },
                new CourseDate
                {
                    Id = 9,
                    CourseId = 3,
                    Date = DateTime.ParseExact("2019-03-12", "yyyy-MM-dd", CultureInfo.InvariantCulture)
                },
                new CourseDate
                {
                    Id = 10,
                    CourseId = 4,
                    Date = DateTime.ParseExact("2017-12-24", "yyyy-MM-dd", CultureInfo.InvariantCulture)
                },
                new CourseDate
                {
                    Id = 11,
                    CourseId = 4,
                    Date = DateTime.ParseExact("2018-12-24", "yyyy-MM-dd", CultureInfo.InvariantCulture)
                },
                new CourseDate
                {
                    Id = 12,
                    CourseId = 4,
                    Date = DateTime.ParseExact("2019-12-24", "yyyy-MM-dd", CultureInfo.InvariantCulture)
                },
                new CourseDate
                {
                    Id = 13,
                    CourseId = 5,
                    Date = DateTime.ParseExact("2017-06-30", "yyyy-MM-dd", CultureInfo.InvariantCulture)
                }

                );

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=localhost;Initial Catalog=courseacademy;Integrated Security=True;");
            }
        }
    }
}
