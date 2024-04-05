using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IndoorPlaygroundSafetyCheck.Migrations
{
    /// <inheritdoc />
    public partial class AddQuestionSaveProcedure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var procedure = @"
            IF OBJECT_ID('SafetyCheck.fsp_QuestionSave', 'P') IS NOT NULL
                DROP PROCEDURE SafetyCheck.fsp_QuestionSave;
            GO

            CREATE PROCEDURE SafetyCheck.fsp_QuestionSave
                @Ident INT = NULL,
                @Description NVARCHAR(255),
                @IsDelete BIT = 0
            AS
            BEGIN
                IF @IsDelete = 1
                BEGIN
                    IF @Ident IS NOT NULL
                    BEGIN
                        DELETE FROM [SafetyCheck].[QuestionCatalogue]
                        WHERE Ident = @Ident;
                    END
                END
                ELSE
                BEGIN
                    IF @Ident IS NOT NULL
                    BEGIN
                        UPDATE [SafetyCheck].[QuestionCatalogue]
                        SET Description = @Description,
                            UpdateTimeStamp = GETDATE(),
                            UpdatedBy = SUSER_NAME()
                        WHERE Ident = @Ident;
                    END
                    ELSE
                    BEGIN
                        INSERT INTO [SafetyCheck].[QuestionCatalogue] (Description, InsertTimeStamp, InsertedBy, UpdatedBy, UpdateTimeStamp)
                        VALUES (@Description, GETDATE(), SUSER_NAME(), SUSER_NAME(), GETDATE());
                    END
                END
            END;";

            migrationBuilder.Sql(procedure.Replace("GO", ""));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS SafetyCheck.fsp_QuestionSave");
        }
    }
}
