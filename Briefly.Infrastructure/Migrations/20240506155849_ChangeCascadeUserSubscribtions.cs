#nullable disable

namespace Briefly.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeCascadeUserSubscribtions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RssSubscriptions_AspNetUsers_UserId",
                table: "RssSubscriptions");

            migrationBuilder.AddForeignKey(
                name: "FK_RssSubscriptions_AspNetUsers_UserId",
                table: "RssSubscriptions",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RssSubscriptions_AspNetUsers_UserId",
                table: "RssSubscriptions");

            migrationBuilder.AddForeignKey(
                name: "FK_RssSubscriptions_AspNetUsers_UserId",
                table: "RssSubscriptions",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
