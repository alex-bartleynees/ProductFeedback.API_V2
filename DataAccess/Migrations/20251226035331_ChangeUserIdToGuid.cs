using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ChangeUserIdToGuid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Drop all foreign key constraints and delete data using raw SQL
            migrationBuilder.Sql(@"
                -- Drop foreign key constraints
                ALTER TABLE ""SuggestionsComment"" DROP CONSTRAINT IF EXISTS ""FK_SuggestionsComment_Users_UserId"";
                ALTER TABLE ""SuggestionsCommentReply"" DROP CONSTRAINT IF EXISTS ""FK_SuggestionsCommentReply_Users_UserId"";
                ALTER TABLE ""SuggestionsCommentReply"" DROP CONSTRAINT IF EXISTS ""FK_SuggestionsCommentReply_SuggestionsComment_SuggestionCommentId"";
                ALTER TABLE ""SuggestionsCommentReply"" DROP CONSTRAINT IF EXISTS ""FK_SuggestionsCommentReply_SuggestionsComment_SuggestionCommen~"";

                -- Delete all existing data (will be re-seeded)
                DELETE FROM ""SuggestionsCommentReply"";
                DELETE FROM ""SuggestionsComment"";
                DELETE FROM ""Users"";

                -- Drop AccountId columns
                ALTER TABLE ""Users"" DROP COLUMN IF EXISTS ""AccountId"";
                ALTER TABLE ""SuggestionsCommentReply"" DROP COLUMN IF EXISTS ""AccountId"";
                ALTER TABLE ""SuggestionsComment"" DROP COLUMN IF EXISTS ""AccountId"";

                -- Change Users.Id from integer to uuid
                ALTER TABLE ""Users"" DROP CONSTRAINT ""PK_Users"";
                ALTER TABLE ""Users"" DROP COLUMN ""Id"";
                ALTER TABLE ""Users"" ADD ""Id"" uuid NOT NULL;
                ALTER TABLE ""Users"" ADD CONSTRAINT ""PK_Users"" PRIMARY KEY (""Id"");

                -- Change UserId columns from int to uuid
                ALTER TABLE ""SuggestionsCommentReply"" DROP COLUMN ""UserId"";
                ALTER TABLE ""SuggestionsCommentReply"" ADD ""UserId"" uuid NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000';

                ALTER TABLE ""SuggestionsComment"" DROP COLUMN ""UserId"";
                ALTER TABLE ""SuggestionsComment"" ADD ""UserId"" uuid NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000';
            ");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Image", "Name", "Username" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000001"), "", "/assets/user-images/image-suzanne.jpg", "Suzanne Chang", "upbeat1811" },
                    { new Guid("00000000-0000-0000-0000-000000000002"), "", "/assets/user-images/image-thomas.jpg", "Thomas Hood", "brawnybrave" },
                    { new Guid("00000000-0000-0000-0000-000000000003"), "", "/assets/user-images/image-zena.jpg", "Zena Kelley", "velvetround" },
                    { new Guid("00000000-0000-0000-0000-000000000004"), "", "/assets/user-images/image-elijah.jpg", "Elijah Moss", "hexagon.bestagon" },
                    { new Guid("00000000-0000-0000-0000-000000000005"), "", "/assets/user-images/image-james.jpg", "James Skinner", "hummingbird1" },
                    { new Guid("00000000-0000-0000-0000-000000000006"), "", "/assets/user-images/image-anne.jpg", "Anne Valentine", "annev1990" },
                    { new Guid("00000000-0000-0000-0000-000000000007"), "", "/assets/user-images/image-ryan.jpg", "Ryan Welles", "voyager.344" },
                    { new Guid("00000000-0000-0000-0000-000000000008"), "", "/assets/user-images/image-george.jpg", "George Partridge", "soccerviewer8" },
                    { new Guid("00000000-0000-0000-0000-000000000009"), "", "/assets/user-images/image-javier.jpg", "Javier Pollard", "warlikeduke" },
                    { new Guid("00000000-0000-0000-0000-00000000000a"), "", "/assets/user-images/image-roxanne.jpg", "Roxanne Travis", "peppersprime32" },
                    { new Guid("00000000-0000-0000-0000-00000000000b"), "", "/assets/user-images/image-victoria.jpg", "Victoria Mejia", "arlen_the_marlin" },
                    { new Guid("00000000-0000-0000-0000-00000000000c"), "", "/assets/user-images/image-jackson.jpg", "Jackson Barker", "countryspirit" }
                });

            // Re-insert comments with new Guid UserIds
            migrationBuilder.Sql(@"
                INSERT INTO ""SuggestionsComment"" (""Id"", ""Content"", ""UserId"", ""SuggestionId"") VALUES
                (1, 'Awesome idea! Trying to find framework-specific projects within the hubs can be tedious', '00000000-0000-0000-0000-000000000001', 1),
                (2, 'Please use fun, color-coded labels to easily identify them at a glance', '00000000-0000-0000-0000-000000000002', 1),
                (3, 'Also, please allow styles to be applied based on system preferences. I would love to be able to browse Frontend Mentor in the evening after my device''s dark mode turns on without the bright background it currently has.', '00000000-0000-0000-0000-000000000004', 2),
                (4, 'Second this! I do a lot of late night coding and reading. Adding a dark theme can be great for preventing eye strain and the headaches that result. It''s also quite a trend with modern apps and  apparently saves battery life.', '00000000-0000-0000-0000-000000000005', 2),
                (5, 'Much easier to get answers from devs who can relate, since they''ve either finished the challenge themselves or are in the middle of it.', '00000000-0000-0000-0000-000000000008', 3),
                (6, 'Right now, there is no ability to add images while giving feedback which isn''t ideal because I have to use another app to show what I mean', '00000000-0000-0000-0000-000000000009', 4),
                (7, 'Yes I''d like to see this as well. Sometimes I want to add a short video or gif to explain the site''s behavior..', '00000000-0000-0000-0000-00000000000a', 4),
                (8, 'I also want to be notified when devs I follow submit projects on FEM. Is in-app notification also in the pipeline?', '00000000-0000-0000-0000-00000000000b', 5),
                (9, 'I''ve been saving the profile URLs of a few people and I check what they''ve been doing from time to time. Being able to follow them solves that', '00000000-0000-0000-0000-00000000000c', 5),
                (10, 'This would be awesome! It would be so helpful to see an overview of my code in a way that makes it easy to spot where things could be improved.', '00000000-0000-0000-0000-00000000000b', 7),
                (11, 'Yeah, this would be really good. I''d love to see deeper insights into my code!', '00000000-0000-0000-0000-00000000000c', 7),
                (12, 'Having a path through the challenges that I could follow would be brilliant! Sometimes I''m not sure which challenge would be the best next step to take. So this would help me navigate through them!', '00000000-0000-0000-0000-000000000008', 8),
                (13, 'I haven''t built a portfolio site yet, so this would be really helpful. Might it also be possible to choose layout and colour themes?!', '00000000-0000-0000-0000-000000000007', 9),
                (14, 'This would be great! At the moment, I''m just starting challenges in order to save them. But this means the My Challenges section is overflowing with projects and is hard to manage. Being able to bookmark challenges would be really helpful.', '00000000-0000-0000-0000-000000000001', 10),
                (15, 'I''d love to see this! It always makes me so happy to see little details like these on websites.', '00000000-0000-0000-0000-00000000000b', 12);

                -- Update the sequence
                SELECT setval('""SuggestionsComment_Id_seq""', 15, true);
            ");

            // Re-insert replies with new Guid UserIds
            migrationBuilder.Sql(@"
                INSERT INTO ""SuggestionsCommentReply"" (""Id"", ""Content"", ""ReplyingTo"", ""UserId"", ""SuggestionCommentId"") VALUES
                (1, 'While waiting for dark mode, there are browser extensions that will also do the job. Search for ''dark theme'' followed by your browser. There might be a need to turn off the extension for sites with naturally black backgrounds though.', 'hummingbird1', '00000000-0000-0000-0000-000000000006', 4),
                (2, 'Good point! Using any kind of style extension is great and can be highly customizable, like the ability to change contrast and brightness. I''d prefer not to use one of such extensions, however, for security and privacy reasons.', 'annev1990', '00000000-0000-0000-0000-000000000007', 4),
                (3, 'Bumping this. It would be good to have a tab with a feed of people I follow so it''s easy to see what challenges they''ve done lately. I learn a lot by reading good developers'' code.', 'arlen_the_marlin', '00000000-0000-0000-0000-000000000003', 8),
                (4, 'Me too! I''d also love to see celebrations at specific points as well. It would help people take a moment to celebrate their achievements!', 'arlen_the_marlin', '00000000-0000-0000-0000-000000000001', 15);

                -- Update the sequence
                SELECT setval('""SuggestionsCommentReply_Id_seq""', 4, true);
            ");

            // Recreate foreign key constraints
            migrationBuilder.AddForeignKey(
                name: "FK_SuggestionsComment_Users_UserId",
                table: "SuggestionsComment",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SuggestionsCommentReply_Users_UserId",
                table: "SuggestionsCommentReply",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SuggestionsCommentReply_SuggestionsComment_SuggestionCommentId",
                table: "SuggestionsCommentReply",
                column: "SuggestionCommentId",
                principalTable: "SuggestionsComment",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000004"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000005"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000006"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000007"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000008"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000009"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-00000000000a"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-00000000000b"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-00000000000c"));

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Users",
                type: "integer",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<Guid>(
                name: "AccountId",
                table: "Users",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "SuggestionsCommentReply",
                type: "integer",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<Guid>(
                name: "AccountId",
                table: "SuggestionsCommentReply",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "SuggestionsComment",
                type: "integer",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<Guid>(
                name: "AccountId",
                table: "SuggestionsComment",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "SuggestionsComment",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AccountId", "UserId" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), 1 });

            migrationBuilder.UpdateData(
                table: "SuggestionsComment",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "AccountId", "UserId" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), 2 });

            migrationBuilder.UpdateData(
                table: "SuggestionsComment",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "AccountId", "Content", "UserId" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), "Also, please allow styles to be applied based on system preferences. I would love to be able to browse Frontend Mentor in the evening after my device’s dark mode turns on without the bright background it currently has.", 4 });

            migrationBuilder.UpdateData(
                table: "SuggestionsComment",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "AccountId", "Content", "UserId" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), "Second this! I do a lot of late night coding and reading. Adding a dark theme can be great for preventing eye strain and the headaches that result. It’s also quite a trend with modern apps and  apparently saves battery life.", 5 });

            migrationBuilder.UpdateData(
                table: "SuggestionsComment",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "AccountId", "UserId" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), 8 });

            migrationBuilder.UpdateData(
                table: "SuggestionsComment",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "AccountId", "UserId" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), 9 });

            migrationBuilder.UpdateData(
                table: "SuggestionsComment",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "AccountId", "UserId" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), 10 });

            migrationBuilder.UpdateData(
                table: "SuggestionsComment",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "AccountId", "UserId" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), 11 });

            migrationBuilder.UpdateData(
                table: "SuggestionsComment",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "AccountId", "Content", "UserId" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), "I've been saving the profile URLs of a few people and I check what they’ve been doing from time to time. Being able to follow them solves that", 12 });

            migrationBuilder.UpdateData(
                table: "SuggestionsComment",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "AccountId", "UserId" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), 11 });

            migrationBuilder.UpdateData(
                table: "SuggestionsComment",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "AccountId", "UserId" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), 12 });

            migrationBuilder.UpdateData(
                table: "SuggestionsComment",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "AccountId", "UserId" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), 8 });

            migrationBuilder.UpdateData(
                table: "SuggestionsComment",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "AccountId", "UserId" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), 7 });

            migrationBuilder.UpdateData(
                table: "SuggestionsComment",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "AccountId", "UserId" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), 1 });

            migrationBuilder.UpdateData(
                table: "SuggestionsComment",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "AccountId", "UserId" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), 11 });

            migrationBuilder.UpdateData(
                table: "SuggestionsCommentReply",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AccountId", "UserId" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), 6 });

            migrationBuilder.UpdateData(
                table: "SuggestionsCommentReply",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "AccountId", "UserId" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), 7 });

            migrationBuilder.UpdateData(
                table: "SuggestionsCommentReply",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "AccountId", "Content", "UserId" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), "Bumping this. It would be good to have a tab with a feed of people I follow so it's easy to see what challenges they’ve done lately. I learn a lot by reading good developers' code.", 3 });

            migrationBuilder.UpdateData(
                table: "SuggestionsCommentReply",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "AccountId", "UserId" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), 1 });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccountId", "Email", "Image", "Name", "Username" },
                values: new object[,]
                {
                    { 1, new Guid("00000000-0000-0000-0000-000000000000"), "", "/assets/user-images/image-suzanne.jpg", "Suzanne Chang", "upbeat1811" },
                    { 2, new Guid("00000000-0000-0000-0000-000000000000"), "", "/assets/user-images/image-thomas.jpg", "Thomas Hood", "brawnybrave" },
                    { 3, new Guid("00000000-0000-0000-0000-000000000000"), "", "/assets/user-images/image-zena.jpg", "Zena Kelley", "velvetround" },
                    { 4, new Guid("00000000-0000-0000-0000-000000000000"), "", "/assets/user-images/image-elijah.jpg", "Elijah Moss", "hexagon.bestagon" },
                    { 5, new Guid("00000000-0000-0000-0000-000000000000"), "", "/assets/user-images/image-james.jpg", "James Skinner", "hummingbird1" },
                    { 6, new Guid("00000000-0000-0000-0000-000000000000"), "", "/assets/user-images/image-anne.jpg", "Anne Valentine", "annev1990" },
                    { 7, new Guid("00000000-0000-0000-0000-000000000000"), "", "/assets/user-images/image-ryan.jpg", "Ryan Welles", "voyager.344" },
                    { 8, new Guid("00000000-0000-0000-0000-000000000000"), "", "/assets/user-images/image-george.jpg", "George Partridge", "soccerviewer8" },
                    { 9, new Guid("00000000-0000-0000-0000-000000000000"), "", "/assets/user-images/image-javier.jpg", "Javier Pollard", "warlikeduke" },
                    { 10, new Guid("00000000-0000-0000-0000-000000000000"), "", "/assets/user-images/image-roxanne.jpg", "Roxanne Travis", "peppersprime32" },
                    { 11, new Guid("00000000-0000-0000-0000-000000000000"), "", "/assets/user-images/image-victoria.jpg", "Victoria Mejia", "arlen_the_marlin" },
                    { 12, new Guid("00000000-0000-0000-0000-000000000000"), "", "/assets/user-images/image-jackson.jpg", "Jackson Barker", "countryspirit" }
                });
        }
    }
}
