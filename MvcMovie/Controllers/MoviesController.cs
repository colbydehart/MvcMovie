using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MvcMovie.Models;

namespace MvcMovie.Controllers
{
    public class MoviesController : Controller
    {
        private MovieDBContext db = new MovieDBContext();

        public class MovRate
        {
            public Movie Movie { get; set; }
            public Rating Rating { get; set; }
        }

        // GET: Movies
        public ActionResult Index(string movieGenre, string searchString)
        {
            var GenreList = new List<string>();

            var GenreQry = from d in db.Movies
                           orderby d.Genre
                           select d.Genre;

            GenreList.AddRange(GenreQry.Distinct());
            ViewBag.movieGenre = new SelectList(GenreList);


            var movies = from m in db.Movies
                         select m;

            if (!String.IsNullOrEmpty(searchString)) 
            {
                movies = movies.Where(s => s.Title.Contains(searchString));
            }

            if (!String.IsNullOrEmpty(movieGenre)) 
            {
                movies = movies.Where(x => x.Genre == movieGenre);
            }

            return View(movies);

        }

        // GET: Movies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = db.Movies.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        // GET: Movies/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MovRate vm)
        {
            Movie movie = new Movie
            {
                Title = vm.Movie.Title,
                Genre = vm.Movie.Genre,
                Price = vm.Movie.Price,
                Rating = vm.Movie.Rating,
                Razzie = vm.Movie.Razzie,
                ReleaseDate = vm.Movie.ReleaseDate,
                Ratings = new List<Rating>()
            };
            Rating rating = new Rating
            {
                OneToFive = vm.Rating.OneToFive,
                Movie = movie,
                userName = User.Identity.Name
            };
            movie.Ratings.Add(rating);

            if (ModelState.IsValid)
            {
                db.Movies.Add(movie);
                db.Ratings.Add(rating);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(movie);
        }

        // GET: Movies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = db.Movies.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            var vm = new MovRate { Movie= movie };
            //Get the appropriate rating if it exists
            if (db.Ratings.Any(r => r.userName == User.Identity.Name))
            {
                var rating = (from r in db.Ratings
                              where r.userName == User.Identity.Name
                              select r).First<Rating>();
                vm.Rating = rating;
            }
            else
            {
                vm.Rating = new Rating { userName = User.Identity.Name, MovieId = movie.ID, OneToFive= 3 };
                vm.Movie.Ratings.Add(vm.Rating);
                db.Ratings.Add(vm.Rating);
                db.SaveChanges();
            }
            return View(vm);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MovRate vm)
        {
            Movie movie = vm.Movie;
            Rating rating = vm.Rating;

            if (ModelState.IsValid)
            {
                db.Entry(movie).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(movie);
        }

        // GET: Movies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = db.Movies.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Movie movie = db.Movies.Find(id);
            db.Movies.Remove(movie);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
