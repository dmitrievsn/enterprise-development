using FitnessClub.Application.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace FitnessClub.Api.Host.Controllers;

/// <summary>
/// Базовый контроллер для CRUD-операций
/// </summary>
public abstract class CrudControllerBase<TDto, TCreateUpdateDto, TKey>(
    IApplicationService<TDto, TCreateUpdateDto, TKey> service,
    ILogger logger)
    : ControllerBase
    where TDto : class
    where TCreateUpdateDto : class
    where TKey : struct
{
    /// <summary>
    /// Получить все элементы
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IList<TDto>>> GetAll()
    {
        logger.LogInformation("Получение всех элементов типа {DtoType}",typeof(TDto).Name);

        var result = await service.GetAll();

        return Ok(result);
    }

    /// <summary>
    /// Получить элемент по id
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<TDto>> Get(TKey id)
    {
        logger.LogInformation("Получение элемента типа {DtoType} с Id {Id}",typeof(TDto).Name,id);

        var result = await service.Get(id);

        if (result is null)
        {
            logger.LogWarning("Элемент типа {DtoType} с Id {Id} не найден",typeof(TDto).Name,id);

            return NotFound();
        }

        return Ok(result);
    }

    /// <summary>
    /// Создать элемент
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<TDto>> Post(TCreateUpdateDto dto)
    {
        logger.LogInformation("Создание элемента типа {DtoType}",typeof(TDto).Name);

        var result = await service.Create(dto);

        logger.LogInformation("Элемент типа {DtoType} успешно создан",typeof(TDto).Name);

        return StatusCode(StatusCodes.Status201Created, result);
    }

    /// <summary>
    /// Обновить элемент по id
    /// </summary>
    [HttpPut("{id}")]
    public async Task<ActionResult<TDto>> Update(TCreateUpdateDto dto,TKey id)
    {
        logger.LogInformation("Обновление элемента типа {DtoType} с Id {Id}",typeof(TDto).Name,id);

        try
        {
            var result = await service.Update(dto, id);

            logger.LogInformation("Элемент типа {DtoType} с Id {Id} успешно обновлён",typeof(TDto).Name,id);

            return Ok(result);
        }
        catch (KeyNotFoundException)
        {
            logger.LogWarning("Элемент типа {DtoType} с Id {Id} не найден",typeof(TDto).Name,id);

            return NotFound();
        }
    }

    /// <summary>
    /// Удалить элемент по id
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<ActionResult<TDto>> Delete(TKey id)
    {
        logger.LogInformation("Удаление элемента типа {DtoType} с Id {Id}",typeof(TDto).Name,id);

        var existing = await service.Get(id);

        if (existing is null)
        {
            logger.LogWarning("Элемент типа {DtoType} с Id {Id} не найден",typeof(TDto).Name,id);

            return NotFound();
        }

        var result = await service.Delete(id);

        if (!result)
        {
            logger.LogWarning("Элемент типа {DtoType} с Id {Id} не может быть удалён",typeof(TDto).Name,id);

            return Conflict();
        }

        logger.LogInformation("Элемент типа {DtoType} с Id {Id} успешно удалён",typeof(TDto).Name,id);

        return NoContent();
    }
}