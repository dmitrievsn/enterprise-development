using FitnessClub.Application.Contracts;
using FitnessClub.Application.Contracts.PersonalTrainingSessions;
using Microsoft.AspNetCore.Mvc;

namespace FitnessClub.Api.Host.Controllers;

/// <summary>
/// Контроллер для работы с персональными тренировками
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class PersonalTrainingSessionsController(
    IApplicationService<
        PersonalTrainingSessionDto,
        PersonalTrainingSessionCreateUpdateDto,
        int> service,
    ILogger<PersonalTrainingSessionsController> logger)
    : CrudControllerBase<
        PersonalTrainingSessionDto,
        PersonalTrainingSessionCreateUpdateDto,
        int>(service, logger)
{
}