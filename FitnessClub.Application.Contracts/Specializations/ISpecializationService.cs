using FitnessClub.Application.Contracts.Trainers;

namespace FitnessClub.Application.Contracts.Specializations;

/// <summary>
/// Контракт для работы со специализациями
/// </summary>
public interface ISpecializationService:
    IApplicationService<
    SpecializationDto,
    SpecializationCreateUpdateDto,
    int>
{
    /// <summary>
    /// Получает тренеров выбранной специализации
    /// </summary>
    IList<TrainerDto> GetTrainers(int id);
}