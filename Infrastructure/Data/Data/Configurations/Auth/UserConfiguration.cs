using Domain.Entities.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations.Auth {
    public class UserConfiguration: IEntityTypeConfiguration<User> {

        public void Configure(EntityTypeBuilder<User> entity) {
            
            entity.ToTable("user", "auth");

            entity.HasKey(e => e.Id);

            entity.Property(e => e.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.Name)
                .HasColumnName("name")
                .HasMaxLength(255)
                .IsRequired();

            entity.Property(e => e.Email)  
                .HasColumnName("email")
                .HasMaxLength(255)
                .IsRequired();

            entity.HasIndex(e => e.Email)
                .IsUnique();

            entity.Property(e => e.Sex)
                .HasColumnName("sex")
                .HasConversion<byte>()
                .IsRequired();

            entity.Property(e => e.DateBirth)
                .HasColumnName("date_birth")
                .HasColumnType("date")
                .IsRequired();

            entity.Property(e => e.City)
                .HasColumnName("city")
                .HasMaxLength(155)
                .IsRequired();

            entity.Property(e => e.State)
                .HasColumnName("state")
                .HasMaxLength(2)
                .IsRequired();

            entity.Property(e => e.PasswordHash)
               .HasColumnName("password_hash")
               .HasMaxLength(255)
               .IsRequired();

            entity.Property(e => e.Role)
                .HasColumnName("role")
                .HasConversion<byte>()
                .IsRequired();

            entity.Property(e => e.UserLevel)
              .HasColumnName("user_level")
              .HasConversion<byte>()
              .IsRequired();

            entity.Property(e => e.CreatedAt)
                .HasColumnName("created_at")
                .HasColumnType("datetime2")
                .IsRequired();

            entity.Property(e => e.UpdatedAt)
                .HasColumnName("updated_at")
                .HasColumnType("datetime2")
                .IsRequired();
        }
    }
}
