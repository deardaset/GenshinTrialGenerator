using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GenshinTrialGenerator.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPhotoUrl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhotoUrl",
                table: "Heroes",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhotoUrl",
                table: "Bosses",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhotoUrl",
                table: "Heroes");

            migrationBuilder.DropColumn(
                name: "PhotoUrl",
                table: "Bosses");
        }
    }
}
