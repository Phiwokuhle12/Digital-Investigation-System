using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DigitalInvestigationAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialPostgreSQLCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cases",
                columns: table => new
                {
                    CaseID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CaseName = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cases", x => x.CaseID);
                });

            migrationBuilder.CreateTable(
                name: "Evidence",
                columns: table => new
                {
                    EvidenceID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Location = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Evidence", x => x.EvidenceID);
                });

            migrationBuilder.CreateTable(
                name: "Suspects",
                columns: table => new
                {
                    SuspectID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SuspectName = table.Column<string>(type: "text", nullable: false),
                    Occupation = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suspects", x => x.SuspectID);
                });

            migrationBuilder.CreateTable(
                name: "Investigations",
                columns: table => new
                {
                    InvestigationID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CaseID = table.Column<int>(type: "integer", nullable: false),
                    SuspectID = table.Column<int>(type: "integer", nullable: false),
                    Conclusion = table.Column<string>(type: "text", nullable: false),
                    DateStarted = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Investigations", x => x.InvestigationID);
                    table.ForeignKey(
                        name: "FK_Investigations_Cases_CaseID",
                        column: x => x.CaseID,
                        principalTable: "Cases",
                        principalColumn: "CaseID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Investigations_Suspects_SuspectID",
                        column: x => x.SuspectID,
                        principalTable: "Suspects",
                        principalColumn: "SuspectID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Cases",
                columns: new[] { "CaseID", "CaseName", "Description", "Status" },
                values: new object[] { 1, "The Missing Prototype", "A technology prototype has disappeared from the research laboratory.", "OPEN" });

            migrationBuilder.InsertData(
                table: "Evidence",
                columns: new[] { "EvidenceID", "Description", "Location", "Title" },
                values: new object[,]
                {
                    { 1, "Jamie Smith's access card was used to enter the research laboratory at 23:41.", "Security Office", "Security Access Log" },
                    { 2, "CCTV footage shows a person entering the laboratory at approximately 23:43. The person's face cannot be clearly identified.", "Research Laboratory", "CCTV Report" },
                    { 3, "A partial fingerprint was found on the prototype storage cabinet. The fingerprint belongs to a person who regularly works in the laboratory.", "Research Laboratory", "Fingerprint Report" },
                    { 4, "An email sent shortly before the incident states: \"The prototype must be moved before tomorrow's demonstration.\"", "Archive Room", "Email Message" },
                    { 5, "A photograph taken after the incident shows that the prototype cabinet was open and the laboratory lights were switched off.", "Research Laboratory", "Photograph" }
                });

            migrationBuilder.InsertData(
                table: "Suspects",
                columns: new[] { "SuspectID", "Description", "Occupation", "SuspectName" },
                values: new object[,]
                {
                    { 1, "Alex developed the software used by the prototype and had access to the laboratory.", "Software Developer", "Alex Morgan" },
                    { 2, "Jamie was responsible for security at the building on the night of the incident.", "Security Officer", "Jamie Smith" },
                    { 3, "Taylor worked with the research team and had access to the laboratory during working hours.", "Research Assistant", "Taylor Williams" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Investigations_CaseID",
                table: "Investigations",
                column: "CaseID");

            migrationBuilder.CreateIndex(
                name: "IX_Investigations_SuspectID",
                table: "Investigations",
                column: "SuspectID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Evidence");

            migrationBuilder.DropTable(
                name: "Investigations");

            migrationBuilder.DropTable(
                name: "Cases");

            migrationBuilder.DropTable(
                name: "Suspects");
        }
    }
}
