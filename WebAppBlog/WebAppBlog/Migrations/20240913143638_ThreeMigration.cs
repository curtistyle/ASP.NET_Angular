using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAppBlog.Migrations
{
    /// <inheritdoc />
    public partial class ThreeMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Desciption",
                table: "Posts",
                newName: "ThumbnailUrl");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Posts",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Posts");

            migrationBuilder.RenameColumn(
                name: "ThumbnailUrl",
                table: "Posts",
                newName: "Desciption");
        }
    }
}
