using FitnessClub.Domain.Entities;
using FitnessClub.Domain.Shared.Enums;

namespace FitnessClub.Domain.Data;

/// <summary>
/// Тестовые данные фитнес-клуба
/// </summary>
public static class FitnessClubData
{
    private static readonly DateOnly _today =
        DateOnly.FromDateTime(DateTime.Today);

    /// <summary>
    /// Список специализаций тренеров
    /// </summary>
    public static List<Specialization> Specializations { get; } =
    [
        new Specialization
        {
            Id = 1,
            Name = "Силовые тренировки"
        },
        new Specialization
        {
            Id = 2,
            Name = "Кардиотренировки"
        },
        new Specialization
        {
            Id = 3,
            Name = "Функциональный тренинг"
        },
        new Specialization
        {
            Id = 4,
            Name = "Пилатес"
        },
        new Specialization
        {
            Id = 5,
            Name = "Бокс"
        },
        new Specialization
        {
            Id = 6,
            Name = "Кроссфит"
        },
        new Specialization
        {
            Id = 7,
            Name = "Йога"
        },
        new Specialization
        {
            Id = 8,
            Name = "Калистеника"
        },
        new Specialization
        {
            Id = 9,
            Name = "Пауэрлифтинг"
        },
        new Specialization
        {
            Id = 10,
            Name = "Воркаут"
        }
    ];

    /// <summary>
    /// Список клиентов
    /// </summary>
    public static List<Client> Clients { get; } =
    [
        new Client
        {
            Id = 1,
            PassportNumber = "1111111111",
            LastName = "Иванов",
            FirstName = "Иван",
            Patronymic = "Иванович",
            Gender = Gender.Male,
            DateOfBirth = new DateOnly(2000, 1, 10),
            PhoneNumber = "+79000000001",
            StartSub = _today.AddMonths(-12),
            EndSub = _today.AddMonths(-1)
        },
        new Client
        {
            Id = 2,
            PassportNumber = "2222222222",
            LastName = "Петров",
            FirstName = "Петр",
            Patronymic = "Петрович",
            Gender = Gender.Male,
            DateOfBirth = new DateOnly(1999, 2, 15),
            PhoneNumber = "+79000000002",
            StartSub = _today.AddMonths(-8),
            EndSub = _today.AddDays(-10)
        },
        new Client
        {
            Id = 3,
            PassportNumber = "3333333333",
            LastName = "Монголова",
            FirstName = "Анна",
            Patronymic = "Олеговна",
            Gender = Gender.Female,
            DateOfBirth = new DateOnly(2001, 3, 20),
            PhoneNumber = "+79000000003",
            StartSub = _today.AddMonths(-6),
            EndSub = _today.AddDays(-5)
        },
        new Client
        {
            Id = 4,
            PassportNumber = "4444444444",
            LastName = "Пупкин",
            FirstName = "Василий",
            Patronymic = "Павлович",
            Gender = Gender.Male,
            DateOfBirth = new DateOnly(1998, 4, 25),
            PhoneNumber = "+79000000004",
            StartSub = _today.AddMonths(-4),
            EndSub = _today.AddDays(-1)
        },
        new Client
        {
            Id = 5,
            PassportNumber = "5555555555",
            LastName = "Заплуткин",
            FirstName = "Дмитрий",
            Patronymic = null,
            Gender = Gender.Male,
            DateOfBirth = new DateOnly(2002, 5, 5),
            PhoneNumber = "+79000000005",
            StartSub = _today.AddMonths(-2),
            EndSub = _today.AddMonths(4)
        },
        new Client
        {
            Id = 6,
            PassportNumber = "6666666666",
            LastName = "Черноголовкин",
            FirstName = "Дмитрий",
            Patronymic = "Андреевич",
            Gender = Gender.Male,
            DateOfBirth = new DateOnly(1997, 6, 12),
            PhoneNumber = "+79000000006",
            StartSub = _today.AddMonths(-1),
            EndSub = _today.AddMonths(5)
        },
        new Client
        {
            Id = 7,
            PassportNumber = "7777777777",
            LastName = "Волкова",
            FirstName = "Елена",
            Patronymic = "Игоревна",
            Gender = Gender.Female,
            DateOfBirth = new DateOnly(2000, 7, 18),
            PhoneNumber = "+79000000007",
            StartSub = _today.AddMonths(-3),
            EndSub = _today.AddMonths(3)
        },
        new Client
        {
            Id = 8,
            PassportNumber = "8888888888",
            LastName = "Соколов",
            FirstName = "Максим",
            Patronymic = "Алексеевич",
            Gender = Gender.Male,
            DateOfBirth = new DateOnly(2003, 8, 23),
            PhoneNumber = "+79000000008",
            StartSub = _today.AddMonths(-2),
            EndSub = _today.AddMonths(2)
        },
        new Client
        {
            Id = 9,
            PassportNumber = "9999999999",
            LastName = "Морозова",
            FirstName = "Ольга",
            Patronymic = "Викторовна",
            Gender = Gender.Female,
            DateOfBirth = new DateOnly(1996, 9, 30),
            PhoneNumber = "+79000000009",
            StartSub = _today.AddMonths(-1),
            EndSub = _today.AddMonths(8)
        },
        new Client
        {
            Id = 10,
            PassportNumber = "1010101010",
            LastName = "Лебедев",
            FirstName = "Артем",
            Patronymic = null,
            Gender = Gender.Male,
            DateOfBirth = new DateOnly(2001, 10, 8),
            PhoneNumber = "+79000000010",
            StartSub = _today,
            EndSub = _today.AddMonths(6)
        }
    ];

