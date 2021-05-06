using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CapstoneProject.Migrations
{
    public partial class addedpropertytosalesperson : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c00203fb-0b14-42a9-b5ea-8c4fdbd066b0");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cd78db74-1a48-4bf2-bedb-f0c741c67625");

            migrationBuilder.AddColumn<DateTime>(
                name: "NewAppointment",
                table: "Salespeople",
                type: "datetime2",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "e6825553-704b-425e-b444-e358836575d4", "c41849a3-7461-479d-8a4f-a3865c62a75f", "Salesperson", "SALESPERSON" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "8cdd601e-63e6-4df1-a391-b9f5f2ceca20", "6ea55d73-a1a9-468f-9e5d-358da195ede2", "Customer", "CUSTOMER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8cdd601e-63e6-4df1-a391-b9f5f2ceca20");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e6825553-704b-425e-b444-e358836575d4");

            migrationBuilder.DropColumn(
                name: "NewAppointment",
                table: "Salespeople");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c00203fb-0b14-42a9-b5ea-8c4fdbd066b0", "01ff2793-4b25-4730-a498-f4fb7242e2f4", "Salesperson", "SALESPERSON" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "cd78db74-1a48-4bf2-bedb-f0c741c67625", "b0ba3768-bdd5-4ca2-b0bf-d82a7707ef45", "Customer", "CUSTOMER" });
        }
    }
}
