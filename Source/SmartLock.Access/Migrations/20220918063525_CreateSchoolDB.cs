using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartLock.Access.Migrations
{
    public partial class CreateSchoolDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Offices",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Offices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Firstname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmployeeId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsAdmin = table.Column<byte>(type: "tinyint", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Doors",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OfficeId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doors", x => x.Id);
                    table.ForeignKey(
                        name: "FK__Doors__OfficeId__29572725",
                        column: x => x.OfficeId,
                        principalTable: "Offices",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserOfficeRoleMapping",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    OfficeId = table.Column<long>(type: "bigint", nullable: false),
                    RoleId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserOfficeRoleMapping", x => x.Id);
                    table.ForeignKey(
                        name: "FK__UserOffic__DoorI__34C8D9D1",
                        column: x => x.OfficeId,
                        principalTable: "Offices",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__UserOffic__RoleI__35BCFE0A",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__UserOffic__UserI__36B12243",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AuditLogs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    DoorId = table.Column<long>(type: "bigint", nullable: false),
                    OfficeId = table.Column<long>(type: "bigint", nullable: false),
                    Status = table.Column<byte>(type: "tinyint", nullable: false),
                    EventTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK__AuditLogs__DoorI__2D27B809",
                        column: x => x.DoorId,
                        principalTable: "Doors",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__AuditLogs__Offic__2C3393D0",
                        column: x => x.OfficeId,
                        principalTable: "Offices",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__AuditLogs__UserI__2E1BDC42",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DoorRoleMappings",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DoorId = table.Column<long>(type: "bigint", nullable: false),
                    RoleId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoorRoleMappings", x => x.Id);
                    table.ForeignKey(
                        name: "FK__DoorRoleM__DoorI__30F848ED",
                        column: x => x.DoorId,
                        principalTable: "Doors",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__DoorRoleM__RoleI__31EC6D26",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Offices",
                columns: new[] { "Id", "Address", "Name" },
                values: new object[,]
                {
                    { 1L, "Amsterdam", "Clay" },
                    { 2L, "Amsterdam", "Booking.com" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1L, "HeadStaff" },
                    { 2L, "Travel" },
                    { 3L, "Engineer" },
                    { 4L, "Finance" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "EmailId", "EmployeeId", "Firstname", "IsAdmin", "LastName", "Password", "UserName" },
                values: new object[,]
                {
                    { 1L, "sheldoncooper@gmail.com", "1000", "Sheldon", (byte)1, "Cooper", "c2hlbGRvbg==", "sheldon" },
                    { 2L, "adia@gmail.com", "1001", "Adia", (byte)0, "Bugg", "YWRpYQ==", "adia" },
                    { 3L, "olive@gmail.com", "1002", "Olive", (byte)0, "yew", "b2xpdmU=", "olive" },
                    { 4L, "peg@gmail.com", "1003", "Peg", (byte)0, "Legge", "cGVn", "peg" },
                    { 5L, "allie@gmail.com", "1004", "Allie", (byte)0, "Grater", "YWxsaWU=", "allie" }
                });

            migrationBuilder.InsertData(
                table: "Doors",
                columns: new[] { "Id", "Name", "OfficeId", "Type" },
                values: new object[,]
                {
                    { 1L, "FrontDoor", 1L, "Main" },
                    { 2L, "BackDoor", 1L, "Back" },
                    { 3L, "StoreRoom", 1L, "Store" },
                    { 4L, "FrontDoor", 2L, "Main" }
                });

            migrationBuilder.InsertData(
                table: "UserOfficeRoleMapping",
                columns: new[] { "Id", "OfficeId", "RoleId", "UserId" },
                values: new object[,]
                {
                    { 1L, 1L, 1L, 1L },
                    { 2L, 1L, 2L, 2L },
                    { 3L, 1L, 3L, 3L },
                    { 4L, 1L, 4L, 4L },
                    { 5L, 2L, 1L, 5L }
                });

            migrationBuilder.InsertData(
                table: "DoorRoleMappings",
                columns: new[] { "Id", "DoorId", "RoleId" },
                values: new object[,]
                {
                    { 1L, 1L, 1L },
                    { 2L, 1L, 2L },
                    { 3L, 1L, 3L },
                    { 4L, 1L, 4L },
                    { 5L, 2L, 1L },
                    { 6L, 2L, 3L },
                    { 7L, 3L, 1L },
                    { 8L, 4L, 1L },
                    { 9L, 4L, 1L }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuditLog_UserId",
                table: "AuditLogs",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AuditLogs_DoorId",
                table: "AuditLogs",
                column: "DoorId");

            migrationBuilder.CreateIndex(
                name: "IX_AuditLogs_OfficeId",
                table: "AuditLogs",
                column: "OfficeId");

            migrationBuilder.CreateIndex(
                name: "IX_DoorRoleMappings_DoorId",
                table: "DoorRoleMappings",
                column: "DoorId");

            migrationBuilder.CreateIndex(
                name: "IX_DoorRoleMappings_RoleId",
                table: "DoorRoleMappings",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Doors_OfficeId",
                table: "Doors",
                column: "OfficeId");

            migrationBuilder.CreateIndex(
                name: "IX_UserOfficeRoleMapping_OfficeId",
                table: "UserOfficeRoleMapping",
                column: "OfficeId");

            migrationBuilder.CreateIndex(
                name: "IX_UserOfficeRoleMapping_RoleId",
                table: "UserOfficeRoleMapping",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserOfficeRoleMapping_UserId",
                table: "UserOfficeRoleMapping",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_User_UserName",
                table: "Users",
                column: "UserName",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuditLogs");

            migrationBuilder.DropTable(
                name: "DoorRoleMappings");

            migrationBuilder.DropTable(
                name: "UserOfficeRoleMapping");

            migrationBuilder.DropTable(
                name: "Doors");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Offices");
        }
    }
}
