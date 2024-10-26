﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace proyectoCnbs.Migrations
{
    /// <inheritdoc />
    public partial class CreacionTablaApiData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApiData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NroTransaccion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaHoraTrn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaAConsultar = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CuentaDeclaraciones = table.Column<int>(type: "int", nullable: false),
                    JsonDatos = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApiData", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApiData");
        }
    }
}
