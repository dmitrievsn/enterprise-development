using FitnessClub.Application.Contracts.PersonalTrainingSessions;
using FitnessClub.Application.Contracts.Trainers;
using Microsoft.AspNetCore.Mvc;

namespace FitnessClub.Api.Host.Controllers;

/// <summary>
/// Контроллер для работы с тренерами
/// </summary>
[ApiController]
[Route("api/[controller]")]
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
    public async Task<ActionResult<IList<PersonalTrainingSessionDto>>> GetPersonalTrainingSessions(int id)
    {
        logger.LogInformation("Получение тренировок тренера с Id {Id}",id);

        try
        {
            var result = await service.GetPersonalTrainingSessions(id);

            return Ok(result);
        }
        catch (KeyNotFoundException)
        {
            logger.LogWarning("Тренер с Id {Id} не найден",id);

            return NotFound();
        }
    }
}