using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NoemiPOS.Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class Create_Tenant_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tenants",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    full_name = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    dni = table.Column<string>(type: "character varying(13)", maxLength: 13, nullable: false),
                    rtn = table.Column<string>(type: "character varying(14)", maxLength: 14, nullable: false),
                    phone = table.Column<string>(type: "character varying(8)", maxLength: 8, nullable: false),
                    secondary_phone = table.Column<string>(type: "character varying(8)", maxLength: 8, nullable: true),
                    description = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    address_country = table.Column<string>(type: "text", nullable: false),
                    address_state = table.Column<string>(type: "text", nullable: false),
                    address_city = table.Column<string>(type: "text", nullable: false),
                    address_street = table.Column<string>(type: "text", nullable: false),
                    address_postal_code = table.Column<string>(type: "text", nullable: false),
                    management_note = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tenants", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "ix_tenants_dni",
                table: "tenants",
                column: "dni",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_tenants_email",
                table: "tenants",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_tenants_rtn",
                table: "tenants",
                column: "rtn",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tenants");
        }
    }
}
