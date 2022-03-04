using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecipeWebsiteMVC.Migrations
{
    public partial class UpdatedDriection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Directions_Recipes_RecipeId",
                table: "Directions");

            migrationBuilder.AlterColumn<string>(
                name: "RecipeId",
                table: "Directions",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_Directions_Recipes_RecipeId",
                table: "Directions",
                column: "RecipeId",
                principalTable: "Recipes",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Directions_Recipes_RecipeId",
                table: "Directions");

            migrationBuilder.AlterColumn<string>(
                name: "RecipeId",
                table: "Directions",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Directions_Recipes_RecipeId",
                table: "Directions",
                column: "RecipeId",
                principalTable: "Recipes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
