using FitnessClub.Infrastructure.Data;
using FitnessClub.Domain.Entities;
using FitnessClub.Domain.Repositories;

namespace FitnessClub.Infrastructure.Repositories;

/// <summary>
/// In-memory репозиторий персональных тренировок
/// </summary>
public class PersonalTrainingSessionRepository: IRepository<PersonalTrainingSession, int>
{
    /// <inheritdoc/>
    public PersonalTrainingSession? Get(int id)
    {
        return FitnessClubData.PersonalTrainingSessions.FirstOrDefault(session => session.Id == id);
    }
    /// <inheritdoc/>
    public IReadOnlyList<PersonalTrainingSession> GetAll()
    {
        return FitnessClubData.PersonalTrainingSessions;
    }
    /// <inheritdoc/>
    public void Add(PersonalTrainingSession entity)
    {
        var sessions = FitnessClubData.PersonalTrainingSessions;
        entity.Id = sessions.Count == 0?1:sessions.Max(x => x.Id) + 1;
        sessions.Add(entity);
    }
    /// <inheritdoc/>
    public void Update(PersonalTrainingSession entity)
    {
        var sessions = FitnessClubData.PersonalTrainingSessions;
        var index = sessions.FindIndex(session => session.Id == entity.Id);
        if (index == -1)
        {
            throw new KeyNotFoundException($"Персональная тренировка с Id: {entity.Id} не найдена");
        }
        sessions[index] = entity;
    }
    /// <inheritdoc/>
    public bool Delete(int id)
    {
        var session = Get(id);
        if (session is null)
        {
            return false;
        }
        return FitnessClubData.PersonalTrainingSessions.Remove(session);
    }
}