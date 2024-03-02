using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddSuggestions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Suggestions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Upvotes = table.Column<int>(type: "int", nullable: false),
                    Category = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Status = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suggestions", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Username = table.Column<string>(type: "longtext", nullable: false),
                    Name = table.Column<string>(type: "longtext", nullable: false),
                    Image = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "SuggestionsComment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Content = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    SuggestionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SuggestionsComment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SuggestionsComment_Suggestions_SuggestionId",
                        column: x => x.SuggestionId,
                        principalTable: "Suggestions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SuggestionsComment_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "SuggestionsCommentReply",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Content = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false),
                    ReplyingTo = table.Column<string>(type: "longtext", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    SuggestionCommentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SuggestionsCommentReply", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SuggestionsCommentReply_SuggestionsComment_SuggestionComment~",
                        column: x => x.SuggestionCommentId,
                        principalTable: "SuggestionsComment",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SuggestionsCommentReply_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Suggestions",
                columns: new[] { "Id", "Category", "Description", "Status", "Title", "Upvotes" },
                values: new object[,]
                {
                    { 1, "enhancement", "Easier to search for solutions based on a specific stack.", "live", "Add tags for solutions okay", 144 },
                    { 2, "feature", "It would help people with light sensitivities and who prefer dark mode.", "planned", "Add a dark theme option", 122 },
                    { 3, "feature", "Challenge-specific Q&A would make for easy reference.", "suggestion", "Q&A within the challenge hubs", 65 },
                    { 4, "enhancement", "Images and screencasts can enhance comments on solutions.", "suggestion", "Add image/video upload to feedback", 51 },
                    { 5, "feature", "Stay updated on comments and solutions other people post.", "suggestion", "Ability to follow others", 42 },
                    { 6, "bug", "Challenge preview images are missing when you apply a filter.", "suggestion", "Preview images not loading", 3 },
                    { 7, "feature", "It would be great to see a more detailed breakdown of solutions.", "planned", "More comprehensive reports", 123 },
                    { 8, "feature", "Sequenced projects for different goals to help people improve.", "planned", "Learning paths", 28 },
                    { 9, "feature", "Add ability to create professional looking portfolio from profile.", "in-progress", "One-click portfolio generation", 62 },
                    { 10, "feature", "Be able to bookmark challenges to take later on.", "in-progress", "Bookmark challenges", 31 },
                    { 11, "bug", "Screenshots of solutions with animations don’t display correctly.", "in-progress", "Animated solution screenshots", 9 },
                    { 12, "enhancement", "Small animations at specific points can add delight.", "live", "Add micro-interactions", 71 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Image", "Name", "Username" },
                values: new object[,]
                {
                    { 1, "./assets/user-images/image-suzanne.jpg", "Suzanne Chang", "upbeat1811" },
                    { 2, "./assets/user-images/image-thomas.jpg", "Thomas Hood", "brawnybrave" },
                    { 3, "./assets/user-images/image-zena.jpg", "Zena Kelley", "velvetround" },
                    { 4, "./assets/user-images/image-elijah.jpg", "Elijah Moss", "hexagon.bestagon" },
                    { 5, "./assets/user-images/image-james.jpg", "James Skinner", "hummingbird1" },
                    { 6, "./assets/user-images/image-anne.jpg", "Anne Valentine", "annev1990" },
                    { 7, "./assets/user-images/image-ryan.jpg", "Ryan Welles", "voyager.344" },
                    { 8, "./assets/user-images/image-george.jpg", "George Partridge", "soccerviewer8" },
                    { 9, "./assets/user-images/image-javier.jpg", "Javier Pollard", "warlikeduke" },
                    { 10, "./assets/user-images/image-roxanne.jpg", "Roxanne Travis", "peppersprime32" },
                    { 11, "./assets/user-images/image-victoria.jpg", "Victoria Mejia", "arlen_the_marlin" },
                    { 12, "./assets/user-images/image-jackson.jpg", "Jackson Barker", "countryspirit" }
                });

            migrationBuilder.InsertData(
                table: "SuggestionsComment",
                columns: new[] { "Id", "Content", "SuggestionId", "UserId" },
                values: new object[,]
                {
                    { 1, "Awesome idea! Trying to find framework-specific projects within the hubs can be tedious", 1, 1 },
                    { 2, "Please use fun, color-coded labels to easily identify them at a glance", 1, 2 },
                    { 3, "Also, please allow styles to be applied based on system preferences. I would love to be able to browse Frontend Mentor in the evening after my device’s dark mode turns on without the bright background it currently has.", 2, 4 },
                    { 4, "Second this! I do a lot of late night coding and reading. Adding a dark theme can be great for preventing eye strain and the headaches that result. It’s also quite a trend with modern apps and  apparently saves battery life.", 2, 5 },
                    { 5, "Much easier to get answers from devs who can relate, since they've either finished the challenge themselves or are in the middle of it.", 3, 8 },
                    { 6, "Right now, there is no ability to add images while giving feedback which isn't ideal because I have to use another app to show what I mean", 4, 9 },
                    { 7, "Yes I'd like to see this as well. Sometimes I want to add a short video or gif to explain the site's behavior..", 4, 10 },
                    { 8, "I also want to be notified when devs I follow submit projects on FEM. Is in-app notification also in the pipeline?", 5, 11 },
                    { 9, "I've been saving the profile URLs of a few people and I check what they’ve been doing from time to time. Being able to follow them solves that", 5, 12 },
                    { 10, "This would be awesome! It would be so helpful to see an overview of my code in a way that makes it easy to spot where things could be improved.", 7, 11 },
                    { 11, "Yeah, this would be really good. I'd love to see deeper insights into my code!", 7, 12 },
                    { 12, "Having a path through the challenges that I could follow would be brilliant! Sometimes I'm not sure which challenge would be the best next step to take. So this would help me navigate through them!", 8, 8 },
                    { 13, "I haven't built a portfolio site yet, so this would be really helpful. Might it also be possible to choose layout and colour themes?!", 9, 7 },
                    { 14, "This would be great! At the moment, I'm just starting challenges in order to save them. But this means the My Challenges section is overflowing with projects and is hard to manage. Being able to bookmark challenges would be really helpful.", 10, 1 },
                    { 15, "I'd love to see this! It always makes me so happy to see little details like these on websites.", 12, 11 }
                });

            migrationBuilder.InsertData(
                table: "SuggestionsCommentReply",
                columns: new[] { "Id", "Content", "ReplyingTo", "SuggestionCommentId", "UserId" },
                values: new object[,]
                {
                    { 1, "While waiting for dark mode, there are browser extensions that will also do the job. Search for 'dark theme' followed by your browser. There might be a need to turn off the extension for sites with naturally black backgrounds though.", "hummingbird1", 4, 6 },
                    { 2, "Good point! Using any kind of style extension is great and can be highly customizable, like the ability to change contrast and brightness. I'd prefer not to use one of such extensions, however, for security and privacy reasons.", "annev1990", 4, 7 },
                    { 3, "Bumping this. It would be good to have a tab with a feed of people I follow so it's easy to see what challenges they’ve done lately. I learn a lot by reading good developers' code.", "arlen_the_marlin", 8, 3 },
                    { 4, "Me too! I'd also love to see celebrations at specific points as well. It would help people take a moment to celebrate their achievements!", "arlen_the_marlin", 15, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_SuggestionsComment_SuggestionId",
                table: "SuggestionsComment",
                column: "SuggestionId");

            migrationBuilder.CreateIndex(
                name: "IX_SuggestionsComment_UserId",
                table: "SuggestionsComment",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SuggestionsCommentReply_SuggestionCommentId",
                table: "SuggestionsCommentReply",
                column: "SuggestionCommentId");

            migrationBuilder.CreateIndex(
                name: "IX_SuggestionsCommentReply_UserId",
                table: "SuggestionsCommentReply",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SuggestionsCommentReply");

            migrationBuilder.DropTable(
                name: "SuggestionsComment");

            migrationBuilder.DropTable(
                name: "Suggestions");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
