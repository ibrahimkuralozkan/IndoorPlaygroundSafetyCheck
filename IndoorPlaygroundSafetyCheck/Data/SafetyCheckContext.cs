//using System;
//using System.Collections.Generic;
//using IndoorPlaygroundSafetyCheck.Models;
//using Microsoft.Data.SqlClient;
//using Microsoft.EntityFrameworkCore;

//namespace IndoorPlaygroundSafetyCheck.Data;

//public partial class SafetyCheckContext : DbContext
//{
//    public SafetyCheckContext()
//    {
//    }
//    public DbSet<InspectionQuestionResult> InspectionQuestionResults { get; set; }


//    public DbSet<Station> Stations { get; set; }


//        // Define your OnConfiguring or use a constructor with options to set up the DbContext


//    public SafetyCheckContext(DbContextOptions<SafetyCheckContext> options)
//        : base(options)
//    {
//    }
//    public virtual DbSet<Message> Messages { get; set; }
//    public virtual DbSet<Repair> Repairs { get; set; }

//    public virtual DbSet<Employee> Employees { get; set; }

//    public virtual DbSet<Inspection> Inspections { get; set; }



//    public virtual DbSet<QuestionCatalogue> QuestionCatalogues { get; set; }



//    public virtual DbSet<StationQuestion> StationQuestions { get; set; }

//    public virtual DbSet<Training> Training { get; set; }
//    public void DeleteEmployee(string rfidUid)
//    {
//        // Setting up the parameter for the stored procedure call
//        var rfidUidParam = new SqlParameter("@RfidUid", rfidUid);

//        // Execute the stored procedure
//        this.Database.ExecuteSqlRaw("EXEC SafetyCheck.fsp_DeleteEmployee @RfidUid", rfidUidParam);
//    }


//    public int AddEmployee(string firstName, string lastName, int position, string rfidUid)
//    {
//        // Setting up the parameters for the stored procedure call
//        var firstNameParam = new SqlParameter("@FirstName", firstName);
//        var lastNameParam = new SqlParameter("@LastName", lastName ?? (object)DBNull.Value); // Handling possible null lastName
//        var positionParam = new SqlParameter("@Position", position);
//        var rfidUidParam = new SqlParameter("@RfidUid", rfidUid);

//        // Execute the stored procedure and return the result
//        var result = this.Database.ExecuteSqlRaw("EXEC SafetyCheck.fsp_EmployeeSave @FirstName, @LastName, @Position, @RfidUid", firstNameParam, lastNameParam, positionParam, rfidUidParam);

//        return result; // The result here should be the new employee's ID, adjust as necessary based on your stored procedure's behavior
//    }
//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)

//         => optionsBuilder.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;Initial Catalog=EventManager;Integrated Security=True;TrustServerCertificate=True;");
//    protected override void OnModelCreating(ModelBuilder modelBuilder)
//    {

//        modelBuilder.Entity<AllOpenErrorsV>(entity =>
//        {
//            entity
//                .HasNoKey()
//                .ToView("AllOpenErrors_V", "SafetyCheck");

//            entity.Property(e => e.AnswerNotes).HasMaxLength(255);
//            entity.Property(e => e.CheckDone).HasColumnType("datetime");
//            entity.Property(e => e.CheckStart).HasColumnType("datetime");
//            entity.Property(e => e.QuestionText).HasMaxLength(255);
//            entity.Property(e => e.StationName).HasMaxLength(255);
//        });
//        modelBuilder.Entity<Message>(entity =>
//        {
//            entity.ToTable("Message", "SafetyCheck");
//            entity.HasKey(e => e.Ident);
//        });
//        modelBuilder.Entity<InspectionQuestionResult>()
//           .ToTable("InspectionQuestionResult", schema: "SafetyCheck")
//           .HasKey(iqr => iqr.Ident);

//        // Other configurations

//        base.OnModelCreating(modelBuilder);

//        OnModelCreatingPartial(modelBuilder);

//        modelBuilder.Entity<AverageErrorPerDayV>(entity =>
//        {
//            entity
//                .HasNoKey()
//                .ToView("AverageErrorPerDay_V", "SafetyCheck");

//            entity.Property(e => e.AllErrorsPerDay).HasColumnType("decimal(18, 2)");
//            entity.Property(e => e.Error3PerDay).HasColumnType("decimal(18, 2)");
//            entity.Property(e => e.StationName).HasMaxLength(255);
//        });

