using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using mottu_challenge.Model;

namespace mottu_challenge.Data.Mappings
{
    public class UserMapping : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("T_MTU_USER");
         
            builder.HasKey(u => u.IdUser);

            builder.Property(u => u.IdUser)
                .IsRequired()
                .HasColumnName("id_user")
                .HasColumnType("number(10)");


            builder.Property(u => u.Username)
                .HasColumnName("nm_username")
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(u => u.Email)
                .HasColumnName("ds_email")
                .IsRequired()
                .HasMaxLength(320);

            builder.Property(u => u.Password)
                .HasColumnName("vl_password")
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(u => u.FlagAtivo)
                .HasColumnName("fl_ativo")
                .HasColumnType("char(1)")
                .IsRequired();

  


            builder.Property(u => u.CreatedAt)
                .HasColumnName("dt_criacao")
                .HasColumnType("timestamp")
                .IsRequired();


            builder.HasOne(u => u.Role)
                .WithMany(r => r.Users)
                .HasConstraintName("FK_T_MTU_ROLE_USER")
                .HasForeignKey(u => u.RoleId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(u => u.RoleId)
                .HasColumnName("id_role")
                .HasColumnType("number(2)")
                .IsRequired();
        }
    }
}
