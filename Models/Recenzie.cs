using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace test2019.Models
{
    public class Recenzie
    {
        [Key]
        public int IDRecenzie { get; set; }
        public int Nota { get; set; }
        public string Autor { get; set; }
        public int IDFilm { get; set; }

        [NotMapped]
        public Film Film { get; set; }

        [NotMapped]
        public IEnumerable<SelectListItem> FilmList { get; set; }

        public class DbCtx : DbContext
        {
            public DbCtx() : base("DbConnectionString")
            {
                Database.SetInitializer<DbCtx>(new Initp());
                //Database.SetInitializer<DbCtx>(new CreateDatabaseIfNotExists<DbCtx>());
                //Database.SetInitializer<DbCtx>(new DropCreateDatabaseIfModelChanges<DbCtx>());
                //Database.SetInitializer<DbCtx>(new DropCreateDatabaseAlways<DbCtx>());
            }
            public DbSet<Film> Film { get; set; }
            public DbSet<Recenzie> Recenzie { get; set; }
        }

        public class Initp : DropCreateDatabaseAlways<DbCtx>
        {
            protected override void Seed(DbCtx ctx)
            {
                ctx.Film.Add(new Film { IDFilm = 1, Denumire = "Jurrasic Park" });
                ctx.Film.Add(new Film { IDFilm = 2, Denumire = "Godfather" });
                ctx.Film.Add(new Film { IDFilm = 3, Denumire = "Good fellas" });

                ctx.Recenzie.Add(new Recenzie {
                    IDRecenzie = 1,
                    Nota = 4,
                    Autor = "Cristi",
                    IDFilm = 1,
                });

                ctx.Recenzie.Add(new Recenzie
                {
                    IDRecenzie = 2,
                    Nota = 3,
                    Autor = "Adi",
                    IDFilm = 1,
                });

                ctx.Recenzie.Add(new Recenzie
                {
                    IDRecenzie = 3,
                    Nota = 3,
                    Autor = "Cristi",
                    IDFilm = 2,
                });

                ctx.Recenzie.Add(new Recenzie
                {
                    IDRecenzie = 4,
                    Nota = 5,
                    Autor = "Adi",
                    IDFilm = 3,
                });

                ctx.SaveChanges();
                base.Seed(ctx);
            }
        }
    }
}