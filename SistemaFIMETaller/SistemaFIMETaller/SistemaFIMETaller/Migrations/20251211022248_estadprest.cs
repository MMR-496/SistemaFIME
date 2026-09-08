using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaFIMETaller.Migrations
{
    /// <inheritdoc />
    public partial class estadprest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Estado",
                table: "Prestamos",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Estado",
                table: "Prestamos");
        }
    }
}