    /// <summary>
    /// Список тренеров
    /// </summary>
    public static List<Trainer> Trainers { get; } =
    [
        new Trainer
        {
            Id = 1,
            PassportNumber = "2000000001",
            LastName = "Орлов",
            FirstName = "Александр",
            Patronymic = "Игоревич",
            Gender = Gender.Male,
            DateOfBirth = new DateOnly(1990, 1, 15),
            Specialization = Specializations[0],
            WorkYears = 3
        },
        new Trainer
        {
            Id = 2,
            PassportNumber = "2000000002",
            LastName = "Федоров",
            FirstName = "Михаил",
            Patronymic = "Сергеевич",
            Gender = Gender.Male,
            DateOfBirth = new DateOnly(1988, 2, 20),
            Specialization = Specializations[1],
            WorkYears = 4
        },
        new Trainer
        {
            Id = 3,
            PassportNumber = "2000000003",
            LastName = "Крылова",
            FirstName = "Анна",
            Patronymic = "Олеговна",
            Gender = Gender.Female,
            DateOfBirth = new DateOnly(1994, 3, 10),
            Specialization = Specializations[2],
            WorkYears = 5
        },
        new Trainer
        {
            Id = 4,
            PassportNumber = "2000000004",
            LastName = "Семенов",
            FirstName = "Илья",
            Patronymic = "Павлович",
            Gender = Gender.Male,
            DateOfBirth = new DateOnly(1989, 4, 5),
            Specialization = Specializations[3],
            WorkYears = 6
        },
        new Trainer
        {
            Id = 5,
            PassportNumber = "2000000005",
            LastName = "Белова",
            FirstName = "Елена",
            Patronymic = "Андреевна",
            Gender = Gender.Female,
            DateOfBirth = new DateOnly(1992, 5, 18),
            Specialization = Specializations[4],
            WorkYears = 7
        },
        new Trainer
        {
            Id = 6,
            PassportNumber = "2000000006",
            LastName = "Макаров",
            FirstName = "Денис",
            Patronymic = "Викторович",
            Gender = Gender.Male,
            DateOfBirth = new DateOnly(1987, 6, 25),
            Specialization = Specializations[5],
            WorkYears = 8
        },
        new Trainer
        {
            Id = 7,
            PassportNumber = "2000000007",
            LastName = "Зайцева",
            FirstName = "Мария",
            Patronymic = "Ивановна",
            Gender = Gender.Female,
            DateOfBirth = new DateOnly(1993, 7, 12),
            Specialization = Specializations[6],
            WorkYears = 9
        },
        new Trainer
        {
            Id = 8,
            PassportNumber = "2000000008",
            LastName = "Власов",
            FirstName = "Роман",
            Patronymic = "Алексеевич",
            Gender = Gender.Male,
            DateOfBirth = new DateOnly(1986, 8, 30),
            Specialization = Specializations[7],
            WorkYears = 10
        },
        new Trainer
        {
            Id = 9,
            PassportNumber = "2000000009",
            LastName = "Никитина",
            FirstName = "Ольга",
            Patronymic = "Сергеевна",
            Gender = Gender.Female,
            DateOfBirth = new DateOnly(1991, 9, 14),
            Specialization = Specializations[8],
            WorkYears = 12
        },
        new Trainer
        {
            Id = 10,
            PassportNumber = "2000000010",
            LastName = "Громов",
            FirstName = "Артем",
            Patronymic = null,
            Gender = Gender.Male,
            DateOfBirth = new DateOnly(1985, 10, 7),
            Specialization = Specializations[9],
            WorkYears = 15
        }
    ];

