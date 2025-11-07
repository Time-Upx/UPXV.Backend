using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UPXV.Backend.Migrations
{
    /// <inheritdoc />
    public partial class ImprovedDeletionBehavior : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Consumables_Units_UnitId",
                table: "Consumables");

            migrationBuilder.DropForeignKey(
                name: "FK_Patrimonies_Status_StatusId",
                table: "Patrimonies");

            migrationBuilder.DropForeignKey(
                name: "FK_QRCodes_Intents_IntentId",
                table: "QRCodes");

            migrationBuilder.DropTable(
                name: "ConsumableTag");

            migrationBuilder.DropTable(
                name: "PatrimonyTag");

            migrationBuilder.CreateTable(
                name: "ConsumablesTags",
                columns: table => new
                {
                    PatrimonyId = table.Column<int>(type: "int", nullable: false),
                    TagId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConsumablesTags", x => new { x.PatrimonyId, x.TagId });
                    table.ForeignKey(
                        name: "FK_ConsumablesTags_Consumables_PatrimonyId",
                        column: x => x.PatrimonyId,
                        principalTable: "Consumables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ConsumablesTags_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PatrimoniesTags",
                columns: table => new
                {
                    ConsumableId = table.Column<int>(type: "int", nullable: false),
                    TagId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatrimoniesTags", x => new { x.ConsumableId, x.TagId });
                    table.ForeignKey(
                        name: "FK_PatrimoniesTags_Patrimonies_ConsumableId",
                        column: x => x.ConsumableId,
                        principalTable: "Patrimonies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PatrimoniesTags_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_ConsumablesTags_TagId",
                table: "ConsumablesTags",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_PatrimoniesTags_TagId",
                table: "PatrimoniesTags",
                column: "TagId");

            migrationBuilder.AddForeignKey(
                name: "FK_Consumables_Units_UnitId",
                table: "Consumables",
                column: "UnitId",
                principalTable: "Units",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Patrimonies_Status_StatusId",
                table: "Patrimonies",
                column: "StatusId",
                principalTable: "Status",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_QRCodes_Intents_IntentId",
                table: "QRCodes",
                column: "IntentId",
                principalTable: "Intents",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Consumables_Units_UnitId",
                table: "Consumables");

            migrationBuilder.DropForeignKey(
                name: "FK_Patrimonies_Status_StatusId",
                table: "Patrimonies");

            migrationBuilder.DropForeignKey(
                name: "FK_QRCodes_Intents_IntentId",
                table: "QRCodes");

            migrationBuilder.DropTable(
                name: "ConsumablesTags");

            migrationBuilder.DropTable(
                name: "PatrimoniesTags");

            migrationBuilder.CreateTable(
                name: "ConsumableTag",
                columns: table => new
                {
                    ConsumableId = table.Column<int>(type: "int", nullable: false),
                    TagsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConsumableTag", x => new { x.ConsumableId, x.TagsId });
                    table.ForeignKey(
                        name: "FK_ConsumableTag_Consumables_ConsumableId",
                        column: x => x.ConsumableId,
                        principalTable: "Consumables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ConsumableTag_Tags_TagsId",
                        column: x => x.TagsId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PatrimonyTag",
                columns: table => new
                {
                    PatrimonyId = table.Column<int>(type: "int", nullable: false),
                    TagsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatrimonyTag", x => new { x.PatrimonyId, x.TagsId });
                    table.ForeignKey(
                        name: "FK_PatrimonyTag_Patrimonies_PatrimonyId",
                        column: x => x.PatrimonyId,
                        principalTable: "Patrimonies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PatrimonyTag_Tags_TagsId",
                        column: x => x.TagsId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_ConsumableTag_TagsId",
                table: "ConsumableTag",
                column: "TagsId");

            migrationBuilder.CreateIndex(
                name: "IX_PatrimonyTag_TagsId",
                table: "PatrimonyTag",
                column: "TagsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Consumables_Units_UnitId",
                table: "Consumables",
                column: "UnitId",
                principalTable: "Units",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Patrimonies_Status_StatusId",
                table: "Patrimonies",
                column: "StatusId",
                principalTable: "Status",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QRCodes_Intents_IntentId",
                table: "QRCodes",
                column: "IntentId",
                principalTable: "Intents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
