using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PdfExportSample.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PatientTable",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Age = table.Column<int>(type: "integer", nullable: false),
                    MRN = table.Column<string>(type: "text", nullable: false),
                    UIN = table.Column<string>(type: "text", nullable: false),
                    City = table.Column<string>(type: "text", nullable: false),
                    Sex = table.Column<string>(type: "text", nullable: false),
                    BillNo = table.Column<string>(type: "text", nullable: false),
                    BillDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IpaNo = table.Column<string>(type: "text", nullable: false),
                    PreauthId = table.Column<string>(type: "text", nullable: false),
                    DateOfAdmission = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateOfDischarge = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CCN = table.Column<string>(type: "text", nullable: false),
                    Surgery = table.Column<string>(type: "text", nullable: false),
                    ApprovedAmount = table.Column<decimal>(type: "numeric", nullable: false),
                    PackageRate = table.Column<int>(type: "integer", nullable: false),
                    BalanceAmountCollected = table.Column<decimal>(type: "numeric", nullable: false),
                    CoPaymentAddition = table.Column<decimal>(type: "numeric", nullable: false),
                    TPAName = table.Column<string>(type: "text", nullable: false),
                    InsuranceName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientTable", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PatientTable");
        }
    }
}
