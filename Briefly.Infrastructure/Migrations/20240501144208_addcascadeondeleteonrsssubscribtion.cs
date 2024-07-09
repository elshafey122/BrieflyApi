#nullable disable

namespace Briefly.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addcascadeondeleteonrsssubscribtion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RssSubscriptions_RSSes_RssId",
                table: "RssSubscriptions");

            migrationBuilder.AddForeignKey(
                name: "FK_RssSubscriptions_RSSes_RssId",
                table: "RssSubscriptions",
                column: "RssId",
                principalTable: "RSSes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RssSubscriptions_RSSes_RssId",
                table: "RssSubscriptions");

            migrationBuilder.AddForeignKey(
                name: "FK_RssSubscriptions_RSSes_RssId",
                table: "RssSubscriptions",
                column: "RssId",
                principalTable: "RSSes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
