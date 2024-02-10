using Domain.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;

namespace DataAccess.DbContexts
{
    public class SuggestionContext : DbContext
    {
        protected readonly IConfiguration Configuration;
        public DbSet<Suggestion> Suggestions { get; set; } = null!;

        public DbSet<SuggestionComment> SuggestionsComment { get; set; } = null!;

        public DbSet<SuggestionCommentReply> SuggestionsCommentReply { get; set; } = null!;

        public DbSet<User> Users { get; set; } = null!;

        public SuggestionContext(DbContextOptions<SuggestionContext> options, IHttpContextAccessor accessor) : base(options)
        {
            var conn = Database.GetDbConnection() as SqlConnection;
            conn!.AccessToken = accessor?.HttpContext?.Request.Headers["X-MS-TOKEN-AAD-ACCESS-TOKEN"];
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to sql server with connection string from app settings
            options.UseSqlServer(Configuration.GetConnectionString("SuggestionDBConnectionString"));
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Suggestion>()
                .HasData(
                    new Suggestion("Add tags for solutions okay", "enhancement", "live", "Easier to search for solutions based on a specific stack.")
                    {
                        Id = 1,
                        Upvotes = 144,
                    },
                       new Suggestion("Add a dark theme option", "feature", "planned", "It would help people with light sensitivities and who prefer dark mode.")
                       {
                           Id = 2,
                           Upvotes = 122,
                       }
                    );

            modelBuilder.Entity<SuggestionComment>()
                .HasData(
                    new SuggestionComment("Awesome idea! Trying to find framework-specific projects within the hubs can be tedious")
                    {
                        Id = 1,
                        SuggestionId = 1,
                        UserId = 1,
                    },
                        new SuggestionComment("Please use fun, color-coded labels to easily identify them at a glance")
                        {
                            Id = 2,
                            SuggestionId = 1,
                            UserId = 2,
                        },
                        new SuggestionComment("Also, please allow styles to be applied based on system preferences. I would love to be able to browse Frontend Mentor in the evening after my device’s dark mode turns on without the bright background it currently has.")
                        {
                            Id = 3,
                            SuggestionId = 2,
                            UserId = 4,
                        },
                        new SuggestionComment("Second this! I do a lot of late night coding and reading. Adding a dark theme can be great for preventing eye strain and the headaches that result. It’s also quite a trend with modern apps and  apparently saves battery life.")
                        {
                            Id = 4,
                            SuggestionId = 2,
                            UserId = 5,
                        }
                    );

            modelBuilder.Entity<SuggestionCommentReply>()
                .HasData(
                    new SuggestionCommentReply("While waiting for dark mode, there are browser extensions that will also do the job. Search for 'dark theme' followed by your browser. There might be a need to turn off the extension for sites with naturally black backgrounds though.", "hummingbird1")
                    {
                        Id = 1,
                        UserId = 6,
                        SuggestionCommentId = 4
                    },
                    new SuggestionCommentReply("Good point! Using any kind of style extension is great and can be highly customizable, like the ability to change contrast and brightness. I'd prefer not to use one of such extensions, however, for security and privacy reasons.", "annev1990")
                    {
                        Id = 2,
                        UserId = 7,
                        SuggestionCommentId = 4
                    }
                    );

            modelBuilder.Entity<User>()
                .HasData(
                    new User("Suzanne Chang", "upbeat1811", "./assets/user-images/image-suzanne.jpg")
                    {
                        Id = 1,

                    },
                    new User("Thomas Hood", "brawnybrave", "./assets/user-images/image-thomas.jpg")
                    {
                        Id = 2,
                    },
                    new User("Zena Kelley", "velvetround", "./assets/user-images/image-zena.jpg")
                    {
                        Id = 3,

                    },
                    new User("Elijah Moss", "hexagon.bestagon", "./assets/user-images/image-elijah.jpg")
                    {
                        Id = 4,

                    },
                    new User("James Skinner", "hummingbird1", "./assets/user-images/image-james.jpg")
                    {
                        Id = 5,

                    },
                    new User("Anne Valentine", "annev1990", "./assets/user-images/image-anne.jpg")
                    {
                        Id = 6,

                    },
                    new User("Ryan Welles", "voyager.344", "./assets/user-images/image-ryan.jpg")
                    {
                        Id = 7,

                    }
                    );
        }
    }
}
