using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Lib.Migrations
{
    /// <inheritdoc />
    public partial class addconfigs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Configs",
                columns: table => new
                {
                    Key = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastTimeUpdate = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Configs", x => x.Key);
                });

            migrationBuilder.InsertData(
                table: "Configs",
                columns: new[] { "Key", "LastTimeUpdate", "Value" },
                values: new object[,]
                {
                    { "buvid3", 0L, "AFE917A3-F157-AB06-AD81-F5E97E8B5D9A60062infoc" },
                    { "buvid4", 0L, "DAC8A899-9006-0900-5D29-5D64C5BB0F8824741-023013104-NLgo%2F2RgzmTINRwjdsii8w%3D%3D" },
                    { "SESSDATA", 0L, "" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Configs");
        }
    }
}
