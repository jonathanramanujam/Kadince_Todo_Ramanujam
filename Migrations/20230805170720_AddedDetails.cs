using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kadince_Todo_Ramanujam.Migrations
{
    /// <inheritdoc />
    public partial class AddedDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Details",
                table: "TodoItem",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Details",
                table: "TodoItem");
        }
    }
}
