using FitnessClub.Application.Contracts.Clients;
using FitnessClub.Application.Contracts.PersonalTrainingSessions;
using FitnessClub.Application.Contracts.Trainers;
using FitnessClub.Application.Contracts.Specializations;
using FitnessClub.Domain.Data;
using FitnessClub.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace FitnessClub.Application.Services;

/// <summary>
/// Cервис для работы с тренерами
/// </summary>
public class TrainerService(ILogger<TrainerService> logger): ITrainerService
{
    public Task<TrainerDto> Create(TrainerCreateUpdateDto dto)
    {
        logger.LogInformation("Создание тренера");

        var specialization = FitnessClubData.Specializations.FirstOrDefault(x => x.Id == dto.SpecializationId);

        if (specialization is null)
        {
            logger.LogWarning("Специализация c Id: {Id} не найдена",dto.SpecializationId);

            throw new KeyNotFoundException($"Специализация с Id: {dto.SpecializationId} не найдена");
        }

        var newId = FitnessClubData.Trainers.Count==0?1:FitnessClubData.Trainers.Max(x => x.Id) + 1;

        var trainer = new Trainer
        {
            Id = newId,
            PassportNumber = dto.PassportNumber,
            LastName = dto.LastName,
            FirstName = dto.FirstName,
            Patronymic = dto.Patronymic,
            Gender = dto.Gender,
            DateOfBirth = dto.DateOfBirth,
            Specialization = specialization,
            WorkYears = dto.WorkYears
        };

        FitnessClubData.Trainers.Add(trainer);

        var result = new TrainerDto(
            trainer.Id,
            trainer.PassportNumber,
            trainer.LastName,
            trainer.FirstName,
            trainer.Patronymic,
            trainer.Gender,
            trainer.DateOfBirth,
            new SpecializationDto(
                trainer.Specialization.Id,
                trainer.Specialization.Name),
            trainer.WorkYears
        );

        logger.LogInformation("Id созданного тренера: {Id}",trainer.Id);

        return Task.FromResult(result);
    }

    public Task<TrainerDto?> Get(int id)
    {
        logger.LogInformation("Получение тренера c Id: {Id}",id);

        var trainer = FitnessClubData.Trainers.FirstOrDefault(x => x.Id==id);

        if (trainer is null)
        {
            logger.LogWarning("Тренер c Id: {Id} не найден",id);

            return Task.FromResult<TrainerDto?>(null);
        }

        var result = new TrainerDto(
            trainer.Id,
            trainer.PassportNumber,
            trainer.LastName,
            trainer.FirstName,
            trainer.Patronymic,
            trainer.Gender,
            trainer.DateOfBirth,
            new SpecializationDto(
                trainer.Specialization.Id,
                trainer.Specialization.Name),
            trainer.WorkYears         
        );

        logger.LogInformation("Тренер c Id: {Id} найден",id);

        return Task.FromResult<TrainerDto?>(result);
    }

    public Task<IList<TrainerDto>> GetAll()
    {
        logger.LogInformation("Получение списка тренеров");

        IList<TrainerDto> result = FitnessClubData.Trainers
            .Select(trainer=>new TrainerDto
            (
                trainer.Id,
                trainer.PassportNumber,
                trainer.LastName,
                trainer.FirstName,
                trainer.Patronymic,
                trainer.Gender,
                trainer.DateOfBirth,
                new SpecializationDto(
                    trainer.Specialization.Id,
                    trainer.Specialization.Name),
                trainer.WorkYears ))     
            .ToList();

        logger.LogInformation("Получено тренеров: {Count}",result.Count);

        return Task.FromResult(result);
    }

    public Task<TrainerDto> Update(TrainerCreateUpdateDto dto, int id)
    {
        logger.LogInformation("Обновление тренера с Id: {Id}",id);

        var trainer = FitnessClubData.Trainers.FirstOrDefault(x => x.Id==id);

        if (trainer is null)
        {
            logger.LogWarning("Тренер c Id: {Id} не найден",id);

            throw new KeyNotFoundException($"Тренер с Id: {id} не найден");
        }

        var specialization = FitnessClubData.Specializations.FirstOrDefault(x => x.Id == dto.SpecializationId);

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

        var result= new TrainerDto
        (
            trainer.Id,
            trainer.PassportNumber,
            trainer.LastName,
            trainer.FirstName,
            trainer.Patronymic,
            trainer.Gender,
            trainer.DateOfBirth,
            new SpecializationDto(
                trainer.Specialization.Id,
                trainer.Specialization.Name),
            trainer.WorkYears );

        logger.LogInformation("Тренер с Id: {Id} обновлен",id);

        return Task.FromResult(result);
    }

    public Task<bool> Delete(int id)
    {
        logger.LogInformation("Удаление тренера с Id: {Id}",id);

        var trainer = FitnessClubData.Trainers.FirstOrDefault(x => x.Id==id);

        if (trainer is null)
        {
            logger.LogWarning("Тренер c Id: {Id} не найден",id);

            return Task.FromResult(false);
        }
        var hasPersonalTrainingSessions = FitnessClubData.PersonalTrainingSessions.Any(x => x.Trainer.Id == id);

        if (hasPersonalTrainingSessions)
        {
            logger.LogWarning(
                "Тренер с Id: {Id} не может быть удален, у него есть персональные тренировки",
                id);

            return Task.FromResult(false);
        }

        FitnessClubData.Trainers.Remove(trainer);

        logger.LogInformation("Тренер с Id: {Id} удален",id);

        return Task.FromResult(true);
    }

    public Task<IList<PersonalTrainingSessionDto>> GetPersonalTrainingSessions(int id)
    {
        logger.LogInformation("Получение персональных тренировок тренера с Id: {Id}",id);

        var trainer = FitnessClubData.Trainers.FirstOrDefault(x => x.Id==id);

        if (trainer is null)
        {
            logger.LogWarning("Тренер c Id: {Id} не найден",id);

            throw new KeyNotFoundException($"Тренер с Id: {id} не найден");
        }

        IList<PersonalTrainingSessionDto> result = FitnessClubData.PersonalTrainingSessions.Where(p => p.Trainer.Id==id)
            .Select(p =>new PersonalTrainingSessionDto
            (
                p.Id,
                new ClientDto(
                    p.Client.Id,
                    p.Client.PassportNumber,
                    p.Client.LastName,
                    p.Client.FirstName,
                    p.Client.Patronymic,
                    p.Client.Gender,
                    p.Client.DateOfBirth,
                    p.Client.PhoneNumber,
                    p.Client.StartSub,
                    p.Client.EndSub),
                new TrainerDto(
                    p.Trainer.Id,
                    p.Trainer.PassportNumber,
                    p.Trainer.LastName,
                    p.Trainer.FirstName,
                    p.Trainer.Patronymic,
                    p.Trainer.Gender,
                    p.Trainer.DateOfBirth,
                    new SpecializationDto(
                        p.Trainer.Specialization.Id,
                        p.Trainer.Specialization.Name),
                    p.Trainer.WorkYears),
                p.TrainDay,
                p.HallName,
                p.IsTrial
                ))
            .ToList();

        logger.LogInformation("Получено персональных тренировок тренера с Id {Id}: {Count}",id,result.Count);

        return Task.FromResult(result);
    }
}