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
    public Task<IList<TrainerDto>> GetTrainers(int id);
}