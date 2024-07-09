
namespace Briefly.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addclusterlab : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ClusterLabel",
                table: "Articles",
                type: "int",
                nullable: false,
                defaultValue: -1);

            migrationBuilder.AddColumn<int>(
                name: "CommentsNumber",
                table: "Articles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Articles_ClusterLabel",
                table: "Articles",
                column: "ClusterLabel");

            
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.DropIndex(
                name: "IX_Articles_ClusterLabel",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "ClusterLabel",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "CommentsNumber",
                table: "Articles");
        }
    }
}
