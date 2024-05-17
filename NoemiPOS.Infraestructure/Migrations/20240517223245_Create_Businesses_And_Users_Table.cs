using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NoemiPOS.Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class Create_Businesses_And_Users_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "businesses",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    description = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    rtn = table.Column<string>(type: "character varying(14)", maxLength: 14, nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    phone = table.Column<string>(type: "character varying(8)", maxLength: 8, nullable: false),
                    secondary_phone = table.Column<string>(type: "character varying(8)", maxLength: 8, nullable: true),
                    web_site_url = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    address_country = table.Column<string>(type: "text", nullable: false),
                    address_state = table.Column<string>(type: "text", nullable: false),
                    address_city = table.Column<string>(type: "text", nullable: false),
                    address_street = table.Column<string>(type: "text", nullable: false),
                    address_postal_code = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: false),
                    type = table.Column<string>(type: "text", nullable: false),
                    management_note = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_businesses", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    business_id = table.Column<Guid>(type: "uuid", nullable: false),
                    first_name = table.Column<string>(type: "character varying(125)", maxLength: 125, nullable: false),
                    last_name = table.Column<string>(type: "character varying(125)", maxLength: 125, nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    username = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false),
                    dni = table.Column<string>(type: "character varying(13)", maxLength: 13, nullable: true),
                    phone = table.Column<string>(type: "character varying(8)", maxLength: 8, nullable: false),
                    hash_password = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_users", x => new { x.id, x.business_id });
                });

            migrationBuilder.CreateIndex(
                name: "ix_businesses_email",
                table: "businesses",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_businesses_name",
                table: "businesses",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_businesses_rtn",
                table: "businesses",
                column: "rtn",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_users_dni",
                table: "users",
                column: "dni",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_users_email",
                table: "users",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_users_username",
                table: "users",
                column: "username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "businesses");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
