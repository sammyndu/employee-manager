using Microsoft.EntityFrameworkCore.Migrations;

namespace EManager3.Migrations
{
    public partial class Subs4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Subordinates");
            // migrationBuilder.CreateTable(
            //     name: "Subordinates",
            //     columns: table => new
            //     {
            //         Id = table.Column<int>(nullable: false)
            //             .Annotation("SqlServer:Identity", "1, 1"),
            //         UserId = table.Column<string>(nullable: true),
            //         SubordinateId = table.Column<string>(nullable: true)
            //     },
            //     constraints: table =>
            //     {
            //         table.PrimaryKey("PK_Subordinates", x => x.Id);
            //         table.ForeignKey(
            //             name: "FK_Subordinates_AspNetUsers_SubordinateId",
            //             column: x => x.SubordinateId,
            //             principalTable: "AspNetUsers",
            //             principalColumn: "Id",
            //             onDelete: ReferentialAction.Restrict);
            //         table.ForeignKey(
            //             name: "FK_Subordinates_AspNetUsers_UserId",
            //             column: x => x.UserId,
            //             principalTable: "AspNetUsers",
            //             principalColumn: "Id",
            //             onDelete: ReferentialAction.Cascade);
            //     });

            // migrationBuilder.CreateIndex(
            //     name: "IX_Subordinates_SubordinateId",
            //     table: "Subordinates",
            //     column: "SubordinateId");

            // migrationBuilder.CreateIndex(
            //     name: "IX_Subordinates_UserId",
            //     table: "Subordinates",
            //     column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Subordinates");
        }
    }
}
