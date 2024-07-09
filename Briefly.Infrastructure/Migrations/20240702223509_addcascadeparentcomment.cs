#nullable disable

namespace Briefly.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addcascadeparentcomment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommentsArticle_CommentsArticle_ParentCommentId",
                table: "CommentsArticle");

            migrationBuilder.AddForeignKey(
                name: "FK_CommentsArticle_CommentsArticle_ParentCommentId",
                table: "CommentsArticle",
                column: "ParentCommentId",
                principalTable: "CommentsArticle",  
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommentsArticle_CommentsArticle_ParentCommentId",
                table: "CommentsArticle");

            migrationBuilder.AddForeignKey(
                name: "FK_CommentsArticle_CommentsArticle_ParentCommentId",
                table: "CommentsArticle",
                column: "ParentCommentId",
                principalTable: "CommentsArticle",
                principalColumn: "Id");
        }
    }
}
