using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blog.Web.Migrations
{
    /// <inheritdoc />
    public partial class AddComment3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_BlogPostComments_BlogPostId",
                table: "BlogPostComments",
                column: "BlogPostId");

            migrationBuilder.AddForeignKey(
                name: "FK_BlogPostComments_BlogPosts_BlogPostId",
                table: "BlogPostComments",
                column: "BlogPostId",
                principalTable: "BlogPosts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogPostComments_BlogPosts_BlogPostId",
                table: "BlogPostComments");

            migrationBuilder.DropIndex(
                name: "IX_BlogPostComments_BlogPostId",
                table: "BlogPostComments");
        }
    }
}
