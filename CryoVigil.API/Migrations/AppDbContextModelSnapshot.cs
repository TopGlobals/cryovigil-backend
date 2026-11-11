using System;
using System.Collections.Generic;
using CryoVigil.API.Alerts.Domain.Model.ValueObjects;
using CryoVigil.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CryoVigil.API.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "10.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("CryoVigil.API.Alerts.Domain.Model.Aggregates.Alert", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<DateTime?>("AcknowledgedAt")
                        .HasColumnType("datetime")
                        .HasColumnName("acknowledged_at");

                    b.Property<string>("AcknowledgedBy")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("acknowledged_by");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("code");

                    b.Property<string>("LaboratoryName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("laboratory_name");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)")
                        .HasColumnName("message");

                    b.Property<double?>("ReadingCelsius")
                        .HasColumnType("double")
                        .HasColumnName("reading_celsius");

                    b.Property<DateTime>("RaisedAt")
                        .HasColumnType("datetime")
                        .HasColumnName("raised_at");

                    b.Property<DateTime?>("ResolvedAt")
                        .HasColumnType("datetime")
                        .HasColumnName("resolved_at");

                    b.Property<string>("ResolvedBy")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("resolved_by");

                    b.Property<double?>("SafeMaxCelsius")
                        .HasColumnType("double")
                        .HasColumnName("safe_max_celsius");

                    b.Property<double?>("SafeMinCelsius")
                        .HasColumnType("double")
                        .HasColumnName("safe_min_celsius");

                    b.Property<string>("SensorCode")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("sensor_code");

                    b.Property<string>("Severity")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)")
                        .HasColumnName("severity");

                    b.Property<string>("SourceType")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("source_type");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)")
                        .HasColumnName("status");

                    b.Property<string>("StorageUnitName")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("storage_unit_name");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("varchar(80)")
                        .HasColumnName("title");

                    b.HasKey("Id")
                        .HasName("pk_alerts");

                    b.HasIndex("Code")
                        .IsUnique()
                        .HasDatabaseName("ix_alerts_code");

                    b.ToTable("alerts", (string)null);
                });

            modelBuilder.Entity("CryoVigil.API.Alerts.Domain.Model.Entities.AlertReading", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<int>("AlertId")
                        .HasColumnType("int")
                        .HasColumnName("alert_id");

                    b.Property<DateTime>("TakenAt")
                        .HasColumnType("datetime")
                        .HasColumnName("taken_at");

                    b.Property<double>("ValueCelsius")
                        .HasColumnType("double")
                        .HasColumnName("value_celsius");

                    b.HasKey("Id")
                        .HasName("pk_alert_readings");

                    b.HasIndex("AlertId")
                        .HasDatabaseName("ix_alert_readings_alert_id");

                    b.ToTable("alert_readings", (string)null);
                });

            modelBuilder.Entity("CryoVigil.API.Alerts.Domain.Model.Entities.CorrectiveAction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<int>("AlertId")
                        .HasColumnType("int")
                        .HasColumnName("alert_id");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)")
                        .HasColumnName("description");

                    b.Property<string>("TechnicianName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("technician_name");

                    b.Property<DateTime>("TakenAt")
                        .HasColumnType("datetime")
                        .HasColumnName("taken_at");

                    b.HasKey("Id")
                        .HasName("pk_corrective_actions");

                    b.HasIndex("AlertId")
                        .HasDatabaseName("ix_corrective_actions_alert_id");

                    b.ToTable("corrective_actions", (string)null);
                });

            modelBuilder.Entity("CryoVigil.API.History.Domain.Model.Aggregates.HistoryEvent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<List<string>>("ActionsTakenKeys")
                        .HasMaxLength(1000)
                        .HasColumnType("varchar(1000)")
                        .HasColumnName("actions_taken_keys");

                    b.Property<bool>("IsResolved")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("is_resolved");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("location");

                    b.Property<string>("MessageKey")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)")
                        .HasColumnName("message_key");

                    b.Property<string>("Sensor")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("sensor");

                    b.Property<string>("Severity")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)")
                        .HasColumnName("severity");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("datetime")
                        .HasColumnName("timestamp");

                    b.Property<string>("TitleKey")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("title_key");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("type");

                    b.HasKey("Id")
                        .HasName("pk_history_events");

                    b.ToTable("history_events", (string)null);
                });

            modelBuilder.Entity("CryoVigil.API.Laboratories.Domain.Model.Aggregates.Laboratory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<int?>("AirQuality")
                        .HasColumnType("int")
                        .HasColumnName("air_quality");

                    b.Property<string>("Building")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("building");

                    b.Property<int>("Co2Max")
                        .HasColumnType("int")
                        .HasColumnName("co2_max");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("code");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)")
                        .HasColumnName("description");

                    b.Property<string>("Escalation")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)")
                        .HasColumnName("escalation");

                    b.Property<string>("Floor")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)")
                        .HasColumnName("floor");

                    b.Property<string>("GasSensitivity")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)")
                        .HasColumnName("gas_sensitivity");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("name");

                    b.Property<string>("Room")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("room");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)")
                        .HasColumnName("status");

                    b.Property<decimal?>("Temperature")
                        .HasPrecision(8, 2)
                        .HasColumnType("decimal(8,2)")
                        .HasColumnName("temperature");

                    b.Property<decimal>("TemperatureMax")
                        .HasPrecision(8, 2)
                        .HasColumnType("decimal(8,2)")
                        .HasColumnName("temperature_max");

                    b.Property<decimal>("TemperatureMin")
                        .HasPrecision(8, 2)
                        .HasColumnType("decimal(8,2)")
                        .HasColumnName("temperature_min");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)")
                        .HasColumnName("type");

                    b.Property<int?>("Unknown")
                        .HasColumnType("int")
                        .HasColumnName("unknown");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("updated_at");

                    b.Property<decimal>("VibrationMax")
                        .HasPrecision(8, 2)
                        .HasColumnType("decimal(8,2)")
                        .HasColumnName("vibration_max");

                    b.HasKey("Id")
                        .HasName("pk_laboratories");

                    b.HasIndex("Code")
                        .IsUnique()
                        .HasDatabaseName("ix_laboratories_code");

                    b.ToTable("laboratories", (string)null);
                });

            modelBuilder.Entity("CryoVigil.API.Settings.Domain.Model.Aggregates.GeneralSettings", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<string>("Region")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("region");

                    b.Property<string>("TemperatureUnit")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)")
                        .HasColumnName("temperature_unit");

                    b.HasKey("Id")
                        .HasName("pk_general_settings");

                    b.ToTable("general_settings", (string)null);
                });

            modelBuilder.Entity("CryoVigil.API.Alerts.Domain.Model.Entities.AlertReading", b =>
                {
                    b.HasOne("CryoVigil.API.Alerts.Domain.Model.Aggregates.Alert", null)
                        .WithMany("Readings")
                        .HasForeignKey("AlertId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_alert_readings_alerts_alert_id");
                });

            modelBuilder.Entity("CryoVigil.API.Alerts.Domain.Model.Entities.CorrectiveAction", b =>
                {
                    b.HasOne("CryoVigil.API.Alerts.Domain.Model.Aggregates.Alert", null)
                        .WithMany("CorrectiveActions")
                        .HasForeignKey("AlertId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_corrective_actions_alerts_alert_id");
                });

            modelBuilder.Entity("CryoVigil.API.Laboratories.Domain.Model.Aggregates.Laboratory", b =>
                {
                    b.OwnsOne("CryoVigil.API.Laboratories.Domain.Model.ValueObjects.LaboratorySensors", "Sensors", b1 =>
                        {
                            b1.Property<int>("LaboratoryId")
                                .HasColumnType("int");

                            b1.Property<bool>("AiDetection")
                                .HasColumnType("tinyint(1)")
                                .HasColumnName("sensor_ai_detection");

                            b1.Property<bool>("AirConditioning")
                                .HasColumnType("tinyint(1)")
                                .HasColumnName("sensor_air_conditioning");

                            b1.Property<bool>("AirQuality")
                                .HasColumnType("tinyint(1)")
                                .HasColumnName("sensor_air_quality");

                            b1.Property<bool>("Temperature")
                                .HasColumnType("tinyint(1)")
                                .HasColumnName("sensor_temperature");

                            b1.HasKey("LaboratoryId");

                            b1.ToTable("laboratories");

                            b1.WithOwner()
                                .HasForeignKey("LaboratoryId");
                        });

                    b.OwnsOne("CryoVigil.API.Laboratories.Domain.Model.ValueObjects.LaboratoryNotifications", "Notifications", b1 =>
                        {
                            b1.Property<int>("LaboratoryId")
                                .HasColumnType("int");

                            b1.Property<bool>("CriticalOnly")
                                .HasColumnType("tinyint(1)")
                                .HasColumnName("notification_critical_only");

                            b1.Property<bool>("Email")
                                .HasColumnType("tinyint(1)")
                                .HasColumnName("notification_email");

                            b1.Property<bool>("Push")
                                .HasColumnType("tinyint(1)")
                                .HasColumnName("notification_push");

                            b1.Property<bool>("Sms")
                                .HasColumnType("tinyint(1)")
                                .HasColumnName("notification_sms");

                            b1.HasKey("LaboratoryId");

                            b1.ToTable("laboratories");

                            b1.WithOwner()
                                .HasForeignKey("LaboratoryId");
                        });

                    b.Navigation("Notifications")
                        .IsRequired();

                    b.Navigation("Sensors")
                        .IsRequired();
                });

            modelBuilder.Entity("CryoVigil.API.Alerts.Domain.Model.Aggregates.Alert", b =>
                {
                    b.Navigation("CorrectiveActions");

                    b.Navigation("Readings");
                });
#pragma warning restore 612, 618
        }
    }
}
