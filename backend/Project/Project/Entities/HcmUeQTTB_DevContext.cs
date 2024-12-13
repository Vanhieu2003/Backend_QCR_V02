using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Project.Dto;

namespace Project.Entities
{
    public partial class HcmUeQTTB_DevContext : DbContext
    {
        public HcmUeQTTB_DevContext()
        {
        }

        public HcmUeQTTB_DevContext(DbContextOptions<HcmUeQTTB_DevContext> options)
            : base(options)
        {
        }
        public DbSet<Application> Application { get; set; }

        public virtual DbSet<Block> Blocks { get; set; } = null!;
        public virtual DbSet<Campus> Campuses { get; set; } = null!;
        public virtual DbSet<CleaningForm> CleaningForms { get; set; } = null!;
        public virtual DbSet<CleaningReport> CleaningReports { get; set; } = null!;
        public virtual DbSet<CriteriaReport> CriteriaReports { get; set; } = null!;
        public virtual DbSet<CriteriasPerForm> CriteriasPerForms { get; set; } = null!;
        public virtual DbSet<Criteria> Criteria { get; set; } = null!;
        public virtual DbSet<Department> Departments { get; set; } = null!;
        public virtual DbSet<DepartmentRoleMapping> DepartmentRoleMappings { get; set; } = null!;
        public virtual DbSet<Equipment> Equipments { get; set; } = null!;
        public virtual DbSet<EquipmentCategory> EquipmentCategories { get; set; } = null!;
        public virtual DbSet<EquipmentCategoryGroup> EquipmentCategoryGroups { get; set; } = null!;
        public virtual DbSet<Event> Events { get; set; } = null!;
        public virtual DbSet<EventType> EventTypes { get; set; } = null!;
        public virtual DbSet<FieldTemplate> FieldTemplates { get; set; } = null!;
        public virtual DbSet<Floor> Floors { get; set; } = null!;
        public virtual DbSet<FloorOfBlock> FloorOfBlocks { get; set; } = null!;
        public virtual DbSet<FormCollection> FormCollections { get; set; } = null!;
        public virtual DbSet<FormTemplate> FormTemplates { get; set; } = null!;
        public virtual DbSet<FormTemplateField> FormTemplateFields { get; set; } = null!;
        public virtual DbSet<FormTemplateGroup> FormTemplateGroups { get; set; } = null!;
        public virtual DbSet<FormTemplateProcess> FormTemplateProcesses { get; set; } = null!;
        public virtual DbSet<FormTemplateProcessApprover> FormTemplateProcessApprovers { get; set; } = null!;
        public virtual DbSet<FormTemplateProcessTrigger> FormTemplateProcessTriggers { get; set; } = null!;
        public virtual DbSet<FormTemplateWatcher> FormTemplateWatchers { get; set; } = null!;
        public virtual DbSet<Group> Groups { get; set; } = null!;
        public virtual DbSet<GroupRoom> GroupRooms { get; set; } = null!;
        public virtual DbSet<Item> Items { get; set; } = null!;
        public virtual DbSet<ItemGroup> ItemGroups { get; set; } = null!;
        public virtual DbSet<Lesson> Lessons { get; set; } = null!;
        public virtual DbSet<LessonPractice> LessonPractices { get; set; } = null!;
        public virtual DbSet<LessonPracticeQuotum> LessonPracticeQuota { get; set; } = null!;
        public virtual DbSet<Notification> Notifications { get; set; } = null!;
        public virtual DbSet<Permission> Permissions { get; set; } = null!;
        public virtual DbSet<PermissionRoleMapping> PermissionRoleMappings { get; set; } = null!;
        public virtual DbSet<PersistedGrant> PersistedGrants { get; set; } = null!;
        public virtual DbSet<Repair> Repairs { get; set; } = null!;
        public virtual DbSet<Request> Requests { get; set; } = null!;
        public virtual DbSet<RequestForm> RequestForms { get; set; } = null!;
        public virtual DbSet<RequestFormActivityLog> RequestFormActivityLogs { get; set; } = null!;
        public virtual DbSet<RequestFormCommunicate> RequestFormCommunicates { get; set; } = null!;
        public virtual DbSet<RequestFormField> RequestFormFields { get; set; } = null!;
        public virtual DbSet<RequestFormGroup> RequestFormGroups { get; set; } = null!;
        public virtual DbSet<RequestFormHandleWorkFlow> RequestFormHandleWorkFlows { get; set; } = null!;
        public virtual DbSet<RequestFormProcess> RequestFormProcesses { get; set; } = null!;
        public virtual DbSet<RequestFormProcessApprover> RequestFormProcessApprovers { get; set; } = null!;
        public virtual DbSet<RequestFormProcessTrigger> RequestFormProcessTriggers { get; set; } = null!;
        public virtual DbSet<RequestFormWatcher> RequestFormWatchers { get; set; } = null!;
        public virtual DbSet<ResponsibleGroup> ResponsibleGroups { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<Room> Rooms { get; set; } = null!;
        public virtual DbSet<RoomByGroup> RoomByGroups { get; set; } = null!;
        public virtual DbSet<RoomCategory> RoomCategories { get; set; } = null!;
        public virtual DbSet<RoomSchedule> RoomSchedules { get; set; } = null!;
        public virtual DbSet<Schedule> Schedules { get; set; } = null!;
        public virtual DbSet<ScheduleDetail> ScheduleDetails { get; set; } = null!;
        public virtual DbSet<Section> Sections { get; set; } = null!;
        public virtual DbSet<Shift> Shifts { get; set; } = null!;
        public virtual DbSet<Specialization> Specializations { get; set; } = null!;
        public virtual DbSet<SpecializationAdditional> SpecializationAdditionals { get; set; } = null!;
        public virtual DbSet<Staff> Staffs { get; set; } = null!;
        public virtual DbSet<Student> Students { get; set; } = null!;
        public virtual DbSet<Tag> Tags { get; set; } = null!;
        public virtual DbSet<TagsPerCriteria> TagsPerCriteria { get; set; } = null!;
        public virtual DbSet<UploadHistory> UploadHistories { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<UserDepartment> UserDepartments { get; set; } = null!;
        public virtual DbSet<UserGroup> UserGroups { get; set; } = null!;
        public virtual DbSet<UserPerResGroup> UserPerResGroups { get; set; } = null!;
        public virtual DbSet<UserPerTag> UserPerTags { get; set; } = null!;
        public virtual DbSet<UserRequestBasis> UserRequestBases { get; set; } = null!;
        public virtual DbSet<UserRole> UserRoles { get; set; } = null!;
        public virtual DbSet<UserScore> UserScores { get; set; } = null!;
        public virtual DbSet<Visitor> Visitors { get; set; } = null!;
        public virtual DbSet<TagGroupDto> TagGroupDtos { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=WIN-9HQD014PA4B;Database=HcmUeQTTB_Dev;Persist Security Info=True;User ID=hung;Password=hung123@;Encrypt=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("Latin1_General_CI_AS");

            modelBuilder.Entity<GroupWithRoomsViewDto>().HasNoKey();
            modelBuilder.Entity<CleaningReportYearDto>().HasNoKey();
            modelBuilder.Entity<BlockReportDto>().HasNoKey();
            modelBuilder.Entity<CleaningReportCountDto>().HasNoKey();
            modelBuilder.Entity<CleaningReportDto>().HasNoKey();
            modelBuilder.Entity<ReportInADayValueDto>().HasNoKey();
            modelBuilder.Entity<CampusAverageValueDto>().HasNoKey();
            modelBuilder.Entity<ResponsiableGroupViewDto>().HasNoKey();
            modelBuilder.Entity<CriteriaValueDto>().HasNoKey();
            modelBuilder.Entity<TagGroupDto>().HasNoKey();
            modelBuilder.Entity<ResponsibleTagDto>().HasNoKey();
            modelBuilder.Entity<UserScoreDto>().HasNoKey();

            modelBuilder.Entity<Block>(entity =>
            {
                entity.Property(e => e.AssetTypeCode)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.AssetTypeName).HasMaxLength(100);

                entity.Property(e => e.BlockCode)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.BlockName).HasMaxLength(250);

                entity.Property(e => e.BlockName2).HasMaxLength(250);

                entity.Property(e => e.BlockNo).HasMaxLength(30);

                entity.Property(e => e.CampusCode)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CampusId).HasMaxLength(450);

                entity.Property(e => e.CampusName).HasMaxLength(100);

                entity.Property(e => e.CentralFunding).HasColumnType("money");

                entity.Property(e => e.Dvtcode)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("DVTCode");

                entity.Property(e => e.Dvtname)
                    .HasMaxLength(60)
                    .HasColumnName("DVTName");

                entity.Property(e => e.FunctionCode)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.FunctionName).HasMaxLength(250);

                entity.Property(e => e.LocalFunding).HasColumnType("money");

                entity.Property(e => e.ManageDepartmentCode)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.ManageDepartmentName).HasMaxLength(100);

