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
            // Map entities to lowercase table names
            modelBuilder.Entity<Survey>().ToTable("surveys");
            modelBuilder.Entity<Question>().ToTable("questions");
            modelBuilder.Entity<Answer>().ToTable("answers");
            modelBuilder.Entity<Interview>().ToTable("interviews");
            modelBuilder.Entity<Result>().ToTable("results");
            modelBuilder.Entity<User>().ToTable("users");

            // Map properties to lowercase column names
            modelBuilder.Entity<Interview>().Property(s => s.Id).HasColumnName("id");
            modelBuilder.Entity<Interview>().Property(s => s.SurveyId).HasColumnName("surveyid");
            modelBuilder.Entity<Interview>().Property(s => s.UserId).HasColumnName("userid");
            modelBuilder.Entity<Interview>().Property(s => s.DateStarted).HasColumnName("datestarted");
            modelBuilder.Entity<Interview>().Property(s => s.DateCompleted).HasColumnName("datecompleted");

            modelBuilder.Entity<Question>().Property(q => q.Id).HasColumnName("id");
            modelBuilder.Entity<Question>().Property(q => q.Text).HasColumnName("text");
            modelBuilder.Entity<Question>().Property(q => q.SurveyId).HasColumnName("surveyid");
            modelBuilder.Entity<Question>().Property(q => q.QuestionOrder).HasColumnName("questionorder");

            modelBuilder.Entity<Result>().Property(s => s.Id).HasColumnName("id");
            modelBuilder.Entity<Result>().Property(s => s.InterviewId).HasColumnName("interviewid");
            modelBuilder.Entity<Result>().Property(s => s.QuestionId).HasColumnName("questionid");
            modelBuilder.Entity<Result>().Property(s => s.AnswerId).HasColumnName("answerid");

            modelBuilder.Entity<Answer>().Property(a => a.Id).HasColumnName("id");
            modelBuilder.Entity<Answer>().Property(a => a.Text).HasColumnName("text");
            modelBuilder.Entity<Answer>().Property(a => a.QuestionId).HasColumnName("questionid");

            modelBuilder.Entity<Survey>().Property(s => s.Id).HasColumnName("id");
            modelBuilder.Entity<Survey>().Property(s => s.Title).HasColumnName("title");
            modelBuilder.Entity<Survey>().Property(s => s.Description).HasColumnName("description");

            modelBuilder.Entity<User>().Property(u => u.Id).HasColumnName("id");

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
