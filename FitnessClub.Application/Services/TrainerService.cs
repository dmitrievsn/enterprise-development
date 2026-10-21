using FitnessClub.Application.Contracts.PersonalTrainingSessions;
using FitnessClub.Application.Contracts.Trainers;
using FitnessClub.Domain.Repositories;
using FitnessClub.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace FitnessClub.Application.Services;

/// <summary>
/// Cервис для работы с тренерами
/// </summary>
public class TrainerService(
    IRepository<Trainer, int> trainerRepository,
    IRepository<Specialization, int> specializationRepository,
    IRepository<PersonalTrainingSession, int> sessionRepository,
    ILogger<TrainerService> logger): ITrainerService
{
    /// <inheritdoc/>
    public TrainerDto Create(TrainerCreateUpdateDto dto)
    {
        logger.LogInformation("Создание тренера");
        var specialization = specializationRepository.Get(dto.SpecializationId);
        if (specialization is null)
        {
            logger.LogWarning("Специализация c Id: {Id} не найдена",dto.SpecializationId);
            throw new KeyNotFoundException($"Специализация с Id: {dto.SpecializationId} не найдена");
        }
        var trainer = new Trainer
        {
            Id = 0,
            PassportNumber = dto.PassportNumber,
            LastName = dto.LastName,
            FirstName = dto.FirstName,
            Patronymic = dto.Patronymic,
            Gender = dto.Gender,
            DateOfBirth = dto.DateOfBirth,
            Specialization = specialization,
            WorkYears = dto.WorkYears
        };
        trainerRepository.Add(trainer);
        var result = new TrainerDto(
            trainer.Id,
            trainer.PassportNumber,
            trainer.LastName,
            trainer.FirstName,
            trainer.Patronymic,
            trainer.Gender,
            trainer.DateOfBirth,
            trainer.Specialization.Id,
            trainer.WorkYears
        );
        logger.LogInformation("Id созданного тренера: {Id}",trainer.Id);
        return result;
    }
    /// <inheritdoc/>
    public TrainerDto? Get(int id)
    {
        logger.LogInformation("Получение тренера c Id: {Id}",id);
        var trainer = trainerRepository.Get(id);
        if (trainer is null)
        {
            logger.LogWarning("Тренер c Id: {Id} не найден",id);
            return null;
        }
        var result = new TrainerDto(
            trainer.Id,
            trainer.PassportNumber,
            trainer.LastName,
            trainer.FirstName,
            trainer.Patronymic,
            trainer.Gender,
            trainer.DateOfBirth,
            trainer.Specialization.Id,
            trainer.WorkYears         
        );
        logger.LogInformation("Тренер c Id: {Id} найден",id);
        return result;
    }
    /// <inheritdoc/>
    public IList<TrainerDto> GetAll()
    {
        logger.LogInformation("Получение списка тренеров");
        var result = trainerRepository.GetAll()
            .Select(trainer=>new TrainerDto
            (
                trainer.Id,
                trainer.PassportNumber,
                trainer.LastName,
                trainer.FirstName,
                trainer.Patronymic,
                trainer.Gender,
                trainer.DateOfBirth,
                trainer.Specialization.Id,
                trainer.WorkYears ))     
            .ToList();
        logger.LogInformation("Получено тренеров: {Count}",result.Count);
        return result;
    }
    /// <inheritdoc/>
    public TrainerDto Update(TrainerCreateUpdateDto dto, int id)
    {
        logger.LogInformation("Обновление тренера с Id: {Id}",id);
        var trainer = trainerRepository.Get(id);
        if (trainer is null)
        {
            logger.LogWarning("Тренер c Id: {Id} не найден",id);
            throw new KeyNotFoundException($"Тренер с Id: {id} не найден");
        }
        var specialization = specializationRepository.Get(dto.SpecializationId);
        if (specialization is null)
        {
            logger.LogWarning("Специализация c Id: {Id} не найдена",dto.SpecializationId);
            throw new KeyNotFoundException($"Специализация с Id: {dto.SpecializationId} не найдена");
        }
        trainer.PassportNumber = dto.PassportNumber;
        trainer.LastName = dto.LastName;
        trainer.FirstName = dto.FirstName;
        trainer.Patronymic = dto.Patronymic;
        trainer.Gender = dto.Gender;
        trainer.DateOfBirth = dto.DateOfBirth;
        trainer.Specialization = specialization;
        trainer.WorkYears = dto.WorkYears;
        trainerRepository.Update(trainer);
        var result= new TrainerDto
        (
            trainer.Id,
            trainer.PassportNumber,
            trainer.LastName,
            trainer.FirstName,
            trainer.Patronymic,
            trainer.Gender,
            trainer.DateOfBirth,
            trainer.Specialization.Id,
            trainer.WorkYears );
        logger.LogInformation("Тренер с Id: {Id} обновлен",id);
        return result;
    }
    /// <inheritdoc/>
    public bool Delete(int id)
    {
        logger.LogInformation("Удаление тренера с Id: {Id}",id);
        var trainer = trainerRepository.Get(id);
        if (trainer is null)
        {
            logger.LogWarning("Тренер c Id: {Id} не найден",id);
            return false;
        }
        var hasPersonalTrainingSessions = sessionRepository.GetAll().Any(x => x.Trainer.Id == id);
        if (hasPersonalTrainingSessions)
        {
            logger.LogWarning(
                "Тренер с Id: {Id} не может быть удален, у него есть персональные тренировки",
                id);
            return false;
        }
        var result = trainerRepository.Delete(id);
        if (result)
        {
            logger.LogInformation("Тренер с Id: {Id} удалён",id);
        }
        return result;
    }
    /// <inheritdoc/>
    public IList<PersonalTrainingSessionDto> GetPersonalTrainingSessions(int id)
    {
        logger.LogInformation("Получение персональных тренировок тренера с Id: {Id}",id);
        var trainer = trainerRepository.Get(id);
        if (trainer is null)
        {
            logger.LogWarning("Тренер c Id: {Id} не найден",id);
            throw new KeyNotFoundException($"Тренер с Id: {id} не найден");
        }
        var result = sessionRepository.GetAll().Where(p => p.Trainer.Id==id)
            .Select(p =>new PersonalTrainingSessionDto
            (
                p.Id,
                p.Client.Id,
                p.Trainer.Id,
                p.TrainDay,
                p.HallName,
                p.IsTrial
                ))
            .ToList();
        logger.LogInformation("Получено персональных тренировок тренера с Id {Id}: {Count}",id,result.Count);
        return result;
    }
}