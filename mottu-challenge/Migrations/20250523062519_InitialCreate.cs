using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace mottu_challenge.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "T_MTU_ROLE",
                columns: table => new
                {
                    id_role = table.Column<byte>(type: "number(2)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    nm_role = table.Column<string>(type: "varchar2(30)", nullable: false),
                    ds_role = table.Column<string>(type: "varchar2(100)", nullable: false),
                    dt_criacao = table.Column<DateTime>(type: "timestamp", nullable: false),
                    fl_ativo = table.Column<string>(type: "char(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_MTU_ROLE", x => x.id_role);
                });

            migrationBuilder.CreateTable(
                name: "T_MTU_USER",
                columns: table => new
                {
                    id_user = table.Column<int>(type: "number(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    nm_username = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    ds_email = table.Column<string>(type: "NVARCHAR2(320)", maxLength: 320, nullable: false),
                    vl_password = table.Column<string>(type: "NVARCHAR2(255)", maxLength: 255, nullable: false),
                    fl_ativo = table.Column<string>(type: "char(1)", nullable: false),
                    dt_criacao = table.Column<DateTime>(type: "timestamp", nullable: false),
                    id_role = table.Column<byte>(type: "number(2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_MTU_USER", x => x.id_user);
                    table.ForeignKey(
                        name: "FK_T_MTU_ROLE_USER",
                        column: x => x.id_role,
                        principalTable: "T_MTU_ROLE",
                        principalColumn: "id_role",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "T_MTU_ROLE",
                columns: new[] { "id_role", "dt_criacao", "fl_ativo", "ds_role", "nm_role" },
                values: new object[,]
                {
                    { (byte)1, new DateTime(2023, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), "S", "Perfil com acesso total ao sistema", "ADMIN" },
                    { (byte)2, new DateTime(2023, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), "S", "Perfil com acesso restrito ao sistema", "USER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_T_MTU_USER_id_role",
                table: "T_MTU_USER",
                column: "id_role");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "T_MTU_USER");

            migrationBuilder.DropTable(
                name: "T_MTU_ROLE");
        }
    }
}
