using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IndoorPlaygroundSafetyCheck.Migrations
{
    /// <inheritdoc />
    public partial class AddIsSentToInspections : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "SafetyCheck");

            migrationBuilder.CreateTable(
                name: "Employee",
                schema: "SafetyCheck",
                columns: table => new
                {
                    Ident = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PositionIdent = table.Column<int>(type: "int", nullable: false),
                    RfidUid = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    InsertTimeStamp = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    InsertedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValueSql: "(suser_name())"),
                    UpdateTimeStamp = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    UpdatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValueSql: "(suser_name())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Employee__177FD81EFDDD6A08", x => x.Ident);
                });

            migrationBuilder.CreateTable(
                name: "Inspection",
                schema: "SafetyCheck",
                columns: table => new
                {
                    Ident = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CheckStart = table.Column<DateTime>(type: "datetime", nullable: true),
                    CheckDone = table.Column<DateTime>(type: "datetime", nullable: true),
                    Type = table.Column<int>(type: "int", nullable: true),
                    Signature = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    InsertTimeStamp = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    InsertedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValueSql: "(suser_name())"),
                    UpdatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValueSql: "(suser_name())"),
                    UpdateTimeStamp = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    RfidUid = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    IsSent = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inspection", x => x.Ident);
                });

            migrationBuilder.CreateTable(
                name: "Position",
                schema: "SafetyCheck",
                columns: table => new
                {
                    Ident = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    InsertTimeStamp = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    InsertedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValueSql: "(suser_name())"),
                    UpdateTimeStamp = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    UpdatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValueSql: "(suser_name())"),
                    AuthenticationCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Position__177FD81EFD7E87D4", x => x.Ident);
                });

            migrationBuilder.CreateTable(
                name: "QuestionCatalogue",
                schema: "SafetyCheck",
                columns: table => new
                {
                    Ident = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    InsertTimeStamp = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    InsertedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValueSql: "(suser_name())"),
                    UpdatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValueSql: "(suser_name())"),
                    UpdateTimeStamp = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionCatalogue", x => x.Ident);
                });

            migrationBuilder.CreateTable(
                name: "Station",
                schema: "SafetyCheck",
                columns: table => new
                {
                    Ident = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    InsertTimeStamp = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    InsertedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValueSql: "(suser_name())"),
                    UpdatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValueSql: "(suser_name())"),
                    UpdateTimeStamp = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Station", x => x.Ident);
                });

            migrationBuilder.CreateTable(
                name: "StationQuestion",
                schema: "SafetyCheck",
                columns: table => new
                {
                    Ident = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionCatalogueIdent = table.Column<int>(type: "int", nullable: false),
                    StationIdent = table.Column<int>(type: "int", nullable: false),
                    InsertTimeStamp = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    InsertedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValueSql: "(suser_name())"),
                    UpdatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValueSql: "(suser_name())"),
                    UpdateTimeStamp = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    QuestionText = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StationQuestion_1", x => x.Ident);
                    table.ForeignKey(
                        name: "FK_StationQuestion_QuestionCatalogue",
                        column: x => x.QuestionCatalogueIdent,
                        principalSchema: "SafetyCheck",
                        principalTable: "QuestionCatalogue",
                        principalColumn: "Ident");
                    table.ForeignKey(
                        name: "FK_StationQuestion_Station",
                        column: x => x.StationIdent,
                        principalSchema: "SafetyCheck",
                        principalTable: "Station",
                        principalColumn: "Ident");
                });

            migrationBuilder.CreateTable(
                name: "Training",
                schema: "SafetyCheck",
                columns: table => new
                {
                    UpdateTimeStamp = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    Ident = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StationIdent = table.Column<int>(type: "int", nullable: false),
                    CorrectSetupImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CommonErrorsImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InsertTimeStamp = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    InsertedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValueSql: "(suser_name())"),
                    UpdatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValueSql: "(suser_name())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Training", x => x.UpdateTimeStamp);
                    table.ForeignKey(
                        name: "FK_Training_Station",
                        column: x => x.StationIdent,
                        principalSchema: "SafetyCheck",
                        principalTable: "Station",
                        principalColumn: "Ident");
                });

            migrationBuilder.CreateTable(
                name: "InspectionQuestionResult",
                schema: "SafetyCheck",
                columns: table => new
                {
                    Ident = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InspectionIdent = table.Column<int>(type: "int", nullable: false),
                    StationQuestionIdent = table.Column<int>(type: "int", nullable: false),
                    QuestionText = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ErrorType = table.Column<int>(type: "int", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    InsertTimeStamp = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    InsertedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValueSql: "(suser_name())"),
                    UpdatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValueSql: "(suser_name())"),
                    UpdateTimeStamp = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_List", x => x.Ident);
                    table.ForeignKey(
                        name: "FK_List_Inspection",
                        column: x => x.InspectionIdent,
                        principalSchema: "SafetyCheck",
                        principalTable: "Inspection",
                        principalColumn: "Ident");
                    table.ForeignKey(
                        name: "FK_List_StationQuestion",
                        column: x => x.StationQuestionIdent,
                        principalSchema: "SafetyCheck",
                        principalTable: "StationQuestion",
                        principalColumn: "Ident");
                });

            migrationBuilder.CreateIndex(
                name: "IX_InspectionQuestionResult_InspectionIdent",
                schema: "SafetyCheck",
                table: "InspectionQuestionResult",
                column: "InspectionIdent");

            migrationBuilder.CreateIndex(
                name: "IX_InspectionQuestionResult_StationQuestionIdent",
                schema: "SafetyCheck",
                table: "InspectionQuestionResult",
                column: "StationQuestionIdent");

            migrationBuilder.CreateIndex(
                name: "IX_StationQuestion_QuestionCatalogueIdent",
                schema: "SafetyCheck",
                table: "StationQuestion",
                column: "QuestionCatalogueIdent");

            migrationBuilder.CreateIndex(
                name: "IX_StationQuestion_StationIdent",
                schema: "SafetyCheck",
                table: "StationQuestion",
                column: "StationIdent");

            migrationBuilder.CreateIndex(
                name: "IX_Training_StationIdent",
                schema: "SafetyCheck",
                table: "Training",
                column: "StationIdent");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employee",
                schema: "SafetyCheck");

            migrationBuilder.DropTable(
                name: "InspectionQuestionResult",
                schema: "SafetyCheck");

            migrationBuilder.DropTable(
                name: "Position",
                schema: "SafetyCheck");

            migrationBuilder.DropTable(
                name: "Training",
                schema: "SafetyCheck");

            migrationBuilder.DropTable(
                name: "Inspection",
                schema: "SafetyCheck");

            migrationBuilder.DropTable(
                name: "StationQuestion",
                schema: "SafetyCheck");

            migrationBuilder.DropTable(
                name: "QuestionCatalogue",
                schema: "SafetyCheck");

            migrationBuilder.DropTable(
                name: "Station",
                schema: "SafetyCheck");
        }
    }
}
