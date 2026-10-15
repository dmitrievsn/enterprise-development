using FitnessClub.Application.Contracts.Specializations;
using FitnessClub.Application.Contracts.Trainers;
using Microsoft.AspNetCore.Mvc;

namespace FitnessClub.Api.Host.Controllers;

/// <summary>
/// Контроллер для работы со специализациями
/// </summary>
public class SpecializationsController(
    ISpecializationService service,
    ILogger<SpecializationsController> logger)
    : CrudControllerBase<
        SpecializationDto,
        SpecializationCreateUpdateDto,
        int>(service, logger)
{
    /// <summary>
    /// Получить тренеров выбранной специализации
    /// </summary>
    [HttpGet("{id}/trainers")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<IList<TrainerDto>> GetTrainers(int id)
    {
        try
        {
            logger.LogInformation("Получение тренеров специализации с Id {Id}",id);
            var result = service.GetTrainers(id);
            return Ok(result);
        }
        catch (KeyNotFoundException exception)
        {
            logger.LogWarning(exception,"Специализация с Id {Id} не найдена",id);
            return NotFound();
        }
        catch (Exception exception)
        {
            logger.LogError(exception,"Ошибка при получении тренеров специализации с Id {Id}",id);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}