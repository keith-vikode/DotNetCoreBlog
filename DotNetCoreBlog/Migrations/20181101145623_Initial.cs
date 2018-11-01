using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DotNetCoreBlog.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UrlSlug = table.Column<string>(maxLength: 32, nullable: true),
                    Title = table.Column<string>(maxLength: 100, nullable: false),
                    Summary = table.Column<string>(maxLength: 500, nullable: false),
                    Body = table.Column<string>(nullable: false),
                    Tags = table.Column<string>(maxLength: 500, nullable: true),
                    PubDate = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "Body", "PubDate", "Summary", "Tags", "Title", "UrlSlug" },
                values: new object[] { 1, @"# Creavit ante inde vestras inde

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
murmure, a!", new DateTimeOffset(new DateTime(2018, 11, 1, 14, 56, 23, 296, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Lorem markdownum lapis temptamina prima, qua ululare umbras territaque videt. Etillum, credi saepe ille rursus accipite Solis, clara ferro haud, simulacraque gentis ? Omni removi crematisregia suis ante Cytherea rapitur latera.Manus audet mora et vidit litora fit in considere usus arma *sedet auxilio*, ingemuit natus protexi Tartareas.", "first,demo", "First post!", "first-post" });

            migrationBuilder.CreateIndex(
                name: "IX_Posts_UrlSlug",
                table: "Posts",
                column: "UrlSlug",
                unique: true,
                filter: "[UrlSlug] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Posts");
        }
    }
}
