using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bank.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class chngForeignKeyForCstProfile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerProfile_Customer_Id",
                table: "CustomerProfile");
            migrationBuilder.DropPrimaryKey(
                name: "PK_CustomerProfile",
                table: "CustomerProfile");
            migrationBuilder.DropColumn(
                name: "Id",
                table: "CustomerProfile");
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "CustomerProfile",
                type: "int",
                nullable: false)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerProfile_CustomerId",
                table: "CustomerProfile",
                column: "CustomerId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerProfile_Customer_CustomerId",
                table: "CustomerProfile",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerProfile_Customer_CustomerId",
                table: "CustomerProfile");

            migrationBuilder.DropIndex(
                name: "IX_CustomerProfile_CustomerId",
                table: "CustomerProfile");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "CustomerProfile",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerProfile_Customer_Id",
                table: "CustomerProfile",
                column: "Id",
                principalTable: "Customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
