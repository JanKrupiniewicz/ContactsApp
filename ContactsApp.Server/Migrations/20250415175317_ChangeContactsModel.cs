using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContactsApp.Server.Migrations
{
    /// <inheritdoc />
    public partial class ChangeContactsModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_Users_UsersId",
                table: "Contacts");

            migrationBuilder.DropIndex(
                name: "IX_Contacts_UsersId",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "UsersId",
                table: "Contacts");

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_UserId",
                table: "Contacts",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contacts_Users_UserId",
                table: "Contacts",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_Users_UserId",
                table: "Contacts");

            migrationBuilder.DropIndex(
                name: "IX_Contacts_UserId",
                table: "Contacts");

            migrationBuilder.AddColumn<int>(
                name: "UsersId",
                table: "Contacts",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_UsersId",
                table: "Contacts",
                column: "UsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contacts_Users_UsersId",
                table: "Contacts",
                column: "UsersId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
