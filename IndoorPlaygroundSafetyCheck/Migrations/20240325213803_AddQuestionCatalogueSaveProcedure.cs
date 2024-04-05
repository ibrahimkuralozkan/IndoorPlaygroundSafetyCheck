using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IndoorPlaygroundSafetyCheck.Migrations
{
    /// <inheritdoc />
    public partial class AddQuestionCatalogueSaveProcedure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var procedure = @"
            IF OBJECT_ID('SafetyCheck.fsp_QuestionCatalogueSave', 'P') IS NOT NULL
                DROP PROCEDURE SafetyCheck.fsp_QuestionCatalogueSave;
            GO

            CREATE PROCEDURE [SafetyCheck].[fsp_QuestionCatalogueSave]
                @Action NVARCHAR(10),
                @Ident INT = NULL,
                @Description NVARCHAR(255) = NULL
            AS
            BEGIN
                -- Procedure implementation
            END;
        ";

            migrationBuilder.Sql(procedure.Replace("GO", ""));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS SafetyCheck.fsp_QuestionCatalogueSave");
        }
    }
}
