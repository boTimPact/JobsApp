using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Worktastic.Data.Migrations
{
    /// <inheritdoc />
    public partial class isCompanyField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isCompanyAccount",
                table: "AspNetUsers",
                type: "bit",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isCompanyAccount",
                table: "AspNetUsers");
        }
    }
}
