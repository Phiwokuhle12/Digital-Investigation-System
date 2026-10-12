
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DigitalInvestigationAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddInvestigationSignature : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Signature",
                table: "Investigations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Signature",
                table: "Investigations");
        }
    }
}

