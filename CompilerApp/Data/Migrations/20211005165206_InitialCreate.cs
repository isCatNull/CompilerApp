using Microsoft.EntityFrameworkCore.Migrations;

namespace CompilerApp.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CompilationSummary",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    SourceCode = table.Column<string>(nullable: false),
                    Output = table.Column<string>(nullable: true),
                    StatusCode = table.Column<int>(nullable: false),
                    Memory = table.Column<string>(nullable: true),
                    CpuTime = table.Column<string>(nullable: true),
                    Error = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompilationSummary", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompilationSummary");
        }
    }
}
