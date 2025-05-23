using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using mottu_challenge.Model;

namespace mottu_challenge.Data.Mappings
{
    public class RoleMapping : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("T_MTU_ROLE");

            builder.HasKey(r => r.IdRole);
            
            
            builder.Property(r => r.IdRole)
                .IsRequired()
                .HasColumnName("id_role")
                .HasColumnType("number(2)");

            builder.Property(r => r.RoleName)
                .IsRequired()
                .HasColumnName("nm_role")
                .HasColumnType("varchar2(30)");

            builder.Property(r => r.RoleDescription)
                .IsRequired()
                .HasColumnName("ds_role")
                .HasColumnType("varchar2(100)");

            builder.Property(r => r.CreatedAt)
                .HasColumnName("dt_criacao")
                .HasColumnType("timestamp");

            builder.Property(r => r.FlagAtivo)
                .HasColumnName("fl_ativo")
                .HasColumnType("char(1)");


            builder.HasMany(r => r.Users)
                .WithOne(u => u.Role)
                .HasConstraintName("FK_T_MTU_ROLE_USER")
                .HasForeignKey(u => u.RoleId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasData(
                new Role { IdRole = 1, RoleName = "ADMIN", 
                    RoleDescription = "Perfil com acesso total ao sistema", 
                    FlagAtivo = "S", CreatedAt = new DateTime(2023, 1, 1, 10, 0, 0)
                },
                new Role { IdRole = 2, RoleName = "USER", 
                    RoleDescription = "Perfil com acesso restrito ao sistema", 
                    FlagAtivo = "S", CreatedAt =  new DateTime(2023, 1, 1, 10, 0, 0)
                }
            );


        }


    }
}
