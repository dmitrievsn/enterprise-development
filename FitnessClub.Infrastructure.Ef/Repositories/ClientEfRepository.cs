using FitnessClub.Domain.Entities;
using FitnessClub.Domain.Repositories;

namespace FitnessClub.Infrastructure.Ef.Repositories;

/// <summary>
/// Репозиторий клиентов с хранением в БД
/// </summary>
public class ClientEfRepository(FitnessClubDbContext context): IRepository<Client, int>
{
    /// <inheritdoc/>
    public Client? Get(int id)
    {
        return context.Clients.Find(id);
    }
    /// <inheritdoc/>
    public IReadOnlyList<Client> GetAll()
    {
        return context.Clients.ToList();
    }
    /// <inheritdoc/>
    public void Add(Client entity)
    {
        context.Clients.Add(entity);
        context.SaveChanges();
    }
    /// <inheritdoc/>
    public void Update(Client entity)
    {
        context.Clients.Update(entity);
        context.SaveChanges();
    }
    /// <inheritdoc/>
    public bool Delete(int id)
    {
        var client = context.Clients.Find(id);
        if (client is null) return false;
        context.Clients.Remove(client);
        context.SaveChanges();
        return true;
    }
}
