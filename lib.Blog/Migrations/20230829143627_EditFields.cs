using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace lib.Blog.Migrations
{
    /// <inheritdoc />
    public partial class EditFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "title",
                schema: "blog",
                table: "posts",
                type: "varchar(100)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(100)");

            migrationBuilder.AddColumn<string>(
                name: "status",
                schema: "security",
                table: "app_users",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "status",
                schema: "security",
                table: "app_users");

            migrationBuilder.AlterColumn<string>(
                name: "title",
                schema: "blog",
                table: "posts",
                type: "varchar(100)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldNullable: true);
        }
    }
}
