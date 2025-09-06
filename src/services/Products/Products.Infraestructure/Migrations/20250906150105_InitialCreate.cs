using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Products.Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductControls",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Producto = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NombreCliente = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Marca = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PorcentajeMiga = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    PesoDrenado = table.Column<decimal>(type: "decimal(18,3)", nullable: false),
                    PesoEnvase = table.Column<decimal>(type: "decimal(18,3)", nullable: false),
                    EstaActivo = table.Column<bool>(type: "bit", nullable: false),
                    PesoNeto = table.Column<decimal>(type: "decimal(18,3)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductControls", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductControlDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductControlId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Peso = table.Column<decimal>(type: "decimal(18,3)", nullable: false),
                    TipoControl = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductControlDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductControlDetails_ProductControls_ProductControlId",
                        column: x => x.ProductControlId,
                        principalTable: "ProductControls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductControlDetails_ProductControlId",
                table: "ProductControlDetails",
                column: "ProductControlId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductControlDetails");

            migrationBuilder.DropTable(
                name: "ProductControls");
        }
    }
}
