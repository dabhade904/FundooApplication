using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RepositoryLayer.Migrations
{
    public partial class lableMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                    UserId = table.Column<long>(nullable: false),
                    userEntityUserId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NoteTable", x => x.noteID);
                    table.ForeignKey(
                        name: "FK_NoteTable_UserTable_userEntityUserId",
                        column: x => x.userEntityUserId,
                        principalTable: "UserTable",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CollaboratorTable",
                columns: table => new
                {
                    collabId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    collabEmail = table.Column<string>(nullable: false),
                    modifyDate = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<long>(nullable: false),
                    noteID = table.Column<long>(nullable: false),
                    userEntityUserId = table.Column<long>(nullable: true),
                    noteEntitynoteID = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollaboratorTable", x => x.collabId);
                    table.ForeignKey(
                        name: "FK_CollaboratorTable_NoteTable_noteEntitynoteID",
                        column: x => x.noteEntitynoteID,
                        principalTable: "NoteTable",
                        principalColumn: "noteID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CollaboratorTable_UserTable_userEntityUserId",
                        column: x => x.userEntityUserId,
                        principalTable: "UserTable",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LableTable",
                columns: table => new
                {
                    lableId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    lableName = table.Column<string>(nullable: true),
                    UserId = table.Column<long>(nullable: false),
                    noteID = table.Column<long>(nullable: false),
                    userEntityUserId = table.Column<long>(nullable: true),
                    noteEntitynoteID = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LableTable", x => x.lableId);
                    table.ForeignKey(
                        name: "FK_LableTable_NoteTable_noteEntitynoteID",
                        column: x => x.noteEntitynoteID,
                        principalTable: "NoteTable",
                        principalColumn: "noteID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LableTable_UserTable_userEntityUserId",
                        column: x => x.userEntityUserId,
                        principalTable: "UserTable",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CollaboratorTable_noteEntitynoteID",
                table: "CollaboratorTable",
                column: "noteEntitynoteID");

            migrationBuilder.CreateIndex(
                name: "IX_CollaboratorTable_userEntityUserId",
                table: "CollaboratorTable",
                column: "userEntityUserId");

            migrationBuilder.CreateIndex(
                name: "IX_LableTable_noteEntitynoteID",
                table: "LableTable",
                column: "noteEntitynoteID");

            migrationBuilder.CreateIndex(
                name: "IX_LableTable_userEntityUserId",
                table: "LableTable",
                column: "userEntityUserId");

            migrationBuilder.CreateIndex(
                name: "IX_NoteTable_userEntityUserId",
                table: "NoteTable",
                column: "userEntityUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CollaboratorTable");

            migrationBuilder.DropTable(
                name: "LableTable");

            migrationBuilder.DropTable(
                name: "NoteTable");

            migrationBuilder.DropTable(
                name: "UserTable");
        }
    }
}
