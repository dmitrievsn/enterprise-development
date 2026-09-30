using FitnessClub.Application.Contracts;
using FitnessClub.Application.Contracts.Clients;
using FitnessClub.Application.Contracts.PersonalTrainingSessions;
using FitnessClub.Application.Contracts.Trainers;
using Microsoft.AspNetCore.Mvc;

namespace FitnessClub.Api.Host.Controllers;

/// <summary>
/// Контроллер для аналитических запросов
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class AnalyticsController(
    IAnalyticsService service,
    ILogger<AnalyticsController> logger)
    : ControllerBase
{
    /// <summary>
    /// Получает информацию о всех тренерах, стаж работы которых не менее 5 лет
    /// </summary>
    [HttpGet("experienced-trainers")]
    public async Task<ActionResult<IList<TrainerDto>>> GetTrainersWithFiveOrMoreYearsOfExperience()
    {
        logger.LogInformation("Получение тренеров со стажем не менее 5 лет");

        var result = await service.GetTrainersWithFiveOrMoreYearsOfExperience();

        logger.LogInformation("Получено тренеров со стажем не менее 5 лет: {Count}",result.Count);

        return Ok(result);
    }

    /// <summary>
    /// Проверяет, является ли зал доступным для записи в данный момент
    /// </summary>
    [HttpGet("hall-availability")]
    public async Task<ActionResult<bool>> CheckHallAvailability(
        [FromQuery] string hallName,
        [FromQuery] DateTime checkTime)
    {
        logger.LogInformation(
            "Проверка, является ли зал {HallName} доступным для записи в {CheckTime}",
            hallName,
            checkTime);

        var result = await service.CheckHallAvailability(hallName,checkTime);

        logger.LogInformation(
            "Доступность зала {HallName} на {CheckTime}: {IsAvailable}",
            hallName,
            checkTime,
            result);

        return Ok(result);
    }

    /// <summary>
    /// Выводит информацию о клиентах, у которых просрочен абонемент, упорядочена по ФИО
    /// </summary>
    [HttpGet("expired-subscriptions")]
    public async Task<ActionResult<IList<ClientDto>>> GetClientsWithExpiredSubscription()
    {
        logger.LogInformation("Получение клиентов с просроченным абонементом");

        var result = await service.GetClientsWithExpiredSubscription();

        logger.LogInformation("Получено клиентов с просроченным абонементом: {Count}",result.Count);

        return Ok(result);
    }

    /// <summary>
    /// Выводит информацию о занятиях за текущий месяц, проходящих в выбранном зале
    /// </summary>
    [HttpGet("current-month-sessions")]
    public async Task<ActionResult<IList<PersonalTrainingSessionDto>>> GetSessionsForCurrentMonthInSelectedHall(
        [FromQuery] string hallName)
    {
        logger.LogInformation("Получение информации о занятиях за текущий месяц, проходящих в зале {HallName}",hallName);

        var result = await service.GetSessionsForCurrentMonthInSelectedHall(hallName);

        logger.LogInformation("Получено занятий за текущий месяц в зале {HallName}: {Count}",hallName,result.Count);

        return Ok(result);
    }

    /// <summary>
    /// Выводит топ 5 наиболее популярных тренеров
    /// </summary>
    [HttpGet("top-trainers")]
    public async Task<ActionResult<IList<TrainerDto>>> GetTopFivePopularTrainers()
    {
        logger.LogInformation("Получение топ 5 наиболее популярных тренеров");

        var result = await service.GetTopFivePopularTrainers();

        logger.LogInformation("Получено популярных тренеров: {Count}",result.Count);

        return Ok(result);
    }
}