using FitnessClub.Domain.Entities;
using FitnessClub.Domain.Repositories;

namespace FitnessClub.Infrastructure.Ef.Repositories;

/// <summary>
/// Репозиторий тренеров с хранением в БД
/// </summary>
public class TrainerEfRepository(FitnessClubDbContext context): IRepository<Trainer, int>
{
    /// <inheritdoc/>
    public Trainer? Get(int id)
    {
        return context.Trainers.FirstOrDefault(t => t.Id == id);
    }
    /// <inheritdoc/>
    public IReadOnlyList<Trainer> GetAll()
    {
        return context.Trainers.ToList();
    }
    /// <inheritdoc/>
    public void Add(Trainer entity)
    {
        var specialization = context.Specializations.Find(entity.Specialization.Id)?? throw new ArgumentException("Специализация не найдена");
        entity.Specialization = specialization;
        context.Trainers.Add(entity);
        context.SaveChanges();
    }
    /// <inheritdoc/>
    public void Update(Trainer entity)
    {
        var trainer = context.Trainers.FirstOrDefault(t => t.Id == entity.Id)?? throw new ArgumentException("Тренер не найден");
        var specialization = context.Specializations.Find(entity.Specialization.Id)?? throw new ArgumentException("Специализация не найдена");
        context.Entry(trainer).CurrentValues.SetValues(entity);
        trainer.Specialization = specialization;
        context.SaveChanges();
    }
    /// <inheritdoc/>
    public bool Delete(int id)
    {
        var trainer = context.Trainers.Find(id);
        if (trainer is null) return false;
        context.Trainers.Remove(trainer);
        context.SaveChanges();
        return true;
    }
}
