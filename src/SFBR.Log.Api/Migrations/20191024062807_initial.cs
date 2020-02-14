using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SFBR.Log.Api.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ActionLogs",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Account = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    DeviceCode = table.Column<string>(nullable: true),
                    DeviceName = table.Column<string>(nullable: true),
                    FunctionName = table.Column<string>(nullable: true),
                    Paramaters = table.Column<string>(nullable: true),
                    ApiUrl = table.Column<string>(nullable: true),
                    ActionDesciption = table.Column<string>(nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    ActionTime = table.Column<DateTime>(nullable: false),
                    ApplicationContext = table.Column<string>(nullable: true),
                    ContentType = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActionLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AlarmLogs",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    DeviceId = table.Column<string>(nullable: true),
                    EquipNum = table.Column<string>(nullable: true),
                    AlarmCode = table.Column<string>(nullable: true),
                    AlarmName = table.Column<string>(nullable: true),
                    GroupName = table.Column<string>(nullable: true),
                    AlarmType = table.Column<int>(nullable: false),
                    AlarmFrom = table.Column<int>(nullable: false),
                    TargetCode = table.Column<string>(nullable: true),
                    AlarmLevel = table.Column<int>(nullable: false),
                    AlarmStatus = table.Column<string>(nullable: true),
                    AlarmingDescription = table.Column<string>(nullable: true),
                    AlarmedDescription = table.Column<string>(nullable: true),
                    RepairTime = table.Column<double>(nullable: false),
                    AlarmTime = table.Column<DateTime>(nullable: false),
                    IsClear = table.Column<bool>(nullable: false),
                    ClearTime = table.Column<DateTime>(nullable: true),
                    ClearReason = table.Column<int>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    IsStatistics = table.Column<bool>(nullable: false),
                    RealData = table.Column<double>(nullable: true),
                    UpperLimit = table.Column<double>(nullable: true),
                    LowerLimit = table.Column<double>(nullable: true),
                    Disposed = table.Column<bool>(nullable: false),
                    DisposalContent = table.Column<string>(maxLength: 400, nullable: true),
                    DisposalUser = table.Column<string>(maxLength: 50, nullable: true),
                    DisposalUserName = table.Column<string>(maxLength: 50, nullable: true),
                    DisposalTime = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlarmLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Devices",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    DeviceName = table.Column<string>(nullable: true),
                    DeviceTypeCode = table.Column<string>(nullable: true),
                    ModelCode = table.Column<string>(nullable: true),
                    EquipNum = table.Column<string>(nullable: true),
                    RegionId = table.Column<string>(nullable: true),
                    RegionCode = table.Column<string>(nullable: true),
                    RegionName = table.Column<string>(nullable: true),
                    TentantId = table.Column<string>(nullable: true),
                    TentantName = table.Column<string>(nullable: true),
                    ParentId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Devices", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActionLogs");

            migrationBuilder.DropTable(
                name: "AlarmLogs");

            migrationBuilder.DropTable(
                name: "Devices");
        }
    }
}