    /// <summary>
    /// Список персональных тренеровок
    /// </summary>
    public static List<PersonalTrainingSession> PersonalTrainingSessions { get; } =
    [
        new()
        {
            Id = 1,
            Client = Clients[0],
            Trainer = Trainers[0],
            TrainDay = GetSessionDate(0, 2, 10),
            HallName = "Зал 1",
            IsTrial = true
        },
        new()
        {
            Id = 2,
            Client = Clients[1],
            Trainer = Trainers[0],
            TrainDay = GetSessionDate(0, 4, 12),
            HallName = "Зал 1",
            IsTrial = false
        },
        new()
        {
            Id = 3,
            Client = Clients[2],
            Trainer = Trainers[0],
            TrainDay = GetSessionDate(0, 6, 14),
            HallName = "Зал 2",
            IsTrial = false
        },
        new()
        {
            Id = 4,
            Client = Clients[3],
            Trainer = Trainers[0],
            TrainDay = GetSessionDate(-1, 8, 16),
            HallName = "Зал 1",
            IsTrial = false
        },
        new()
        {
            Id = 5,
            Client = Clients[4],
            Trainer = Trainers[0],
            TrainDay = GetSessionDate(-1, 10, 18),
            HallName = "Зал 2",
            IsTrial = false
        },
        new()
        {
            Id = 6,
            Client = Clients[5],
            Trainer = Trainers[0],
            TrainDay = GetSessionDate(1, 12, 10),
            HallName = "Зал 3",
            IsTrial = false
        },

        new()
        {
            Id = 7,
            Client = Clients[0],
            Trainer = Trainers[1],
            TrainDay = GetSessionDate(0, 8, 10),
            HallName = "Зал 1",
            IsTrial = false
        },
        new()
        {
            Id = 8,
            Client = Clients[1],
            Trainer = Trainers[1],
            TrainDay = GetSessionDate(0, 10, 12),
            HallName = "Зал 2",
            IsTrial = true
        },
        new()
        {
            Id = 9,
            Client = Clients[2],
            Trainer = Trainers[1],
            TrainDay = GetSessionDate(0, 12, 14),
            HallName = "Зал 1",
            IsTrial = false
        },
        new()
        {
            Id = 10,
            Client = Clients[3],
            Trainer = Trainers[1],
            TrainDay = GetSessionDate(-1, 14, 16),
            HallName = "Зал 2",
            IsTrial = false
        },
        new()
        {
            Id = 11,
            Client = Clients[4],
            Trainer = Trainers[1],
            TrainDay = GetSessionDate(1, 16, 18),
            HallName = "Зал 3",
            IsTrial = false
        },

        new()
        {
            Id = 12,
            Client = Clients[5],
            Trainer = Trainers[2],
            TrainDay = GetSessionDate(0, 14, 10),
            HallName = "Зал 1",
            IsTrial = false
        },
        new()
        {
            Id = 13,
            Client = Clients[6],
            Trainer = Trainers[2],
            TrainDay = GetSessionDate(0, 16, 12),
            HallName = "Зал 2",
            IsTrial = false
        },
        new()
        {
            Id = 14,
            Client = Clients[7],
            Trainer = Trainers[2],
            TrainDay = GetSessionDate(0, 18, 14),
            HallName = "Зал 1",
            IsTrial = true
        },
        new()
        {
            Id = 15,
            Client = Clients[8],
            Trainer = Trainers[2],
            TrainDay = GetSessionDate(-1, 20, 16),
            HallName = "Зал 3",
            IsTrial = false
        },

        new()
        {
            Id = 16,
            Client = Clients[9],
            Trainer = Trainers[3],
            TrainDay = GetSessionDate(0, 20, 10),
            HallName = "Зал 1",
            IsTrial = false
        },
        new()
        {
            Id = 17,
            Client = Clients[0],
            Trainer = Trainers[3],
            TrainDay = GetSessionDate(0, 22, 12),
            HallName = "Зал 2",
            IsTrial = false
        },
        new()
        {
            Id = 18,
            Client = Clients[1],
            Trainer = Trainers[3],
            TrainDay = GetSessionDate(1, 24, 14),
            HallName = "Зал 3",
            IsTrial = false
        },

        new()
        {
            Id = 19,
            Client = Clients[2],
            Trainer = Trainers[4],
            TrainDay = GetSessionDate(0, 24, 16),
            HallName = "Зал 1",
            IsTrial = false
        },
        new()
        {
            Id = 20,
            Client = Clients[3],
            Trainer = Trainers[4],
            TrainDay = GetSessionDate(0, 26, 18),
            HallName = "Зал 2",
            IsTrial = true
        },

        new()
        {
            Id = 21,
            Client = Clients[4],
            Trainer = Trainers[5],
            TrainDay = GetSessionDate(0, 5, 18),
            HallName = "Зал 3",
            IsTrial = false
        },
        new()
        {
            Id = 22,
            Client = Clients[5],
            Trainer = Trainers[6],
            TrainDay = GetSessionDate(0, 7, 18),
            HallName = "Зал 2",
            IsTrial = false
        },
        new()
        {
            Id = 23,
            Client = Clients[6],
            Trainer = Trainers[7],
            TrainDay = GetSessionDate(-1, 9, 18),
            HallName = "Зал 3",
            IsTrial = false
        },
        new()
        {
            Id = 24,
            Client = Clients[7],
            Trainer = Trainers[8],
            TrainDay = GetSessionDate(1, 11, 18),
            HallName = "Зал 1",
            IsTrial = false
        },
        new()
        {
            Id = 25,
            Client = Clients[8],
            Trainer = Trainers[9],
            TrainDay = GetSessionDate(1, 13, 18),
            HallName = "Зал 2",
            IsTrial = true
        }
    ];

    private static DateTime GetSessionDate(
        int monthOffset,
        int day,
        int hour)
    {
        var month = DateTime.Today.AddMonths(monthOffset);

        return new DateTime(
            month.Year,
            month.Month,
            day,
            hour,
            0,
            0);
    }
}