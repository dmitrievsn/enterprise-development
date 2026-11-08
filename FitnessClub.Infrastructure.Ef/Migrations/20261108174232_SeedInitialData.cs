using FitnessClub.Domain.Shared.Enums;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitnessClub.Infrastructure.Ef.Migrations;

public partial class SeedInitialData : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.InsertData(
            table: "Specializations",
            columns: new[] { "Id", "Name" },
            values: new object[,]
            {
                { 1, "Силовые тренировки" },
                { 2, "Кардиотренировки" },
                { 3, "Функциональный тренинг" },
                { 4, "Пилатес" },
                { 5, "Бокс" },
                { 6, "Кроссфит" },
                { 7, "Йога" },
                { 8, "Калистеника" },
                { 9, "Пауэрлифтинг" },
                { 10, "Воркаут" }
            });
        migrationBuilder.InsertData(
            table: "Clients",
            columns: new[] { "Id", "PassportNumber", "LastName", "FirstName", "Patronymic", "Gender", "DateOfBirth", "PhoneNumber", "StartSub", "EndSub" },
            values: new object[,]
            {
                { 1, "1111111111", "Иванов", "Иван", "Иванович", (int)Gender.Male, new DateOnly(2000, 1, 10), "+79000000001", new DateOnly(2025, 11, 8), new DateOnly(2026, 10, 8) },
                { 2, "2222222222", "Петров", "Петр", "Петрович", (int)Gender.Male, new DateOnly(1999, 2, 15), "+79000000002", new DateOnly(2026, 3, 8), new DateOnly(2026, 10, 29) },
                { 3, "3333333333", "Монголова", "Анна", "Олеговна", (int)Gender.Female, new DateOnly(2001, 3, 20), "+79000000003", new DateOnly(2026, 5, 8), new DateOnly(2026, 11, 3) },
                { 4, "4444444444", "Пупкин", "Василий", "Павлович", (int)Gender.Male, new DateOnly(1998, 4, 25), "+79000000004", new DateOnly(2026, 7, 8), new DateOnly(2026, 11, 7) },
                { 5, "5555555555", "Заплуткин", "Дмитрий", null, (int)Gender.Male, new DateOnly(2002, 5, 5), "+79000000005", new DateOnly(2026, 9, 8), new DateOnly(2027, 3, 8) },
                { 6, "6666666666", "Черноголовкин", "Дмитрий", "Андреевич", (int)Gender.Male, new DateOnly(1997, 6, 12), "+79000000006", new DateOnly(2026, 10, 8), new DateOnly(2027, 4, 8) },
                { 7, "7777777777", "Волкова", "Елена", "Игоревна", (int)Gender.Female, new DateOnly(2000, 7, 18), "+79000000007", new DateOnly(2026, 8, 8), new DateOnly(2027, 2, 8) },
                { 8, "8888888888", "Соколов", "Максим", "Алексеевич", (int)Gender.Male, new DateOnly(2003, 8, 23), "+79000000008", new DateOnly(2026, 9, 8), new DateOnly(2027, 1, 8) },
                { 9, "9999999999", "Морозова", "Ольга", "Викторовна", (int)Gender.Female, new DateOnly(1996, 9, 30), "+79000000009", new DateOnly(2026, 10, 8), new DateOnly(2027, 7, 8) },
                { 10, "1010101010", "Лебедев", "Артем", null, (int)Gender.Male, new DateOnly(2001, 10, 8), "+79000000010", new DateOnly(2026, 11, 8), new DateOnly(2027, 5, 8) }
            });
        migrationBuilder.InsertData(
            table: "Trainers",
            columns: new[] { "Id", "PassportNumber", "LastName", "FirstName", "Patronymic", "Gender", "DateOfBirth", "SpecializationId", "WorkYears" },
            values: new object[,]
            {
                { 1, "2000000001", "Орлов", "Александр", "Игоревич", (int)Gender.Male, new DateOnly(1990, 1, 15), 1, 3 },
                { 2, "2000000002", "Федоров", "Михаил", "Сергеевич", (int)Gender.Male, new DateOnly(1988, 2, 20), 2, 4 },
                { 3, "2000000003", "Крылова", "Анна", "Олеговна", (int)Gender.Female, new DateOnly(1994, 3, 10), 3, 5 },
                { 4, "2000000004", "Семенов", "Илья", "Павлович", (int)Gender.Male, new DateOnly(1989, 4, 5), 4, 6 },
                { 5, "2000000005", "Белова", "Елена", "Андреевна", (int)Gender.Female, new DateOnly(1992, 5, 18), 5, 7 },
                { 6, "2000000006", "Макаров", "Денис", "Викторович", (int)Gender.Male, new DateOnly(1987, 6, 25), 6, 8 },
                { 7, "2000000007", "Зайцева", "Мария", "Ивановна", (int)Gender.Female, new DateOnly(1993, 7, 12), 7, 9 },
                { 8, "2000000008", "Власов", "Роман", "Алексеевич", (int)Gender.Male, new DateOnly(1986, 8, 30), 8, 10 },
                { 9, "2000000009", "Никитина", "Ольга", "Сергеевна", (int)Gender.Female, new DateOnly(1991, 9, 14), 9, 12 },
                { 10, "2000000010", "Громов", "Артем", null, (int)Gender.Male, new DateOnly(1985, 10, 7), 10, 15 }
            });
        migrationBuilder.InsertData(
            table: "PersonalTrainingSessions",
            columns: new[] { "Id", "ClientId", "TrainerId", "TrainDay", "HallName", "IsTrial" },
            values: new object[,]
            {
                { 1, 1, 1, new DateTime(2026, 11, 2, 10, 0, 0), "Зал 1", true },
                { 2, 2, 1, new DateTime(2026, 11, 4, 12, 0, 0), "Зал 1", false },
                { 3, 3, 1, new DateTime(2026, 11, 6, 14, 0, 0), "Зал 2", false },
                { 4, 4, 1, new DateTime(2026, 10, 8, 16, 0, 0), "Зал 1", false },
                { 5, 5, 1, new DateTime(2026, 10, 10, 18, 0, 0), "Зал 2", false },
                { 6, 6, 1, new DateTime(2026, 12, 12, 10, 0, 0), "Зал 3", false },
                { 7, 1, 2, new DateTime(2026, 11, 8, 10, 0, 0), "Зал 1", false },
                { 8, 2, 2, new DateTime(2026, 11, 10, 12, 0, 0), "Зал 2", true },
                { 9, 3, 2, new DateTime(2026, 11, 12, 14, 0, 0), "Зал 1", false },
                { 10, 4, 2, new DateTime(2026, 10, 14, 16, 0, 0), "Зал 2", false },
                { 11, 5, 2, new DateTime(2026, 12, 16, 18, 0, 0), "Зал 3", false },
                { 12, 6, 3, new DateTime(2026, 11, 14, 10, 0, 0), "Зал 1", false },
                { 13, 7, 3, new DateTime(2026, 11, 16, 12, 0, 0), "Зал 2", false },
                { 14, 8, 3, new DateTime(2026, 11, 18, 14, 0, 0), "Зал 1", true },
                { 15, 9, 3, new DateTime(2026, 10, 20, 16, 0, 0), "Зал 3", false },
                { 16, 10, 4, new DateTime(2026, 11, 20, 10, 0, 0), "Зал 1", false },
                { 17, 1, 4, new DateTime(2026, 11, 22, 12, 0, 0), "Зал 2", false },
                { 18, 2, 4, new DateTime(2026, 12, 24, 14, 0, 0), "Зал 3", false },
                { 19, 3, 5, new DateTime(2026, 11, 24, 16, 0, 0), "Зал 1", false },
                { 20, 4, 5, new DateTime(2026, 11, 26, 18, 0, 0), "Зал 2", true },
                { 21, 5, 6, new DateTime(2026, 11, 5, 18, 0, 0), "Зал 3", false },
                { 22, 6, 7, new DateTime(2026, 11, 7, 18, 0, 0), "Зал 2", false },
                { 23, 7, 8, new DateTime(2026, 10, 9, 18, 0, 0), "Зал 3", false },
                { 24, 8, 9, new DateTime(2026, 12, 11, 18, 0, 0), "Зал 1", false },
                { 25, 9, 10, new DateTime(2026, 12, 13, 18, 0, 0), "Зал 2", true }
            });
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DeleteData(
            table: "PersonalTrainingSessions",
            keyColumn: "Id",
            keyValues: new object[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25 });
        migrationBuilder.DeleteData(
            table: "Trainers",
            keyColumn: "Id",
            keyValues: new object[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 });
        migrationBuilder.DeleteData(
            table: "Clients",
            keyColumn: "Id",
            keyValues: new object[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 });
        migrationBuilder.DeleteData(
            table: "Specializations",
            keyColumn: "Id",
            keyValues: new object[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 });
    }
}
