using Microsoft.EntityFrameworkCore;
using System;

namespace DotNetCoreBlog.Models
{
    public class BlogContext : DbContext
    {
        public BlogContext(DbContextOptions<BlogContext> options)
            : base(options)
        {
        }

        protected BlogContext()
        {
        }

        public DbSet<BlogPost> Posts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BlogPost>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.Property(p => p.UrlSlug).HasMaxLength(32);
                entity.Property(p => p.Title).IsRequired().HasMaxLength(100).IsUnicode();
                entity.Property(p => p.Summary).IsRequired().HasMaxLength(500).IsUnicode();
                entity.Property(p => p.Body).IsRequired().IsUnicode();
                entity.Property(p => p.Tags).HasMaxLength(500);

                // URL slugs must be unique across all posts
                entity.HasIndex(p => p.UrlSlug).IsUnique();
            });

            modelBuilder.Entity<BlogPost>().HasData(
                new BlogPost
                {
                    Id = 1,
                    Title = "First post!",
                    UrlSlug = "first-post",
                    PubDate = DateTimeOffset.Now,
                    Tags = "first,demo",
                    Summary = "Lorem markdownum lapis temptamina prima, qua ululare umbras territaque videt. Etillum, credi saepe ille rursus accipite Solis, clara ferro haud, simulacraque gentis ? Omni removi crematisregia suis ante Cytherea rapitur latera.Manus audet mora et vidit litora fit in considere usus arma *sedet auxilio*, ingemuit natus protexi Tartareas.",
                    Body = FirstPostContent
                });
        }

        #region FirstPostContent
        const string FirstPostContent =
@"# Creavit ante inde vestras inde

## Ulixes nec violas tepido adveniens et petitur

Lorem markdownum lapis temptamina prima, qua ululare umbras territaque videt. Et
illum, credi saepe ille rursus accipite Solis, clara ferro haud, simulacraque
gentis? Omni removi crematisregia suis ante Cytherea rapitur latera. Manus audet
mora et vidit litora fit in considere usus arma *sedet auxilio*, ingemuit natus
protexi Tartareas.

## Carent nec stratis lina genis terram in

Unus alios sinistra reddidit in sicco. Huic permulcens vident, non est thalamo
prima cum obstitit et harenam tectus signa. Monstri globos sertaque caderet
Alcmene quippe spicula; amisit non vita possit atras domo est, culta. Plaustrum
vultumque gentemque tecto Hectoris labor, latens ne quam edidit Procne
contemptaque inde fragiles?

## Manum instantem flammis addere

Retia magna da guttur firma: quem: et cum est altaria negaretur est tuta
perdidimus! Hostem populos **me discussa** Alcmene habet, cum semper est latera
notavi matres, rursus.

Aera praemia; nec ecce foci, celsum tacito terra; pallor motaque minimos nollet.
Huc alios priori indignave maenala precor serpentem quoque mandata adpulit,
paravi te nubibus concutio. Et currus saepe tenebris ignes tegens hunc, *petiit
coniuge* opacas. Auro non solent.

## Feretrumque talia potentia datum in quid inpubesque

Stratis nec res placuisse, maritum, nusquam; aura *sole silicem simul*. Sed
secus spesque, placet latebras parvas sollicitis graniferumque manibus?

> Mutantur propiora mea Pleiadum candida Achilles etiam conducat colubrasque
> altis meritumque suarum labant. Quam opposuitque tibi spargimur gramina haec.
> Inde adhuc et latices recentes tenebrisque virgo sidoniae.

Caeli obside. Qui mecum ac aera est **lupum quondam silvas** centumque distabat:
sceptra inde petendo. Rogaberis inania, sed ipsa. Nam bella collumque nectare
iussit, nec fallaces, induit adnuit. Nutricisque meum homine *conatibus*
murmure, a!";
        #endregion
    }
}
