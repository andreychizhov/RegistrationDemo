using Microsoft.EntityFrameworkCore.Migrations;

namespace PM.Model.Migrations.SqliteMigrations
{
    public partial class CountriesAndProvincesAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true),
                    LocationType = table.Column<int>(nullable: false),
                    ParentId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Locations_Locations_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Locations",
                columns: new[] { "Id", "LocationType", "Name" },
                values: new object[] { 1, 0, "USA" });

            migrationBuilder.InsertData(
                table: "Locations",
                columns: new[] { "Id", "LocationType", "Name" },
                values: new object[] { 5, 0, "Ireland" });

            migrationBuilder.InsertData(
                table: "Locations",
                columns: new[] { "Id", "LocationType", "Name", "ParentId" },
                values: new object[] { 2, 1, "USA Province 1", 1 });

            migrationBuilder.InsertData(
                table: "Locations",
                columns: new[] { "Id", "LocationType", "Name", "ParentId" },
                values: new object[] { 3, 1, "USA Province 2", 1 });

            migrationBuilder.InsertData(
                table: "Locations",
                columns: new[] { "Id", "LocationType", "Name", "ParentId" },
                values: new object[] { 4, 1, "USA Province 3", 1 });

            migrationBuilder.InsertData(
                table: "Locations",
                columns: new[] { "Id", "LocationType", "Name", "ParentId" },
                values: new object[] { 6, 1, "Ireland Province 1", 5 });

            migrationBuilder.InsertData(
                table: "Locations",
                columns: new[] { "Id", "LocationType", "Name", "ParentId" },
                values: new object[] { 7, 1, "Ireland Province 2", 5 });

            migrationBuilder.CreateIndex(
                name: "IX_Locations_ParentId",
                table: "Locations",
                column: "ParentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Locations");
        }
    }
}
