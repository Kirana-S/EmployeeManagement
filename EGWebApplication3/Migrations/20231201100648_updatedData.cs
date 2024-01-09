using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EGWebApplication3.Migrations
{
    /// <inheritdoc />
    public partial class updatedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.AddColumn<string>(
                name: "PhotoPath",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 6,
                column: "PhotoPath",
                value: "images/noimage.jpg");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhotoPath",
                table: "Employees");

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "Department", "Email", "Name" },
                values: new object[,]
                {
                    { 4, 2, "ram@gmail.com", "Ram" },
                    { 5, 1, "ra@gmail.com", "Raj" },
                    { 7, 1, "xyz@gmail.com", "xyz" },
                    { 8, 1, "Tom@gmail.com", "Tom" }
                });
        }
    }
}
