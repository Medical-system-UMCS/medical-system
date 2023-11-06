using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WpfAppMedicalSystemsDraft.Models
{
    public partial class MedicalSystemsContext : DbContext
    {
        private string connectionString;
        public MedicalSystemsContext(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public MedicalSystemsContext(DbContextOptions<MedicalSystemsContext> options, string connectionString)
            : base(options)
        {
            this.connectionString = connectionString;
        }

        public virtual DbSet<Appointment> Appointments { get; set; } = null!;
        public virtual DbSet<Doctor> Doctors { get; set; } = null!;
        public virtual DbSet<Examination> Examinations { get; set; } = null!;
        public virtual DbSet<Patient> Patients { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Appointment>(entity =>
            {
                entity.ToTable("Appointment");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('appointment_id_seq'::regclass)");

                entity.Property(e => e.AppointmentType)
                    .HasMaxLength(50)
                    .HasColumnName("appointment_type");

                entity.Property(e => e.Date)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("date");

                entity.Property(e => e.DoctorId).HasColumnName("doctor_id");

                entity.Property(e => e.ExaminRoom)
                    .HasMaxLength(10)
                    .HasColumnName("examin_room");

                entity.Property(e => e.PatientId).HasColumnName("patient_id");

                entity.HasOne(d => d.Doctor)
                    .WithMany(p => p.Appointments)
                    .HasForeignKey(d => d.DoctorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Appointment_Doctor_id_fk");

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.Appointments)
                    .HasForeignKey(d => d.PatientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Appointment_patient_id_fk");
            });

            modelBuilder.Entity<Doctor>(entity =>
            {
                entity.ToTable("Doctor");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('\"doctor_Id_seq\"'::regclass)");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(30)
                    .HasColumnName("first_name");

                entity.Property(e => e.LastName)
                    .HasMaxLength(40)
                    .HasColumnName("last_name");

                entity.Property(e => e.ScientificDegree)
                    .HasMaxLength(50)
                    .HasColumnName("scientific_degree");

                entity.Property(e => e.Specialization)
                    .HasMaxLength(50)
                    .HasColumnName("specialization");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Doctors)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Doctor_user_id_fk");
            });

            modelBuilder.Entity<Examination>(entity =>
            {
                entity.ToTable("examination");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AppointmentId).HasColumnName("appointment_id");

                entity.Property(e => e.Diagnosis)
                    .HasMaxLength(100)
                    .HasColumnName("diagnosis");

                entity.Property(e => e.Symptoms)
                    .HasMaxLength(100)
                    .HasColumnName("symptoms");

                entity.Property(e => e.Treatment)
                    .HasMaxLength(100)
                    .HasColumnName("treatment");

                entity.HasOne(d => d.Appointment)
                    .WithMany(p => p.Examinations)
                    .HasForeignKey(d => d.AppointmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("examination_Appointment_id_fk");
            });

            modelBuilder.Entity<Patient>(entity =>
            {
                entity.ToTable("patient");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Dayofbirth).HasColumnName("dayofbirth");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(30)
                    .HasColumnName("first_name");

                entity.Property(e => e.Height).HasColumnName("height");

                entity.Property(e => e.LastName)
                    .HasMaxLength(30)
                    .HasColumnName("last_name");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.Weight).HasColumnName("weight");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Patients)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("user_fkey");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AccountType)
                    .HasMaxLength(30)
                    .HasColumnName("account_type");

                entity.Property(e => e.Login)
                    .HasMaxLength(50)
                    .HasColumnName("login");

                entity.Property(e => e.Password)
                    .HasMaxLength(32)
                    .HasColumnName("password");

                entity.Property(e => e.Verified).HasColumnName("verified");
            });

            modelBuilder.HasSequence<int>("appointment_id_seq");

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
