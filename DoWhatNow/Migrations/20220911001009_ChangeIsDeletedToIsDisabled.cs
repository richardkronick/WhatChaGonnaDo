using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DoWhatNow.Migrations
{
    public partial class ChangeIsDeletedToIsDisabled : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "ToDoLists",
                newName: "IsDisabled");

            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "ToDoItems",
                newName: "IsDisabled");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsDisabled",
                table: "ToDoLists",
                newName: "IsDeleted");

            migrationBuilder.RenameColumn(
                name: "IsDisabled",
                table: "ToDoItems",
                newName: "IsDeleted");
        }
    }
}
