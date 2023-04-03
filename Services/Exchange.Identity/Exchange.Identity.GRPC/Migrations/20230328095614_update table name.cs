using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Exchange.Identity.GRPC.Migrations
{
    /// <inheritdoc />
    public partial class updatetablename : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JWTWhiteList");

            migrationBuilder.CreateTable(
                name: "TBL_ADM_JWT_WHITE_LIST",
                columns: table => new
                {
                    WhiteListKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AccessToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccessTokenExpiredIn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RefreshTokenExpiredIn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsExpired = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_ADM_JWT_WHITE_LIST", x => x.WhiteListKey);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TBL_ADM_JWT_WHITE_LIST");

            migrationBuilder.CreateTable(
                name: "JWTWhiteList",
                columns: table => new
                {
                    WhiteListKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AccessToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccessTokenExpiredIn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsExpired = table.Column<bool>(type: "bit", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RefreshTokenExpiredIn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JWTWhiteList", x => x.WhiteListKey);
                });
        }
    }
}
