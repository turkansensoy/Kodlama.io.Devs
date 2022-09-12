using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class addtechnology : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Technology",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProgrammingLanguageId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Technology", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Technology_ProgrammingLanguage_ProgrammingLanguageId",
                        column: x => x.ProgrammingLanguageId,
                        principalTable: "ProgrammingLanguage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Technology",
                columns: new[] { "Id", "Name", "ProgrammingLanguageId" },
                values: new object[] { 1, "WPF", 1 });

            migrationBuilder.InsertData(
                table: "Technology",
                columns: new[] { "Id", "Name", "ProgrammingLanguageId" },
                values: new object[] { 2, "ASP.NET", 1 });

            migrationBuilder.InsertData(
                table: "Technology",
                columns: new[] { "Id", "Name", "ProgrammingLanguageId" },
                values: new object[] { 3, "Spring", 2 });

            migrationBuilder.CreateIndex(
                name: "IX_Technology_ProgrammingLanguageId",
                table: "Technology",
                column: "ProgrammingLanguageId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Technology");
        }
    }
}
