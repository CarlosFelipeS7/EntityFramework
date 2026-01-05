using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FuscaFilmes.Migrations
{
    /// <inheritdoc />
    public partial class M2MCreatingCollection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
             migrationBuilder.CreateTable(
                name: "DiretorFilme",
                columns: table => new
                {
                    DiretoresId = table.Column<int>(type: "INTEGER", nullable: false),
                    FilmesId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiretorFilme", x => new { x.DiretoresId, x.FilmesId });
                    table.ForeignKey(
                        name: "FK_DiretorFilme_Diretores_DiretoresId",
                        column: x => x.DiretoresId,
                        principalTable: "Diretores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DiretorFilme_Filmes_FilmesId",
                        column: x => x.FilmesId,
                        principalTable: "Filmes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Diretores",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Steven Spielberg" },
                    { 2, "Christopher Nolan" },
                    { 3, "Quentin Tarantino" },
                    { 4, "James Cameron" }
                });

            migrationBuilder.InsertData(
                table: "Filmes",
                columns: new[] { "Id", "Ano", "Titulo" },
                values: new object[,]
                {
                    { 1, 2010, "TinTin" },
                    { 2, 1993, "Jurassic Park" },
                    { 3, 2010, "Inception" },
                    { 4, 2014, "Interstellar" },
                    { 5, 1994, "Pulp Fiction" },
                    { 6, 2003, "Kill Bill" },
                    { 7, 2009, "Avatar" },
                    { 8, 1997, "Titanic" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_DiretorFilme_FilmesId",
                table: "DiretorFilme",
                column: "FilmesId");

            migrationBuilder.CreateIndex(
               name: "IX_DiretorFilme_DiretoresId",
               table: "DiretorFilme",
               column: "DiretoresId");

            migrationBuilder.Sql(@"
                INSERT INTO DiretorFilme (FilmesId, DiretoresId) ;
                SELECT Id, DiretorId FROM Filmes WHERE DiretorId is not null"
            );


            migrationBuilder.DropForeignKey(
                name: "FK_Filmes_Diretores_DiretorId",
                table: "Filmes");

            migrationBuilder.DropIndex(
                name: "IX_Filmes_DiretorId",
                table: "Filmes");

            migrationBuilder.DropColumn(
                name: "DiretorId",
                table: "Filmes");


        }




        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
             name: "DiretorId",
             table: "Filmes",
             type: "INTEGER",
             nullable: false,
             defaultValue: 0);

            migrationBuilder.Sql(@"
                UPDATE Filme
             SET DiretorId = DF.DiretorId   
            FROM Filmes F
            JOIN DiretorFilme DF ON F.Id = DF.FilmesId
            WHERE DF.DiretoresId IS NOT NULL"
           );


            migrationBuilder.DropTable(
                name: "DiretorFilme");

            migrationBuilder.DeleteData(
                table: "Diretores",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Diretores",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Diretores",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Diretores",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Filmes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Filmes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Filmes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Filmes",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Filmes",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Filmes",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Filmes",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Filmes",
                keyColumn: "Id",
                keyValue: 8);

         

            migrationBuilder.CreateIndex(
                name: "IX_Filmes_DiretorId",
                table: "Filmes",
                column: "DiretorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Filmes_Diretores_DiretorId",
                table: "Filmes",
                column: "DiretorId",
                principalTable: "Diretores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
