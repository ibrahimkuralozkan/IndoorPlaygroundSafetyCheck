using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IndoorPlaygroundSafetyCheck.Migrations
{
    /// <inheritdoc />
    public partial class AddEmployeeSaveProcedure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var createProcedureSql = @"
CREATE OR ALTER PROCEDURE SafetyCheck.fsp_EmployeeSave
    @FirstName NVARCHAR(50),
    @LastName NVARCHAR(50),
    @Position INT = 0, -- Default Position to 0 if not specified
    @RfidUid NVARCHAR(10)
AS
BEGIN
    -- Insert the new employee into the Employee table with default handling
    INSERT INTO SafetyCheck.Employee 
        (FirstName, LastName, Position, RfidUid, InsertTimeStamp, InsertedBy, UpdateTimeStamp, UpdatedBy)
    VALUES 
        (@FirstName, @LastName, @Position, @RfidUid, GETDATE(), SUSER_NAME(), GETDATE(), SUSER_NAME());
    
    -- Return the ID of the newly added employee
    SELECT SCOPE_IDENTITY() AS NewEmployeeIdent;
END;";

            migrationBuilder.Sql(createProcedureSql);
            migrationBuilder.DropTable(
                name: "Position",
                schema: "SafetyCheck");

            migrationBuilder.RenameColumn(
                name: "PositionIdent",
                schema: "SafetyCheck",
                table: "Employee",
                newName: "Position");

            migrationBuilder.AlterColumn<string>(
                name: "QuestionText",
                schema: "SafetyCheck",
                table: "StationQuestion",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StationIdent",
                schema: "SafetyCheck",
                table: "InspectionQuestionResult",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "IsSent",
                schema: "SafetyCheck",
                table: "Inspection",
                type: "int",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                schema: "SafetyCheck",
                table: "Employee",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Message",
                schema: "SafetyCheck",
                columns: table => new
                {
                    Ident = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InspectionIdent = table.Column<int>(type: "int", nullable: false),
                    InspectionQuestionResultIdent = table.Column<int>(type: "int", nullable: false),
                    SenderIdent = table.Column<int>(type: "int", nullable: false),
                    ReceiverIdent = table.Column<int>(type: "int", nullable: false),
                    PictureData = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InsertTimeStamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InsertedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdateTimeStamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsRead = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Message", x => x.Ident);
                });

            migrationBuilder.CreateTable(
                name: "Repair",
                schema: "SafetyCheck",
                columns: table => new
                {
                    Ident = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StationIdent = table.Column<int>(type: "int", nullable: false),
                    InspectionIdent = table.Column<int>(type: "int", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    RepairDatePlan = table.Column<DateTime>(type: "date", nullable: true),
                    IsRepaired = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    RepairedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    InsertTimeStamp = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    InsertedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValueSql: "suser_name()"),
                    UpdateTimeStamp = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    UpdatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValueSql: "suser_name()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Repair", x => x.Ident);
                    table.ForeignKey(
                        name: "FK_Repair_Station",
                        column: x => x.StationIdent,
                        principalSchema: "SafetyCheck",
                        principalTable: "Station",
                        principalColumn: "Ident");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Repair_StationIdent",
                schema: "SafetyCheck",
                table: "Repair",
                column: "StationIdent");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS SafetyCheck.fsp_EmployeeSave");
            migrationBuilder.DropTable(
                name: "Message",
                schema: "SafetyCheck");

            migrationBuilder.DropTable(
                name: "Repair",
                schema: "SafetyCheck");

            migrationBuilder.DropColumn(
                name: "StationIdent",
                schema: "SafetyCheck",
                table: "InspectionQuestionResult");

            migrationBuilder.RenameColumn(
                name: "Position",
                schema: "SafetyCheck",
                table: "Employee",
                newName: "PositionIdent");

            migrationBuilder.AlterColumn<string>(
                name: "QuestionText",
                schema: "SafetyCheck",
                table: "StationQuestion",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<bool>(
                name: "IsSent",
                schema: "SafetyCheck",
                table: "Inspection",
                type: "bit",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                schema: "SafetyCheck",
                table: "Employee",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.CreateTable(
                name: "Position",
                schema: "SafetyCheck",
                columns: table => new
                {
                    Ident = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AuthenticationCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    InsertTimeStamp = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    InsertedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValueSql: "(suser_name())"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UpdateTimeStamp = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    UpdatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValueSql: "(suser_name())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Position__177FD81EFD7E87D4", x => x.Ident);
                });
        }
    }
}