//        modelBuilder.Entity<AverageInspectionPerDayV>(entity =>
//        {
//            entity
//                .HasNoKey()
//                .ToView("AverageInspectionPerDay_V", "SafetyCheck");

//            entity.Property(e => e.LocationName).HasMaxLength(50);
//        });

//        modelBuilder.Entity<AverageInspectionTimeV>(entity =>
//        {
//            entity
//                .HasNoKey()
//                .ToView("AverageInspectionTime_V", "SafetyCheck");

//            entity.Property(e => e.Station).HasMaxLength(255);
//        });

//        modelBuilder.Entity<Employee>(entity =>
//        {
//            entity.HasKey(e => e.Ident).HasName("PK__Employee__177FD81EFDDD6A08");

//            entity.ToTable("Employee", "SafetyCheck");

//            entity.Property(e => e.FirstName).HasMaxLength(50);
//            entity.Property(e => e.InsertTimeStamp)
//                .HasDefaultValueSql("(getdate())")
//                .HasColumnType("datetime");
//            entity.Property(e => e.InsertedBy)
//                .HasMaxLength(50)
//                .HasDefaultValueSql("(suser_name())");
//            entity.Property(e => e.LastName).HasMaxLength(50);
//            entity.Property(e => e.RfidUid).HasMaxLength(10);
//            entity.Property(e => e.UpdateTimeStamp)
//                .HasDefaultValueSql("(getdate())")
//                .HasColumnType("datetime");
//            entity.Property(e => e.UpdatedBy)
//                .HasMaxLength(50)
//                .HasDefaultValueSql("(suser_name())");
//        });

//        modelBuilder.Entity<Inspection>(entity =>
//        {
//            entity.HasKey(e => e.Ident);

//            entity.ToTable("Inspection", "SafetyCheck");

//            entity.Property(e => e.CheckDone).HasColumnType("datetime");
//            entity.Property(e => e.CheckStart).HasColumnType("datetime");
//            entity.Property(e => e.InsertTimeStamp)
//                .HasDefaultValueSql("(getdate())")
//                .HasColumnType("datetime");
//            entity.Property(e => e.InsertedBy)
//                .HasMaxLength(50)
//                .HasDefaultValueSql("(suser_name())");
//            entity.Property(e => e.RfidUid)
//                .HasMaxLength(255)
//                .IsUnicode(false);
//            entity.Property(e => e.UpdateTimeStamp)
//                .HasDefaultValueSql("(getdate())")
//                .HasColumnType("datetime");
//            entity.Property(e => e.UpdatedBy)
//                .HasMaxLength(50)
//                .HasDefaultValueSql("(suser_name())");
//        });

//        modelBuilder.Entity<InspectionQuestionResult>(entity =>
//        {
//            entity.HasKey(e => e.Ident).HasName("PK_List");

//            entity.ToTable("InspectionQuestionResult", "SafetyCheck");

//            entity.Property(e => e.InsertTimeStamp)
//                .HasDefaultValueSql("(getdate())")
//                .HasColumnType("datetime");
//            entity.Property(e => e.InsertedBy)
//                .HasMaxLength(50)
//                .HasDefaultValueSql("(suser_name())");
//            entity.Property(e => e.Notes).HasMaxLength(255);
//            entity.Property(e => e.QuestionText).HasMaxLength(255);
//            entity.Property(e => e.UpdateTimeStamp)
//                .HasDefaultValueSql("(getdate())")
//                .HasColumnType("datetime");
//            entity.Property(e => e.UpdatedBy)
//                .HasMaxLength(50)
//                .HasDefaultValueSql("(suser_name())");

//            entity.HasOne(d => d.InspectionIdentNavigation).WithMany(p => p.InspectionQuestionResults)
//                .HasForeignKey(d => d.InspectionIdent)
//                .OnDelete(DeleteBehavior.ClientSetNull)
//                .HasConstraintName("FK_List_Inspection");

//            entity.HasOne(d => d.StationQuestionIdentNavigation).WithMany(p => p.InspectionQuestionResults)
//                .HasForeignKey(d => d.StationQuestionIdent)
//                .OnDelete(DeleteBehavior.ClientSetNull)
//                .HasConstraintName("FK_List_StationQuestion");
//        });

