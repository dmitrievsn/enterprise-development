using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitnessClub.Infrastructure.Ef.Migrations;

/// <inheritdoc />
public partial class InitialCreate : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Clients",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                PhoneNumber = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                StartSub = table.Column<DateOnly>(type: "date", nullable: false),
                EndSub = table.Column<DateOnly>(type: "date", nullable: false),
                PassportNumber = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                Patronymic = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                Gender = table.Column<int>(type: "int", nullable: false),
                DateOfBirth = table.Column<DateOnly>(type: "date", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Clients", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Specializations",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Specializations", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Trainers",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                SpecializationId = table.Column<int>(type: "int", nullable: false),
                WorkYears = table.Column<int>(type: "int", nullable: false),
                PassportNumber = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                Patronymic = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                Gender = table.Column<int>(type: "int", nullable: false),
                DateOfBirth = table.Column<DateOnly>(type: "date", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Trainers", x => x.Id);
                table.ForeignKey(
                    name: "FK_Trainers_Specializations_SpecializationId",
                    column: x => x.SpecializationId,
                    principalTable: "Specializations",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
            });

        migrationBuilder.CreateTable(
            name: "PersonalTrainingSessions",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                ClientId = table.Column<int>(type: "int", nullable: false),
                TrainerId = table.Column<int>(type: "int", nullable: false),
                TrainDay = table.Column<DateTime>(type: "datetime2", nullable: false),
                HallName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                IsTrial = table.Column<bool>(type: "bit", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_PersonalTrainingSessions", x => x.Id);
                table.ForeignKey(
                    name: "FK_PersonalTrainingSessions_Clients_ClientId",
                    column: x => x.ClientId,
                    principalTable: "Clients",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
                table.ForeignKey(
                    name: "FK_PersonalTrainingSessions_Trainers_TrainerId",
                    column: x => x.TrainerId,
                    principalTable: "Trainers",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
            });

        migrationBuilder.CreateIndex(
            name: "IX_PersonalTrainingSessions_ClientId",
            table: "PersonalTrainingSessions",
            column: "ClientId");

        migrationBuilder.CreateIndex(
            name: "IX_PersonalTrainingSessions_TrainerId",
            table: "PersonalTrainingSessions",
            column: "TrainerId");

        migrationBuilder.CreateIndex(
            name: "IX_Trainers_SpecializationId",
            table: "Trainers",
            column: "SpecializationId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "PersonalTrainingSessions");

        migrationBuilder.DropTable(
            name: "Clients");

        migrationBuilder.DropTable(
            name: "Trainers");

        migrationBuilder.DropTable(
            name: "Specializations");
    }
}
