using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lib.Migrations
{
    /// <inheritdoc />
    public partial class addreleationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "TypeId",
                table: "Videos",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<long>(
                name: "MostRecentVideoRecordId",
                table: "Videos",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<long>(
                name: "UserUid",
                table: "Videos",
                type: "bigint",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "BvId",
                table: "VideoRecords",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "ViewingNum",
                table: "VideoRecords",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Videos_AuthorUid",
                table: "Videos",
                column: "AuthorUid");

            migrationBuilder.CreateIndex(
                name: "IX_Videos_MostRecentVideoRecordId",
                table: "Videos",
                column: "MostRecentVideoRecordId",
                unique: true,
                filter: "[MostRecentVideoRecordId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Videos_TypeId",
                table: "Videos",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Videos_UserUid",
                table: "Videos",
                column: "UserUid");

            migrationBuilder.CreateIndex(
                name: "IX_VideoRecords_BvId",
                table: "VideoRecords",
                column: "BvId");

            migrationBuilder.AddForeignKey(
                name: "FK_VideoRecords_Videos_BvId",
                table: "VideoRecords",
                column: "BvId",
                principalTable: "Videos",
                principalColumn: "BvId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Videos_Users_AuthorUid",
                table: "Videos",
                column: "AuthorUid",
                principalTable: "Users",
                principalColumn: "Uid",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Videos_Users_UserUid",
                table: "Videos",
                column: "UserUid",
                principalTable: "Users",
                principalColumn: "Uid");

            migrationBuilder.AddForeignKey(
                name: "FK_Videos_VideoRecords_MostRecentVideoRecordId",
                table: "Videos",
                column: "MostRecentVideoRecordId",
                principalTable: "VideoRecords",
                principalColumn: "VrId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Videos_VideoTypes_TypeId",
                table: "Videos",
                column: "TypeId",
                principalTable: "VideoTypes",
                principalColumn: "TypeId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VideoRecords_Videos_BvId",
                table: "VideoRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_Videos_Users_AuthorUid",
                table: "Videos");

            migrationBuilder.DropForeignKey(
                name: "FK_Videos_Users_UserUid",
                table: "Videos");

            migrationBuilder.DropForeignKey(
                name: "FK_Videos_VideoRecords_MostRecentVideoRecordId",
                table: "Videos");

            migrationBuilder.DropForeignKey(
                name: "FK_Videos_VideoTypes_TypeId",
                table: "Videos");

            migrationBuilder.DropIndex(
                name: "IX_Videos_AuthorUid",
                table: "Videos");

            migrationBuilder.DropIndex(
                name: "IX_Videos_MostRecentVideoRecordId",
                table: "Videos");

            migrationBuilder.DropIndex(
                name: "IX_Videos_TypeId",
                table: "Videos");

            migrationBuilder.DropIndex(
                name: "IX_Videos_UserUid",
                table: "Videos");

            migrationBuilder.DropIndex(
                name: "IX_VideoRecords_BvId",
                table: "VideoRecords");

            migrationBuilder.DropColumn(
                name: "UserUid",
                table: "Videos");

            migrationBuilder.DropColumn(
                name: "ViewingNum",
                table: "VideoRecords");

            migrationBuilder.AlterColumn<int>(
                name: "TypeId",
                table: "Videos",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "MostRecentVideoRecordId",
                table: "Videos",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "BvId",
                table: "VideoRecords",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
