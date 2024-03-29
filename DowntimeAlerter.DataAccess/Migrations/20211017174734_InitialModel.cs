﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DowntimeAlerter.DataAccess.Migrations
{
    public partial class InitialModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NotificationLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    State = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SiteName = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    NotificationType = table.Column<int>(type: "int", nullable: false),
                    CheckedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Site",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    Url = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false),
                    IntervalTime = table.Column<long>(type: "bigint", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Site", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Logs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Message = table.Column<string>(type: "nvarchar(MAX)", maxLength: int.MaxValue, nullable: true),
                    MessageTemplate = table.Column<string>(type: "nvarchar(MAX)", maxLength: int.MaxValue, nullable: true),
                    Level = table.Column<string>(type: "nvarchar(MAX)", maxLength: int.MaxValue, nullable: true),
                    TimeStamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Exception = table.Column<string>(type: "nvarchar(MAX)", maxLength: int.MaxValue, nullable: true),
                    Properties = table.Column<string>(type: "nvarchar(MAX)", maxLength: int.MaxValue, nullable: true)
                });


            migrationBuilder.InsertData(
                table: "Site",
                columns: new[] { "Id", "Email", "IntervalTime", "Name", "Url" },
                values: new object[,]
                {
                    { 1, "onur.arrslan@gmail.com", 40L, "Google", "https://google.com" },
                    { 2, "onur.arrslan@gmail.com", 30L, "Down Site Example", "https://example.org/impolite" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Name", "Password", "Type", "UserName" },
                values: new object[,]
                {
                    { 1, "Onur ARSLAN", "61EDC5202B80B64056B78436F6385B9C", 1, "onurarslan" },
                    { 2, "User STANDART", "9B794565FE25E980414A6594670D93CC", 2, "user" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NotificationLogs");

            migrationBuilder.DropTable(
                name: "Site");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
