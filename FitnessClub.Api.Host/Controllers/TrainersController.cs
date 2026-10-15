using FitnessClub.Application.Contracts.PersonalTrainingSessions;
using FitnessClub.Application.Contracts.Trainers;
using Microsoft.AspNetCore.Mvc;

namespace FitnessClub.Api.Host.Controllers;

/// <summary>
/// Контроллер для работы с тренерами
/// </summary>
public class TrainersController(
    ITrainerService service,
    ILogger<TrainersController> logger)
    : CrudControllerBase<
        TrainerDto,
        TrainerCreateUpdateDto,
        int>(service, logger)
{
    /// <summary>
    /// Получить персональные тренировки тренера
    /// </summary>
    [HttpGet("{id}/sessions")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<IList<PersonalTrainingSessionDto>> GetPersonalTrainingSessions(int id)
    {
        try
        {
            logger.LogInformation("Получение тренировок тренера с Id {Id}",id);
            var result = service.GetPersonalTrainingSessions(id);
            return Ok(result);
        }
        catch (KeyNotFoundException exception)
        {
            logger.LogWarning(exception,"Тренер с Id {Id} не найден",id);
            return NotFound();
        }
        catch (Exception exception)
        {
            logger.LogError(exception,"Ошибка при получении тренировок тренера с Id {Id}",id);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}