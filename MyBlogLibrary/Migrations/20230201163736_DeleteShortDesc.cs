using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyBlogLibrary.Migrations
{
    /// <inheritdoc />
    public partial class DeleteShortDesc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShortDescription",
                table: "Posts");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ShortDescription",
                table: "Posts",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");
        }
    }
}