                entity.Property(e => e.OriginalPrice).HasColumnType("money");

                entity.Property(e => e.OtherFunding).HasColumnType("money");

                entity.Property(e => e.StatusCode)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.StatusName).HasMaxLength(100);

                entity.Property(e => e.UseDepartmentCode)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.UseDepartmentName).HasMaxLength(100);

                entity.Property(e => e.ValueSettlement).HasColumnType("money");
            });

            modelBuilder.Entity<Application>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<Campus>(entity =>
            {
                entity.ToTable("Campus");

                entity.Property(e => e.CampusCode)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CampusName).HasMaxLength(100);

                entity.Property(e => e.CampusName2).HasMaxLength(100);

                entity.Property(e => e.CampusSymbol)
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Notes).HasMaxLength(60);
            });

            modelBuilder.Entity<CleaningForm>(entity =>
            {
                entity.ToTable("CleaningForm");

                entity.Property(e => e.FormName).HasMaxLength(250);

                entity.Property(e => e.RoomId).HasMaxLength(450);
            });

            modelBuilder.Entity<CleaningReport>(entity =>
            {
                entity.ToTable("CleaningReport");

                entity.Property(e => e.FormId).HasMaxLength(450);

                entity.Property(e => e.ShiftId).HasMaxLength(450);

                entity.Property(e => e.UserId).HasMaxLength(450);
            });

            modelBuilder.Entity<CriteriaReport>(entity =>
            {
                entity.ToTable("CriteriaReport");

                entity.Property(e => e.CriteriaId).HasMaxLength(450);

                entity.Property(e => e.FormId).HasMaxLength(450);

                entity.Property(e => e.ImageUrl).HasMaxLength(255);

                entity.Property(e => e.Note).HasMaxLength(450);

                entity.Property(e => e.ReportId).HasMaxLength(450);
            });

            modelBuilder.Entity<CriteriasPerForm>(entity =>
            {
                entity.ToTable("CriteriasPerForm");

                entity.Property(e => e.CriteriaId).HasMaxLength(450);

                entity.Property(e => e.FormId).HasMaxLength(450);
            });

            modelBuilder.Entity<Criteria>(entity =>
            {
                entity.Property(e => e.CriteriaName).HasMaxLength(250);

                entity.Property(e => e.CriteriaType).HasMaxLength(50);

                entity.Property(e => e.RoomCategoryId).HasMaxLength(450);

                entity.Property(e => e.Status).HasMaxLength(50);
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.Property(e => e.Campus).HasMaxLength(450);

                entity.Property(e => e.Code).HasMaxLength(256);

                entity.Property(e => e.IsRoom).HasDefaultValueSql("((0))");

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.RequestUserName)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasMany(d => d.Specializations)
                    .WithMany(p => p.Departments)
                    .UsingEntity<Dictionary<string, object>>(
                        "DepartmentSpecializationMapping",
                        l => l.HasOne<Specialization>().WithMany().HasForeignKey("SpecializationId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_DepartmentSpecializationMapping_Specializations"),
                        r => r.HasOne<Department>().WithMany().HasForeignKey("DepartmentId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_DepartmentSpecializationMapping_Departments"),
                        j =>
                        {
                            j.HasKey("DepartmentId", "SpecializationId");

                            j.ToTable("DepartmentSpecializationMapping");
                        });
            });

            modelBuilder.Entity<DepartmentRoleMapping>(entity =>
            {
                entity.ToTable("DepartmentRoleMapping");

                entity.Property(e => e.DepartmentId).HasMaxLength(450);

                entity.Property(e => e.RoleId).HasMaxLength(450);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.DepartmentRoleMappings)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DepartmentRoleMapping_Roles");
            });

            modelBuilder.Entity<Equipment>(entity =>
            {
                entity.Property(e => e.AssetGroupCode)
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.AssetGroupName).HasMaxLength(100);

                entity.Property(e => e.AssetTypeGroupCode)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.AssetTypeGroupName).HasMaxLength(100);

                entity.Property(e => e.BarCode)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Brand).HasMaxLength(60);

                entity.Property(e => e.CentralFunding).HasColumnType("money");

                entity.Property(e => e.Description).HasMaxLength(100);

                entity.Property(e => e.DriverName).HasMaxLength(100);

                entity.Property(e => e.Dvtcode)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("DVTCode");

                entity.Property(e => e.Dvtname)
                    .HasMaxLength(60)
                    .HasColumnName("DVTName");

                entity.Property(e => e.EquipmentCategoryId).HasMaxLength(450);

                entity.Property(e => e.EquipmentCode)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.EquipmentName).HasMaxLength(250);

                entity.Property(e => e.EquipmentName2).HasMaxLength(250);

                entity.Property(e => e.FatypeId)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("FATypeID");

                entity.Property(e => e.FrequencyUse).HasMaxLength(100);

                entity.Property(e => e.LicensePlates).HasMaxLength(60);

                entity.Property(e => e.LocalFunding).HasColumnType("money");

                entity.Property(e => e.ManageDepartmentCode)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.ManageDepartmentName).HasMaxLength(100);

                entity.Property(e => e.ManufactDate).HasColumnType("date");

                entity.Property(e => e.OriginalPrice).HasColumnType("money");

                entity.Property(e => e.OtherFunding).HasColumnType("money");

                entity.Property(e => e.ProductCountry).HasMaxLength(60);

                entity.Property(e => e.ProjectCode)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.ProjectName).HasMaxLength(250);

                entity.Property(e => e.RoomId).HasMaxLength(450);

                entity.Property(e => e.Specification).HasMaxLength(120);

                entity.Property(e => e.StatusCode)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.StatusName).HasMaxLength(100);

                entity.Property(e => e.SuperiorsAssetCode)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.UseDepartmentCode)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.UseDepartmentName).HasMaxLength(100);

                entity.Property(e => e.ValueSettlement).HasColumnType("money");

                entity.Property(e => e.VehicleType).HasMaxLength(60);

                entity.HasOne(d => d.EquipmentCategory)
                    .WithMany(p => p.Equipment)
                    .HasForeignKey(d => d.EquipmentCategoryId)
                    .HasConstraintName("FK_Equipment_EquipmentCategories");
            });

            modelBuilder.Entity<EquipmentCategory>(entity =>
            {
                entity.Property(e => e.CategoryCode).HasMaxLength(50);
            });

            modelBuilder.Entity<EquipmentCategoryGroup>(entity =>
            {
                entity.Property(e => e.GroupCode).HasMaxLength(50);
            });

            modelBuilder.Entity<Event>(entity =>
            {
                entity.Property(e => e.EventTypeId).HasMaxLength(450);

                entity.Property(e => e.ScheduleId).HasMaxLength(450);

                entity.Property(e => e.Title).HasMaxLength(255);

                entity.HasOne(d => d.Schedule)
                    .WithMany(p => p.Events)
                    .HasForeignKey(d => d.ScheduleId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Event_Schedule_ScheduleId");
            });

            modelBuilder.Entity<EventType>(entity =>
            {
                entity.ToTable("EventType");

                entity.Property(e => e.Name).HasMaxLength(255);
            });

            modelBuilder.Entity<FieldTemplate>(entity =>
            {
                entity.ToTable("FieldTemplate");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.DisplayOrder).HasDefaultValueSql("((0))");

                entity.Property(e => e.FieldName).HasMaxLength(100);

                entity.Property(e => e.FormTemplateId).HasMaxLength(450);

                entity.Property(e => e.InputKey)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.InputMask)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.IsDeactivate).HasDefaultValueSql("((0))");

                entity.Property(e => e.IsHiddenField).HasDefaultValueSql("((0))");

                entity.Property(e => e.IsRequired).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.FormTemplate)
                    .WithMany(p => p.FieldTemplates)
                    .HasForeignKey(d => d.FormTemplateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FieldTemplate_FormTemplate");
            });

            modelBuilder.Entity<Floor>(entity =>
            {
                entity.Property(e => e.Description).HasMaxLength(100);

                entity.Property(e => e.FloorCode)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.FloorName).HasMaxLength(100);

                entity.Property(e => e.Notes).HasMaxLength(60);
            });

            modelBuilder.Entity<FloorOfBlock>(entity =>
            {
                entity.Property(e => e.BlockId).HasMaxLength(450);

                entity.Property(e => e.FloorId).HasMaxLength(450);
            });

            modelBuilder.Entity<FormCollection>(entity =>
            {
                entity.ToTable("FormCollection");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.Name).HasMaxLength(250);
            });

            modelBuilder.Entity<FormTemplate>(entity =>
            {
                entity.ToTable("FormTemplate");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.CreatedById).HasMaxLength(450);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DepartmentId).HasMaxLength(450);

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.ExpiratedDate).HasColumnType("datetime");

                entity.Property(e => e.FormCollectionId).HasMaxLength(450);

                entity.Property(e => e.LastModified)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name).HasMaxLength(250);

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.FormTemplates)
                    .HasForeignKey(d => d.DepartmentId)
                    .HasConstraintName("FK_FormTemplate_Department");

                entity.HasOne(d => d.FormCollection)
                    .WithMany(p => p.FormTemplates)
                    .HasForeignKey(d => d.FormCollectionId)
                    .HasConstraintName("FK_FormTemplate_FormCollection");
            });

            modelBuilder.Entity<FormTemplateField>(entity =>
            {
                entity.ToTable("FormTemplateField");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.DisplayOrder).HasDefaultValueSql("((0))");

                entity.Property(e => e.FieldName).HasMaxLength(100);

                entity.Property(e => e.FieldTemplateId).HasMaxLength(450);

                entity.Property(e => e.FormTemplateId).HasMaxLength(450);

                entity.Property(e => e.InputKey)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.InputMask)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.IsDeactivate).HasDefaultValueSql("((0))");

                entity.Property(e => e.IsHiddenField).HasDefaultValueSql("((0))");

                entity.Property(e => e.IsRequired).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.FieldTemplate)
                    .WithMany(p => p.FormTemplateFields)
                    .HasForeignKey(d => d.FieldTemplateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FormTemplateField_FieldTemplate");

                entity.HasOne(d => d.FormTemplate)
                    .WithMany(p => p.FormTemplateFields)
                    .HasForeignKey(d => d.FormTemplateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FormTemplateField_FormTemplate");
            });

            modelBuilder.Entity<FormTemplateGroup>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.FormTemplateId).HasMaxLength(450);

                entity.Property(e => e.GroupId).HasMaxLength(450);

                entity.Property(e => e.GroupName).HasMaxLength(450);

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.FormTemplateGroups)
                    .HasForeignKey(d => d.GroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FormTemplateGroups_Group");
            });

            modelBuilder.Entity<FormTemplateProcess>(entity =>
            {
                entity.ToTable("FormTemplateProcess");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.ExpiratedDate).HasColumnType("datetime");

                entity.Property(e => e.FormTemplateId).HasMaxLength(450);

                entity.Property(e => e.NextProcessIfDeniedId).HasMaxLength(450);

                entity.Property(e => e.ProcessName).HasMaxLength(100);

                entity.Property(e => e.ProcessOrder).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.FormTemplate)
                    .WithMany(p => p.FormTemplateProcesses)
                    .HasForeignKey(d => d.FormTemplateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FormTemplateProcess_FormTemplate");

                entity.HasOne(d => d.NextProcessIfDenied)
                    .WithMany(p => p.InverseNextProcessIfDenied)
                    .HasForeignKey(d => d.NextProcessIfDeniedId)
                    .HasConstraintName("FK_NextProcessIfDeniedId_NextProcessIfDeniedId");
            });

            modelBuilder.Entity<FormTemplateProcessApprover>(entity =>
            {
                entity.ToTable("FormTemplateProcessApprover");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.DepartmentId).HasMaxLength(450);

                entity.Property(e => e.FormTemplateProcessId).HasMaxLength(450);

                entity.Property(e => e.GroupId).HasMaxLength(450);

                entity.Property(e => e.UserId).HasMaxLength(450);

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.FormTemplateProcessApprovers)
                    .HasForeignKey(d => d.DepartmentId)
                    .HasConstraintName("FK_FormTemplateProcessApprover_Department");

                entity.HasOne(d => d.FormTemplateProcess)
                    .WithMany(p => p.FormTemplateProcessApprovers)
                    .HasForeignKey(d => d.FormTemplateProcessId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FormTemplateProcessApprover_FormTemplateProcess");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.FormTemplateProcessApprovers)
                    .HasForeignKey(d => d.GroupId)
                    .HasConstraintName("FK_FormTemplateProcessApprover_Group");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.FormTemplateProcessApprovers)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_FormTemplateProcessApprover_User");
            });

            modelBuilder.Entity<FormTemplateProcessTrigger>(entity =>
            {
                entity.ToTable("FormTemplateProcessTrigger");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.ApiUrl).IsUnicode(false);

                entity.Property(e => e.ContentType)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.FormTemplateProcessId).HasMaxLength(450);

                entity.Property(e => e.HttpMethod)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.TriggerName).HasMaxLength(100);

                entity.HasOne(d => d.FormTemplateProcess)
                    .WithMany(p => p.FormTemplateProcessTriggers)
                    .HasForeignKey(d => d.FormTemplateProcessId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FormTemplateProcessTrigger_FormTemplateProcess");
            });

            modelBuilder.Entity<FormTemplateWatcher>(entity =>
            {
                entity.ToTable("FormTemplateWatcher");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Email).HasMaxLength(450);

                entity.Property(e => e.FormTemplateId).HasMaxLength(450);

                entity.Property(e => e.FullName).HasMaxLength(450);

                entity.Property(e => e.UserId).HasMaxLength(450);

                entity.Property(e => e.UserName).HasMaxLength(450);

                entity.HasOne(d => d.FormTemplate)
                    .WithMany(p => p.FormTemplateWatchers)
                    .HasForeignKey(d => d.FormTemplateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FormTemplateWatcher_FormTemplate");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.FormTemplateWatchers)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FormTemplateWatcher_User");
            });

            modelBuilder.Entity<Group>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Description).HasMaxLength(450);

                entity.Property(e => e.Name).HasMaxLength(450);
            });

            modelBuilder.Entity<GroupRoom>(entity =>
            {
                entity.ToTable("GroupRoom");

                entity.HasIndex(e => e.Id, "UQ__GroupRoo__3214EC06B34C7557")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasMaxLength(450)
                    .IsUnicode(false);

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.GroupName).HasMaxLength(255);
            });

            modelBuilder.Entity<Item>(entity =>
            {
                entity.ToTable("Item");

                entity.HasIndex(e => e.GroupCode, "idx_groupCode");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Code).HasMaxLength(100);

                entity.Property(e => e.GroupCode)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Name).HasMaxLength(450);

                entity.Property(e => e.Specification).HasMaxLength(250);

                entity.Property(e => e.Trademark).HasMaxLength(250);

                entity.Property(e => e.Unit)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ItemGroup>(entity =>
            {
                entity.ToTable("ItemGroup");

                entity.HasIndex(e => e.Code, "uidx_groupCode")
                    .IsUnique();

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Code).HasMaxLength(100);

                entity.Property(e => e.Name).HasMaxLength(450);
            });

            modelBuilder.Entity<Lesson>(entity =>
            {
                entity.ToTable("Lesson");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Code).HasMaxLength(100);

                entity.Property(e => e.DepartmentId).HasMaxLength(450);

                entity.Property(e => e.Description).HasMaxLength(450);

                entity.Property(e => e.FromDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.OnlineLearningRate).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.RoomId).HasMaxLength(450);

                entity.Property(e => e.RoomName).HasMaxLength(450);

                entity.Property(e => e.SectionId).HasMaxLength(450);

                entity.Property(e => e.SpecializationId).HasMaxLength(450);

                entity.Property(e => e.ToDate).HasColumnType("datetime");

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.Lessons)
                    .HasForeignKey(d => d.DepartmentId)
                    .HasConstraintName("FK_Lesson_Department");

                entity.HasOne(d => d.Room)
                    .WithMany(p => p.Lessons)
                    .HasForeignKey(d => d.RoomId)
                    .HasConstraintName("FK_Lesson_Room");

                entity.HasOne(d => d.Section)
                    .WithMany(p => p.Lessons)
                    .HasForeignKey(d => d.SectionId)
                    .HasConstraintName("FK_Lesson_Section");

                entity.HasOne(d => d.Specialization)
                    .WithMany(p => p.Lessons)
                    .HasForeignKey(d => d.SpecializationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Lesson_Specialization");
            });

            modelBuilder.Entity<LessonPractice>(entity =>
            {
                entity.ToTable("LessonPractice");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Description).HasMaxLength(450);

                entity.Property(e => e.FromDate).HasColumnType("datetime");

                entity.Property(e => e.LessonCode).HasMaxLength(100);

                entity.Property(e => e.LessonId).HasMaxLength(450);

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.ToDate).HasColumnType("datetime");

                entity.HasOne(d => d.Lesson)
                    .WithMany(p => p.LessonPractices)
                    .HasForeignKey(d => d.LessonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LessonPractice_Lessons");
            });

            modelBuilder.Entity<LessonPracticeQuotum>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.CompleteQuantification).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.Depreciation).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.ItemGroupCode).HasMaxLength(256);

                entity.Property(e => e.ItemGroupId).HasMaxLength(450);

                entity.Property(e => e.ItemId).HasMaxLength(450);

                entity.Property(e => e.ItemName).HasMaxLength(500);

                entity.Property(e => e.LessonPracticeId).HasMaxLength(450);

                entity.Property(e => e.Quota).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.Result).HasMaxLength(1000);

                entity.Property(e => e.Unit).HasMaxLength(50);

                entity.HasOne(d => d.ItemGroup)
                    .WithMany(p => p.LessonPracticeQuota)
                    .HasForeignKey(d => d.ItemGroupId)
                    .HasConstraintName("FK_LessonPracticeQuota_ItemGroup");
            });

            modelBuilder.Entity<Notification>(entity =>
            {
                entity.Property(e => e.ClassId).HasMaxLength(450);

                entity.Property(e => e.CreatedBy).HasMaxLength(450);

                entity.Property(e => e.ModifiedBy).HasMaxLength(450);

                entity.Property(e => e.Title).HasMaxLength(1000);

                entity.Property(e => e.UserId).HasMaxLength(450);
            });

            modelBuilder.Entity<Permission>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Description).HasMaxLength(450);

                entity.Property(e => e.Name).HasMaxLength(200);
            });

            modelBuilder.Entity<PermissionRoleMapping>(entity =>
            {
                entity.ToTable("PermissionRoleMapping");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.PermissionId).HasMaxLength(450);

                entity.Property(e => e.RoleId).HasMaxLength(450);

                entity.HasOne(d => d.Permission)
                    .WithMany(p => p.PermissionRoleMappings)
                    .HasForeignKey(d => d.PermissionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PermissionRoleMapping_Permissions");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.PermissionRoleMappings)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PermissionRoleMapping_Roles");
            });

            modelBuilder.Entity<PersistedGrant>(entity =>
            {
                entity.HasKey(e => e.Key);

                entity.Property(e => e.Key).HasMaxLength(200);

                entity.Property(e => e.ClientId).HasMaxLength(200);

                entity.Property(e => e.SubjectId).HasMaxLength(200);

                entity.Property(e => e.Type).HasMaxLength(50);
            });

            modelBuilder.Entity<Repair>(entity =>
            {
                entity.Property(e => e.RequestId).HasMaxLength(450);
            });

            modelBuilder.Entity<Request>(entity =>
            {
                entity.Property(e => e.EquipmentCategoryId).HasMaxLength(450);

                entity.Property(e => e.EquipmentId).HasMaxLength(450);

                entity.Property(e => e.RequestorEmail).HasMaxLength(250);

                entity.Property(e => e.RequestorMobile).HasMaxLength(30);

                entity.Property(e => e.RoomId).HasMaxLength(450);

                entity.HasOne(d => d.Equipment)
                    .WithMany(p => p.Requests)
                    .HasForeignKey(d => d.EquipmentId)
                    .HasConstraintName("FK_Requests_Equipment");
            });

            modelBuilder.Entity<RequestForm>(entity =>
            {
                entity.ToTable("RequestForm");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.FormTemplateId).HasMaxLength(450);

                entity.Property(e => e.LastModified).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(250);

                entity.Property(e => e.NextWorkFlowId).HasMaxLength(450);

                entity.Property(e => e.UserId).HasMaxLength(450);

                entity.HasOne(d => d.FormTemplate)
                    .WithMany(p => p.RequestForms)
                    .HasForeignKey(d => d.FormTemplateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RequestForm_RequestForm");

                entity.HasOne(d => d.NextWorkFlow)
                    .WithMany(p => p.RequestForms)
                    .HasForeignKey(d => d.NextWorkFlowId)
                    .HasConstraintName("FK_RequestForm_RequestFormHandleWorkFlow");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.RequestForms)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RequestForm_User");
            });

            modelBuilder.Entity<RequestFormActivityLog>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Note).HasMaxLength(100);

                entity.Property(e => e.RequestFormId).HasMaxLength(450);

                entity.Property(e => e.UserId).HasMaxLength(450);

                entity.HasOne(d => d.RequestForm)
                    .WithMany(p => p.RequestFormActivityLogs)
                    .HasForeignKey(d => d.RequestFormId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RequestFormActivityLogs_RequestForm");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.RequestFormActivityLogs)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RequestFormActivityLogs_User");
            });

            modelBuilder.Entity<RequestFormCommunicate>(entity =>
            {
                entity.ToTable("RequestFormCommunicate");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsRead).HasDefaultValueSql("((0))");

                entity.Property(e => e.ReadAt).HasColumnType("datetime");

                entity.Property(e => e.RequestFormId).HasMaxLength(450);

                entity.Property(e => e.RootId).HasMaxLength(450);

                entity.Property(e => e.UserId).HasMaxLength(450);

                entity.HasOne(d => d.RequestForm)
                    .WithMany(p => p.RequestFormCommunicates)
                    .HasForeignKey(d => d.RequestFormId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RequestFormCommunicate_RequestForm");

                entity.HasOne(d => d.Root)
                    .WithMany(p => p.InverseRoot)
                    .HasForeignKey(d => d.RootId)
                    .HasConstraintName("FK_RequestFormCommunicate_RequestFormCommunicate");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.RequestFormCommunicates)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RequestFormCommunicate_User");
            });

            modelBuilder.Entity<RequestFormField>(entity =>
            {
                entity.ToTable("RequestFormField");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.FieldName).HasMaxLength(100);

                entity.Property(e => e.InputKey)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.InputMask)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RequestFormId).HasMaxLength(450);

                entity.HasOne(d => d.RequestForm)
                    .WithMany(p => p.RequestFormFields)
                    .HasForeignKey(d => d.RequestFormId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RequestFormField_RequestForm");
            });

            modelBuilder.Entity<RequestFormGroup>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.GroupId).HasMaxLength(450);

                entity.Property(e => e.GroupName).HasMaxLength(450);

                entity.Property(e => e.RequestFormId).HasMaxLength(450);

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.RequestFormGroups)
                    .HasForeignKey(d => d.GroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RequestFormGroups_Group");

                entity.HasOne(d => d.RequestForm)
                    .WithMany(p => p.RequestFormGroups)
                    .HasForeignKey(d => d.RequestFormId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RequestFormGroups_RequestForm");
            });

            modelBuilder.Entity<RequestFormHandleWorkFlow>(entity =>
            {
                entity.ToTable("RequestFormHandleWorkFlow");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.LastModified)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Note).HasMaxLength(100);

                entity.Property(e => e.RequestFormId).HasMaxLength(450);

                entity.Property(e => e.RequestFormProcessId).HasMaxLength(450);

                entity.HasOne(d => d.RequestForm)
                    .WithMany(p => p.RequestFormHandleWorkFlows)
                    .HasForeignKey(d => d.RequestFormId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RequestFormHandleWorkFlow_RequestForm");

                entity.HasOne(d => d.RequestFormProcess)
                    .WithMany(p => p.RequestFormHandleWorkFlows)
                    .HasForeignKey(d => d.RequestFormProcessId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RequestFormHandleWorkFlow_RequestFormProcess");
            });

            modelBuilder.Entity<RequestFormProcess>(entity =>
            {
                entity.ToTable("RequestFormProcess");

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.ExpiratedDate).HasColumnType("datetime");

                entity.Property(e => e.NextProcessIfDeniedId).HasMaxLength(450);

                entity.Property(e => e.ProcessName).HasMaxLength(100);

                entity.Property(e => e.ProcessOrder).HasDefaultValueSql("((0))");

                entity.Property(e => e.RequestFormId).HasMaxLength(450);

                entity.HasOne(d => d.NextProcessIfDenied)
                    .WithMany(p => p.InverseNextProcessIfDenied)
                    .HasForeignKey(d => d.NextProcessIfDeniedId)
                    .HasConstraintName("FK_RequestFormProcess_RequestFormProcessd");

                entity.HasOne(d => d.RequestForm)
                    .WithMany(p => p.RequestFormProcesses)
                    .HasForeignKey(d => d.RequestFormId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RequestFormProcess_RequestForm");
            });

            modelBuilder.Entity<RequestFormProcessApprover>(entity =>
            {
                entity.ToTable("RequestFormProcessApprover");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Approver).HasDefaultValueSql("((0))");

                entity.Property(e => e.DepartmentId).HasMaxLength(450);

                entity.Property(e => e.GroupId).HasMaxLength(450);

                entity.Property(e => e.RequestFormProcessId).HasMaxLength(450);

                entity.Property(e => e.UserId).HasMaxLength(450);

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.RequestFormProcessApprovers)
                    .HasForeignKey(d => d.DepartmentId)
                    .HasConstraintName("FK_RequestFormProcessApprover_Department");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.RequestFormProcessApprovers)
                    .HasForeignKey(d => d.GroupId)
                    .HasConstraintName("FK_RequestFormProcessApprover_Group");

                entity.HasOne(d => d.RequestFormProcess)
                    .WithMany(p => p.RequestFormProcessApprovers)
                    .HasForeignKey(d => d.RequestFormProcessId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RequestFormProcessApprover_RequestFormProcess");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.RequestFormProcessApprovers)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_RequestFormProcessApprover_User");
            });

            modelBuilder.Entity<RequestFormProcessTrigger>(entity =>
            {
                entity.ToTable("RequestFormProcessTrigger");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.ApiUrl).IsUnicode(false);

                entity.Property(e => e.ContentType)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.HttpMethod)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.RequestFormProcessId).HasMaxLength(450);

                entity.Property(e => e.TriggerName).HasMaxLength(100);

                entity.HasOne(d => d.RequestFormProcess)
                    .WithMany(p => p.RequestFormProcessTriggers)
                    .HasForeignKey(d => d.RequestFormProcessId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RequestFormProcessTrigger_RequestFormProcess");
            });

            modelBuilder.Entity<RequestFormWatcher>(entity =>
            {
                entity.ToTable("RequestFormWatcher");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Email).HasMaxLength(450);

                entity.Property(e => e.FullName).HasMaxLength(450);

                entity.Property(e => e.RequestFormId).HasMaxLength(450);

                entity.Property(e => e.UserId).HasMaxLength(450);

                entity.Property(e => e.UserName).HasMaxLength(450);

                entity.HasOne(d => d.RequestForm)
                    .WithMany(p => p.RequestFormWatchers)
                    .HasForeignKey(d => d.RequestFormId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RequestFormWatcher_RequestForm");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.RequestFormWatchers)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RequestFormWatcher_User");
            });

            modelBuilder.Entity<ResponsibleGroup>(entity =>
            {
                entity.ToTable("ResponsibleGroup");

                entity.HasIndex(e => e.Id, "UQ__Responsi__3214EC0635275CFB")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasMaxLength(450)
                    .IsUnicode(false);

                entity.Property(e => e.Color)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Description).HasMaxLength(450);

                entity.Property(e => e.GroupName).HasMaxLength(450);
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(256);
            });

            modelBuilder.Entity<Room>(entity =>
            {
                entity.Property(e => e.AssetTypeCode)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.AssetTypeName).HasMaxLength(100);

                entity.Property(e => e.BlockId).HasMaxLength(450);

                entity.Property(e => e.CentralFunding).HasColumnType("money");

                entity.Property(e => e.Description).HasMaxLength(250);

                entity.Property(e => e.Dvtcode)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("DVTCode");

                entity.Property(e => e.Dvtname)
                    .HasMaxLength(60)
                    .HasColumnName("DVTName");

                entity.Property(e => e.FloorId).HasMaxLength(450);

                entity.Property(e => e.LocalFunding).HasColumnType("money");

                entity.Property(e => e.ManageDepartmentCode)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.ManageDepartmentName).HasMaxLength(100);

                entity.Property(e => e.OriginalPrice).HasColumnType("money");

                entity.Property(e => e.OtherFunding).HasColumnType("money");

                entity.Property(e => e.RoomCategoryId).HasMaxLength(450);

                entity.Property(e => e.RoomCode)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.RoomName).HasMaxLength(250);

                entity.Property(e => e.RoomNo).HasMaxLength(30);

                entity.Property(e => e.StatusCode)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.StatusName).HasMaxLength(100);

                entity.Property(e => e.UseDepartmentCode)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.UseDepartmentName).HasMaxLength(100);

                entity.Property(e => e.ValueSettlement).HasColumnType("money");

                entity.HasOne(d => d.RoomCategory)
                    .WithMany(p => p.Rooms)
                    .HasForeignKey(d => d.RoomCategoryId)
                    .HasConstraintName("FK_Room_RoomCategories");
            });

            modelBuilder.Entity<RoomByGroup>(entity =>
            {
                entity.ToTable("RoomByGroup");

                entity.HasIndex(e => e.Id, "UQ__RoomByGr__3214EC06203CD7EE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasMaxLength(450)
                    .IsUnicode(false);

                entity.Property(e => e.GroupRoomId)
                    .HasMaxLength(450)
                    .IsUnicode(false);

                entity.Property(e => e.RoomId)
                    .HasMaxLength(450)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<RoomCategory>(entity =>
            {
                entity.Property(e => e.CategoryCode).HasMaxLength(255);
            });

            modelBuilder.Entity<RoomSchedule>(entity =>
            {
                entity.Property(e => e.CreatorEmail).HasMaxLength(255);

                entity.Property(e => e.CreatorName).HasMaxLength(255);

                entity.Property(e => e.CreatorPhone).HasMaxLength(50);

                entity.Property(e => e.EndDate).HasColumnType("date");

                entity.Property(e => e.RoomId).HasMaxLength(450);

                entity.Property(e => e.ScheduleCode).HasMaxLength(255);

                entity.Property(e => e.StartDate).HasColumnType("date");

                entity.Property(e => e.SubjectCode).HasMaxLength(255);

                entity.Property(e => e.SubjectName).HasMaxLength(250);

                entity.Property(e => e.TeacherCode).HasMaxLength(20);

                entity.Property(e => e.TeacherName).HasMaxLength(100);
            });

            modelBuilder.Entity<Schedule>(entity =>
            {
                entity.ToTable("Schedule");

                entity.HasIndex(e => e.Id, "UQ__Schedule__3214EC063E1DD9DB")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasMaxLength(450)
                    .IsUnicode(false);

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.RecurrenceRule).HasMaxLength(255);

                entity.Property(e => e.ResponsibleGroupId)
                    .HasMaxLength(450)
                    .IsUnicode(false);

                entity.Property(e => e.Title).HasMaxLength(255);
            });

            modelBuilder.Entity<ScheduleDetail>(entity =>
            {
                entity.ToTable("ScheduleDetail");

                entity.HasIndex(e => e.Id, "UQ__Schedule__3214EC06E036C730")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasMaxLength(450)
                    .IsUnicode(false);

                entity.Property(e => e.RoomId)
                    .HasMaxLength(450)
                    .IsUnicode(false);

                entity.Property(e => e.RoomType).HasMaxLength(255);

                entity.Property(e => e.ScheduleId)
                    .HasMaxLength(450)
                    .IsUnicode(false);

                entity.Property(e => e.UserId)
                    .HasMaxLength(450)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Section>(entity =>
            {
                entity.ToTable("Section");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.DepartmentId).HasMaxLength(450);

                entity.Property(e => e.Name).HasMaxLength(250);

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.Sections)
                    .HasForeignKey(d => d.DepartmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Section_Department");
            });

            modelBuilder.Entity<Shift>(entity =>
            {
                entity.ToTable("Shift");

                entity.Property(e => e.RoomCategoryId).HasMaxLength(450);

                entity.Property(e => e.ShiftName).HasMaxLength(250);

                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .HasDefaultValueSql("('ENABLE')");
            });

            modelBuilder.Entity<Specialization>(entity =>
            {
                entity.ToTable("Specialization");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Code).HasMaxLength(20);

                entity.Property(e => e.Description).HasMaxLength(450);

                entity.Property(e => e.Name).HasMaxLength(256);
            });

            modelBuilder.Entity<SpecializationAdditional>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("SpecializationAdditional");

                entity.HasIndex(e => e.SpecializationId, "UQ__Speciali__5809D86E8B4DAC65")
                    .IsUnique();

                entity.Property(e => e.PeriodId).HasMaxLength(450);

                entity.Property(e => e.Periods).HasMaxLength(250);

                entity.Property(e => e.Year).HasColumnType("datetime");

                entity.HasOne(d => d.Specialization)
                    .WithOne()
                    .HasForeignKey<SpecializationAdditional>(d => d.SpecializationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SpecializationAdditional_Specialization");
            });

            modelBuilder.Entity<Staff>(entity =>
            {
                entity.Property(e => e.Department).HasMaxLength(256);

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.EmployeeCode).HasMaxLength(256);

                entity.Property(e => e.Position).HasMaxLength(256);

                entity.Property(e => e.Rfid)
                    .HasMaxLength(450)
                    .HasColumnName("rfid");

                entity.Property(e => e.TeacherName).HasMaxLength(256);
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.Property(e => e.Department).HasMaxLength(256);

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.PhoneNumber).HasMaxLength(30);

                entity.Property(e => e.Rfid)
                    .HasMaxLength(450)
                    .HasColumnName("rfid");

                entity.Property(e => e.SchoolYear).HasMaxLength(30);

                entity.Property(e => e.StudentCode).HasMaxLength(256);

                entity.Property(e => e.StudentName).HasMaxLength(256);
            });

            modelBuilder.Entity<Tag>(entity =>
            {
                entity.ToTable("Tag");

                entity.Property(e => e.TagName).HasMaxLength(250);
            });

            modelBuilder.Entity<TagsPerCriteria>(entity =>
            {
                entity.Property(e => e.CriteriaId).HasMaxLength(450);

                entity.Property(e => e.TagId).HasMaxLength(450);
            });

            modelBuilder.Entity<UploadHistory>(entity =>
            {
                entity.Property(e => e.CreatedBy).HasMaxLength(450);

                entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.InfoNo).HasMaxLength(50);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.EmployeeCode).HasMaxLength(256);

                entity.Property(e => e.ExternalId).HasMaxLength(450);

                entity.Property(e => e.FirstName).HasMaxLength(256);

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.LastName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);
            });

            modelBuilder.Entity<UserDepartment>(entity =>
            {
                entity.Property(e => e.DepartmentId).HasMaxLength(450);

                entity.Property(e => e.UserId).HasMaxLength(450);

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.UserDepartments)
                    .HasForeignKey(d => d.DepartmentId)
                    .HasConstraintName("FK_UserDepartments_Departments");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserDepartments)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_UserDepartments_User");
            });

            modelBuilder.Entity<UserGroup>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.GroupId).HasMaxLength(450);

                entity.Property(e => e.IsAdmin).HasDefaultValueSql("((0))");

                entity.Property(e => e.UserId).HasMaxLength(450);

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.UserGroups)
                    .HasForeignKey(d => d.GroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserGroup_Group");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserGroups)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserGroup_User");
            });

            modelBuilder.Entity<UserPerResGroup>(entity =>
            {
                entity.ToTable("UserPerResGroup");

                entity.HasIndex(e => e.Id, "UQ__UserPerR__3214EC06C90B60BB")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasMaxLength(450)
                    .IsUnicode(false);

                entity.Property(e => e.ResponsiableGroupId)
                    .HasMaxLength(450)
                    .IsUnicode(false);

                entity.Property(e => e.UserId)
                    .HasMaxLength(450)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<UserPerTag>(entity =>
            {
                entity.ToTable("UserPerTag");

                entity.Property(e => e.TagId).HasMaxLength(450);

                entity.Property(e => e.UserId).HasMaxLength(450);
            });

            modelBuilder.Entity<UserRequestBasis>(entity =>
            {
                entity.Property(e => e.Content).HasMaxLength(1000);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Email).HasMaxLength(450);

                entity.Property(e => e.RequestBaseGid)
                    .HasMaxLength(450)
                    .HasColumnName("RequestBaseGId");

                entity.Property(e => e.RequestBaseHid)
                    .HasMaxLength(450)
                    .HasColumnName("RequestBaseHId");

                entity.Property(e => e.RequestBaseId).HasMaxLength(450);

                entity.Property(e => e.Title).HasMaxLength(1000);

                entity.Property(e => e.UserId).HasMaxLength(450);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserRequestBases)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserRequestBases_User");
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });
            });

            modelBuilder.Entity<UserScore>(entity =>
            {
                entity.ToTable("UserScore");

                entity.HasIndex(e => e.Id, "UQ__UserScor__3214EC064E5EE63A")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasMaxLength(450)
                    .IsUnicode(false);

                entity.Property(e => e.CreateAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ReportId)
                    .HasMaxLength(450)
                    .IsUnicode(false)
                    .HasColumnName("reportId");

                entity.Property(e => e.Score).HasColumnName("score");

                entity.Property(e => e.TagId)
                    .HasMaxLength(450)
                    .IsUnicode(false)
                    .HasColumnName("tagId");

                entity.Property(e => e.UpdateAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UserId)
                    .HasMaxLength(450)
                    .IsUnicode(false)
                    .HasColumnName("userId");
            });

            modelBuilder.Entity<Visitor>(entity =>
            {
                entity.Property(e => e.Department).HasMaxLength(256);

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.Rfid)
                    .HasMaxLength(450)
                    .HasColumnName("rfid");

                entity.Property(e => e.VisitorCode).HasMaxLength(256);

                entity.Property(e => e.VisitorName).HasMaxLength(256);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
