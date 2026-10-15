using FitnessClub.Infrastructure.Data;
using FitnessClub.Domain.Entities;
using FitnessClub.Domain.Repositories;

namespace FitnessClub.Infrastructure.Repositories;

/// <summary>
/// In-memory репозиторий специализаций
/// </summary>
public class SpecializationRepository: IRepository<Specialization, int>
{
    /// <inheritdoc/>
    public Specialization? Get(int id)
    {
        return FitnessClubData.Specializations.FirstOrDefault(specialization => specialization.Id == id);
    }
    /// <inheritdoc/>
    public IReadOnlyList<Specialization> GetAll()
    {
        return FitnessClubData.Specializations;
    }
    /// <inheritdoc/>
    public void Add(Specialization entity)
    {
        var specializations = FitnessClubData.Specializations;
        entity.Id = specializations.Count == 0?1:specializations.Max(x => x.Id) + 1;
        specializations.Add(entity);
    }
    /// <inheritdoc/>
    public void Update(Specialization entity)
    {
        var specializations = FitnessClubData.Specializations;
        var index = specializations.FindIndex(specialization => specialization.Id == entity.Id);
        if (index == -1)
        {
            throw new KeyNotFoundException($"Специализация с Id: {entity.Id} не найдена");
        }
        specializations[index] = entity;
    }
    /// <inheritdoc/>
    public bool Delete(int id)
    {
        var specialization = Get(id);
        if (specialization is null)
        {
            return false;
        }
        return FitnessClubData.Specializations.Remove(specialization);
    }
}