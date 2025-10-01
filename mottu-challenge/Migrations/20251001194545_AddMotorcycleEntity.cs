using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace mottu_challenge.Migrations
{
    /// <inheritdoc />
    public partial class AddMotorcycleEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Motorcycles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Year = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Model = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    Plate = table.Column<string>(type: "NVARCHAR2(10)", maxLength: 10, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    FlagAtivo = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Motorcycles", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Motorcycles");
        }
    }
}
