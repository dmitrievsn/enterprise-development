namespace FitnessClub.Application.Contracts.Specializations;

/// <summary>
/// dto для специализаций тренера
/// </summary>
/// <param name="Id">id специализации</param>
/// <param name="Name">Название специализации</param>
public record SpecializationDto(
    int Id,
    string Name);