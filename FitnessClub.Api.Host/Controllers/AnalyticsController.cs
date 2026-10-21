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
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<IList<TrainerDto>> GetTrainersWithFiveOrMoreYearsOfExperience()
    {
        try
        {
            logger.LogInformation("Получение тренеров со стажем не менее 5 лет");
            var result = service.GetTrainersWithFiveOrMoreYearsOfExperience();
            logger.LogInformation("Получено тренеров со стажем не менее 5 лет: {Count}",result.Count);
            return Ok(result);
        }
        catch (Exception exception)
        {
            logger.LogError(exception,"Ошибка при получении тренеров со стажем не менее 5 лет");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    /// <summary>
    /// Проверяет, является ли зал доступным для записи в данный момент
    /// </summary>
    [HttpGet("hall-availability")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<bool> CheckHallAvailability(
        [FromQuery] string hallName,
        [FromQuery] DateTime checkTime)
    {
        try
        {
            logger.LogInformation("Проверка, является ли зал {HallName} доступным для записи в {CheckTime}",hallName,checkTime);
            var result = service.CheckHallAvailability(hallName,checkTime);
            logger.LogInformation("Доступность зала {HallName} на {CheckTime}: {IsAvailable}",hallName,checkTime,result);
            return Ok(result);
        }
        catch (ArgumentException exception)
        {
            logger.LogWarning(exception,"Некорректные данные при проверке доступности зала");
            return BadRequest(exception.Message);
        }
        catch (Exception exception)
        {
            logger.LogError(exception,"Ошибка при проверке доступности зала {HallName} на {CheckTime}",hallName,checkTime);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    /// <summary>
    /// Выводит информацию о клиентах, у которых просрочен абонемент, упорядочена по ФИО
    /// </summary>
    [HttpGet("expired-subscriptions")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<IList<ClientDto>> GetClientsWithExpiredSubscription()
    {
        try
        {
            logger.LogInformation("Получение клиентов с просроченным абонементом");
            var result = service.GetClientsWithExpiredSubscription();
            logger.LogInformation("Получено клиентов с просроченным абонементом: {Count}",result.Count);
            return Ok(result);
        }
        catch (Exception exception)
        {
            logger.LogError(exception,"Ошибка при получении клиентов с просроченным абонементом");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    /// <summary>
    /// Выводит информацию о занятиях за текущий месяц, проходящих в выбранном зале
    /// </summary>
    [HttpGet("current-month-sessions")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<IList<PersonalTrainingSessionDto>> GetSessionsForCurrentMonthInSelectedHall(
        [FromQuery] string hallName)
    {
        try
        {
            logger.LogInformation("Получение информации о занятиях за текущий месяц, проходящих в зале {HallName}",hallName);
            var result = service.GetSessionsForCurrentMonthInSelectedHall(hallName);
            logger.LogInformation("Получено занятий за текущий месяц в зале {HallName}: {Count}",hallName,result.Count);
            return Ok(result);
        }
        catch (Exception exception)
        {
            logger.LogError(exception,"Ошибка при получении занятий за текущий месяц в зале {HallName}",hallName);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    /// <summary>
    /// Выводит топ 5 наиболее популярных тренеров
    /// </summary>
    [HttpGet("top-trainers")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<IList<TrainerDto>> GetTopFivePopularTrainers()
    {
        try
        {
            logger.LogInformation("Получение топ 5 наиболее популярных тренеров");
            var result = service.GetTopFivePopularTrainers();
            logger.LogInformation("Получено популярных тренеров: {Count}",result.Count);
            return Ok(result);
        }
        catch (Exception exception)
        {
            logger.LogError(exception,"Ошибка при получении топ 5 наиболее популярных тренеров");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}