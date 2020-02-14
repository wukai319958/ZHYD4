using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SFBR.Device.Infrastructure.Migrations
{
    public partial class terminalcode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Brands",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 50, nullable: false),
                    BrandName = table.Column<string>(maxLength: 50, nullable: false),
                    GroupKey = table.Column<string>(maxLength: 50, nullable: true),
                    TentantId = table.Column<string>(maxLength: 50, nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brands", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DevicePowers",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 50, nullable: false),
                    DeviceId = table.Column<string>(maxLength: 50, nullable: false),
                    AccountId = table.Column<string>(maxLength: 50, nullable: false),
                    AlarmBlackList = table.Column<string>(nullable: true),
                    FunctionBlackList = table.Column<string>(nullable: true),
                    SendSMS = table.Column<bool>(nullable: true),
                    SendEmail = table.Column<bool>(nullable: true),
                    CallPhone = table.Column<bool>(nullable: true),
                    Alarmed_SendSMS = table.Column<bool>(nullable: true),
                    Alarmed_SendEmail = table.Column<bool>(nullable: true),
                    Alarmed_CallPhone = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DevicePowers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DeviceTypes",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 50, nullable: false),
                    Code = table.Column<string>(maxLength: 50, nullable: false),
                    Model = table.Column<string>(maxLength: 50, nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    GroupKey = table.Column<string>(maxLength: 50, nullable: true),
                    Enabled = table.Column<bool>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    AutoCreate = table.Column<bool>(nullable: false),
                    IsMaster = table.Column<bool>(nullable: false),
                    TransferType = table.Column<int>(maxLength: 50, nullable: false),
                    ProtocolType = table.Column<int>(maxLength: 50, nullable: false),
                    ProtocolVersion = table.Column<string>(maxLength: 50, nullable: true),
                    CompanyId = table.Column<string>(maxLength: 50, nullable: true),
                    OprationId = table.Column<string>(maxLength: 50, nullable: true),
                    BrandId = table.Column<string>(maxLength: 50, nullable: true),
                    Warranty = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 50, nullable: false),
                    UseMapType = table.Column<int>(nullable: false),
                    Latitude = table.Column<double>(nullable: false),
                    Longitude = table.Column<double>(nullable: false),
                    Address = table.Column<string>(maxLength: 500, nullable: true),
                    Province = table.Column<string>(maxLength: 450, nullable: true),
                    City = table.Column<string>(maxLength: 450, nullable: true),
                    District = table.Column<string>(maxLength: 450, nullable: true),
                    Street = table.Column<string>(maxLength: 450, nullable: true),
                    StreetNumber = table.Column<string>(maxLength: 450, nullable: true),
                    Enabled = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Regions",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 50, nullable: false),
                    RegionCode = table.Column<string>(maxLength: 150, nullable: false),
                    RegionName = table.Column<string>(maxLength: 50, nullable: false),
                    ParentId = table.Column<string>(maxLength: 50, nullable: true),
                    TentantId = table.Column<string>(maxLength: 50, nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Regions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Regions_Regions_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Regions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 50, nullable: false),
                    Account = table.Column<string>(maxLength: 150, nullable: true),
                    Password = table.Column<string>(maxLength: 150, nullable: true),
                    Name = table.Column<string>(maxLength: 150, nullable: true),
                    UserType = table.Column<int>(nullable: false),
                    TentantId = table.Column<string>(maxLength: 50, nullable: true),
                    Phone = table.Column<string>(maxLength: 50, nullable: true),
                    Email = table.Column<string>(maxLength: 150, nullable: true),
                    IsDeveloper = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DeviceTypeAlarms",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 50, nullable: false),
                    DeviceTypeId = table.Column<string>(maxLength: 50, nullable: false),
                    AlarmCode = table.Column<string>(maxLength: 50, nullable: false),
                    AlarmName = table.Column<string>(maxLength: 50, nullable: false),
                    GroupName = table.Column<string>(nullable: true),
                    AlarmType = table.Column<int>(nullable: false),
                    AlarmFrom = table.Column<int>(nullable: false),
                    TargetCode = table.Column<string>(nullable: true),
                    AlarmLevel = table.Column<int>(nullable: false),
                    Enabled = table.Column<bool>(nullable: false),
                    NormalValue = table.Column<string>(maxLength: 50, nullable: false),
                    StatusMapDescription = table.Column<string>(nullable: true),
                    AlarmingDescription = table.Column<string>(nullable: true),
                    AlarmedDescription = table.Column<string>(nullable: true),
                    SendAlarmingMessage = table.Column<bool>(nullable: false),
                    SendAlarmedMessage = table.Column<bool>(nullable: false),
                    CallAlarmingPhone = table.Column<bool>(nullable: false),
                    CallAlarmedPhone = table.Column<bool>(nullable: false),
                    SendAlarmingEmail = table.Column<bool>(nullable: false),
                    SendAlarmedEmail = table.Column<bool>(nullable: false),
                    AutoSendOrder = table.Column<bool>(nullable: false),
                    IsStatistics = table.Column<bool>(nullable: false),
                    RepairTime = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceTypeAlarms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeviceTypeAlarms_DeviceTypes_DeviceTypeId",
                        column: x => x.DeviceTypeId,
                        principalTable: "DeviceTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DeviceTypeChannels",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 50, nullable: false),
                    DeviceTypeId = table.Column<string>(maxLength: 50, nullable: false),
                    PortNumber = table.Column<int>(nullable: false),
                    PortDefaultName = table.Column<string>(maxLength: 50, nullable: false),
                    PortType = table.Column<int>(nullable: false),
                    OutputType = table.Column<int>(nullable: false),
                    OutputThreePhase = table.Column<bool>(nullable: false),
                    OutputValue = table.Column<double>(nullable: false),
                    Enabled = table.Column<bool>(nullable: false),
                    Sort = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceTypeChannels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeviceTypeChannels_DeviceTypes_DeviceTypeId",
                        column: x => x.DeviceTypeId,
                        principalTable: "DeviceTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DeviceTypeControllers",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 50, nullable: false),
                    DeviceTypeId = table.Column<string>(maxLength: 50, nullable: false),
                    PortNumber = table.Column<int>(nullable: false),
                    ControllerCode = table.Column<string>(maxLength: 50, nullable: false),
                    ControllerType = table.Column<int>(nullable: false),
                    Buttons = table.Column<string>(nullable: true),
                    Enabled = table.Column<bool>(nullable: false),
                    ControllerStatus = table.Column<string>(maxLength: 50, nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceTypeControllers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeviceTypeControllers_DeviceTypes_DeviceTypeId",
                        column: x => x.DeviceTypeId,
                        principalTable: "DeviceTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DeviceTypeFunctions",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 50, nullable: false),
                    PortNumber = table.Column<int>(nullable: false),
                    DeviceTypeId = table.Column<string>(maxLength: 50, nullable: true),
                    Enabled = table.Column<bool>(nullable: false),
                    FunctionName = table.Column<string>(maxLength: 150, nullable: false),
                    FunctionCode = table.Column<string>(maxLength: 100, nullable: true),
                    Sort = table.Column<string>(nullable: true),
                    FunctionType = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Setting = table.Column<string>(nullable: true),
                    SettingTypeName = table.Column<string>(maxLength: 2000, nullable: true),
                    CallbackCodes = table.Column<string>(maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceTypeFunctions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeviceTypeFunctions_DeviceTypes_DeviceTypeId",
                        column: x => x.DeviceTypeId,
                        principalTable: "DeviceTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DeviceTypeParts",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 50, nullable: false),
                    DeviceTypeId = table.Column<string>(maxLength: 50, nullable: false),
                    PortNumber = table.Column<int>(nullable: false),
                    PartCode = table.Column<string>(maxLength: 50, nullable: false),
                    PartName = table.Column<string>(maxLength: 50, nullable: false),
                    PartType = table.Column<int>(nullable: false),
                    HasStatus = table.Column<bool>(nullable: false),
                    StatusIndex = table.Column<int>(nullable: false),
                    Enabled = table.Column<bool>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    CompanyId = table.Column<string>(maxLength: 50, nullable: true),
                    OprationId = table.Column<string>(maxLength: 50, nullable: true),
                    BrandId = table.Column<string>(maxLength: 50, nullable: true),
                    Warranty = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceTypeParts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeviceTypeParts_DeviceTypes_DeviceTypeId",
                        column: x => x.DeviceTypeId,
                        principalTable: "DeviceTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DeviceTypeSensors",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 50, nullable: false),
                    DeviceTypeId = table.Column<string>(maxLength: 50, nullable: false),
                    PortNumber = table.Column<int>(nullable: false),
                    SensorCode = table.Column<string>(maxLength: 50, nullable: false),
                    SensorName = table.Column<string>(maxLength: 50, nullable: false),
                    SensorType = table.Column<int>(maxLength: 50, nullable: false),
                    UpperValue = table.Column<double>(nullable: true),
                    LowerValue = table.Column<double>(nullable: true),
                    Enabled = table.Column<bool>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceTypeSensors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeviceTypeSensors_DeviceTypes_DeviceTypeId",
                        column: x => x.DeviceTypeId,
                        principalTable: "DeviceTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Devices",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 50, nullable: false),
                    EquipNum = table.Column<string>(maxLength: 50, nullable: false),
                    DeviceTypeCode = table.Column<string>(maxLength: 50, nullable: false),
                    ModelCode = table.Column<string>(maxLength: 50, nullable: true),
                    IsMaster = table.Column<bool>(nullable: true),
                    DeviceIP = table.Column<string>(maxLength: 50, nullable: true),
                    DevicePort = table.Column<int>(nullable: false),
                    ServerIP = table.Column<string>(maxLength: 50, nullable: true),
                    ServerPort = table.Column<int>(nullable: false),
                    ParentId = table.Column<string>(maxLength: 50, nullable: true),
                    Enabled = table.Column<bool>(nullable: false),
                    Connection = table.Column<int>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    DeviceName = table.Column<string>(maxLength: 50, nullable: true),
                    TentantId = table.Column<string>(maxLength: 50, nullable: true),
                    Description = table.Column<string>(nullable: true),
                    CompanyId = table.Column<string>(maxLength: 50, nullable: true),
                    OprationId = table.Column<string>(maxLength: 50, nullable: true),
                    BrandId = table.Column<string>(maxLength: 50, nullable: true),
                    Warranty = table.Column<double>(nullable: false),
                    InstallTime = table.Column<DateTime>(nullable: true),
                    PortNumber = table.Column<int>(nullable: true),
                    TerminalId = table.Column<string>(nullable: true),
                    RegionId = table.Column<string>(maxLength: 50, nullable: true),
                    LocationId = table.Column<string>(maxLength: 50, nullable: true),
                    TerminalCode = table.Column<int>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Devices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Devices_Devices_TerminalId",
                        column: x => x.TerminalId,
                        principalTable: "Devices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Devices_Devices_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Devices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Devices_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Channels",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 50, nullable: false),
                    DeviceId = table.Column<string>(maxLength: 50, nullable: false),
                    PortNumber = table.Column<int>(nullable: false),
                    PortType = table.Column<int>(nullable: false),
                    PortDefaultName = table.Column<string>(maxLength: 50, nullable: true),
                    OutputType = table.Column<int>(nullable: true),
                    OutputThreePhase = table.Column<bool>(nullable: true),
                    OutputValue = table.Column<double>(nullable: true),
                    Enabled = table.Column<bool>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Sort = table.Column<string>(maxLength: 50, nullable: true),
                    TerminalId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Channels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Channels_Devices_TerminalId",
                        column: x => x.TerminalId,
                        principalTable: "Devices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Controllers",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 50, nullable: false),
                    DeviceId = table.Column<string>(maxLength: 50, nullable: false),
                    ControllerCode = table.Column<string>(maxLength: 50, nullable: false),
                    PortNumber = table.Column<int>(nullable: true),
                    Buttons = table.Column<string>(maxLength: 350, nullable: true),
                    Enabled = table.Column<bool>(nullable: true),
                    ControllerStatus = table.Column<string>(maxLength: 50, nullable: true),
                    Description = table.Column<string>(nullable: true),
                    TerminalId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Controllers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Controllers_Devices_TerminalId",
                        column: x => x.TerminalId,
                        principalTable: "Devices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DeviceAlarms",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 50, nullable: false),
                    DeviceId = table.Column<string>(maxLength: 50, nullable: false),
                    AlarmCode = table.Column<string>(maxLength: 50, nullable: false),
                    Status = table.Column<string>(maxLength: 50, nullable: false),
                    TargetCode = table.Column<string>(maxLength: 50, nullable: false),
                    NormalValue = table.Column<string>(maxLength: 50, nullable: true),
                    AlarmTime = table.Column<DateTime>(nullable: true),
                    RepairTime = table.Column<double>(nullable: true),
                    Enabled = table.Column<bool>(nullable: true),
                    TerminalId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceAlarms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeviceAlarms_Devices_TerminalId",
                        column: x => x.TerminalId,
                        principalTable: "Devices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DeviceFunctions",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 50, nullable: false),
                    FunctionCode = table.Column<string>(maxLength: 50, nullable: false),
                    DeviceId = table.Column<string>(maxLength: 50, nullable: false),
                    PortNumber = table.Column<int>(nullable: true),
                    Setting = table.Column<string>(nullable: true),
                    Enabled = table.Column<bool>(nullable: true),
                    LockSetting = table.Column<bool>(nullable: false),
                    Sort = table.Column<string>(maxLength: 10, nullable: true),
                    CallbackCodes = table.Column<string>(maxLength: 150, nullable: true),
                    SettingTypeName = table.Column<string>(maxLength: 2000, nullable: true),
                    TerminalId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceFunctions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeviceFunctions_Devices_TerminalId",
                        column: x => x.TerminalId,
                        principalTable: "Devices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DeviceProp",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 50, nullable: false),
                    DeviceId = table.Column<string>(maxLength: 50, nullable: false),
                    PropName = table.Column<string>(maxLength: 150, nullable: false),
                    PropText = table.Column<string>(maxLength: 150, nullable: true),
                    PropType = table.Column<string>(maxLength: 1500, nullable: true),
                    GroupName = table.Column<string>(maxLength: 50, nullable: true),
                    Enabled = table.Column<bool>(nullable: false),
                    PropValue = table.Column<string>(nullable: true),
                    CanRemove = table.Column<bool>(nullable: false),
                    CameraId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceProp", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeviceProp_Devices_CameraId",
                        column: x => x.CameraId,
                        principalTable: "Devices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Parts",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 50, nullable: false),
                    DeviceId = table.Column<string>(maxLength: 50, nullable: false),
                    PortNumber = table.Column<int>(nullable: true),
                    PartCode = table.Column<string>(maxLength: 50, nullable: false),
                    Enabled = table.Column<bool>(nullable: true),
                    Status = table.Column<string>(maxLength: 50, nullable: true),
                    AlarmStatus = table.Column<string>(maxLength: 50, nullable: true),
                    CompanyId = table.Column<string>(nullable: true),
                    OprationId = table.Column<string>(nullable: true),
                    BrandId = table.Column<string>(nullable: true),
                    Warranty = table.Column<double>(nullable: true),
                    InstallTime = table.Column<DateTime>(nullable: true),
                    TerminalId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Parts_Devices_TerminalId",
                        column: x => x.TerminalId,
                        principalTable: "Devices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Sensors",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 50, nullable: false),
                    DeviceId = table.Column<string>(maxLength: 50, nullable: false),
                    SensorCode = table.Column<string>(maxLength: 50, nullable: false),
                    PortNumber = table.Column<int>(nullable: true),
                    AlarmStatus = table.Column<string>(maxLength: 50, nullable: true),
                    UpperValue = table.Column<double>(nullable: true),
                    LowerValue = table.Column<double>(nullable: true),
                    RealValue = table.Column<double>(nullable: true),
                    Enabled = table.Column<bool>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    TerminalId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sensors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sensors_Devices_TerminalId",
                        column: x => x.TerminalId,
                        principalTable: "Devices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TimedTasks",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 50, nullable: false),
                    DeviceId = table.Column<string>(maxLength: 50, nullable: false),
                    PortNumber = table.Column<int>(nullable: false),
                    TaskId = table.Column<string>(maxLength: 50, nullable: true),
                    Enabled = table.Column<bool>(nullable: false),
                    ExecType = table.Column<int>(nullable: false),
                    ExecAction = table.Column<int>(nullable: false),
                    Executed = table.Column<int>(nullable: false),
                    LoopType = table.Column<int>(nullable: false),
                    Moment_Month = table.Column<int>(nullable: false),
                    Moment_Day = table.Column<int>(nullable: false),
                    Moment_Hour = table.Column<int>(nullable: false),
                    Moment_Minute = table.Column<int>(nullable: false),
                    Moment_Second = table.Column<int>(nullable: false),
                    LoopMonent = table.Column<string>(maxLength: 500, nullable: true),
                    Description = table.Column<string>(nullable: true),
                    TerminalId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimedTasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TimedTasks_Devices_TerminalId",
                        column: x => x.TerminalId,
                        principalTable: "Devices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "Id", "BrandName", "CreationTime", "Description", "GroupKey", "TentantId" },
                values: new object[,]
                {
                    { "d53c7ef7-892c-4aa4-9291-1543f0b1a2cd", "海康", new DateTime(2019, 10, 28, 11, 6, 9, 851, DateTimeKind.Utc).AddTicks(8023), null, null, null },
                    { "898bc149-63e1-4a68-a58b-d3a2a761fdba", "大华", new DateTime(2019, 10, 28, 11, 6, 9, 852, DateTimeKind.Utc).AddTicks(467), null, null, null }
                });

            migrationBuilder.InsertData(
                table: "DeviceTypes",
                columns: new[] { "Id", "AutoCreate", "BrandId", "Code", "CompanyId", "Description", "Enabled", "GroupKey", "IsMaster", "Model", "Name", "OprationId", "ProtocolType", "ProtocolVersion", "TransferType", "Warranty" },
                values: new object[] { "a2cc66e6-dcf5-4326-a085-fe2a779f29e8", true, "", "Terminal", "", "", true, "智维终端", true, "1.0", "智维终端", "", 1, null, 1, 0.0 });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Account", "Email", "IsDeveloper", "Name", "Password", "Phone", "TentantId", "UserType" },
                values: new object[] { "d70c1daf-3c9f-4fed-8b22-094760d68a8e", "admin", null, true, "超级管理员", "123456", null, null, 0 });

            migrationBuilder.InsertData(
                table: "DeviceTypeAlarms",
                columns: new[] { "Id", "AlarmCode", "AlarmFrom", "AlarmLevel", "AlarmName", "AlarmType", "AlarmedDescription", "AlarmingDescription", "AutoSendOrder", "CallAlarmedPhone", "CallAlarmingPhone", "DeviceTypeId", "Enabled", "GroupName", "IsStatistics", "NormalValue", "RepairTime", "SendAlarmedEmail", "SendAlarmedMessage", "SendAlarmingEmail", "SendAlarmingMessage", "StatusMapDescription", "TargetCode" },
                values: new object[,]
                {
                    { "481a876f-77da-4761-9a7c-b034a509dd8c", "Terminal_1.0_21", 0, 0, "网络通讯报警状态", 0, null, null, false, false, false, "a2cc66e6-dcf5-4326-a085-fe2a779f29e8", false, "网络发生故障", false, "0,5", 0.0, false, false, false, false, "0网络故障报警解除,1发生网络故障报警", "Terminal_1" },
                    { "df286864-c32e-40cc-aeba-b5a14d83310c", "Terminal_1.0_38", 3, 0, "自复位重合闸报警状态", 0, null, null, false, false, false, "a2cc66e6-dcf5-4326-a085-fe2a779f29e8", true, "其他类型警报", false, "0,5", 0.0, false, false, false, false, "0重合闸恢复正常,1自复位重合闸发生异常", "Part_4" },
                    { "0893a733-79d6-484d-bce9-88c445871f80", "Terminal_1.0_37", 3, 0, "网络防雷器报警状态", 0, null, null, false, false, false, "a2cc66e6-dcf5-4326-a085-fe2a779f29e8", true, "防雷器警报", false, "0,5", 0.0, false, false, false, false, "0网络防雷器恢复正常,1网络防雷器发生警报", "Part_3" },
                    { "bcc07561-12b2-491a-b1dc-352b10344045", "Terminal_1.0_36", 3, 0, "电源防雷器报警状态", 0, null, null, false, false, false, "a2cc66e6-dcf5-4326-a085-fe2a779f29e8", true, "防雷器警报", false, "0,5", 0.0, false, false, false, false, "0电源防雷器恢复正常,1电源防雷器发生警报", "Part_2" },
                    { "50d18bc3-deab-4dea-b647-259a286b6556", "Terminal_1.0_35", 3, 0, "开门报警状态", 0, null, null, false, false, false, "a2cc66e6-dcf5-4326-a085-fe2a779f29e8", true, "布撤防警报", true, "0,5", 0.0, false, false, false, false, "0门已关闭,1门被打开", "Part_1" },
                    { "5005e79a-6d5f-4887-8b65-218faf8eac6f", "Terminal_1.0_33", 2, 0, "湿度报警状态", 0, null, null, false, false, false, "a2cc66e6-dcf5-4326-a085-fe2a779f29e8", true, "湿度过高警报", false, "0,5", 0.0, false, false, false, false, "0湿度恢复正常,1湿度超过上限值", "Sensor_4" },
                    { "28471abf-7fa8-4be6-8ea1-6a22463f1067", "Terminal_1.0_32", 2, 0, "温度报警状态", 0, null, null, false, false, false, "a2cc66e6-dcf5-4326-a085-fe2a779f29e8", true, "温度警报", false, "0,5", 0.0, false, false, false, false, "0温度恢复正常,1温度超过上限值,2温度低于下限值", "Sensor_3" },
                    { "07f6fc5e-3ad8-4a82-b310-02e634d0fc7b", "Terminal_1.0_31", 2, 0, "电流报警状态", 0, null, null, false, false, false, "a2cc66e6-dcf5-4326-a085-fe2a779f29e8", true, "电流警报", false, "0,5", 0.0, false, false, false, false, "0电流恢复正常,1电流超过上限值,2电流低于下限值", "Sensor_2" },
                    { "e8356744-1673-4c8a-b553-ffbb2a18f6e0", "Terminal_1.0_30", 2, 0, "电压报警状态", 0, null, null, false, false, false, "a2cc66e6-dcf5-4326-a085-fe2a779f29e8", true, "过欠压警报", false, "0,5", 0.0, false, false, false, false, "0电压恢复正常,1电压超过上限值,2电压低于下限值,3电压值偏高,4电压值偏低", "Sensor_1" },
                    { "c31aede4-e614-4fd6-a129-950d5f98b878", "Terminal_1.0_34", 3, 0, "停电报警状态", 0, null, null, false, false, false, "a2cc66e6-dcf5-4326-a085-fe2a779f29e8", true, "市电停电警报", true, "0,5", 0.0, false, false, false, false, "0市电恢复正常,1发生停电报警", "Part_8" },
                    { "f71327d6-4968-4ece-afbe-6d097310317d", "Terminal_1.0_28", 1, 0, "7号位摄像机报警状态", 0, null, null, false, false, false, "a2cc66e6-dcf5-4326-a085-fe2a779f29e8", true, "设备离线", true, "0,5", 0.0, false, false, false, false, "07号位摄像机恢复正常,17号位摄像机离线", "Camera_7" },
                    { "5f25559e-edb5-448a-af80-f3dd860d8c85", "Terminal_1.0_27", 1, 0, "6号位摄像机报警状态", 0, null, null, false, false, false, "a2cc66e6-dcf5-4326-a085-fe2a779f29e8", true, "设备离线", true, "0,5", 0.0, false, false, false, false, "06号位摄像机恢复正常,16号位摄像机离线", "Camera_6" },
                    { "9de7ff33-5bfc-4a59-9999-8cb6ecd99cfb", "Terminal_1.0_26", 1, 0, "5号位摄像机报警状态", 0, null, null, false, false, false, "a2cc66e6-dcf5-4326-a085-fe2a779f29e8", true, "设备离线", true, "0,5", 0.0, false, false, false, false, "05号位摄像机恢复正常,15号位摄像机离线", "Camera_5" },
                    { "2cfb2f8b-157e-4ccb-987a-84b642e4dc12", "Terminal_1.0_25", 1, 0, "4号位摄像机报警状态", 0, null, null, false, false, false, "a2cc66e6-dcf5-4326-a085-fe2a779f29e8", true, "设备离线", true, "0,5", 0.0, false, false, false, false, "04号位摄像机恢复正常,14号位摄像机离线", "Camera_4" },
                    { "62e0ce0c-add6-4ffa-acc8-110ab3db073d", "Terminal_1.0_24", 1, 0, "3号位摄像机报警状态", 0, null, null, false, false, false, "a2cc66e6-dcf5-4326-a085-fe2a779f29e8", true, "设备离线", true, "0,5", 0.0, false, false, false, false, "03号位摄像机恢复正常,13号位摄像机离线", "Camera_3" },
                    { "1b81269f-4f87-49f0-9067-5bdc8c658f9a", "Terminal_1.0_23", 1, 0, "2号位摄像机报警状态", 0, null, null, false, false, false, "a2cc66e6-dcf5-4326-a085-fe2a779f29e8", true, "设备离线", true, "0,5", 0.0, false, false, false, false, "02号位摄像机恢复正常,12号位摄像机离线", "Camera_2" },
                    { "dd9800db-0752-465e-bd5a-8603953f5e5c", "Terminal_1.0_22", 1, 0, "1号位摄像机报警状态", 0, null, null, false, false, false, "a2cc66e6-dcf5-4326-a085-fe2a779f29e8", true, "设备离线", true, "0,5", 0.0, false, false, false, false, "01号位摄像机恢复正常,11号位摄像机离线", "Camera_1" },
                    { "f71071e9-51f7-4497-9c67-44523ab43e53", "Terminal_1.0_29", 1, 0, "8号位摄像机报警状态", 0, null, null, false, false, false, "a2cc66e6-dcf5-4326-a085-fe2a779f29e8", true, "设备离线", true, "0,5", 0.0, false, false, false, false, "08号位摄像机恢复正常,18号位摄像机离线", "Camera_8" }
                });

            migrationBuilder.InsertData(
                table: "DeviceTypeChannels",
                columns: new[] { "Id", "DeviceTypeId", "Enabled", "OutputThreePhase", "OutputType", "OutputValue", "PortDefaultName", "PortNumber", "PortType", "Sort" },
                values: new object[,]
                {
                    { "214eaaa2-7202-460b-b277-d1776af7a4e9", "a2cc66e6-dcf5-4326-a085-fe2a779f29e8", true, false, 1, 220.0, "输出二", 7, 0, "7" },
                    { "b9cb2624-f7d6-4667-ac81-339e660a690d", "a2cc66e6-dcf5-4326-a085-fe2a779f29e8", true, false, 1, 220.0, "输出一", 6, 0, "6" },
                    { "e36d6fe7-8450-4191-a51e-6728e1735ac3", "a2cc66e6-dcf5-4326-a085-fe2a779f29e8", true, false, 2, 12.0, "风扇端口", 5, 4, "5" },
                    { "68fb7bfe-1641-49cd-87e9-c2e9f96d170c", "a2cc66e6-dcf5-4326-a085-fe2a779f29e8", true, false, 1, 220.0, "补光端口", 1, 2, "1" },
                    { "a257b204-48e7-4d8e-8e71-be3a7f8925ba", "a2cc66e6-dcf5-4326-a085-fe2a779f29e8", true, false, 1, 220.0, "光通端口", 3, 1, "3" },
                    { "7354f931-d8b1-4950-94aa-757641648f4d", "a2cc66e6-dcf5-4326-a085-fe2a779f29e8", true, false, 1, 220.0, "视频端口", 2, 0, "2" },
                    { "75abdbdc-b48b-49c6-aa52-b24eb2ee6335", "a2cc66e6-dcf5-4326-a085-fe2a779f29e8", true, false, 1, 220.0, "加热端口", 4, 3, "4" }
                });

            migrationBuilder.InsertData(
                table: "DeviceTypeControllers",
                columns: new[] { "Id", "Buttons", "ControllerCode", "ControllerStatus", "ControllerType", "Description", "DeviceTypeId", "Enabled", "PortNumber" },
                values: new object[,]
                {
                    { "acfdbd27-fbe8-4bc2-a461-a4cdd5972e4a", "开,关", "Controller_25", "0", 0, "风扇", "a2cc66e6-dcf5-4326-a085-fe2a779f29e8", true, 5 },
                    { "1bd0eaf6-2460-4f10-89cc-9b3ef8d624b2", "开,关", "Controller_24", "0", 0, "加热开关", "a2cc66e6-dcf5-4326-a085-fe2a779f29e8", true, 4 },
                    { "01d788bc-40c0-4ae4-bfcb-e81e4ac5cec3", "开,关", "Controller_22", "0", 0, "光通开关", "a2cc66e6-dcf5-4326-a085-fe2a779f29e8", true, 3 },
                    { "cae27080-162b-47a2-8789-b216f7b17c33", "开,关", "Controller_21", "0", 0, "视频开关", "a2cc66e6-dcf5-4326-a085-fe2a779f29e8", true, 2 },
                    { "655fc4bd-212a-4089-9946-cd6ae7c927f3", "开,关", "Controller_23", "0", 0, "补光开关", "a2cc66e6-dcf5-4326-a085-fe2a779f29e8", true, 1 }
                });

            migrationBuilder.InsertData(
                table: "DeviceTypeFunctions",
                columns: new[] { "Id", "CallbackCodes", "Description", "DeviceTypeId", "Enabled", "FunctionCode", "FunctionName", "FunctionType", "PortNumber", "Setting", "SettingTypeName", "Sort" },
                values: new object[,]
                {
                    { "847b81ca-2f89-4884-a53b-0bce5651926f", "ChannelStatus", null, "a2cc66e6-dcf5-4326-a085-fe2a779f29e8", false, "onoff", "回路开关", 1, 0, "{\"ChannelType\":0,\"Switch\":0,\"ReStart\":0,\"AutoControl\":0}", "SFBR.Device.Common.ConfigModel.SkynetTerminal.ChannelControlDto", "11" },
                    { "0f878c80-0caf-4aa3-aaed-a6dc54e1692e", "RdDeviceInfo", null, "a2cc66e6-dcf5-4326-a085-fe2a779f29e8", false, "deviceinfo", "终端信息", 1, 0, "{\"DeviceInfo\":{\"EnableVedioChannel\":false,\"EnableOpticalChannel\":false,\"EnableLEDChannel\":false,\"EnableHeatingChannel\":false,\"EnableFanChannel\":false,\"Enable4G\":false,\"EnableWifi\":false,\"EnableElectromagneticDoor\":false,\"EnablePowerSupplyArrester\":false,\"EnableNetworkLightningArrester\":false,\"EnableAutomaticReclosing\":false,\"GPS\":{\"Enable\":false,\"Latitude\":0.0,\"Longitude\":0.0,\"EW\":0,\"EWDegree\":null,\"EWMinute\":null,\"EWSecond\":null,\"NS\":0,\"NSDegree\":null,\"NSMinute\":null,\"NSSecond\":null}},\"UniqueId\":null,\"Success\":false,\"ErrorMessage\":null,\"CmdType\":0,\"CmdName\":null,\"ProtocolType\":0,\"TipMessage\":null,\"Data\":null,\"FromIP\":null,\"FromPort\":0}", "SFBR.Device.Common.ConfigModel.SkynetTerminal.Models.DeviceInfoResultDto", "10" },
                    { "25d45c5e-8738-4d2b-bdcf-fecae2ed2d4e", "RdLatitudeAndLongitude", null, "a2cc66e6-dcf5-4326-a085-fe2a779f29e8", false, "position", "终端定位", 1, 0, "{\"LatitudeAndLongitude\":{\"Enable\":false,\"Latitude\":0.0,\"Longitude\":0.0,\"EW\":0,\"EWDegree\":null,\"EWMinute\":null,\"EWSecond\":null,\"NS\":0,\"NSDegree\":null,\"NSMinute\":null,\"NSSecond\":null},\"UniqueId\":null,\"Success\":false,\"ErrorMessage\":null,\"CmdType\":0,\"CmdName\":null,\"ProtocolType\":0,\"TipMessage\":null,\"Data\":null,\"FromIP\":null,\"FromPort\":0}", "SFBR.Device.Common.ConfigModel.SkynetTerminal.Models.LatitudeAndLongitudeResultDto", "09" },
                    { "48a71287-a1fe-4e0a-89b0-6ff690f4aaf5", "RdChannelMode", null, "a2cc66e6-dcf5-4326-a085-fe2a779f29e8", true, "model", "工作模式", 1, 0, "[{\"ChannelType\":0,\"AutoControlMode\":1,\"UniqueId\":null,\"Success\":false,\"ErrorMessage\":null,\"CmdType\":0,\"CmdName\":null,\"ProtocolType\":0,\"TipMessage\":null,\"Data\":null,\"FromIP\":null,\"FromPort\":0},{\"ChannelType\":1,\"AutoControlMode\":1,\"UniqueId\":null,\"Success\":false,\"ErrorMessage\":null,\"CmdType\":0,\"CmdName\":null,\"ProtocolType\":0,\"TipMessage\":null,\"Data\":null,\"FromIP\":null,\"FromPort\":0},{\"ChannelType\":2,\"AutoControlMode\":1,\"UniqueId\":null,\"Success\":false,\"ErrorMessage\":null,\"CmdType\":0,\"CmdName\":null,\"ProtocolType\":0,\"TipMessage\":null,\"Data\":null,\"FromIP\":null,\"FromPort\":0},{\"ChannelType\":3,\"AutoControlMode\":1,\"UniqueId\":null,\"Success\":false,\"ErrorMessage\":null,\"CmdType\":0,\"CmdName\":null,\"ProtocolType\":0,\"TipMessage\":null,\"Data\":null,\"FromIP\":null,\"FromPort\":0},{\"ChannelType\":4,\"AutoControlMode\":1,\"UniqueId\":null,\"Success\":false,\"ErrorMessage\":null,\"CmdType\":0,\"CmdName\":null,\"ProtocolType\":0,\"TipMessage\":null,\"Data\":null,\"FromIP\":null,\"FromPort\":0}]", "System.Collections.Generic.List`1[[SFBR.Device.Common.ConfigModel.SkynetTerminal.Models.ChannelModeResultDto, SFBR.Device.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]]", "08" },
                    { "3f9f73d6-96b1-4afe-987f-8fc9bdc7ba00", "DeviceTime", null, "a2cc66e6-dcf5-4326-a085-fe2a779f29e8", true, "deviceTime", "终端时间", 1, 0, "{\"OperateType\":0,\"Time\":null,\"TimeString\":null,\"ServerTime\":\"2019-10-28T19:06:10.0257283+08:00\",\"UniqueId\":null,\"Success\":false,\"ErrorMessage\":null,\"CmdType\":0,\"CmdName\":null,\"ProtocolType\":0,\"TipMessage\":null,\"Data\":null,\"FromIP\":null,\"FromPort\":0}", "SFBR.Device.Common.ConfigModel.SkynetTerminal.Models.DeviceTimeResultDto", "06" },
                    { "58ed7c2e-89b0-40db-b166-fa5008315e29", "RdCameraFaultCheckTime,WdCameraFaultCheckTime", null, "a2cc66e6-dcf5-4326-a085-fe2a779f29e8", true, "checkfrequency", "检测时长", 1, 0, "{\"Interval\":0,\"UniqueId\":null,\"Success\":false,\"ErrorMessage\":null,\"CmdType\":0,\"CmdName\":null,\"ProtocolType\":0,\"TipMessage\":null,\"Data\":null,\"FromIP\":null,\"FromPort\":0}", "SFBR.Device.Common.ConfigModel.SkynetTerminal.Models.CameraFaultCheckTimeResultDto", "07" },
                    { "c2db1494-ad27-4ce1-910f-87cd9edbfe06", "RdVATHLimit,WdVATHLimit", null, "a2cc66e6-dcf5-4326-a085-fe2a779f29e8", true, "threshold", "报警阈值", 1, 0, "{\"Limit\":{\"UpperV\":0.0,\"LowerV\":0.0,\"UpperA\":0.0,\"LowerA\":0.0,\"UpperT\":0.0,\"LowerT\":0.0,\"UpperH\":0},\"UniqueId\":null,\"Success\":false,\"ErrorMessage\":null,\"CmdType\":0,\"CmdName\":null,\"ProtocolType\":0,\"TipMessage\":null,\"Data\":null,\"FromIP\":null,\"FromPort\":0}", "SFBR.Device.Common.ConfigModel.SkynetTerminal.Models.VATHLimitResultDto", "04" },
                    { "8a96a8c3-c4f7-420c-ab46-42484e543443", "RdDisarmControl,RdDisarmControl", null, "a2cc66e6-dcf5-4326-a085-fe2a779f29e8", true, "defending", "撤防时间", 1, 0, "{\"Enable\":false,\"StartTime\":\"0001-01-01T00:00:00\",\"EndTime\":\"0001-01-01T00:00:00\",\"UniqueId\":null,\"Success\":false,\"ErrorMessage\":null,\"CmdType\":0,\"CmdName\":null,\"ProtocolType\":0,\"TipMessage\":null,\"Data\":null,\"FromIP\":null,\"FromPort\":0}", "SFBR.Device.Common.ConfigModel.SkynetTerminal.Models.DisarmResultDto", "03" },
                    { "123c1db7-d3d8-4b6d-a236-4d943bbae14c", "WdVedioAssign,RdVedioAssign", null, "a2cc66e6-dcf5-4326-a085-fe2a779f29e8", false, "mountPort", "挂载端口", 1, 0, "[{\"CameraChannel\":1,\"VedioChannelType\":1,\"UniqueId\":null,\"Success\":false,\"ErrorMessage\":null,\"CmdType\":0,\"CmdName\":null,\"ProtocolType\":0,\"TipMessage\":null,\"Data\":null,\"FromIP\":null,\"FromPort\":0},{\"CameraChannel\":2,\"VedioChannelType\":1,\"UniqueId\":null,\"Success\":false,\"ErrorMessage\":null,\"CmdType\":0,\"CmdName\":null,\"ProtocolType\":0,\"TipMessage\":null,\"Data\":null,\"FromIP\":null,\"FromPort\":0},{\"CameraChannel\":3,\"VedioChannelType\":1,\"UniqueId\":null,\"Success\":false,\"ErrorMessage\":null,\"CmdType\":0,\"CmdName\":null,\"ProtocolType\":0,\"TipMessage\":null,\"Data\":null,\"FromIP\":null,\"FromPort\":0},{\"CameraChannel\":4,\"VedioChannelType\":1,\"UniqueId\":null,\"Success\":false,\"ErrorMessage\":null,\"CmdType\":0,\"CmdName\":null,\"ProtocolType\":0,\"TipMessage\":null,\"Data\":null,\"FromIP\":null,\"FromPort\":0},{\"CameraChannel\":5,\"VedioChannelType\":1,\"UniqueId\":null,\"Success\":false,\"ErrorMessage\":null,\"CmdType\":0,\"CmdName\":null,\"ProtocolType\":0,\"TipMessage\":null,\"Data\":null,\"FromIP\":null,\"FromPort\":0},{\"CameraChannel\":6,\"VedioChannelType\":1,\"UniqueId\":null,\"Success\":false,\"ErrorMessage\":null,\"CmdType\":0,\"CmdName\":null,\"ProtocolType\":0,\"TipMessage\":null,\"Data\":null,\"FromIP\":null,\"FromPort\":0},{\"CameraChannel\":7,\"VedioChannelType\":1,\"UniqueId\":null,\"Success\":false,\"ErrorMessage\":null,\"CmdType\":0,\"CmdName\":null,\"ProtocolType\":0,\"TipMessage\":null,\"Data\":null,\"FromIP\":null,\"FromPort\":0},{\"CameraChannel\":8,\"VedioChannelType\":1,\"UniqueId\":null,\"Success\":false,\"ErrorMessage\":null,\"CmdType\":0,\"CmdName\":null,\"ProtocolType\":0,\"TipMessage\":null,\"Data\":null,\"FromIP\":null,\"FromPort\":0}]", "SFBR.Device.Common.ConfigModel.SkynetTerminal.Models.VedioChannelAssignResultDto", "02" },
                    { "6cce2c19-7857-4a7e-b1bb-6087c38a3c62", "VedioTask,FanTask,HeatingTask,LEDTask,OpticalTask", null, "a2cc66e6-dcf5-4326-a085-fe2a779f29e8", true, "task", "定时任务", 1, 0, "[{\"ChannelType\":0,\"OperateType\":1,\"ListTaskPlan\":[{\"Number\":1,\"Enable\":false,\"TaskType\":0,\"StartTime\":\"0001-01-01T00:00:00\",\"EndTime\":\"0001-01-01T00:00:00\"},{\"Number\":2,\"Enable\":false,\"TaskType\":0,\"StartTime\":\"0001-01-01T00:00:00\",\"EndTime\":\"0001-01-01T00:00:00\"},{\"Number\":3,\"Enable\":false,\"TaskType\":0,\"StartTime\":\"0001-01-01T00:00:00\",\"EndTime\":\"0001-01-01T00:00:00\"}],\"NextExeTimeString\":null,\"NextAction\":-1,\"CurrentTimeString\":\"2019-10-28T19:06:09.+08:00\",\"UniqueId\":null,\"Success\":false,\"ErrorMessage\":null,\"CmdType\":0,\"CmdName\":null,\"ProtocolType\":0,\"TipMessage\":null,\"Data\":null,\"FromIP\":null,\"FromPort\":0},{\"ChannelType\":1,\"OperateType\":1,\"ListTaskPlan\":[{\"Number\":1,\"Enable\":false,\"TaskType\":0,\"StartTime\":\"0001-01-01T00:00:00\",\"EndTime\":\"0001-01-01T00:00:00\"},{\"Number\":2,\"Enable\":false,\"TaskType\":0,\"StartTime\":\"0001-01-01T00:00:00\",\"EndTime\":\"0001-01-01T00:00:00\"},{\"Number\":3,\"Enable\":false,\"TaskType\":0,\"StartTime\":\"0001-01-01T00:00:00\",\"EndTime\":\"0001-01-01T00:00:00\"}],\"NextExeTimeString\":null,\"NextAction\":-1,\"CurrentTimeString\":\"2019-10-28T19:06:10.+08:00\",\"UniqueId\":null,\"Success\":false,\"ErrorMessage\":null,\"CmdType\":0,\"CmdName\":null,\"ProtocolType\":0,\"TipMessage\":null,\"Data\":null,\"FromIP\":null,\"FromPort\":0},{\"ChannelType\":2,\"OperateType\":1,\"ListTaskPlan\":[{\"Number\":1,\"Enable\":false,\"TaskType\":0,\"StartTime\":\"0001-01-01T00:00:00\",\"EndTime\":\"0001-01-01T00:00:00\"},{\"Number\":2,\"Enable\":false,\"TaskType\":0,\"StartTime\":\"0001-01-01T00:00:00\",\"EndTime\":\"0001-01-01T00:00:00\"},{\"Number\":3,\"Enable\":false,\"TaskType\":0,\"StartTime\":\"0001-01-01T00:00:00\",\"EndTime\":\"0001-01-01T00:00:00\"}],\"NextExeTimeString\":null,\"NextAction\":-1,\"CurrentTimeString\":\"2019-10-28T19:06:10.+08:00\",\"UniqueId\":null,\"Success\":false,\"ErrorMessage\":null,\"CmdType\":0,\"CmdName\":null,\"ProtocolType\":0,\"TipMessage\":null,\"Data\":null,\"FromIP\":null,\"FromPort\":0},{\"ChannelType\":3,\"OperateType\":1,\"ListTaskPlan\":[{\"Number\":1,\"Enable\":false,\"TaskType\":0,\"StartTime\":\"0001-01-01T00:00:00\",\"EndTime\":\"0001-01-01T00:00:00\"},{\"Number\":2,\"Enable\":false,\"TaskType\":0,\"StartTime\":\"0001-01-01T00:00:00\",\"EndTime\":\"0001-01-01T00:00:00\"},{\"Number\":3,\"Enable\":false,\"TaskType\":0,\"StartTime\":\"0001-01-01T00:00:00\",\"EndTime\":\"0001-01-01T00:00:00\"}],\"NextExeTimeString\":null,\"NextAction\":-1,\"CurrentTimeString\":\"2019-10-28T19:06:10.+08:00\",\"UniqueId\":null,\"Success\":false,\"ErrorMessage\":null,\"CmdType\":0,\"CmdName\":null,\"ProtocolType\":0,\"TipMessage\":null,\"Data\":null,\"FromIP\":null,\"FromPort\":0},{\"ChannelType\":4,\"OperateType\":1,\"ListTaskPlan\":[{\"Number\":1,\"Enable\":false,\"TaskType\":0,\"StartTime\":\"0001-01-01T00:00:00\",\"EndTime\":\"0001-01-01T00:00:00\"},{\"Number\":2,\"Enable\":false,\"TaskType\":0,\"StartTime\":\"0001-01-01T00:00:00\",\"EndTime\":\"0001-01-01T00:00:00\"},{\"Number\":3,\"Enable\":false,\"TaskType\":0,\"StartTime\":\"0001-01-01T00:00:00\",\"EndTime\":\"0001-01-01T00:00:00\"}],\"NextExeTimeString\":null,\"NextAction\":-1,\"CurrentTimeString\":\"2019-10-28T19:06:10.+08:00\",\"UniqueId\":null,\"Success\":false,\"ErrorMessage\":null,\"CmdType\":0,\"CmdName\":null,\"ProtocolType\":0,\"TipMessage\":null,\"Data\":null,\"FromIP\":null,\"FromPort\":0}]", "System.Collections.Generic.List`1[[SFBR.Device.Common.ConfigModel.SkynetTerminal.Models.ChannelTaskPlanResultDto, SFBR.Device.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]]", "01" },
                    { "adf224d1-9a29-4f10-b1d4-8e024e655cc4", "RdCameraIP,WdCameraIP", null, "a2cc66e6-dcf5-4326-a085-fe2a779f29e8", true, "deviceip", "设备IP", 1, 0, "[{\"CameraIP\":{\"Number\":1,\"IP\":\"0.0.0.0\",\"Enable\":false},\"UniqueId\":null,\"Success\":false,\"ErrorMessage\":null,\"CmdType\":0,\"CmdName\":null,\"ProtocolType\":0,\"TipMessage\":null,\"Data\":null,\"FromIP\":null,\"FromPort\":0},{\"CameraIP\":{\"Number\":2,\"IP\":\"0.0.0.0\",\"Enable\":false},\"UniqueId\":null,\"Success\":false,\"ErrorMessage\":null,\"CmdType\":0,\"CmdName\":null,\"ProtocolType\":0,\"TipMessage\":null,\"Data\":null,\"FromIP\":null,\"FromPort\":0},{\"CameraIP\":{\"Number\":3,\"IP\":\"0.0.0.0\",\"Enable\":false},\"UniqueId\":null,\"Success\":false,\"ErrorMessage\":null,\"CmdType\":0,\"CmdName\":null,\"ProtocolType\":0,\"TipMessage\":null,\"Data\":null,\"FromIP\":null,\"FromPort\":0},{\"CameraIP\":{\"Number\":4,\"IP\":\"0.0.0.0\",\"Enable\":false},\"UniqueId\":null,\"Success\":false,\"ErrorMessage\":null,\"CmdType\":0,\"CmdName\":null,\"ProtocolType\":0,\"TipMessage\":null,\"Data\":null,\"FromIP\":null,\"FromPort\":0},{\"CameraIP\":{\"Number\":5,\"IP\":\"0.0.0.0\",\"Enable\":false},\"UniqueId\":null,\"Success\":false,\"ErrorMessage\":null,\"CmdType\":0,\"CmdName\":null,\"ProtocolType\":0,\"TipMessage\":null,\"Data\":null,\"FromIP\":null,\"FromPort\":0},{\"CameraIP\":{\"Number\":6,\"IP\":\"0.0.0.0\",\"Enable\":false},\"UniqueId\":null,\"Success\":false,\"ErrorMessage\":null,\"CmdType\":0,\"CmdName\":null,\"ProtocolType\":0,\"TipMessage\":null,\"Data\":null,\"FromIP\":null,\"FromPort\":0},{\"CameraIP\":{\"Number\":7,\"IP\":\"0.0.0.0\",\"Enable\":false},\"UniqueId\":null,\"Success\":false,\"ErrorMessage\":null,\"CmdType\":0,\"CmdName\":null,\"ProtocolType\":0,\"TipMessage\":null,\"Data\":null,\"FromIP\":null,\"FromPort\":0},{\"CameraIP\":{\"Number\":8,\"IP\":\"0.0.0.0\",\"Enable\":false},\"UniqueId\":null,\"Success\":false,\"ErrorMessage\":null,\"CmdType\":0,\"CmdName\":null,\"ProtocolType\":0,\"TipMessage\":null,\"Data\":null,\"FromIP\":null,\"FromPort\":0}]", "System.Collections.Generic.List`1[[SFBR.Device.Common.ConfigModel.SkynetTerminal.Models.CameraIPResultDto, SFBR.Device.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]]", "05" }
                });

            migrationBuilder.InsertData(
                table: "DeviceTypeParts",
                columns: new[] { "Id", "BrandId", "CompanyId", "Description", "DeviceTypeId", "Enabled", "HasStatus", "OprationId", "PartCode", "PartName", "PartType", "PortNumber", "StatusIndex", "Warranty" },
                values: new object[,]
                {
                    { "8f47ec80-f1be-48a3-b5bb-6fa6ff4bebd3", null, null, null, "a2cc66e6-dcf5-4326-a085-fe2a779f29e8", true, true, null, "Part_7", "GPS模块", 2, -1, 0, 0.0 },
                    { "9b6a420e-7c54-43b9-8b2c-f45952ede634", null, null, null, "a2cc66e6-dcf5-4326-a085-fe2a779f29e8", true, true, null, "Part_10", "UPS", 3, -1, 0, 0.0 },
                    { "20d7fa01-0fc9-4845-8651-cf4f148511b7", null, null, null, "a2cc66e6-dcf5-4326-a085-fe2a779f29e8", true, false, null, "Part_9", "自定义电源", 3, -1, 0, 0.0 },
                    { "2f181b8c-b505-4eea-8169-6635e5fe7f6f", null, null, null, "a2cc66e6-dcf5-4326-a085-fe2a779f29e8", true, true, null, "Part_8", "市电", 3, -1, 0, 0.0 },
                    { "140f8334-9caa-442f-8640-8768010d5d42", null, null, null, "a2cc66e6-dcf5-4326-a085-fe2a779f29e8", true, true, null, "Part_6", "4G模块", 2, -1, 0, 0.0 },
                    { "74611702-c9d4-4601-b7f7-acac9a8c9f79", null, null, null, "a2cc66e6-dcf5-4326-a085-fe2a779f29e8", true, true, null, "Part_1", "门磁", 1, -1, 26, 0.0 },
                    { "c1cad59c-f059-428c-9aca-26b26feb8ad4", null, null, null, "a2cc66e6-dcf5-4326-a085-fe2a779f29e8", true, true, null, "Part_4", "重合闸", 2, -1, 0, 0.0 },
                    { "1ff2fdfa-68bd-49c0-ae5b-05aa064d9701", null, null, null, "a2cc66e6-dcf5-4326-a085-fe2a779f29e8", true, true, null, "Part_3", "网络防雷器", 2, -1, 28, 0.0 },
                    { "a0524f9c-023b-4250-adf5-bab290f19609", null, null, null, "a2cc66e6-dcf5-4326-a085-fe2a779f29e8", true, true, null, "Part_2", "电源防雷器", 1, -1, 27, 0.0 },
                    { "a24dee82-e6b3-48ee-95bf-fe9fc2051b33", null, null, null, "a2cc66e6-dcf5-4326-a085-fe2a779f29e8", true, true, null, "Part_5", "WIFI模块", 2, -1, 0, 0.0 }
                });

            migrationBuilder.InsertData(
                table: "DeviceTypeSensors",
                columns: new[] { "Id", "Description", "DeviceTypeId", "Enabled", "LowerValue", "PortNumber", "SensorCode", "SensorName", "SensorType", "UpperValue" },
                values: new object[,]
                {
                    { "bfe61838-b26d-468c-b95b-6c1a66c4fa91", null, "a2cc66e6-dcf5-4326-a085-fe2a779f29e8", true, -20.0, -1, "Sensor_3", "温度传感器", 3, 65.0 },
                    { "f499c2f7-0bf6-4ea0-8b04-50919ae7b346", null, "a2cc66e6-dcf5-4326-a085-fe2a779f29e8", true, 180.0, -1, "Sensor_1", "电压传感器", 1, 260.0 },
                    { "39e6c5f9-b2bf-4686-9990-c8846181fcf8", null, "a2cc66e6-dcf5-4326-a085-fe2a779f29e8", true, 0.0, -1, "Sensor_2", "电流传感器", 2, 0.0 },
                    { "376cbc88-b5ce-4d4d-a104-83901eea60d8", null, "a2cc66e6-dcf5-4326-a085-fe2a779f29e8", true, null, -1, "Sensor_4", "湿度传感器", 5, 90.0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Channels_TerminalId",
                table: "Channels",
                column: "TerminalId");

            migrationBuilder.CreateIndex(
                name: "IX_Controllers_TerminalId",
                table: "Controllers",
                column: "TerminalId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceAlarms_TerminalId",
                table: "DeviceAlarms",
                column: "TerminalId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceFunctions_TerminalId",
                table: "DeviceFunctions",
                column: "TerminalId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceProp_CameraId",
                table: "DeviceProp",
                column: "CameraId");

            migrationBuilder.CreateIndex(
                name: "IX_Devices_TerminalId",
                table: "Devices",
                column: "TerminalId");

            migrationBuilder.CreateIndex(
                name: "IX_Devices_ParentId",
                table: "Devices",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Devices_LocationId",
                table: "Devices",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceTypeAlarms_DeviceTypeId",
                table: "DeviceTypeAlarms",
                column: "DeviceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceTypeChannels_DeviceTypeId",
                table: "DeviceTypeChannels",
                column: "DeviceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceTypeControllers_DeviceTypeId",
                table: "DeviceTypeControllers",
                column: "DeviceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceTypeFunctions_DeviceTypeId",
                table: "DeviceTypeFunctions",
                column: "DeviceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceTypeParts_DeviceTypeId",
                table: "DeviceTypeParts",
                column: "DeviceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceTypeSensors_DeviceTypeId",
                table: "DeviceTypeSensors",
                column: "DeviceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Parts_TerminalId",
                table: "Parts",
                column: "TerminalId");

            migrationBuilder.CreateIndex(
                name: "IX_Regions_ParentId",
                table: "Regions",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Sensors_TerminalId",
                table: "Sensors",
                column: "TerminalId");

            migrationBuilder.CreateIndex(
                name: "IX_TimedTasks_TerminalId",
                table: "TimedTasks",
                column: "TerminalId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Brands");

            migrationBuilder.DropTable(
                name: "Channels");

            migrationBuilder.DropTable(
                name: "Controllers");

            migrationBuilder.DropTable(
                name: "DeviceAlarms");

            migrationBuilder.DropTable(
                name: "DeviceFunctions");

            migrationBuilder.DropTable(
                name: "DevicePowers");

            migrationBuilder.DropTable(
                name: "DeviceProp");

            migrationBuilder.DropTable(
                name: "DeviceTypeAlarms");

            migrationBuilder.DropTable(
                name: "DeviceTypeChannels");

            migrationBuilder.DropTable(
                name: "DeviceTypeControllers");

            migrationBuilder.DropTable(
                name: "DeviceTypeFunctions");

            migrationBuilder.DropTable(
                name: "DeviceTypeParts");

            migrationBuilder.DropTable(
                name: "DeviceTypeSensors");

            migrationBuilder.DropTable(
                name: "Parts");

            migrationBuilder.DropTable(
                name: "Regions");

            migrationBuilder.DropTable(
                name: "Sensors");

            migrationBuilder.DropTable(
                name: "TimedTasks");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "DeviceTypes");

            migrationBuilder.DropTable(
                name: "Devices");

            migrationBuilder.DropTable(
                name: "Locations");
        }
    }
}
