using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WC.Service.PersonalData.Data.PostgreSql.Migrations
{
    /// <inheritdoc />
    public partial class PersonalDaTa_EmployeeID_Is_Unique : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PersonalData_Email",
                table: "PersonalData");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalData_Email_EmployeeId",
                table: "PersonalData",
                columns: new[] { "Email", "EmployeeId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PersonalData_Email_EmployeeId",
                table: "PersonalData");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalData_Email",
                table: "PersonalData",
                column: "Email",
                unique: true);
        }
    }
}
