namespace FitnessClub.Tests;

using FitnessClub.Domain.Data;

/// <summary>
/// Юнит-тесты для фитнес-клуба
/// </summary>
public class QueriesTests
{
    /// <summary>
    /// 1. Вывести информацию о всех тренерах, стаж работы которых не менее 5 лет. 
    /// </summary>
    [Fact]
    public void GetTrainersWithFiveOrMoreYearsOfExperience()
    {
        var result = FitnessClubTestData.Trainers
            .Where(trainer => trainer.WorkYears >= 5)
            .ToList();
        Assert.Equal(8, result.Count);
    }
    /// <summary>
    /// 2. Проверить, является ли зал доступным для записи в данный момент. 
    /// </summary>
    [Fact]
    public void CheckHallAvailability()
    {
        var hallName = "Зал 1";
        var checkTime = FitnessClubTestData.PersonalTrainingSessions[0].TrainDay;

        var isAvailable = !FitnessClubTestData.PersonalTrainingSessions
            .Any(session =>
                session.HallName == hallName &&
                session.TrainDay == checkTime);

        Assert.False(isAvailable);
    }
    /// <summary>
    /// 3. Вывести информацию о клиентах, у которых просрочен абонемент, упорядочить по ФИО. 
    /// </summary>
    [Fact]
    public void GetClientsWithExpiredSubscription()
    {
        var today = DateOnly.FromDateTime(DateTime.Today);

        var result = FitnessClubTestData.Clients
            .Where(client => client.EndSub < today)
            .OrderBy(client => client.LastName)
            .ThenBy(client => client.FirstName)
            .ThenBy(client => client.Patronymic)
            .ToList();

        Assert.Equal(4, result.Count);
    }
    /// <summary>
    /// 4. Вывести информацию о занятиях за текущий месяц, проходящих в выбранном зале.
    /// </summary>
    [Fact]
    public void GetSessionsForCurrentMonthInSelectedHall()
    {
        var hallName = "Зал 1";
        var today = DateTime.Today;

        var result = FitnessClubTestData.PersonalTrainingSessions
            .Where(session =>
                session.HallName == hallName &&
                session.TrainDay.Year == today.Year &&
                session.TrainDay.Month == today.Month)
            .ToList();

        Assert.Equal(8, result.Count);
    }
    /// <summary>
    /// 5. Вывести топ 5 наиболее популярных тренеров.
    /// </summary>
    [Fact]
    public void GetTopFivePopularTrainers()
    {
        var result = FitnessClubTestData.PersonalTrainingSessions
            .GroupBy(session => session.Trainer.Id)
            .OrderByDescending(group => group.Count())
            .Take(5)
            .Select(group => group.Key)
            .ToList();

        var trainers = result
            .Select(id => FitnessClubTestData.Trainers
                .First(trainer => trainer.Id == id))
            .ToList();

        Assert.Equal(
            new[] { 1, 2, 3, 4, 5 },
            trainers.Select(trainer => trainer.Id)
        );
    }
}