using FitnessClub.Infrastructure.Data;
using FitnessClub.Domain.Entities;
using FitnessClub.Domain.Repositories;

namespace FitnessClub.Infrastructure.Repositories;

/// <summary>
/// In-memory репозиторий клиентов
/// </summary>
public class ClientRepository: IRepository<Client, int>
{
    /// <inheritdoc/>
    public Client? Get(int id)
    {
        return FitnessClubData.Clients.FirstOrDefault(client => client.Id == id);
    }
    /// <inheritdoc/>
    public IReadOnlyList<Client> GetAll()
    {
        return FitnessClubData.Clients;
    }
    /// <inheritdoc/>
    public void Add(Client entity)
    {
        var clients = FitnessClubData.Clients;
        entity.Id = clients.Count == 0?1:clients.Max(x => x.Id) + 1;
        clients.Add(entity);
    }
    /// <inheritdoc/>
    public void Update(Client entity)
    {
        var clients = FitnessClubData.Clients;
        var index = clients.FindIndex(client => client.Id == entity.Id);
        if (index == -1)
        {
            throw new KeyNotFoundException($"Клиент с Id: {entity.Id} не найден");
        }
        clients[index] = entity;
    }
    /// <inheritdoc/>
    public bool Delete(int id)
    {
        var client = Get(id);
        if (client is null)
        {
            return false;
        }
        return FitnessClubData.Clients.Remove(client);
    }
}