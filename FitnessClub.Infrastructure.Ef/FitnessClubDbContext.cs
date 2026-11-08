using FitnessClub.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FitnessClub.Infrastructure.Ef;

/// <summary>
/// Контекст базы данных фитнес-клуба
/// </summary>
public class FitnessClubDbContext(DbContextOptions<FitnessClubDbContext> options) : DbContext(options)
{
    /// <summary>
    /// Клиенты фитнес-клуба
    /// </summary>
    public DbSet<Client> Clients => Set<Client>();
    /// <summary>
    /// Тренеры фитнес-клуба
    /// </summary>
    public DbSet<Trainer> Trainers => Set<Trainer>();
    /// <summary>
    /// Специализации тренеров
    /// </summary>
    public DbSet<Specialization> Specializations => Set<Specialization>();
    /// <summary>
    /// Персональные тренировки
    /// </summary>
    public DbSet<PersonalTrainingSession> PersonalTrainingSessions => Set<PersonalTrainingSession>();
    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(client => client.Id);
            entity.Property(client => client.Id).ValueGeneratedOnAdd();
            entity.Property(client => client.PassportNumber).HasMaxLength(30);
            entity.Property(client => client.LastName).HasMaxLength(100);
            entity.Property(client => client.FirstName).HasMaxLength(100);
            entity.Property(client => client.Patronymic).HasMaxLength(100);
            entity.Property(client => client.PhoneNumber).HasMaxLength(30);
        });
        modelBuilder.Entity<Specialization>(entity =>
        {
            entity.HasKey(specialization => specialization.Id);
            entity.Property(specialization => specialization.Id).ValueGeneratedOnAdd();
            entity.Property(specialization => specialization.Name).HasMaxLength(100);
        });
        modelBuilder.Entity<Trainer>(entity =>
        {
            entity.HasKey(trainer => trainer.Id);
            entity.Property(trainer => trainer.Id).ValueGeneratedOnAdd();
            entity.Property(trainer => trainer.PassportNumber).HasMaxLength(30);
            entity.Property(trainer => trainer.LastName).HasMaxLength(100);
            entity.Property(trainer => trainer.FirstName).HasMaxLength(100);
            entity.Property(trainer => trainer.Patronymic).HasMaxLength(100);
            entity.HasOne(trainer => trainer.Specialization)
                .WithMany()
                .HasForeignKey("SpecializationId")
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
            entity.Navigation(trainer => trainer.Specialization).AutoInclude();
        });
        modelBuilder.Entity<PersonalTrainingSession>(entity =>
        {
            entity.HasKey(session => session.Id);
            entity.Property(session => session.Id).ValueGeneratedOnAdd();
            entity.Property(session => session.HallName).HasMaxLength(100);
            entity.HasOne(session => session.Client)
                .WithMany()
                .HasForeignKey("ClientId")
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(session => session.Trainer)
                .WithMany()
                .HasForeignKey("TrainerId")
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
            entity.Navigation(session => session.Client).AutoInclude();
            entity.Navigation(session => session.Trainer).AutoInclude();
        });
    }
}