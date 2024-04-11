using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BARBERSHOP_V2.Migrations
{
    public partial class update_check : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_Booking_StartDate_CurrentDate",
                table: "Bookings");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Booking_StartDate_DateFounded",
                table: "Bookings");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Booking_StartDate_DateFounded",
                table: "Bookings",
                sql: "startDate >= dateFounded");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_Booking_StartDate_DateFounded",
                table: "Bookings");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Booking_StartDate_CurrentDate",
                table: "Bookings",
                sql: "startDate <= GETDATE()");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Booking_StartDate_DateFounded",
                table: "Bookings",
                sql: "startDate <= dateFounded");
        }
    }
}
