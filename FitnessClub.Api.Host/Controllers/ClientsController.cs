using FitnessClub.Application.Contracts.Clients;
using FitnessClub.Application.Contracts.PersonalTrainingSessions;
using Microsoft.AspNetCore.Mvc;

namespace FitnessClub.Api.Host.Controllers;

/// <summary>
/// Контроллер для работы с клиентами
/// </summary>
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
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<IList<PersonalTrainingSessionDto>> GetPersonalTrainingSessions(int id)
    {
        try
        {
            logger.LogInformation("Получение тренировок клиента с Id {Id}",id);
            var result = service.GetPersonalTrainingSessions(id);
            return Ok(result);
        }
        catch (KeyNotFoundException exception)
        {
            logger.LogWarning(exception,"Клиент с Id {Id} не найден",id);
            return NotFound();
        }
        catch (Exception exception)
        {
            logger.LogError(exception,"Ошибка при получении тренировок клиента с Id {Id}",id);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}