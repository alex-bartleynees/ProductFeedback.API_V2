﻿// <auto-generated />
using System;
using DataAccess.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DataAccess.Migrations
{
    [DbContext(typeof(SuggestionContext))]
    partial class SuggestionContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Domain.Entities.Suggestion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<int>("Upvotes")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Suggestions");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Category = "enhancement",
                            Description = "Easier to search for solutions based on a specific stack.",
                            Status = "live",
                            Title = "Add tags for solutions okay",
                            Upvotes = 144
                        },
                        new
                        {
                            Id = 2,
                            Category = "feature",
                            Description = "It would help people with light sensitivities and who prefer dark mode.",
                            Status = "planned",
                            Title = "Add a dark theme option",
                            Upvotes = 122
                        },
                        new
                        {
                            Id = 3,
                            Category = "feature",
                            Description = "Challenge-specific Q&A would make for easy reference.",
                            Status = "suggestion",
                            Title = "Q&A within the challenge hubs",
                            Upvotes = 65
                        },
                        new
                        {
                            Id = 4,
                            Category = "enhancement",
                            Description = "Images and screencasts can enhance comments on solutions.",
                            Status = "suggestion",
                            Title = "Add image/video upload to feedback",
                            Upvotes = 51
                        },
                        new
                        {
                            Id = 5,
                            Category = "feature",
                            Description = "Stay updated on comments and solutions other people post.",
                            Status = "suggestion",
                            Title = "Ability to follow others",
                            Upvotes = 42
                        },
                        new
                        {
                            Id = 6,
                            Category = "bug",
                            Description = "Challenge preview images are missing when you apply a filter.",
                            Status = "suggestion",
                            Title = "Preview images not loading",
                            Upvotes = 3
                        },
                        new
                        {
                            Id = 7,
                            Category = "feature",
                            Description = "It would be great to see a more detailed breakdown of solutions.",
                            Status = "planned",
                            Title = "More comprehensive reports",
                            Upvotes = 123
                        },
                        new
                        {
                            Id = 8,
                            Category = "feature",
                            Description = "Sequenced projects for different goals to help people improve.",
                            Status = "planned",
                            Title = "Learning paths",
                            Upvotes = 28
                        },
                        new
                        {
                            Id = 9,
                            Category = "feature",
                            Description = "Add ability to create professional looking portfolio from profile.",
                            Status = "in-progress",
                            Title = "One-click portfolio generation",
                            Upvotes = 62
                        },
                        new
                        {
                            Id = 10,
                            Category = "feature",
                            Description = "Be able to bookmark challenges to take later on.",
                            Status = "in-progress",
                            Title = "Bookmark challenges",
                            Upvotes = 31
                        },
                        new
                        {
                            Id = 11,
                            Category = "bug",
                            Description = "Screenshots of solutions with animations don’t display correctly.",
                            Status = "in-progress",
                            Title = "Animated solution screenshots",
                            Upvotes = 9
                        },
                        new
                        {
                            Id = 12,
                            Category = "enhancement",
                            Description = "Small animations at specific points can add delight.",
                            Status = "live",
                            Title = "Add micro-interactions",
                            Upvotes = 71
                        });
                });

            modelBuilder.Entity("Domain.Entities.SuggestionComment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.Property<int>("SuggestionId")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("SuggestionId");

                    b.HasIndex("UserId");

                    b.ToTable("SuggestionsComment");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Content = "Awesome idea! Trying to find framework-specific projects within the hubs can be tedious",
                            SuggestionId = 1,
                            UserId = 1
                        },
                        new
                        {
                            Id = 2,
                            Content = "Please use fun, color-coded labels to easily identify them at a glance",
                            SuggestionId = 1,
                            UserId = 2
                        },
                        new
                        {
                            Id = 3,
                            Content = "Also, please allow styles to be applied based on system preferences. I would love to be able to browse Frontend Mentor in the evening after my device’s dark mode turns on without the bright background it currently has.",
                            SuggestionId = 2,
                            UserId = 4
                        },
                        new
                        {
                            Id = 4,
                            Content = "Second this! I do a lot of late night coding and reading. Adding a dark theme can be great for preventing eye strain and the headaches that result. It’s also quite a trend with modern apps and  apparently saves battery life.",
                            SuggestionId = 2,
                            UserId = 5
                        },
                        new
                        {
                            Id = 5,
                            Content = "Much easier to get answers from devs who can relate, since they've either finished the challenge themselves or are in the middle of it.",
                            SuggestionId = 3,
                            UserId = 8
                        },
                        new
                        {
                            Id = 6,
                            Content = "Right now, there is no ability to add images while giving feedback which isn't ideal because I have to use another app to show what I mean",
                            SuggestionId = 4,
                            UserId = 9
                        },
                        new
                        {
                            Id = 7,
                            Content = "Yes I'd like to see this as well. Sometimes I want to add a short video or gif to explain the site's behavior..",
                            SuggestionId = 4,
                            UserId = 10
                        },
                        new
                        {
                            Id = 8,
                            Content = "I also want to be notified when devs I follow submit projects on FEM. Is in-app notification also in the pipeline?",
                            SuggestionId = 5,
                            UserId = 11
                        },
                        new
                        {
                            Id = 9,
                            Content = "I've been saving the profile URLs of a few people and I check what they’ve been doing from time to time. Being able to follow them solves that",
                            SuggestionId = 5,
                            UserId = 12
                        },
                        new
                        {
                            Id = 10,
                            Content = "This would be awesome! It would be so helpful to see an overview of my code in a way that makes it easy to spot where things could be improved.",
                            SuggestionId = 7,
                            UserId = 11
                        },
                        new
                        {
                            Id = 11,
                            Content = "Yeah, this would be really good. I'd love to see deeper insights into my code!",
                            SuggestionId = 7,
                            UserId = 12
                        },
                        new
                        {
                            Id = 12,
                            Content = "Having a path through the challenges that I could follow would be brilliant! Sometimes I'm not sure which challenge would be the best next step to take. So this would help me navigate through them!",
                            SuggestionId = 8,
                            UserId = 8
                        },
                        new
                        {
                            Id = 13,
                            Content = "I haven't built a portfolio site yet, so this would be really helpful. Might it also be possible to choose layout and colour themes?!",
                            SuggestionId = 9,
                            UserId = 7
                        },
                        new
                        {
                            Id = 14,
                            Content = "This would be great! At the moment, I'm just starting challenges in order to save them. But this means the My Challenges section is overflowing with projects and is hard to manage. Being able to bookmark challenges would be really helpful.",
                            SuggestionId = 10,
                            UserId = 1
                        },
                        new
                        {
                            Id = 15,
                            Content = "I'd love to see this! It always makes me so happy to see little details like these on websites.",
                            SuggestionId = 12,
                            UserId = 11
                        });
                });

            modelBuilder.Entity("Domain.Entities.SuggestionCommentReply", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.Property<string>("ReplyingTo")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("SuggestionCommentId")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("SuggestionCommentId");

                    b.HasIndex("UserId");

                    b.ToTable("SuggestionsCommentReply");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Content = "While waiting for dark mode, there are browser extensions that will also do the job. Search for 'dark theme' followed by your browser. There might be a need to turn off the extension for sites with naturally black backgrounds though.",
                            ReplyingTo = "hummingbird1",
                            SuggestionCommentId = 4,
                            UserId = 6
                        },
                        new
                        {
                            Id = 2,
                            Content = "Good point! Using any kind of style extension is great and can be highly customizable, like the ability to change contrast and brightness. I'd prefer not to use one of such extensions, however, for security and privacy reasons.",
                            ReplyingTo = "annev1990",
                            SuggestionCommentId = 4,
                            UserId = 7
                        },
                        new
                        {
                            Id = 3,
                            Content = "Bumping this. It would be good to have a tab with a feed of people I follow so it's easy to see what challenges they’ve done lately. I learn a lot by reading good developers' code.",
                            ReplyingTo = "arlen_the_marlin",
                            SuggestionCommentId = 8,
                            UserId = 3
                        },
                        new
                        {
                            Id = 4,
                            Content = "Me too! I'd also love to see celebrations at specific points as well. It would help people take a moment to celebrate their achievements!",
                            ReplyingTo = "arlen_the_marlin",
                            SuggestionCommentId = 15,
                            UserId = 1
                        });
                });

            modelBuilder.Entity("Domain.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Image = "/assets/user-images/image-suzanne.jpg",
                            Name = "Suzanne Chang",
                            Username = "upbeat1811"
                        },
                        new
                        {
                            Id = 2,
                            Image = "/assets/user-images/image-thomas.jpg",
                            Name = "Thomas Hood",
                            Username = "brawnybrave"
                        },
                        new
                        {
                            Id = 3,
                            Image = "/assets/user-images/image-zena.jpg",
                            Name = "Zena Kelley",
                            Username = "velvetround"
                        },
                        new
                        {
                            Id = 4,
                            Image = "/assets/user-images/image-elijah.jpg",
                            Name = "Elijah Moss",
                            Username = "hexagon.bestagon"
                        },
                        new
                        {
                            Id = 5,
                            Image = "/assets/user-images/image-james.jpg",
                            Name = "James Skinner",
                            Username = "hummingbird1"
                        },
                        new
                        {
                            Id = 6,
                            Image = "/assets/user-images/image-anne.jpg",
                            Name = "Anne Valentine",
                            Username = "annev1990"
                        },
                        new
                        {
                            Id = 7,
                            Image = "/assets/user-images/image-ryan.jpg",
                            Name = "Ryan Welles",
                            Username = "voyager.344"
                        },
                        new
                        {
                            Id = 8,
                            Image = "/assets/user-images/image-george.jpg",
                            Name = "George Partridge",
                            Username = "soccerviewer8"
                        },
                        new
                        {
                            Id = 9,
                            Image = "/assets/user-images/image-javier.jpg",
                            Name = "Javier Pollard",
                            Username = "warlikeduke"
                        },
                        new
                        {
                            Id = 10,
                            Image = "/assets/user-images/image-roxanne.jpg",
                            Name = "Roxanne Travis",
                            Username = "peppersprime32"
                        },
                        new
                        {
                            Id = 11,
                            Image = "/assets/user-images/image-victoria.jpg",
                            Name = "Victoria Mejia",
                            Username = "arlen_the_marlin"
                        },
                        new
                        {
                            Id = 12,
                            Image = "/assets/user-images/image-jackson.jpg",
                            Name = "Jackson Barker",
                            Username = "countryspirit"
                        });
                });

            modelBuilder.Entity("Domain.Entities.SuggestionComment", b =>
                {
                    b.HasOne("Domain.Entities.Suggestion", null)
                        .WithMany("Comments")
                        .HasForeignKey("SuggestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Entities.SuggestionCommentReply", b =>
                {
                    b.HasOne("Domain.Entities.SuggestionComment", null)
                        .WithMany("Replies")
                        .HasForeignKey("SuggestionCommentId");

                    b.HasOne("Domain.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Entities.Suggestion", b =>
                {
                    b.Navigation("Comments");
                });

            modelBuilder.Entity("Domain.Entities.SuggestionComment", b =>
                {
                    b.Navigation("Replies");
                });
#pragma warning restore 612, 618
        }
    }
}
