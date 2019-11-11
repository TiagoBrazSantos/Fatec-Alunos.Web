using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Fatec.Alunos.Data.Migrations
{
    public partial class add_aluno : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Aluno",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    Nome = table.Column<string>(maxLength: 255, nullable: false),
                    Documento = table.Column<string>(maxLength: 255, nullable: false),
                    Ip = table.Column<string>(maxLength: 255, nullable: false),
                    NomeComputador = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aluno", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Aluno");
        }
    }
}
