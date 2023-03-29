using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Exchange.Identity.GRPC.Migrations
{
    /// <inheritdoc />
    public partial class addwhitelistkey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_JWTWhiteList",
                table: "JWTWhiteList");

            migrationBuilder.AlterColumn<string>(
                name: "AccessToken",
                table: "JWTWhiteList",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "WhiteListKey",
                table: "JWTWhiteList",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_JWTWhiteList",
                table: "JWTWhiteList",
                column: "WhiteListKey");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_JWTWhiteList",
                table: "JWTWhiteList");

            migrationBuilder.DropColumn(
                name: "WhiteListKey",
                table: "JWTWhiteList");

            migrationBuilder.AlterColumn<string>(
                name: "AccessToken",
                table: "JWTWhiteList",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_JWTWhiteList",
                table: "JWTWhiteList",
                column: "AccessToken");
        }
    }
}
