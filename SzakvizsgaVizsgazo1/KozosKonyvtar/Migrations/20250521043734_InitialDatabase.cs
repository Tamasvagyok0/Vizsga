using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KozosKonyvtar.Migrations
{
    /// <inheritdoc />
    public partial class InitialDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Helyszin",
                columns: table => new
                {
                    HelyszinId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HelyszinNev = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Helyszin", x => x.HelyszinId);
                });

            migrationBuilder.CreateTable(
                name: "Orszag",
                columns: table => new
                {
                    OrszagId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrszagKod = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orszag", x => x.OrszagId);
                });

            migrationBuilder.CreateTable(
                name: "Sportolo",
                columns: table => new
                {
                    SportoloId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SportoloNev = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrszagId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sportolo", x => x.SportoloId);
                });

            migrationBuilder.CreateTable(
                name: "Verseny",
                columns: table => new
                {
                    VersenyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Datum = table.Column<DateOnly>(type: "date", nullable: false),
                    SportoloId = table.Column<int>(type: "int", nullable: false),
                    HelyszinId = table.Column<int>(type: "int", nullable: false),
                    Helyezes = table.Column<int>(type: "int", nullable: false),
                    Eredmeny = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Verseny", x => x.VersenyId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Helyszin");

            migrationBuilder.DropTable(
                name: "Orszag");

            migrationBuilder.DropTable(
                name: "Sportolo");

            migrationBuilder.DropTable(
                name: "Verseny");
        }
    }
}
