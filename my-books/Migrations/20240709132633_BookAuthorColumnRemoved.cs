using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace my_books.Migrations
{
    /// <inheritdoc />
    public partial class BookAuthorColumnRemoved : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_book_Authors_Authors_AuthorId",
                table: "book_Authors");

            migrationBuilder.DropForeignKey(
                name: "FK_book_Authors_Books_BookId",
                table: "book_Authors");

            migrationBuilder.DropPrimaryKey(
                name: "PK_book_Authors",
                table: "book_Authors");

            migrationBuilder.DropColumn(
                name: "Author",
                table: "Books");

            migrationBuilder.RenameTable(
                name: "book_Authors",
                newName: "Book_Authors");

            migrationBuilder.RenameIndex(
                name: "IX_book_Authors_BookId",
                table: "Book_Authors",
                newName: "IX_Book_Authors_BookId");

            migrationBuilder.RenameIndex(
                name: "IX_book_Authors_AuthorId",
                table: "Book_Authors",
                newName: "IX_Book_Authors_AuthorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Book_Authors",
                table: "Book_Authors",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Book_Authors_Authors_AuthorId",
                table: "Book_Authors",
                column: "AuthorId",
                principalTable: "Authors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Book_Authors_Books_BookId",
                table: "Book_Authors",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Book_Authors_Authors_AuthorId",
                table: "Book_Authors");

            migrationBuilder.DropForeignKey(
                name: "FK_Book_Authors_Books_BookId",
                table: "Book_Authors");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Book_Authors",
                table: "Book_Authors");

            migrationBuilder.RenameTable(
                name: "Book_Authors",
                newName: "book_Authors");

            migrationBuilder.RenameIndex(
                name: "IX_Book_Authors_BookId",
                table: "book_Authors",
                newName: "IX_book_Authors_BookId");

            migrationBuilder.RenameIndex(
                name: "IX_Book_Authors_AuthorId",
                table: "book_Authors",
                newName: "IX_book_Authors_AuthorId");

            migrationBuilder.AddColumn<string>(
                name: "Author",
                table: "Books",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_book_Authors",
                table: "book_Authors",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_book_Authors_Authors_AuthorId",
                table: "book_Authors",
                column: "AuthorId",
                principalTable: "Authors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_book_Authors_Books_BookId",
                table: "book_Authors",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
