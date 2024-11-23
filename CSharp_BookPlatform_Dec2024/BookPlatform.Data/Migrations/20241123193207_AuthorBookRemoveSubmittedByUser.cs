using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookPlatform.Data.Migrations
{
    /// <inheritdoc />
    public partial class AuthorBookRemoveSubmittedByUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSubmittedByUser",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "IsSubmittedByUser",
                table: "Authors");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsSubmittedByUser",
                table: "Books",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsSubmittedByUser",
                table: "Authors",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
