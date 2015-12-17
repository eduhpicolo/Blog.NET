using BlogEdu.DB.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogEdu.DB.Mapeamentos
{
    public class TagPostConfig : EntityTypeConfiguration<TagPost>
    {
        public TagPostConfig()
        {
            ToTable("TAGPOST");

            HasKey(x => x.Id);

            Property(x => x.Id)
                .HasColumnName("IDTAGPOST")
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(x => x.IdTag)
                .HasColumnName("IDTAG")
                .HasMaxLength(20)
                .IsRequired();

            Property(x => x.IdPost)
                .HasColumnName("IDPOST")
                .IsRequired();

            HasRequired(x => x.Post)
                .WithMany()
                .HasForeignKey(x => x.IdPost);

            HasRequired(x => x.Tag)
                .WithMany()
                .HasForeignKey(x => x.IdTag);


        }
    }
}
