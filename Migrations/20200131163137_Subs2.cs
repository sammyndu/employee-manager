using Microsoft.EntityFrameworkCore.Migrations;

namespace EManager3.Migrations
{
    public partial class Subs2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "SubordinateId",
                table: "Subordinates",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Subordinates_SubordinateId",
                table: "Subordinates",
                column: "SubordinateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Subordinates_AspNetUsers_SubordinateId",
                table: "Subordinates",
                column: "SubordinateId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subordinates_AspNetUsers_SubordinateId",
                table: "Subordinates");

            migrationBuilder.DropIndex(
                name: "IX_Subordinates_SubordinateId",
                table: "Subordinates");

            migrationBuilder.AlterColumn<string>(
                name: "SubordinateId",
                table: "Subordinates",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
