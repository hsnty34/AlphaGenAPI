using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AlphaGenAPI.Migrations
{
    public partial class createdb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Alans",
                columns: table => new
                {
                    AlanId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Hedef = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alans", x => x.AlanId);
                });

            migrationBuilder.CreateTable(
                name: "Harekets",
                columns: table => new
                {
                    HareketId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Target = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Harekets", x => x.HareketId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nick = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "AlanHarekets",
                columns: table => new
                {
                    AlanId = table.Column<int>(type: "int", nullable: false),
                    HareketId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlanHarekets", x => new { x.AlanId, x.HareketId });
                    table.ForeignKey(
                        name: "FK_AlanHarekets_Alans_AlanId",
                        column: x => x.AlanId,
                        principalTable: "Alans",
                        principalColumn: "AlanId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AlanHarekets_Harekets_HareketId",
                        column: x => x.HareketId,
                        principalTable: "Harekets",
                        principalColumn: "HareketId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserAlans",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    AlanId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAlans", x => new { x.UserId, x.AlanId });
                    table.ForeignKey(
                        name: "FK_UserAlans_Alans_AlanId",
                        column: x => x.AlanId,
                        principalTable: "Alans",
                        principalColumn: "AlanId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserAlans_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserInApps",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Soyad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Yas = table.Column<int>(type: "int", nullable: false),
                    Salon = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Adres = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserInApps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserInApps_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserParams",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Old = table.Column<int>(type: "int", nullable: false),
                    Weight = table.Column<double>(type: "float", nullable: false),
                    Lenght = table.Column<double>(type: "float", nullable: false),
                    Biceps = table.Column<double>(type: "float", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserParams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserParams_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AlanHarekets_HareketId",
                table: "AlanHarekets",
                column: "HareketId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAlans_AlanId",
                table: "UserAlans",
                column: "AlanId");

            migrationBuilder.CreateIndex(
                name: "IX_UserInApps_UserId",
                table: "UserInApps",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserParams_UserId",
                table: "UserParams",
                column: "UserId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AlanHarekets");

            migrationBuilder.DropTable(
                name: "UserAlans");

            migrationBuilder.DropTable(
                name: "UserInApps");

            migrationBuilder.DropTable(
                name: "UserParams");

            migrationBuilder.DropTable(
                name: "Harekets");

            migrationBuilder.DropTable(
                name: "Alans");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
