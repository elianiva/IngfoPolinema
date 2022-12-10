using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace IngfoPolinema.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PingTargets",
                columns: table => new
                {
                    PingTargetId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    Address = table.Column<string>(type: "TEXT", nullable: false),
                    Interval = table.Column<double>(type: "REAL", nullable: false),
                    Timeout = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PingTargets", x => x.PingTargetId);
                });

            migrationBuilder.CreateTable(
                name: "PingHistories",
                columns: table => new
                {
                    PingHistoryId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Duration = table.Column<double>(type: "REAL", nullable: false),
                    StatusCode = table.Column<int>(type: "INTEGER", nullable: false),
                    TimeStamp = table.Column<long>(type: "INTEGER", nullable: false),
                    PingTargetId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PingHistories", x => x.PingHistoryId);
                    table.ForeignKey(
                        name: "FK_PingHistories_PingTargets_PingTargetId",
                        column: x => x.PingTargetId,
                        principalTable: "PingTargets",
                        principalColumn: "PingTargetId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "PingTargets",
                columns: new[] { "PingTargetId", "Address", "Description", "Interval", "Name", "Timeout" },
                values: new object[,]
                {
                    { new Guid("41ccae87-1eb3-48cc-8b02-9d21502b23fa"), "http://siakad.polinema.ac.id/beranda", "Sistem Informasi Akademik Politeknik Negeri Malang", 10000.0, "SIAKAD Polinema", 60000.0 },
                    { new Guid("7b592b6f-e5f0-4c0e-b668-271110ad9a80"), "https://www.polinema.ac.id/", "Website utama Politeknik Negeri Malang", 10000.0, "Polinema Main Website", 60000.0 },
                    { new Guid("c26faa8e-d1a2-4540-9457-f2c7f9325786"), "http://jti.polinema.ac.id/", "Website Jurusan Teknologi Informasi Politeknik Negeri Malang", 10000.0, "JTI Polinema Main Website", 60000.0 },
                    { new Guid("db3023f1-f310-4afe-82ce-5ce8ae5986e5"), "https://lmsslc.polinema.ac.id/", "Learning Management System Politeknik Negeri Malang", 10000.0, "Polinema LMS", 60000.0 },
                    { new Guid("dc0e4367-c6f2-465f-8500-59038c7b5681"), "https://slc.polinema.ac.id/spada/", "Portal LMS Politeknik Negeri Malang", 10000.0, "SLC Polinema", 60000.0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_PingHistories_PingTargetId",
                table: "PingHistories",
                column: "PingTargetId");

            migrationBuilder.CreateIndex(
                name: "IX_PingTargets_Address",
                table: "PingTargets",
                column: "Address",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PingHistories");

            migrationBuilder.DropTable(
                name: "PingTargets");
        }
    }
}
