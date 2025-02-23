using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookManagmentApi.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueTitle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_books_Title",
                table: "books",
                column: "Title",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_books_Title",
                table: "books");
        }
    }
}
