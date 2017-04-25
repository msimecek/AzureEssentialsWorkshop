using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace STCapi.Migrations
{
    public partial class Tag : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Tag",
                table: "Links",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Tag",
                table: "Links");
        }
    }
}
