using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Suggestions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Upvotes = table.Column<int>(type: "integer", nullable: false),
                    Category = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suggestions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Username = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Image = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SuggestionsComment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Content = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    SuggestionId = table.Column<int>(type: "integer", nullable: false)
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
                });

            migrationBuilder.CreateTable(
                name: "SuggestionsCommentReply",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Content = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    ReplyingTo = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    SuggestionCommentId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SuggestionsCommentReply", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SuggestionsCommentReply_SuggestionsComment_SuggestionCommen~",
                        column: x => x.SuggestionCommentId,
                        principalTable: "SuggestionsComment",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SuggestionsCommentReply_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Suggestions",
                columns: new[] { "Id", "Category", "Description", "Status", "Title", "Upvotes" },
                values: new object[,]
                {
                    { 1, "enhancement", "Easier to search for solutions based on a specific stack.", "live", "Add tags for solutions okay", 144 },
                    { 2, "feature", "It would help people with light sensitivities and who prefer dark mode.", "planned", "Add a dark theme option", 122 }
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
                    { 7, "./assets/user-images/image-ryan.jpg", "Ryan Welles", "voyager.344" }
                });

            migrationBuilder.InsertData(
                table: "SuggestionsComment",
                columns: new[] { "Id", "Content", "SuggestionId", "UserId" },
                values: new object[,]
                {
                    { 1, "Awesome idea! Trying to find framework-specific projects within the hubs can be tedious", 1, 1 },
                    { 2, "Please use fun, color-coded labels to easily identify them at a glance", 1, 2 },
                    { 3, "Also, please allow styles to be applied based on system preferences. I would love to be able to browse Frontend Mentor in the evening after my device’s dark mode turns on without the bright background it currently has.", 2, 4 },
                    { 4, "Second this! I do a lot of late night coding and reading. Adding a dark theme can be great for preventing eye strain and the headaches that result. It’s also quite a trend with modern apps and  apparently saves battery life.", 2, 5 }
                });

            migrationBuilder.InsertData(
                table: "SuggestionsCommentReply",
                columns: new[] { "Id", "Content", "ReplyingTo", "SuggestionCommentId", "UserId" },
                values: new object[,]
                {
                    { 1, "While waiting for dark mode, there are browser extensions that will also do the job. Search for 'dark theme' followed by your browser. There might be a need to turn off the extension for sites with naturally black backgrounds though.", "hummingbird1", 4, 6 },
                    { 2, "Good point! Using any kind of style extension is great and can be highly customizable, like the ability to change contrast and brightness. I'd prefer not to use one of such extensions, however, for security and privacy reasons.", "annev1990", 4, 7 }
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
