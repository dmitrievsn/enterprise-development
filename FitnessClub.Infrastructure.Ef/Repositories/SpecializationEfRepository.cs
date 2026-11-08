using FitnessClub.Domain.Entities;
using FitnessClub.Domain.Repositories;

namespace FitnessClub.Infrastructure.Ef.Repositories;

/// <summary>
/// Репозиторий специализаций с хранением в БД
/// </summary>
public class SpecializationEfRepository(FitnessClubDbContext context): IRepository<Specialization, int>
{
    /// <inheritdoc/>
    public Specialization? Get(int id)
    {
        return context.Specializations.Find(id);
    }
    /// <inheritdoc/>
    public IReadOnlyList<Specialization> GetAll()
    {
        return context.Specializations.ToList();
    }
    /// <inheritdoc/>
    public void Add(Specialization entity)
    {
        context.Specializations.Add(entity);
        context.SaveChanges();
    }
    /// <inheritdoc/>
    public void Update(Specialization entity)
    {
        context.Specializations.Update(entity);
        context.SaveChanges();
    }
    /// <inheritdoc/>
    public bool Delete(int id)
    {
        var specialization = context.Specializations.Find(id);
        if (specialization is null) return false;
        context.Specializations.Remove(specialization);
        context.SaveChanges();
        return true;
    }
}
