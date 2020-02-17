using Microsoft.EntityFrameworkCore.Migrations;

namespace PM.Model.Migrations.SqliteMigrations
{
    public partial class ClientInfoAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClientInfos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FullAddress = table.Column<string>(nullable: true),
                    CountryId = table.Column<int>(nullable: true),
                    ProvinceId = table.Column<int>(nullable: true),
                    UserId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientInfos_Locations_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ClientInfos_Locations_ProvinceId",
                        column: x => x.ProvinceId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ClientInfos_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClientInfos_CountryId",
                table: "ClientInfos",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientInfos_ProvinceId",
                table: "ClientInfos",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientInfos_UserId",
                table: "ClientInfos",
                column: "UserId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClientInfos");
        }
    }
}
