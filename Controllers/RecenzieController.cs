using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using test2019.Models;
using static test2019.Models.Recenzie;

namespace test2019.Controllers
{
    public class SearchRequest
    {
        public int Nota { get; set; }
        public string Autor { get; set; }
    }
    public class RecenzieController : Controller
    {
        private DbCtx db = new DbCtx();

        // GET: Recenzie
        [HttpGet]
        [Route("Recenzie/AfisareRecenzii")]
        public ActionResult Index(int? nota, string autor)
        {
            if (!nota.ToString().Equals(""))
            {
                if (!string.IsNullOrEmpty(autor))
                {
                    List<Recenzie> recenzii = db.Recenzie.Where(x => x.Nota == nota).Where(x => x.Autor == autor).ToList();
                    List<Recenzie> sortedRecenzii = recenzii.OrderByDescending(x => x.Nota).ToList();
                    ViewBag.Recenzii = sortedRecenzii;
                    return View();
                } else
                {
                    List<Recenzie> recenzii = db.Recenzie.Where(x => x.Nota == nota).ToList();
                    List<Recenzie> sortedRecenzii = recenzii.OrderByDescending(x => x.Nota).ToList();
                    ViewBag.Recenzii = sortedRecenzii;
                    return View();
                }
            } else if (!string.IsNullOrEmpty(autor))
            {
                List<Recenzie> recenzii = db.Recenzie.Where(x => x.Autor == autor).ToList();
                List<Recenzie> sortedRecenzii = recenzii.OrderByDescending(x => x.Nota).ToList();
                ViewBag.Recenzii = sortedRecenzii;
                return View();
            } 
            else
            {
                List<Recenzie> recenzii = db.Recenzie.ToList();
                List<Recenzie> sortedRecenzii = recenzii.OrderByDescending(x => x.Nota).ToList();
                ViewBag.Recenzii = sortedRecenzii;
                return View();
            }
        }

        [HttpGet]
        [Route("Recenzie/Search")]
        public ActionResult Search()
        {
            SearchRequest request = new SearchRequest();
            return View(request);
        }

        [HttpGet]
        [Route("Recenzie/Details/{id}")]
        public ActionResult Details(int? id)
        {
            if (id.HasValue)
            {
                Recenzie recenzie = db.Recenzie.Find(id);
                recenzie.Film = db.Film.Find(recenzie.IDFilm);

                if (recenzie != null)
                {
                    return View(recenzie);
                }
                return HttpNotFound("Couldn't find the book id " + id.ToString());
            }
            return HttpNotFound("Missing recenzie id!");
        }

        [HttpGet]
        [Route("Recenzie/New")]
        public ActionResult New()
        {
            Recenzie recenzie = new Recenzie();
            recenzie.FilmList = GetAllFilmTypes();
            return View(recenzie);
        }

        [HttpPost]
        [Route("Recenzie/New")]
        public ActionResult New(Recenzie recenzieRequest)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Recenzie.Add(recenzieRequest);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(recenzieRequest);
            }
            catch (Exception e)
            {
                return View(recenzieRequest);
            }
        }

        [HttpGet]
        [Route("Recenzie/Edit/{id}")]
        public ActionResult Edit(int? id)
        {
            if (id.HasValue)
            {
                Recenzie recenzie = db.Recenzie.Find(id);
                if (recenzie == null)
                {
                    return HttpNotFound("Couldn't find the review with id " + id.ToString() + "!");
                }
                recenzie.FilmList = GetAllFilmTypes();
                return View(recenzie);
            }
            return HttpNotFound("Missing book id parameter!");
        }

        [HttpPut]
        [Route("Recenzie/Edit/{id}")]
        public ActionResult Edit(int id, Recenzie recenzieRequest)
        {
            recenzieRequest.FilmList = GetAllFilmTypes();

            try
            {
                if (ModelState.IsValid)
                {
                    Recenzie recenzie = db.Recenzie.Find(id);

                    if (TryUpdateModel(recenzie))
                    {
                        recenzie.Autor = recenzieRequest.Autor;
                        recenzie.Autor = recenzieRequest.Autor;
                        recenzie.IDFilm = recenzieRequest.IDFilm;
                        db.SaveChanges();
                    }
                    return RedirectToAction("Index");
                }
                return View(recenzieRequest);
            }
            catch (Exception e)
            {
                return View(recenzieRequest);
            }
        }

        [HttpDelete]
        [Route("Recenzie")]
        public ActionResult Delete(int? id)
        {
            if (id.HasValue)
            {
                Recenzie recenzie = db.Recenzie.Find(id);
                if (recenzie == null)
                {
                    return HttpNotFound("Couldn't find the review with id " + id.ToString() + "!");
                }
                db.Recenzie.Remove(recenzie);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return HttpNotFound("Missing review id parameter!");
        }

        [NonAction]
        public IEnumerable<SelectListItem> GetAllFilmTypes()
        {
            var selectList = new List<SelectListItem>();    

            foreach (var film in db.Film.ToList())
            {
                selectList.Add(new SelectListItem
                {
                    Value = film.IDFilm.ToString(),
                    Text = film.Denumire,
                });
            }
            return selectList;
        }
    }
}