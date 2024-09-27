using Microsoft.EntityFrameworkCore;
using SurveyApi.Entities;
using System.Data.Common;

namespace SurveyApi.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Survey> Surveys { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Interview> Interviews { get; set; }
        public DbSet<Result> Results { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Survey to Questions
            modelBuilder.Entity<Survey>()
                .HasMany(s => s.Questions)
                .WithOne(q => q.Survey)
                .HasForeignKey(q => q.SurveyId)
                .OnDelete(DeleteBehavior.Cascade);

            // Survey to Interviews
            modelBuilder.Entity<Survey>()
                .HasMany(s => s.Interviews)
                .WithOne(i => i.Survey)
                .HasForeignKey(i => i.SurveyId)
                .OnDelete(DeleteBehavior.Cascade);

            // Question to Answers
            modelBuilder.Entity<Question>()
                .HasMany(q => q.Answers)
                .WithOne(a => a.Question)
                .HasForeignKey(a => a.QuestionId)
                .OnDelete(DeleteBehavior.Cascade);

            // Interview to Results
            modelBuilder.Entity<Interview>()
                .HasMany(i => i.Results)
                .WithOne(r => r.Interview)
                .HasForeignKey(r => r.InterviewId)
                .OnDelete(DeleteBehavior.Cascade);

            // Result relationships
            modelBuilder.Entity<Result>()
                .HasOne(r => r.Question)
                .WithMany()
                .HasForeignKey(r => r.QuestionId);

            modelBuilder.Entity<Result>()
                .HasOne(r => r.Answer)
                .WithMany()
                .HasForeignKey(r => r.AnswerId);
        }
    }
}
