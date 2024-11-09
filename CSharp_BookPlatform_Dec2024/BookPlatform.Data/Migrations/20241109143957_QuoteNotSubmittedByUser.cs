using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookPlatform.Data.Migrations
{
    /// <inheritdoc />
    public partial class QuoteNotSubmittedByUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSubmittedByUser",
                table: "Quotes");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsSubmittedByUser",
                table: "Quotes",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
