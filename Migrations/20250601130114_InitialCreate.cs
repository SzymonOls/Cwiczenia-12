using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFTest.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    IdClient = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Telephone = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Pesel = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.IdClient);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    IdCountry = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.IdCountry);
                });

            migrationBuilder.CreateTable(
                name: "Trips",
                columns: table => new
                {
                    IdTrip = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateFrom = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateTo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MaxPeople = table.Column<int>(type: "int", nullable: false),
                    CountryIdCountry = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trips", x => x.IdTrip);
                    table.ForeignKey(
                        name: "FK_Trips_Countries_CountryIdCountry",
                        column: x => x.CountryIdCountry,
                        principalTable: "Countries",
                        principalColumn: "IdCountry");
                });

            migrationBuilder.CreateTable(
                name: "ClientTrips",
                columns: table => new
                {
                    IdClient = table.Column<int>(type: "int", nullable: false),
                    IdTrip = table.Column<int>(type: "int", nullable: false),
                    IdClientTrip = table.Column<int>(type: "int", nullable: false),
                    RegisteredAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PaymentDate = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientTrips", x => new { x.IdClient, x.IdTrip });
                    table.ForeignKey(
                        name: "FK_ClientTrips_Clients_IdClient",
                        column: x => x.IdClient,
                        principalTable: "Clients",
                        principalColumn: "IdClient",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClientTrips_Trips_IdTrip",
                        column: x => x.IdTrip,
                        principalTable: "Trips",
                        principalColumn: "IdTrip",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CountryTrips",
                columns: table => new
                {
                    IdCountry = table.Column<int>(type: "int", nullable: false),
                    IdTrip = table.Column<int>(type: "int", nullable: false),
                    IdCountryTrip = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountryTrips", x => new { x.IdCountry, x.IdTrip });
                    table.ForeignKey(
                        name: "FK_CountryTrips_Countries_IdCountry",
                        column: x => x.IdCountry,
                        principalTable: "Countries",
                        principalColumn: "IdCountry",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CountryTrips_Trips_IdTrip",
                        column: x => x.IdTrip,
                        principalTable: "Trips",
                        principalColumn: "IdTrip",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClientTrips_IdTrip",
                table: "ClientTrips",
                column: "IdTrip");

            migrationBuilder.CreateIndex(
                name: "IX_CountryTrips_IdTrip",
                table: "CountryTrips",
                column: "IdTrip");

            migrationBuilder.CreateIndex(
                name: "IX_Trips_CountryIdCountry",
                table: "Trips",
                column: "CountryIdCountry");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClientTrips");

            migrationBuilder.DropTable(
                name: "CountryTrips");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "Trips");

            migrationBuilder.DropTable(
                name: "Countries");
        }
    }
}
