using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blog.Web.Migrations.AuthDb
{
    /// <inheritdoc />
    public partial class Auth2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "99394798-e721-488e-8865-88f972541410",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "aae163e1-a234-4989-bcfe-a076c6bc9153", "AQAAAAIAAYagAAAAEH4Wbjb8bg5s4cOewrKmReynQOs4n9egUJlJhK5P0nMxpWU2fF71myQJQfX73f03tw==", "9155120c-b2ec-4a7e-aab7-954079b56e96" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "99394798-e721-488e-8865-88f972541410",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f3489ef9-c79f-41a7-8fb3-890ba3a72840", "AQAAAAIAAYagAAAAEKCq1ScZYn5uqayp9cfKRwMkiRwJrJ/5XcP3PBp4CevDu5JIeZEBb6276f7v1oauEw==", "ee42d4e6-439e-4b0d-ab00-d7e559135c00" });
        }
    }
}
