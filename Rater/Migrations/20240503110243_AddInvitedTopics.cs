using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rater.Migrations
{
    /// <inheritdoc />
    public partial class AddInvitedTopics : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "InvitedTopicsIdJson",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InvitedTopicsIdJson",
                table: "Users");
        }
    }
}
