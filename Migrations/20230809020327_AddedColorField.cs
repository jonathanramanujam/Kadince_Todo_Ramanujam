using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kadince_Todo_Ramanujam.Migrations
{
    /// <inheritdoc />
    public partial class AddedColorField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "TodoItem",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Color",
                table: "TodoItem");
        }
    }
}
