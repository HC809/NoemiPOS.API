using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NoemiPOS.Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class FKs_Businesses_And_Users : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "ix_users_business_id",
                table: "users",
                column: "business_id");

            migrationBuilder.CreateIndex(
                name: "ix_businesses_tenant_id",
                table: "businesses",
                column: "tenant_id");

            migrationBuilder.AddForeignKey(
                name: "fk_businesses_tenant_tenant_id",
                table: "businesses",
                column: "tenant_id",
                principalTable: "tenants",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_users_businesses_business_id",
                table: "users",
                column: "business_id",
                principalTable: "businesses",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_businesses_tenant_tenant_id",
                table: "businesses");

            migrationBuilder.DropForeignKey(
                name: "fk_users_businesses_business_id",
                table: "users");

            migrationBuilder.DropIndex(
                name: "ix_users_business_id",
                table: "users");

            migrationBuilder.DropIndex(
                name: "ix_businesses_tenant_id",
                table: "businesses");
        }
    }
}
