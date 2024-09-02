using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace paw_examen.Server.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Locatii",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Judet = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Oras = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Numar_locuitori = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locatii", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Proteste",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Denumire = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descriere = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Numar_participanti = table.Column<int>(type: "int", nullable: false),
                    LocatieId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proteste", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Proteste_Locatii_LocatieId",
                        column: x => x.LocatieId,
                        principalTable: "Locatii",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Locatii",
                columns: new[] { "Id", "Judet", "Numar_locuitori", "Oras" },
                values: new object[,]
                {
                    { 1, "Bucuresti", 10000, "Bucuresti" },
                    { 2, "Ilfov", 2500, "Buftea" },
                    { 3, "Prahova", 7000, "Ploiesti" }
                });

            migrationBuilder.InsertData(
                table: "Proteste",
                columns: new[] { "Id", "Data", "Denumire", "Descriere", "LocatieId", "Numar_participanti" },
                values: new object[,]
                {
                    { 1, new DateTime(2022, 2, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "Protest 1", "Protest despre problema 1", 1, 9000 },
                    { 2, new DateTime(2022, 3, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "Protest 2", "Protest despre problema 2", 1, 3700 },
                    { 3, new DateTime(2022, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "Protest 3", "Protest despre problema 3", 1, 7000 },
                    { 4, new DateTime(2022, 6, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Protest 4", "Protest despre problema 4", 3, 500 },
                    { 5, new DateTime(2022, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Protest 5", "Protest despre problema 5", 2, 1000 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Proteste_LocatieId",
                table: "Proteste",
                column: "LocatieId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Proteste");

            migrationBuilder.DropTable(
                name: "Locatii");
        }
    }
}