//        modelBuilder.Entity<LastInspectionV>(entity =>
//        {
//            entity
//                .HasNoKey()
//                .ToView("LastInspection_V", "SafetyCheck");

//            entity.Property(e => e.LastInspection).HasMaxLength(35);
//            entity.Property(e => e.LocationName).HasMaxLength(50);
//            entity.Property(e => e.StationName).HasMaxLength(255);
//        });

//        modelBuilder.Entity<OpenDefectInspectionsV>(entity =>
//        {
//            entity
//                .HasNoKey()
//                .ToView("OpenDefectInspections_V", "SafetyCheck");

//            entity.Property(e => e.AnswerNotes).HasMaxLength(255);
//            entity.Property(e => e.CheckDone).HasColumnType("datetime");
//            entity.Property(e => e.CheckStart).HasColumnType("datetime");
//            entity.Property(e => e.QuestionText).HasMaxLength(255);
//            entity.Property(e => e.StationName).HasMaxLength(255);
//        });


//        modelBuilder.Entity<QuestionCatalogue>(entity =>
//        {
//            entity.HasKey(e => e.Ident);

//            entity.ToTable("QuestionCatalogue", "SafetyCheck");

//            entity.Property(e => e.Description).HasMaxLength(255);
//            entity.Property(e => e.InsertTimeStamp)
//                .HasDefaultValueSql("(getdate())")
//                .HasColumnType("datetime");
//            entity.Property(e => e.InsertedBy)
//                .HasMaxLength(50)
//                .HasDefaultValueSql("(suser_name())");
//            entity.Property(e => e.UpdateTimeStamp)
//                .HasDefaultValueSql("(getdate())")
//                .HasColumnType("datetime");
//            entity.Property(e => e.UpdatedBy)
//                .HasMaxLength(50)
//                .HasDefaultValueSql("(suser_name())");
//        });

//        modelBuilder.Entity<Station>(entity =>
//        {
//            entity.HasKey(e => e.Ident);

//            entity.ToTable("Station", "SafetyCheck");

//            entity.Property(e => e.InsertTimeStamp)
//                .HasDefaultValueSql("(getdate())")
//                .HasColumnType("datetime");
//            entity.Property(e => e.InsertedBy)
//                .HasMaxLength(50)
//                .HasDefaultValueSql("(suser_name())");
//            entity.Property(e => e.Name).HasMaxLength(255);
//            entity.Property(e => e.UpdateTimeStamp)
//                .HasDefaultValueSql("(getdate())")
//                .HasColumnType("datetime");
//            entity.Property(e => e.UpdatedBy)
//                .HasMaxLength(50)
//                .HasDefaultValueSql("(suser_name())");
//        });

//        modelBuilder.Entity<StationQuestion>(entity =>
//        {
//            entity.HasKey(e => e.Ident).HasName("PK_StationQuestion_1");

//            entity.ToTable("StationQuestion", "SafetyCheck");

//            entity.Property(e => e.InsertTimeStamp)
//                .HasDefaultValueSql("(getdate())")
//                .HasColumnType("datetime");
//            entity.Property(e => e.InsertedBy)
//                .HasMaxLength(50)
//                .HasDefaultValueSql("(suser_name())");
//            entity.Property(e => e.QuestionText).HasMaxLength(255);
//            entity.Property(e => e.UpdateTimeStamp)
//                .HasDefaultValueSql("(getdate())")
//                .HasColumnType("datetime");
//            entity.Property(e => e.UpdatedBy)
//                .HasMaxLength(50)
//                .HasDefaultValueSql("(suser_name())");

//            entity.HasOne(d => d.QuestionCatalogueIdentNavigation).WithMany(p => p.StationQuestions)
//                .HasForeignKey(d => d.QuestionCatalogueIdent)
//                .OnDelete(DeleteBehavior.ClientSetNull)
//                .HasConstraintName("FK_StationQuestion_QuestionCatalogue");

//            entity.HasOne(d => d.StationIdentNavigation).WithMany(p => p.StationQuestions)
//                .HasForeignKey(d => d.StationIdent)
//                .OnDelete(DeleteBehavior.ClientSetNull)
//                .HasConstraintName("FK_StationQuestion_Station");
//        });

