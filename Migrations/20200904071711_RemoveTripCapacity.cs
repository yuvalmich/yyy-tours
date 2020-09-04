using Microsoft.EntityFrameworkCore.Migrations;

namespace yyytours.Migrations
{
    public partial class RemoveTripCapacity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trip_User_GuideId",
                table: "Trip");

            migrationBuilder.DropForeignKey(
                name: "FK_Trip_Place_PlaceId",
                table: "Trip");

            migrationBuilder.DropColumn(
                name: "Capacity",
                table: "Trip");

            migrationBuilder.AlterColumn<string>(
                name: "PlaceId",
                table: "Trip",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "GuideId",
                table: "Trip",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DisplayName",
                table: "Trip",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Trip",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Trip_User_GuideId",
                table: "Trip",
                column: "GuideId",
                principalTable: "User",
                principalColumn: "Email",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Trip_Place_PlaceId",
                table: "Trip",
                column: "PlaceId",
                principalTable: "Place",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trip_User_GuideId",
                table: "Trip");

            migrationBuilder.DropForeignKey(
                name: "FK_Trip_Place_PlaceId",
                table: "Trip");

            migrationBuilder.AlterColumn<string>(
                name: "PlaceId",
                table: "Trip",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "GuideId",
                table: "Trip",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "DisplayName",
                table: "Trip",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Trip",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<int>(
                name: "Capacity",
                table: "Trip",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Trip_User_GuideId",
                table: "Trip",
                column: "GuideId",
                principalTable: "User",
                principalColumn: "Email",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Trip_Place_PlaceId",
                table: "Trip",
                column: "PlaceId",
                principalTable: "Place",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
