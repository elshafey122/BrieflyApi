#nullable disable

namespace Briefly.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class canselhasdefaultvalfalsearticle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasParentAndChildrenComment",
                table: "CommentsArticle");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HasParentAndChildrenComment",
                table: "CommentsArticle",
                type: "bit",
                nullable: true,
                defaultValue: false);
        }
    }
}
