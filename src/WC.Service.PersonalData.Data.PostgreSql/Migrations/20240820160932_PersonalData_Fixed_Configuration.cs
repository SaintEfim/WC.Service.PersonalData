using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WC.Service.PersonalData.Data.PostgreSql.Migrations
{
    /// <inheritdoc />
    public partial class PersonalData_Fixed_Configuration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PersonalData_EmployeeId",
                table: "PersonalData");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_PersonalData_EmployeeId",
                table: "PersonalData",
                column: "EmployeeId",
                unique: true);
        }
    }
}
