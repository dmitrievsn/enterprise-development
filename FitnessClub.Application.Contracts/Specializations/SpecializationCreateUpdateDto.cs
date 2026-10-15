namespace FitnessClub.Application.Contracts.Specializations;

/// <summary>
/// dto для создания и обновления специализаций тренера
/// </summary>
/// <param name="Name">Название специализации</param>
public record SpecializationCreateUpdateDto(string Name);