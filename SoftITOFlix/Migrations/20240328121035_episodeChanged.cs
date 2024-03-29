using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SoftITOFlix.Migrations
{
    public partial class episodeChanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Episodes_MediaId",
                table: "Episodes");

            migrationBuilder.CreateIndex(
                name: "IX_Episodes_MediaId_SeasonNumber_EpisodeNumber",
                table: "Episodes",
                columns: new[] { "MediaId", "SeasonNumber", "EpisodeNumber" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Episodes_MediaId_SeasonNumber_EpisodeNumber",
                table: "Episodes");

            migrationBuilder.CreateIndex(
                name: "IX_Episodes_MediaId",
                table: "Episodes",
                column: "MediaId");
        }
    }
}