//        modelBuilder.Entity<TodaysAllErrorsV>(entity =>
//        {
//            entity
//                .HasNoKey()
//                .ToView("TodaysAllErrors_V", "SafetyCheck");

//            entity.Property(e => e.CheckDone).HasColumnType("datetime");
//            entity.Property(e => e.CheckStart).HasColumnType("datetime");
//            entity.Property(e => e.LocationName).HasMaxLength(50);
//            entity.Property(e => e.Notes).HasMaxLength(255);
//            entity.Property(e => e.QuestionText).HasMaxLength(255);
//            entity.Property(e => e.StationName).HasMaxLength(255);
//        });

//        modelBuilder.Entity<TodaysAllInspectionsV>(entity =>
//        {
//            entity
//                .HasNoKey()
//                .ToView("TodaysAllInspections_V", "SafetyCheck");

//            entity.Property(e => e.CheckDone).HasColumnType("datetime");
//            entity.Property(e => e.CheckStart).HasColumnType("datetime");
//            entity.Property(e => e.LocationName).HasMaxLength(50);
//            entity.Property(e => e.StationName).HasMaxLength(255);
//        });

//        modelBuilder.Entity<TodaysDefectStationsV>(entity =>
//        {
//            entity
//                .HasNoKey()
//                .ToView("TodaysDefectStations_V", "SafetyCheck");

//            entity.Property(e => e.CheckDone).HasColumnType("datetime");
//            entity.Property(e => e.CheckStart).HasColumnType("datetime");
//            entity.Property(e => e.LocationName).HasMaxLength(50);
//            entity.Property(e => e.Notes).HasMaxLength(255);
//            entity.Property(e => e.QuestionText).HasMaxLength(255);
//            entity.Property(e => e.StationName).HasMaxLength(255);
//        });

//        modelBuilder.Entity<Training>(entity =>
//        {
//            entity.HasKey(e => e.UpdateTimeStamp);

//            entity.ToTable("Training", "SafetyCheck");

//            entity.Property(e => e.UpdateTimeStamp)
//                .HasDefaultValueSql("(getdate())")
//                .HasColumnType("datetime");
//            entity.Property(e => e.Ident).ValueGeneratedOnAdd();
//            entity.Property(e => e.InsertTimeStamp)
//                .HasDefaultValueSql("(getdate())")
//                .HasColumnType("datetime");
//            entity.Property(e => e.InsertedBy)
//                .HasMaxLength(50)
//                .HasDefaultValueSql("(suser_name())");
//            entity.Property(e => e.UpdatedBy)
//                .HasMaxLength(50)
//                .HasDefaultValueSql("(suser_name())");

//            entity.HasOne(d => d.StationIdentNavigation).WithMany(p => p.Training)
//                .HasForeignKey(d => d.StationIdent)
//                .OnDelete(DeleteBehavior.ClientSetNull)
//                .HasConstraintName("FK_Training_Station");
//        });
//        modelBuilder.Entity<Repair>(entity =>
//        {
//            entity.ToTable("Repair", "SafetyCheck");

//            entity.HasKey(e => e.Ident);

//            entity.Property(e => e.Notes).HasMaxLength(255); // Assuming max length for notes
//            entity.Property(e => e.RepairDatePlan).HasColumnType("date");
//            entity.Property(e => e.IsRepaired).HasDefaultValue(0); // Assuming default value of 0 for not repaired
//            entity.Property(e => e.InsertTimeStamp).HasDefaultValueSql("getdate()");
//            entity.Property(e => e.InsertedBy).HasMaxLength(50).HasDefaultValueSql("suser_name()");
//            entity.Property(e => e.UpdateTimeStamp).HasDefaultValueSql("getdate()");
//            entity.Property(e => e.UpdatedBy).HasMaxLength(50).HasDefaultValueSql("suser_name()");

//            entity.HasOne(d => d.Station) // Assuming Station navigation property in Repair model
//                .WithMany(p => p.Repairs) // Assuming a collection of Repairs in your Station model
//                .HasForeignKey(d => d.StationIdent)
//                .OnDelete(DeleteBehavior.ClientSetNull)
//                .HasConstraintName("FK_Repair_Station");
//        });


//        OnModelCreatingPartial(modelBuilder);
//    }

//    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
//}

