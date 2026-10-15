namespace FitnessClub.Application.Contracts;

/// <summary>
/// Базовый контракт для CRUD операций
/// </summary>
public interface IApplicationService<TDto, TCreateUpdateDto, TKey>
    where TDto : class
    where TCreateUpdateDto : class
    where TKey : struct
{
    /// <summary>
    /// Создаёт новый элемент
    /// </summary>
    TDto Create(TCreateUpdateDto dto);
    /// <summary>
    /// Получает элемент по идентификатору
    /// </summary>
    TDto? Get(TKey id);
    /// <summary>
    /// Получает все элементы
    /// </summary>
    IList<TDto> GetAll();
    /// <summary>
    /// Обновляет элемент по идентификатору
    /// </summary>
    TDto Update(TCreateUpdateDto dto, TKey id);
    /// <summary>
    /// Удаляет элемент по идентификатору
    /// </summary>
    bool Delete(TKey id);
}