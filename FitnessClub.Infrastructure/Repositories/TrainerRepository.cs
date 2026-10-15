using FitnessClub.Infrastructure.Data;
using FitnessClub.Domain.Entities;
using FitnessClub.Domain.Repositories;

namespace FitnessClub.Infrastructure.Repositories;

/// <summary>
/// In-memory репозиторий тренеров
/// </summary>
public class TrainerRepository: IRepository<Trainer, int>
{
    /// <inheritdoc/>
    public Trainer? Get(int id)
    {
        return FitnessClubData.Trainers.FirstOrDefault(trainer => trainer.Id == id);
    }
    /// <inheritdoc/>
    public IReadOnlyList<Trainer> GetAll()
    {
        return FitnessClubData.Trainers;
    }
    /// <inheritdoc/>
    public void Add(Trainer entity)
    {
        var trainers = FitnessClubData.Trainers;
        entity.Id = trainers.Count == 0?1:trainers.Max(x => x.Id) + 1;
        trainers.Add(entity);
    }
    /// <inheritdoc/>
    public void Update(Trainer entity)
    {
        var trainers = FitnessClubData.Trainers;
        var index = trainers.FindIndex(trainer => trainer.Id == entity.Id);
        if (index == -1)
        {
            throw new KeyNotFoundException($"Тренер с Id: {entity.Id} не найден");
        }
        trainers[index] = entity;
    }
    /// <inheritdoc/>
    public bool Delete(int id)
    {
        var trainer = Get(id);
        if (trainer is null)
        {
            return false;
        }
        return FitnessClubData.Trainers.Remove(trainer);
    }
}