using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Mapping {
    public class AmigoMap : IEntityTypeConfiguration<Amigo> {
        public void Configure(EntityTypeBuilder<Amigo> builder) {
            builder.ToTable("Amigo");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired().ValueGeneratedOnAdd();

            builder.Property(x => x.Nome).IsRequired().HasMaxLength(200);
            builder.Property(x => x.Sobrenome).IsRequired().HasMaxLength(200);
            builder.Property(x => x.Foto).IsRequired().HasMaxLength(500);
            builder.Property(x => x.Email).IsRequired().HasMaxLength(200);
            builder.Property(x => x.Telefone).IsRequired().HasMaxLength(200);
            builder.Property(x => x.Birth).IsRequired();
            builder.HasMany<Amigo>(x => x.Amigos);
            builder.HasOne<Pais>(x => x.Pais);
            builder.HasOne<Estado>(x => x.Estado);
        }
    }
}
