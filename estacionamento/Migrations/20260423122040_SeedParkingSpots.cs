using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace estacionamento.Migrations
{
    /// <inheritdoc />
    public partial class SeedParkingSpots : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Vagas",
                columns: new[] { "Id", "NumeroVaga", "Status", "CarroId", "HoraEntrada" },
                values: new object[,]
                {
                    { 1, "A-01", 0, null, null },
                    { 2, "A-02", 0, null, null },
                    { 3, "A-03", 0, null, null },
                    { 4, "A-04", 0, null, null },
                    { 5, "A-05", 0, null, null },
                    { 6, "B-01", 0, null, null },
                    { 7, "B-02", 0, null, null },
                    { 8, "B-03", 0, null, null },
                    { 9, "B-04", 0, null, null },
                    { 10, "B-05", 0, null, null },
                    { 11, "C-01", 0, null, null },
                    { 12, "C-02", 0, null, null },
                    { 13, "C-03", 0, null, null },
                    { 14, "C-04", 0, null, null },
                    { 15, "C-05", 0, null, null },
                    { 16, "D-01", 0, null, null },
                    { 17, "D-02", 0, null, null },
                    { 18, "D-03", 0, null, null },
                    { 19, "D-04", 0, null, null },
                    { 20, "D-05", 0, null, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Vagas",
                keyColumn: "Id",
                keyValues: new object[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 });
        }
    }
}
