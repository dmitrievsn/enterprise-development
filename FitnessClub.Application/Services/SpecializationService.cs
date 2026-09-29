using FitnessClub.Application.Contracts;
using FitnessClub.Application.Contracts.Specializations;
using FitnessClub.Domain.Data;
using FitnessClub.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace FitnessClub.Application.Services;

/// <summary>
/// Cервис для работы со специализациями
/// </summary>
public class SpecializationService(ILogger<SpecializationService> logger):
    IApplicationService<
    SpecializationDto,
    SpecializationCreateUpdateDto,
    int>
{
    public Task<SpecializationDto> Create(SpecializationCreateUpdateDto dto)
    {
        logger.LogInformation("Создание специализации: {Name}",dto.Name);

        var newId = FitnessClubData.Specializations.Count==0?1:FitnessClubData.Specializations.Max(x => x.Id) + 1;

        var specialization = new Specialization
        {
            Id = newId,
            Name = dto.Name
        };

        FitnessClubData.Specializations.Add(specialization);

        var result = new SpecializationDto(specialization.Id,specialization.Name);

        logger.LogInformation("Id созданной cпециализации: {Id}",specialization.Id);

        return Task.FromResult(result);
    }

    public Task<SpecializationDto?> Get(int id)
    {
        logger.LogInformation("Получение специализации c Id: {Id}",id);

        var specialization = FitnessClubData.Specializations.FirstOrDefault(x => x.Id==id);

        if (specialization is null)
        {
            logger.LogWarning("Cпециализация c Id: {Id} не найдена",id);

            return Task.FromResult<SpecializationDto?>(null);
        }

        var result = new SpecializationDto(specialization.Id,specialization.Name);

        logger.LogInformation("Cпециализация c Id: {Id} найдена",id);

        return Task.FromResult<SpecializationDto?>(result);
    }

    public Task<IList<SpecializationDto>> GetAll()
    {
        logger.LogInformation("Получение списка специализаций");

        IList<SpecializationDto> result = FitnessClubData.Specializations
            .Select(x=>new SpecializationDto(x.Id,x.Name))
            .ToList();

        logger.LogInformation("Получено специализаций: {Count}",result.Count);

        return Task.FromResult(result);
    }

    public Task<SpecializationDto> Update(SpecializationCreateUpdateDto dto, int id)
    {
        logger.LogInformation("Обновление специализации с Id: {Id}",id);

        var specialization = FitnessClubData.Specializations.FirstOrDefault(x => x.Id==id);

        if (specialization is null)
        {
            logger.LogWarning("Cпециализация c Id: {Id} не найдена",id);

            throw new KeyNotFoundException($"Специализация с Id: {id} не найдена");
        }

        specialization.Name = dto.Name;

        var result= new SpecializationDto(specialization.Id,specialization.Name);

        logger.LogInformation("Специализация с Id: {Id} обновлена",id);

        return Task.FromResult(result);
    }

    public Task<bool> Delete(int id)
    {
        logger.LogInformation("Удаление специализации с Id: {Id}",id);

        var specialization = FitnessClubData.Specializations.FirstOrDefault(x => x.Id==id);

        if (specialization is null)
        {
            logger.LogWarning("Cпециализация c Id: {Id} не найдена",id);

            return Task.FromResult(false);
        }

        var hasTrainers = FitnessClubData.Trainers.Any(x => x.Specialization.Id == id);

        if (hasTrainers)
        {
            logger.LogWarning("Специализация с Id: {Id} не может быть удалена, она используется у тренеров",id);

            return Task.FromResult(false);
        }

        FitnessClubData.Specializations.Remove(specialization);

        logger.LogInformation("Специализация с Id: {Id} удалена",id);

        return Task.FromResult(true);
    }
}