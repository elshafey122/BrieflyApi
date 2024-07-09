#nullable disable

namespace Briefly.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class removersscategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RSSes_RssCategory_CategoryId",
                table: "RSSes");

            migrationBuilder.DropTable(
                name: "RssCategory");

            migrationBuilder.DropIndex(
                name: "IX_RSSes_CategoryId",
                table: "RSSes");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "RSSes");

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "Articles",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "Articles");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "RSSes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "RssCategory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RssCategory", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RSSes_CategoryId",
                table: "RSSes",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_RSSes_RssCategory_CategoryId",
                table: "RSSes",
                column: "CategoryId",
                principalTable: "RssCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
