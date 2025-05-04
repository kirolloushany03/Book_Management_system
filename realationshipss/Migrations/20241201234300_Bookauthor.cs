using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace realationshipss.Migrations
{
    /// <inheritdoc />
    public partial class Bookauthor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookAuthor_TbAuthors_AuthorId",
                table: "BookAuthor");

            migrationBuilder.DropForeignKey(
                name: "FK_BookAuthor_TbBOOK_BookId",
                table: "BookAuthor");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BookAuthor",
                table: "BookAuthor");

            migrationBuilder.RenameTable(
                name: "BookAuthor",
                newName: "TbBookAuthor");

            migrationBuilder.RenameIndex(
                name: "IX_BookAuthor_AuthorId",
                table: "TbBookAuthor",
                newName: "IX_TbBookAuthor_AuthorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TbBookAuthor",
                table: "TbBookAuthor",
                columns: new[] { "BookId", "AuthorId" });

            migrationBuilder.AddForeignKey(
                name: "FK_TbBookAuthor_TbAuthors_AuthorId",
                table: "TbBookAuthor",
                column: "AuthorId",
                principalTable: "TbAuthors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TbBookAuthor_TbBOOK_BookId",
                table: "TbBookAuthor",
                column: "BookId",
                principalTable: "TbBOOK",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TbBookAuthor_TbAuthors_AuthorId",
                table: "TbBookAuthor");

            migrationBuilder.DropForeignKey(
                name: "FK_TbBookAuthor_TbBOOK_BookId",
                table: "TbBookAuthor");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TbBookAuthor",
                table: "TbBookAuthor");

            migrationBuilder.RenameTable(
                name: "TbBookAuthor",
                newName: "BookAuthor");

            migrationBuilder.RenameIndex(
                name: "IX_TbBookAuthor_AuthorId",
                table: "BookAuthor",
                newName: "IX_BookAuthor_AuthorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookAuthor",
                table: "BookAuthor",
                columns: new[] { "BookId", "AuthorId" });

            migrationBuilder.AddForeignKey(
                name: "FK_BookAuthor_TbAuthors_AuthorId",
                table: "BookAuthor",
                column: "AuthorId",
                principalTable: "TbAuthors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookAuthor_TbBOOK_BookId",
                table: "BookAuthor",
                column: "BookId",
                principalTable: "TbBOOK",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