using System;
using System.Collections.Generic;
using IndoorPlaygroundSafetyCheck.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace IndoorPlaygroundSafetyCheck.Data
{
    public partial class SafetyCheckContext : DbContext
    {
        public SafetyCheckContext()
        {
        }

        public SafetyCheckContext(DbContextOptions<SafetyCheckContext> options)
            : base(options)
        {
        }

        public DbSet<InspectionQuestionResult> InspectionQuestionResults { get; set; }
        public DbSet<Station> Stations { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<Repair> Repairs { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Inspection> Inspections { get; set; }
        public virtual DbSet<QuestionCatalogue> QuestionCatalogues { get; set; }
        public virtual DbSet<StationQuestion> StationQuestions { get; set; }
        public virtual DbSet<Training> Training { get; set; }

        public void DeleteEmployee(string rfidUid)
        {
            var rfidUidParam = new SqlParameter("@RfidUid", rfidUid);
            this.Database.ExecuteSqlRaw("EXEC SafetyCheck.fsp_DeleteEmployee @RfidUid", rfidUidParam);
        }

        public int AddEmployee(string firstName, string lastName, int position, string rfidUid)
        {
            var firstNameParam = new SqlParameter("@FirstName", firstName);
            var lastNameParam = new SqlParameter("@LastName", lastName ?? (object)DBNull.Value);
            var positionParam = new SqlParameter("@Position", position);
            var rfidUidParam = new SqlParameter("@RfidUid", rfidUid);

            var result = this.Database.ExecuteSqlRaw("EXEC SafetyCheck.fsp_EmployeeSave @FirstName, @LastName, @Position, @RfidUid", firstNameParam, lastNameParam, positionParam, rfidUidParam);

            return result;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=OZKAN\\SQLEXPRESS;Database=EventManager;User Id=ozkan;Password=1;TrustServerCertificate=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AllOpenErrorsV>(entity =>
            {
                entity.HasNoKey()
                      .ToView("AllOpenErrors_V", "SafetyCheck");

                entity.Property(e => e.AnswerNotes).HasMaxLength(255);
                entity.Property(e => e.CheckDone).HasColumnType("datetime");
                entity.Property(e => e.CheckStart).HasColumnType("datetime");
                entity.Property(e => e.QuestionText).HasMaxLength(255);
                entity.Property(e => e.StationName).HasMaxLength(255);
            });

            modelBuilder.Entity<Message>(entity =>
            {
                entity.ToTable("Message", "SafetyCheck");
                entity.HasKey(e => e.Ident);
            });

            modelBuilder.Entity<InspectionQuestionResult>()
                .ToTable("InspectionQuestionResult", schema: "SafetyCheck")
                .HasKey(iqr => iqr.Ident);

            modelBuilder.Entity<AverageErrorPerDayV>(entity =>
            {
                entity.HasNoKey()
                      .ToView("AverageErrorPerDay_V", "SafetyCheck");

                entity.Property(e => e.AllErrorsPerDay).HasColumnType("decimal(18, 2)");
                entity.Property(e => e.Error3PerDay).HasColumnType("decimal(18, 2)");
                entity.Property(e => e.StationName).HasMaxLength(255);
            });

            modelBuilder.Entity<AverageInspectionPerDayV>(entity =>
            {
                entity.HasNoKey()
                      .ToView("AverageInspectionPerDay_V", "SafetyCheck");

                entity.Property(e => e.LocationName).HasMaxLength(50);
            });

            modelBuilder.Entity<AverageInspectionTimeV>(entity =>
            {
                entity.HasNoKey()
                      .ToView("AverageInspectionTime_V", "SafetyCheck");

                entity.Property(e => e.Station).HasMaxLength(255);
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.Ident).HasName("PK__Employee__177FD81EFDDD6A08");

                entity.ToTable("Employee", "SafetyCheck");

                entity.Property(e => e.FirstName).HasMaxLength(50);
                entity.Property(e => e.InsertTimeStamp)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime");
                entity.Property(e => e.InsertedBy)
                    .HasMaxLength(50)
                    .HasDefaultValueSql("(suser_name())");
                entity.Property(e => e.LastName).HasMaxLength(50);
                entity.Property(e => e.RfidUid).HasMaxLength(10);
                entity.Property(e => e.UpdateTimeStamp)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime");
                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(50)
                    .HasDefaultValueSql("(suser_name())");
            });

            modelBuilder.Entity<Inspection>(entity =>
            {
                entity.HasKey(e => e.Ident);

                entity.ToTable("Inspection", "SafetyCheck");

                entity.Property(e => e.CheckDone).HasColumnType("datetime");
                entity.Property(e => e.CheckStart).HasColumnType("datetime");
                entity.Property(e => e.InsertTimeStamp)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime");
                entity.Property(e => e.InsertedBy)
                    .HasMaxLength(50)
                    .HasDefaultValueSql("(suser_name())");
                entity.Property(e => e.RfidUid)
                    .HasMaxLength(255)
                    .IsUnicode(false);
                entity.Property(e => e.UpdateTimeStamp)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime");
                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(50)
                    .HasDefaultValueSql("(suser_name())");
            });

            modelBuilder.Entity<InspectionQuestionResult>(entity =>
            {
                entity.HasKey(e => e.Ident).HasName("PK_List");

                entity.ToTable("InspectionQuestionResult", "SafetyCheck");

                entity.Property(e => e.InsertTimeStamp)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime");
                entity.Property(e => e.InsertedBy)
                    .HasMaxLength(50)
                    .HasDefaultValueSql("(suser_name())");
                entity.Property(e => e.Notes).HasMaxLength(255);
                entity.Property(e => e.QuestionText).HasMaxLength(255);
                entity.Property(e => e.UpdateTimeStamp)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime");
                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(50)
                    .HasDefaultValueSql("(suser_name())");

                entity.HasOne(d => d.InspectionIdentNavigation).WithMany(p => p.InspectionQuestionResults)
                    .HasForeignKey(d => d.InspectionIdent)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_List_Inspection");

                entity.HasOne(d => d.StationQuestionIdentNavigation).WithMany(p => p.InspectionQuestionResults)
                    .HasForeignKey(d => d.StationQuestionIdent)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_List_StationQuestion");
            });

            modelBuilder.Entity<LastInspectionV>(entity =>
            {
                entity.HasNoKey()
                      .ToView("LastInspection_V", "SafetyCheck");

                entity.Property(e => e.LastInspection).HasMaxLength(35);
                entity.Property(e => e.LocationName).HasMaxLength(50);
                entity.Property(e => e.StationName).HasMaxLength(255);
            });

            modelBuilder.Entity<OpenDefectInspectionsV>(entity =>
            {
                entity.HasNoKey()
                      .ToView("OpenDefectInspections_V", "SafetyCheck");

                entity.Property(e => e.AnswerNotes).HasMaxLength(255);
                entity.Property(e => e.CheckDone).HasColumnType("datetime");
                entity.Property(e => e.CheckStart).HasColumnType("datetime");
                entity.Property(e => e.QuestionText).HasMaxLength(255);
                entity.Property(e => e.StationName).HasMaxLength(255);
            });

            modelBuilder.Entity<QuestionCatalogue>(entity =>
            {
                entity.HasKey(e => e.Ident);

                entity.ToTable("QuestionCatalogue", "SafetyCheck");

                entity.Property(e => e.Description).HasMaxLength(255);
                entity.Property(e => e.InsertTimeStamp)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime");
                entity.Property(e => e.InsertedBy)
                    .HasMaxLength(50)
                    .HasDefaultValueSql("(suser_name())");
                entity.Property(e => e.UpdateTimeStamp)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime");
                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(50)
                    .HasDefaultValueSql("(suser_name())");
            });

            modelBuilder.Entity<Station>(entity =>
            {
                entity.HasKey(e => e.Ident);

                entity.ToTable("Station", "SafetyCheck");

                entity.Property(e => e.InsertTimeStamp)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime");
                entity.Property(e => e.InsertedBy)
                    .HasMaxLength(50)
                    .HasDefaultValueSql("(suser_name())");
                entity.Property(e => e.Name).HasMaxLength(255);
                entity.Property(e => e.UpdateTimeStamp)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime");
                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(50)
                    .HasDefaultValueSql("(suser_name())");
            });

            modelBuilder.Entity<StationQuestion>(entity =>
            {
                entity.HasKey(e => e.Ident).HasName("PK_StationQuestion_1");

                entity.ToTable("StationQuestion", "SafetyCheck");

                entity.Property(e => e.InsertTimeStamp)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime");
                entity.Property(e => e.InsertedBy)
                    .HasMaxLength(50)
                    .HasDefaultValueSql("(suser_name())");
                entity.Property(e => e.QuestionText).HasMaxLength(255);
                entity.Property(e => e.UpdateTimeStamp)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime");
                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(50)
                    .HasDefaultValueSql("(suser_name())");

                entity.HasOne(d => d.QuestionCatalogueIdentNavigation).WithMany(p => p.StationQuestions)
                    .HasForeignKey(d => d.QuestionCatalogueIdent)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StationQuestion_QuestionCatalogue");

                entity.HasOne(d => d.StationIdentNavigation).WithMany(p => p.StationQuestions)
                    .HasForeignKey(d => d.StationIdent)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StationQuestion_Station");
            });

            modelBuilder.Entity<TodaysAllErrorsV>(entity =>
            {
                entity.HasNoKey()
                      .ToView("TodaysAllErrors_V", "SafetyCheck");

                entity.Property(e => e.CheckDone).HasColumnType("datetime");
                entity.Property(e => e.CheckStart).HasColumnType("datetime");
                entity.Property(e => e.LocationName).HasMaxLength(50);
                entity.Property(e => e.Notes).HasMaxLength(255);
                entity.Property(e => e.QuestionText).HasMaxLength(255);
                entity.Property(e => e.StationName).HasMaxLength(255);
            });

            modelBuilder.Entity<TodaysAllInspectionsV>(entity =>
            {
                entity.HasNoKey()
                      .ToView("TodaysAllInspections_V", "SafetyCheck");

                entity.Property(e => e.CheckDone).HasColumnType("datetime");
                entity.Property(e => e.CheckStart).HasColumnType("datetime");
                entity.Property(e => e.LocationName).HasMaxLength(50);
                entity.Property(e => e.StationName).HasMaxLength(255);
            });

            modelBuilder.Entity<TodaysDefectStationsV>(entity =>
            {
                entity.HasNoKey()
                      .ToView("TodaysDefectStations_V", "SafetyCheck");

                entity.Property(e => e.CheckDone).HasColumnType("datetime");
                entity.Property(e => e.CheckStart).HasColumnType("datetime");
                entity.Property(e => e.LocationName).HasMaxLength(50);
                entity.Property(e => e.Notes).HasMaxLength(255);
                entity.Property(e => e.QuestionText).HasMaxLength(255);
                entity.Property(e => e.StationName).HasMaxLength(255);
            });

            modelBuilder.Entity<Training>(entity =>
            {
                entity.HasKey(e => e.UpdateTimeStamp);

                entity.ToTable("Training", "SafetyCheck");

                entity.Property(e => e.UpdateTimeStamp)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime");
                entity.Property(e => e.Ident).ValueGeneratedOnAdd();
                entity.Property(e => e.InsertTimeStamp)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime");
                entity.Property(e => e.InsertedBy)
                    .HasMaxLength(50)
                    .HasDefaultValueSql("(suser_name())");
                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(50)
                    .HasDefaultValueSql("(suser_name())");

                entity.HasOne(d => d.StationIdentNavigation).WithMany(p => p.Training)
                    .HasForeignKey(d => d.StationIdent)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Training_Station");
            });

            modelBuilder.Entity<Repair>(entity =>
            {
                entity.ToTable("Repair", "SafetyCheck");

                entity.HasKey(e => e.Ident);

                entity.Property(e => e.Notes).HasMaxLength(255);
                entity.Property(e => e.RepairDatePlan).HasColumnType("date");
                entity.Property(e => e.IsRepaired).HasDefaultValue(0);
                entity.Property(e => e.InsertTimeStamp).HasDefaultValueSql("getdate()");
                entity.Property(e => e.InsertedBy).HasMaxLength(50).HasDefaultValueSql("suser_name()");
                entity.Property(e => e.UpdateTimeStamp).HasDefaultValueSql("getdate()");
                entity.Property(e => e.UpdatedBy).HasMaxLength(50).HasDefaultValueSql("suser_name()");

                entity.HasOne(d => d.Station)
                    .WithMany(p => p.Repairs)
                    .HasForeignKey(d => d.StationIdent)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Repair_Station");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
