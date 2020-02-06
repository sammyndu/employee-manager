using Microsoft.EntityFrameworkCore.Migrations;

namespace EManager3.Migrations
{
    public partial class Subs3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Subordinates");
            // migrationBuilder.DropForeignKey(
            //     name: "FK_Subordinates_AspNetUsers_SubordinateId",
            //     table: "Subordinates");

            // migrationBuilder.AddForeignKey(
            //     name: "FK_Subordinates_AspNetUsers_SubordinateId",
            //     table: "Subordinates",
            //     column: "SubordinateId",
            //     principalTable: "AspNetUsers",
            //     principalColumn: "Id",
            //     onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subordinates_AspNetUsers_SubordinateId",
                table: "Subordinates");

            migrationBuilder.AddForeignKey(
                name: "FK_Subordinates_AspNetUsers_SubordinateId",
                table: "Subordinates",
                column: "SubordinateId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
