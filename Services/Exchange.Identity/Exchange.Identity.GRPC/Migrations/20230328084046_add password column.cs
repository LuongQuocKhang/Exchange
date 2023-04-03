using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Exchange.Identity.GRPC.Migrations
{
    /// <inheritdoc />
    public partial class addpasswordcolumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "JWTWhiteList",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "JWTWhiteList");
        }
    }
}
