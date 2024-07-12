using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelListing.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RevertCodeName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ShortName",
                table: "Countries",
                newName: "CodeName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CodeName",
                table: "Countries",
                newName: "ShortName");
        }
    }
}
