using FitnessClub.Application.Contracts.Trainers;
using FitnessClub.Application.Contracts.Specializations;
using FitnessClub.Domain.Repositories;
using FitnessClub.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace FitnessClub.Application.Services;

/// <summary>
/// Cервис для работы со специализациями
/// </summary>
public class SpecializationService(IRepository<Specialization, int> specializationRepository,
    IRepository<Trainer, int> trainerRepository,
    ILogger<SpecializationService> logger): ISpecializationService
{
    /// <inheritdoc/>
    public SpecializationDto Create(SpecializationCreateUpdateDto dto)
    {
        logger.LogInformation("Создание специализации: {Name}",dto.Name);
        var specialization = new Specialization
        {
            Id = 0,
            Name = dto.Name
        };
        specializationRepository.Add(specialization);
        var result = new SpecializationDto(specialization.Id,specialization.Name);
        logger.LogInformation("Id созданной cпециализации: {Id}",specialization.Id);
        return result;
    }
    /// <inheritdoc/>
    public SpecializationDto? Get(int id)
    {
        logger.LogInformation("Получение специализации c Id: {Id}",id);
        var specialization = specializationRepository.Get(id);
        if (specialization is null)
        {
            logger.LogWarning("Cпециализация c Id: {Id} не найдена",id);
            return null;
        }
        var result = new SpecializationDto(specialization.Id,specialization.Name);
        logger.LogInformation("Cпециализация c Id: {Id} найдена",id);
        return result;
    }
    /// <inheritdoc/>
    public IList<SpecializationDto> GetAll()
    {
        logger.LogInformation("Получение списка специализаций");
        var result = specializationRepository.GetAll()
            .Select(x=>new SpecializationDto(x.Id,x.Name))
            .ToList();
        logger.LogInformation("Получено специализаций: {Count}",result.Count);
        return result;
    }
    /// <inheritdoc/>
    public SpecializationDto Update(SpecializationCreateUpdateDto dto, int id)
    {
        logger.LogInformation("Обновление специализации с Id: {Id}",id);
        var specialization = specializationRepository.Get(id);
        if (specialization is null)
        {
            logger.LogWarning("Cпециализация c Id: {Id} не найдена",id);
            throw new KeyNotFoundException($"Специализация с Id: {id} не найдена");
        }
        specialization.Name = dto.Name;
        specializationRepository.Update(specialization);
        var result= new SpecializationDto(specialization.Id,specialization.Name);
        logger.LogInformation("Специализация с Id: {Id} обновлена",id);
        return result;
    }
    /// <inheritdoc/>
    public bool Delete(int id)
    {
        logger.LogInformation("Удаление специализации с Id: {Id}",id);
        var specialization = specializationRepository.Get(id);
        if (specialization is null)
        {
            logger.LogWarning("Cпециализация c Id: {Id} не найдена",id);
            return false;
        }
        var hasTrainers = trainerRepository.GetAll().Any(trainer => trainer.Specialization.Id == id);
        if (hasTrainers)
        {
            logger.LogWarning("Специализация с Id: {Id} не может быть удалена, она используется у тренеров",id);
            return false;
        }
        var result = specializationRepository.Delete(id);
        if (result)
        {
            logger.LogInformation("Специализация с Id: {Id} удалена",id);
        }
        return result;
    }

    /// <inheritdoc/>
    public IList<TrainerDto> GetTrainers(int id)
    {
        logger.LogInformation("Получение тренеров по специализации с Id: {Id}",id);
        var specialization = specializationRepository.Get(id);
        if (specialization is null)
        {
            logger.LogWarning("Специализация c Id: {Id} не найдена",id);
            throw new KeyNotFoundException($"Специализация с Id: {id} не найдена");
        }
        var result = trainerRepository.GetAll().Where(p => p.Specialization.Id==id)
            .Select(p =>new TrainerDto
            (
                p.Id,
                p.PassportNumber,
                p.LastName,
                p.FirstName,
                p.Patronymic,
                p.Gender,
                p.DateOfBirth,
                p.Specialization.Id,
                p.WorkYears))
            .ToList();
        logger.LogInformation("Получено тренеров по специализации с Id {Id}: {Count}",id,result.Count);
        return result;
    }
}