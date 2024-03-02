using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DataAccess.DbContexts
{
    public class SuggestionContext : DbContext
    {
        protected readonly IConfiguration Configuration;
        public DbSet<Suggestion> Suggestions { get; set; } = null!;

        public DbSet<SuggestionComment> SuggestionsComment { get; set; } = null!;

        public DbSet<SuggestionCommentReply> SuggestionsCommentReply { get; set; } = null!;

        public DbSet<User> Users { get; set; } = null!;

        public SuggestionContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to sql server with connection string from app settings
            var cs = Configuration.GetConnectionString("SuggestionDBConnectionString") ?? throw new ArgumentNullException("No connection string provided");
            var serverVersion = ServerVersion.AutoDetect(cs);
            options.UseMySql(cs, serverVersion);
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
                       },
                       new Suggestion("Q&A within the challenge hubs", "feature", "suggestion", "Challenge-specific Q&A would make for easy reference.")
                       {
                           Id = 3,
                           Upvotes = 65,
                       },
                        new Suggestion("Add image/video upload to feedback", "enhancement", "suggestion", "Images and screencasts can enhance comments on solutions.")
                        {
                            Id = 4,
                            Upvotes = 51,
                        },
                        new Suggestion("Ability to follow others", "feature", "suggestion", "Stay updated on comments and solutions other people post.")
                        {
                            Id = 5,
                            Upvotes = 42,
                        },
                        new Suggestion("Preview images not loading", "bug", "suggestion", "Challenge preview images are missing when you apply a filter.")
                        {
                            Id = 6,
                            Upvotes = 3,
                        },
                        new Suggestion("More comprehensive reports", "feature", "planned", "It would be great to see a more detailed breakdown of solutions.")
                        {
                            Id = 7,
                            Upvotes = 123,
                        },
                        new Suggestion("Learning paths", "feature", "planned", "Sequenced projects for different goals to help people improve.")
                        {
                            Id = 8,
                            Upvotes = 28,
                        },
                        new Suggestion("One-click portfolio generation", "feature", "in-progress", "Add ability to create professional looking portfolio from profile.")
                        {
                            Id = 9,
                            Upvotes = 62,
                        },
                        new Suggestion("Bookmark challenges", "feature", "in-progress", "Be able to bookmark challenges to take later on.")
                        {
                            Id = 10,
                            Upvotes = 31,
                        },
                        new Suggestion("Animated solution screenshots", "bug", "in-progress", "Screenshots of solutions with animations don’t display correctly.")
                        {
                            Id = 11,
                            Upvotes = 9,
                        },
                        new Suggestion("Add micro-interactions", "enhancement", "live", "Small animations at specific points can add delight.")
                        {
                            Id = 12,
                            Upvotes = 71,
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
                        },
                        new SuggestionComment("Much easier to get answers from devs who can relate, since they've either finished the challenge themselves or are in the middle of it.")
                        {
                            Id = 5,
                            SuggestionId = 3,
                            UserId = 8,
                        },
                        new SuggestionComment("Right now, there is no ability to add images while giving feedback which isn't ideal because I have to use another app to show what I mean")
                        {
                            Id = 6,
                            SuggestionId = 4,
                            UserId = 9,
                        },
                        new SuggestionComment("Yes I'd like to see this as well. Sometimes I want to add a short video or gif to explain the site's behavior..")
                        {
                            Id = 7,
                            SuggestionId = 4,
                            UserId = 10,
                        },
                        new SuggestionComment("I also want to be notified when devs I follow submit projects on FEM. Is in-app notification also in the pipeline?")
                        {
                            Id = 8,
                            SuggestionId = 5,
                            UserId = 11,
                        },
                        new SuggestionComment("I've been saving the profile URLs of a few people and I check what they’ve been doing from time to time. Being able to follow them solves that")
                        {
                            Id = 9,
                            SuggestionId = 5,
                            UserId = 12,
                        },
                        new SuggestionComment("This would be awesome! It would be so helpful to see an overview of my code in a way that makes it easy to spot where things could be improved.")
                        {
                            Id = 10,
                            SuggestionId = 7,
                            UserId = 11,
                        },
                        new SuggestionComment("Yeah, this would be really good. I'd love to see deeper insights into my code!")
                        {
                            Id = 11,
                            SuggestionId = 7,
                            UserId = 12,
                        },
                        new SuggestionComment("Having a path through the challenges that I could follow would be brilliant! Sometimes I'm not sure which challenge would be the best next step to take. So this would help me navigate through them!")
                        {
                            Id = 12,
                            SuggestionId = 8,
                            UserId = 8,
                        },
                        new SuggestionComment("I haven't built a portfolio site yet, so this would be really helpful. Might it also be possible to choose layout and colour themes?!")
                        {
                            Id = 13,
                            SuggestionId = 9,
                            UserId = 7,
                        },
                        new SuggestionComment("This would be great! At the moment, I'm just starting challenges in order to save them. But this means the My Challenges section is overflowing with projects and is hard to manage. Being able to bookmark challenges would be really helpful.")
                        {
                            Id = 14,
                            SuggestionId = 10,
                            UserId = 1,
                        },
                        new SuggestionComment("I'd love to see this! It always makes me so happy to see little details like these on websites.")
                        {
                            Id = 15,
                            SuggestionId = 12,
                            UserId = 11,
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
                    },
                    new SuggestionCommentReply("Bumping this. It would be good to have a tab with a feed of people I follow so it's easy to see what challenges they’ve done lately. I learn a lot by reading good developers' code.", "arlen_the_marlin")
                    {
                        Id = 3,
                        UserId = 3,
                        SuggestionCommentId = 8
                    },
                    new SuggestionCommentReply("Me too! I'd also love to see celebrations at specific points as well. It would help people take a moment to celebrate their achievements!", "arlen_the_marlin")
                    {
                        Id = 4,
                        UserId = 1,
                        SuggestionCommentId = 15
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

                    },
                    new User("George Partridge", "soccerviewer8", "./assets/user-images/image-george.jpg")
                    {
                        Id = 8,
                    },
                    new User("Javier Pollard", "warlikeduke", "./assets/user-images/image-javier.jpg")
                    {
                        Id = 9,
                    },
                    new User("Roxanne Travis", "peppersprime32", "./assets/user-images/image-roxanne.jpg")
                    {
                        Id = 10,
                    },
                    new User("Victoria Mejia", "arlen_the_marlin", "./assets/user-images/image-victoria.jpg")
                    {
                        Id = 11,
                    },
                      new User("Jackson Barker", "countryspirit", "./assets/user-images/image-jackson.jpg")
                      {
                          Id = 12,
                      }
                    );
        }
    }
}
