using DAO;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using WexoTask.APIConsumer;
using WexoTask.Models;

namespace WexoTask.Controllers
{
    public class HomeController : Controller
    {
        GenreAPIConsumer genreConsumer;

       public HomeController(GenreAPIConsumer genreConsumer)
        {
            this.genreConsumer = genreConsumer;
        }

        // GET: HomeController
        public async Task<IActionResult> Index()
        {
            List<Genre> genres = await getAllGenresFromMovies();

            return View(genres);
        }

        //Getting all the genres and a number of movies for display
        private async Task<List<Genre>> getAllGenresFromMovies()
        {
            var genres = await genreConsumer.getAllGenres();

            foreach (Genre g in genres)
            {
                int id = g.Id;
                g.numberOfMovies = await genreConsumer.getNumberOfMoviesGenre(id);
                g.Movies = await genreConsumer.getPageOfMovies(id);
            }

            return genres;
        }



    }
}
