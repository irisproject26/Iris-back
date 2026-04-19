using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IRIS_API.Migrations
{
    /// <inheritdoc />
    public partial class AddStatusToVisita : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Visitas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Visitas");
        }
    }
}
