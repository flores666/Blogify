using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyBlogLibrary.Migrations
{
    /// <inheritdoc />
    public partial class DeleteMeta : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Meta",
                table: "Posts");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Meta",
                table: "Posts",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
