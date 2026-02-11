using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VisitSchool.Migrations
{
    /// <inheritdoc />
    public partial class updatediscountCategoryEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "StudentCategories",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "StudentCategories");
        }
    }
}
