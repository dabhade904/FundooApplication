using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RepositoryLayer.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CollaboratorTable",
                columns: table => new
                {
                    collabId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    collabEmail = table.Column<string>(nullable: true),
                    modifyDate = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<long>(nullable: false),
                    noteID = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollaboratorTable", x => x.collabId);
                });

            migrationBuilder.CreateTable(
                name: "NoteTable",
                columns: table => new
                {
                    noteID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(nullable: true),
                    discription = table.Column<string>(nullable: true),
                    reminder = table.Column<DateTime>(nullable: false),
                    color = table.Column<string>(nullable: true),
                    img = table.Column<string>(nullable: true),
                    archive = table.Column<bool>(nullable: false),
                    pin = table.Column<bool>(nullable: false),
                    trash = table.Column<bool>(nullable: false),
                    time_created = table.Column<DateTime>(nullable: false),
                    time_edited = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NoteTable", x => x.noteID);
                });

            migrationBuilder.CreateTable(
                name: "UserTable",
                columns: table => new
                {
                    UserId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    EmailId = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTable", x => x.UserId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CollaboratorTable");

            migrationBuilder.DropTable(
                name: "NoteTable");

            migrationBuilder.DropTable(
                name: "UserTable");
        }
    }
}
