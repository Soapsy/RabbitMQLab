using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LAB.DataScanner.ConfigDatabaseApi.Interfaces;
using Microsoft.EntityFrameworkCore.Design;
namespace LAB.DataScanner.ConfigDatabaseApi.Models
{
    public class BaseContext : DbContext, IConfigDatabaseContext
    {
        public BaseContext(DbContextOptions<BaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ApplicationInstance> ApplicationInstances { get; set; }
        public virtual DbSet<ApplicationType> ApplicationTypes { get; set; }
        public virtual DbSet<Binding> Bindings { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<ApplicationInstance>(entity =>
            {
                entity.HasKey(e => e.InstanceId)
                    .HasName("PK_ApplicationInstance_InstanceID");

                entity.ToTable("ApplicationInstance", "component");

                entity.Property(e => e.InstanceId).HasColumnName("InstanceID");

                entity.Property(e => e.InstanceName).HasMaxLength(50);

                entity.Property(e => e.TypeId).HasColumnName("TypeID");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.ApplicationInstance)
                    .HasForeignKey(d => d.TypeId)
                    .HasConstraintName("FK_ApplicationInstanceID_To_TypeID");
            });

            modelBuilder.Entity<ApplicationType>(entity =>
            {
                entity.HasKey(e => e.TypeId)
                    .HasName("PK_ApplicationType_TypeID");

                entity.ToTable("ApplicationType", "meta");

                entity.Property(e => e.TypeId).HasColumnName("TypeID");

                entity.Property(e => e.TypeName).HasMaxLength(50);

                entity.Property(e => e.TypeVersion).HasMaxLength(12);
            });

            modelBuilder.Entity<Binding>(entity =>
            {
                entity.HasKey(e => new { e.PublisherInstanceId, e.ConsumerInstanceId })
                    .HasName("PK_Binding_ConsumerAndPublisherID");

                entity.ToTable("Binding", "bindings");

                entity.Property(e => e.PublisherInstanceId).HasColumnName("PublisherInstanceID");

                entity.Property(e => e.ConsumerInstanceId).HasColumnName("ConsumerInstanceID");

                entity.HasOne(d => d.ConsumerInstance)
                    .WithMany(p => p.BindingConsumerInstance)
                    .HasForeignKey(d => d.ConsumerInstanceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ConsumerInstanceID");

                entity.HasOne(d => d.PublisherInstance)
                    .WithMany(p => p.BindingPublisherInstance)
                    .HasForeignKey(d => d.PublisherInstanceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PublisherInstanceID");
            });
        }
    }
}
