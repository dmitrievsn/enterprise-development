using FitnessClub.Application.Contracts.Clients;
using FitnessClub.Application.Contracts.PersonalTrainingSessions;
using Microsoft.AspNetCore.Mvc;

namespace FitnessClub.Api.Host.Controllers;

/// <summary>
/// Контроллер для работы с клиентами
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ClientsController(
    IClientService service,
    ILogger<ClientsController> logger)
    : CrudControllerBase<
        ClientDto,
        ClientCreateUpdateDto,
        int>(service, logger)
{
    /// <summary>
    /// Получить персональные тренировки клиента
    /// </summary>
    [HttpGet("{id}/sessions")]
    public async Task<ActionResult<IList<PersonalTrainingSessionDto>>> GetPersonalTrainingSessions(int id)
    {
        logger.LogInformation("Получение тренировок клиента с Id {Id}",id);

        try
        {
            var result = await service.GetPersonalTrainingSessions(id);

            return Ok(result);
        }
        catch (KeyNotFoundException)
        {
            logger.LogWarning("Клиент с Id {Id} не найден",id);

            return NotFound();
        }
    }
}