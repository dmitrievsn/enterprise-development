using FitnessClub.Domain.Entities;
using FitnessClub.Domain.Repositories;

namespace FitnessClub.Infrastructure.Ef.Repositories;

/// <summary>
/// Репозиторий персональных тренировок с хранением в БД
/// </summary>
public class PersonalTrainingSessionEfRepository(FitnessClubDbContext context): IRepository<PersonalTrainingSession, int>
{
    /// <inheritdoc/>
    public PersonalTrainingSession? Get(int id)
    {
        return context.PersonalTrainingSessions.FirstOrDefault(session => session.Id == id);
    }
    /// <inheritdoc/>
    public IReadOnlyList<PersonalTrainingSession> GetAll()
    {
        return context.PersonalTrainingSessions.ToList();
    }
    /// <inheritdoc/>
    public void Add(PersonalTrainingSession entity)
    {
        var client = context.Clients.Find(entity.Client.Id)?? throw new ArgumentException("Клиент не найден");
        var trainer = context.Trainers.Find(entity.Trainer.Id)?? throw new ArgumentException("Тренер не найден");
        entity.Client = client;
        entity.Trainer = trainer;
        context.PersonalTrainingSessions.Add(entity);
        context.SaveChanges();
    }
    /// <inheritdoc/>
    public void Update(PersonalTrainingSession entity)
    {
        var session = context.PersonalTrainingSessions.FirstOrDefault(s => s.Id == entity.Id)?? throw new ArgumentException("Тренировка не найдена");
        var client = context.Clients.Find(entity.Client.Id)?? throw new ArgumentException("Клиент не найден");
        var trainer = context.Trainers.Find(entity.Trainer.Id)?? throw new ArgumentException("Тренер не найден");
        context.Entry(session).CurrentValues.SetValues(entity);
        session.Client = client;
        session.Trainer = trainer;
        context.SaveChanges();
    }
    /// <inheritdoc/>
    public bool Delete(int id)
    {
        var session = context.PersonalTrainingSessions.Find(id);
        if (session is null) return false;
        context.PersonalTrainingSessions.Remove(session);
        context.SaveChanges();
        return true;
    }
}
