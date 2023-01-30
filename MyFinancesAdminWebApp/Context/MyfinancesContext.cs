using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using MyFinancesAdminWebApp.Models;

namespace MyFinancesAdminWebApp.Context;

public partial class MyfinancesContext : DbContext
{
    public MyfinancesContext()
    {
    }

    public MyfinancesContext(DbContextOptions<MyfinancesContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Bank> Banks { get; set; }

    public virtual DbSet<Currency> Currencies { get; set; }

    public virtual DbSet<LocalStorageClassifier> LocalStorageClassifiers { get; set; }

    public virtual DbSet<PaymentSystem> PaymentSystems { get; set; }

    public virtual DbSet<TransactionType> TransactionTypes { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Server=localhost;Port=5432;Database=myfinances;User Id=adef;Password=199as55");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("pgcrypto");

        modelBuilder.Entity<Bank>(entity =>
        {
            entity.HasKey(e => e.BankId).HasName("banks_pkey");

            entity.ToTable("banks", tb => tb.HasComment("В данном классификаторе содержится название банка и его цвет (для визуализации)"));

            entity.Property(e => e.BankId).HasColumnName("bank_id");
            entity.Property(e => e.Colour)
                .HasMaxLength(6)
                .IsFixedLength()
                .HasColumnName("colour");
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Currency>(entity =>
        {
            entity.HasKey(e => e.CurrencyId).HasName("currencies_pkey");

            entity.ToTable("currencies", tb => tb.HasComment("Сущность хранит наименования валют, их сокращенные названия и символы"));

            entity.Property(e => e.CurrencyId).HasColumnName("currency_id");
            entity.Property(e => e.Name)
                .HasMaxLength(10)
                .HasColumnName("name");
            entity.Property(e => e.ShortName)
                .HasMaxLength(3)
                .IsFixedLength()
                .HasColumnName("short_name");
            entity.Property(e => e.Sign)
                .HasMaxLength(1)
                .HasColumnName("sign");
        });

        modelBuilder.Entity<LocalStorageClassifier>(entity =>
        {
            entity.HasKey(e => e.LocalStorageClassifierId).HasName("local_storage_classifier_pkey");

            entity.ToTable("local_storage_classifier", tb => tb.HasComment("В данном классификаторе, содержится информация для визуализации приложения, такая как, название и номер иконки выбранного хранилища"));

            entity.Property(e => e.LocalStorageClassifierId).HasColumnName("local_storage_classifier_id");
            entity.Property(e => e.Name)
                .HasMaxLength(15)
                .HasColumnName("name");
            entity.Property(e => e.PictureNumber).HasColumnName("picture_number");
        });

        modelBuilder.Entity<PaymentSystem>(entity =>
        {
            entity.HasKey(e => e.PaymentSystemId).HasName("payment_systems_pkey");

            entity.ToTable("payment_systems", tb => tb.HasComment("Классификатор платежных систем, содержащий в себе название и изображение"));

            entity.Property(e => e.PaymentSystemId).HasColumnName("payment_system_id");
            entity.Property(e => e.ImagePath).HasColumnName("image_path");
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .HasColumnName("name");
        });

        modelBuilder.Entity<TransactionType>(entity =>
        {
            entity.HasKey(e => e.TransactionTypeId).HasName("transaction_types_pkey");

            entity.ToTable("transaction_types", tb => tb.HasComment("Классификатор для определения вида транзакции (перевод, платеж, пополнение и т.п.)"));

            entity.Property(e => e.TransactionTypeId).HasColumnName("transaction_type_id");
            entity.Property(e => e.Class)
                .HasMaxLength(1)
                .HasColumnName("class");
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .HasColumnName("name");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Login).HasName("users_pkey");

            entity.ToTable("users", tb => tb.HasComment("Сущность содержит в себе информацию о пользователе приложения"));

            entity.Property(e => e.Login)
                .HasMaxLength(20)
                .HasColumnName("login");
            entity.Property(e => e.BirthDate).HasColumnName("birth_date");
            entity.Property(e => e.Email)
                .HasMaxLength(320)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(25)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(25)
                .HasColumnName("last_name");
            entity.Property(e => e.Password).HasColumnName("password");
            entity.Property(e => e.RegistrationDate).HasColumnName("registration_date");
            entity.Property(e => e.Role)
                .HasMaxLength(10)
                .HasColumnName("role");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
