using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PurpleBuzzPr.Migrations
{
    /// <inheritdoc />
    public partial class removeiscoverfield : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCover",
                table: "WorkPhotos");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsCover",
                table: "WorkPhotos",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
