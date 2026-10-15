namespace FitnessClub.Domain.Repositories;

/// <summary>
/// Базовый контракт репозитория для работы с сущностями
/// </summary>
public interface IRepository<TEntity, TKey>
    where TEntity : class
    where TKey : struct
{
    /// <summary>
    /// Получает сущность по идентификатору
    /// </summary>
    TEntity? Get(TKey id);
    /// <summary>
    /// Получает все сущности
    /// </summary>
    IReadOnlyList<TEntity> GetAll();
    /// <summary>
    /// Добавляет сущность
    /// </summary>
    void Add(TEntity entity);
    /// <summary>
    /// Обновляет сущность
    /// </summary>
    void Update(TEntity entity);
    /// <summary>
    /// Удаляет сущность по идентификатору
    /// </summary>
    bool Delete(TKey id);
}