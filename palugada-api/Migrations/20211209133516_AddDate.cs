using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace palugada_api.Migrations
{
    public partial class AddDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateOnly>(
                name: "Date",
                table: "OrderHeader",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "OrderHeader");
        }
    }
}
