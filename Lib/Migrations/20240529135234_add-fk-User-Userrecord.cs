using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lib.Migrations
{
    /// <inheritdoc />
    public partial class addfkUserUserrecord : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "MostRecentUserRecordId",
                table: "Users",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.CreateIndex(
                name: "IX_Users_MostRecentUserRecordId",
                table: "Users",
                column: "MostRecentUserRecordId",
                unique: true,
                filter: "[MostRecentUserRecordId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_UserRecords_Uid",
                table: "UserRecords",
                column: "Uid");

            migrationBuilder.AddForeignKey(
                name: "FK_UserRecords_Users_Uid",
                table: "UserRecords",
                column: "Uid",
                principalTable: "Users",
                principalColumn: "Uid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_UserRecords_MostRecentUserRecordId",
                table: "Users",
                column: "MostRecentUserRecordId",
                principalTable: "UserRecords",
                principalColumn: "UrId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserRecords_Users_Uid",
                table: "UserRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_UserRecords_MostRecentUserRecordId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_MostRecentUserRecordId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_UserRecords_Uid",
                table: "UserRecords");

            migrationBuilder.AlterColumn<long>(
                name: "MostRecentUserRecordId",
                table: "Users",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);
        }
    }
}
