using FitnessClub.Application.Contracts.Specializations;
using FitnessClub.Application.Contracts.Trainers;
using Microsoft.AspNetCore.Mvc;

namespace FitnessClub.Api.Host.Controllers;

/// <summary>
/// Контроллер для работы со специализациями
/// </summary>
[ApiController]
[Route("api/[controller]")]
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
    public async Task<ActionResult<IList<TrainerDto>>> GetTrainers(int id)
    {
        logger.LogInformation("Получение тренеров специализации с Id {Id}",id);

        try
        {
            var result = await service.GetTrainers(id);

            return Ok(result);
        }
        catch (KeyNotFoundException)
        {
            logger.LogWarning("Специализация с Id {Id} не найдена",id);

            return NotFound();
        }
    }
}