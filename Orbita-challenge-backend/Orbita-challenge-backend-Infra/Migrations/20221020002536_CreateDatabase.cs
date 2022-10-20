using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Orbita_challenge_backend_Infra.Migrations
{
    public partial class CreateDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Aluno",
                columns: table => new
                {
                    RA = table.Column<string>(type: "varchar", maxLength: 50, nullable: false),
                    Nome = table.Column<string>(type: "varchar", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "varchar", maxLength: 100, nullable: false),
                    CPF = table.Column<string>(type: "char(14)", maxLength: 14, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aluno", x => x.RA);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Aluno");
        }
    }
}
