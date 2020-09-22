using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Mapping {
    public class ParceiroMap : IEntityTypeConfiguration<Parceiro> {
        public void Configure(EntityTypeBuilder<Parceiro> builder) {
            builder.ToTable("Parceiro");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired().ValueGeneratedOnAdd();

            builder.Property(x => x.Nome).IsRequired().HasMaxLength(200);
            builder.Property(x => x.Sobrenome).IsRequired().HasMaxLength(200);
            builder.Property(x => x.Email).IsRequired().HasMaxLength(200);
            builder.Property(x => x.Telefone).IsRequired().HasMaxLength(200);
            builder.HasOne<Amigo>(x => x.Amigo);
        }
    }
}
