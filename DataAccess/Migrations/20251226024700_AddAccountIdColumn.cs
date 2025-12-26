using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddAccountIdColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AccountId",
                table: "Users",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "AccountId",
                table: "SuggestionsCommentReply",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

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
                column: "AccountId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "SuggestionsComment",
                keyColumn: "Id",
                keyValue: 2,
                column: "AccountId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "SuggestionsComment",
                keyColumn: "Id",
                keyValue: 3,
                column: "AccountId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "SuggestionsComment",
                keyColumn: "Id",
                keyValue: 4,
                column: "AccountId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "SuggestionsComment",
                keyColumn: "Id",
                keyValue: 5,
                column: "AccountId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "SuggestionsComment",
                keyColumn: "Id",
                keyValue: 6,
                column: "AccountId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "SuggestionsComment",
                keyColumn: "Id",
                keyValue: 7,
                column: "AccountId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "SuggestionsComment",
                keyColumn: "Id",
                keyValue: 8,
                column: "AccountId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "SuggestionsComment",
                keyColumn: "Id",
                keyValue: 9,
                column: "AccountId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "SuggestionsComment",
                keyColumn: "Id",
                keyValue: 10,
                column: "AccountId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "SuggestionsComment",
                keyColumn: "Id",
                keyValue: 11,
                column: "AccountId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "SuggestionsComment",
                keyColumn: "Id",
                keyValue: 12,
                column: "AccountId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "SuggestionsComment",
                keyColumn: "Id",
                keyValue: 13,
                column: "AccountId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "SuggestionsComment",
                keyColumn: "Id",
                keyValue: 14,
                column: "AccountId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "SuggestionsComment",
                keyColumn: "Id",
                keyValue: 15,
                column: "AccountId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "SuggestionsCommentReply",
                keyColumn: "Id",
                keyValue: 1,
                column: "AccountId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "SuggestionsCommentReply",
                keyColumn: "Id",
                keyValue: 2,
                column: "AccountId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "SuggestionsCommentReply",
                keyColumn: "Id",
                keyValue: 3,
                column: "AccountId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "SuggestionsCommentReply",
                keyColumn: "Id",
                keyValue: 4,
                column: "AccountId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AccountId", "Email" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), "" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "AccountId", "Email" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), "" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "AccountId", "Email" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), "" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "AccountId", "Email" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), "" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "AccountId", "Email" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), "" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "AccountId", "Email" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), "" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "AccountId", "Email" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), "" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "AccountId", "Email" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), "" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "AccountId", "Email" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), "" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "AccountId", "Email" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), "" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "AccountId", "Email" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), "" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "AccountId", "Email" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), "" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "SuggestionsCommentReply");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "SuggestionsComment");
        }
    }
}
