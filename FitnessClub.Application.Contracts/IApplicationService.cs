namespace FitnessClub.Application.Contracts;

/// <summary>
/// Базовый контракт для CRUD операций
/// </summary>
public interface IApplicationService<TDto, TCreateUpdateDto, TKey>
    where TDto : class
    where TCreateUpdateDto : class
    where TKey : struct
{
    public Task<TDto> Create(TCreateUpdateDto dto);
    public Task<TDto?> Get(TKey id);
    public Task<IList<TDto>> GetAll();
    public Task<TDto> Update(TCreateUpdateDto dto, TKey id);
    public Task<bool> Delete(TKey id);
}