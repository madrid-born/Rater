using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rater.Migrations
{
    /// <inheritdoc />
    public partial class diff : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ItemsJson",
                table: "Topics",
                newName: "ItemsIdJson");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ItemsIdJson",
                table: "Topics",
                newName: "ItemsJson");
        }
    }
}
