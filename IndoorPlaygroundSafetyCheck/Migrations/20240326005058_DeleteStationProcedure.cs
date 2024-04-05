using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IndoorPlaygroundSafetyCheck.Migrations
{
    /// <inheritdoc />
    public partial class DeleteStationProcedure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
        CREATE OR ALTER PROCEDURE [SafetyCheck].[fsp_StationDelete]
            @p_sName NVARCHAR(255)
        AS
        BEGIN
            DECLARE @sMsg NVARCHAR(255);

            DELETE FROM [SafetyCheck].[Station]
            WHERE [Name] = @p_sName;

            IF @@ERROR != 0 OR @@ROWCOUNT != 1
            BEGIN
                SET @sMsg = 'Error deleting Station';
                RAISERROR(@sMsg, 16, 1);
            END
        END
    ");
        }



        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP PROCEDURE IF EXISTS [SafetyCheck].[fsp_StationDelete]");
        }


    }
}
