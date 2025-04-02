using DAO;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WexoTask.APIConsumer;

namespace WexoTask.Controllers
{
    public class MovieController : Controller
    {
        MovieAPIConsumer movieConsumer;
        GenreAPIConsumer genreConsumer;
        CreditsAPIConsumer creditsConsumer;

        public MovieController(MovieAPIConsumer movieConsumer, GenreAPIConsumer genreConsumer, CreditsAPIConsumer creditsConsumer)
        {
            this.movieConsumer = movieConsumer;
            this.genreConsumer = genreConsumer;
            this.creditsConsumer = creditsConsumer;
        }

        // GET: MovieController
        public async Task<IActionResult> Index(int genreId, int page = 1)
        {
            List<Movie> movies = await getAllMoviesFromSpecificGenre(genreId, page);
            return View(movies);
        }


        // GET: MovieController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            return View(await getSpecificMovie(id));
        }

        //Getting all the Movies from a specific Genre
        private async Task<List<Movie>> getAllMoviesFromSpecificGenre(int genreId, int page)
        {
            int numberOfMovies = await genreConsumer.getNumberOfMoviesGenre(genreId);
            List<Movie> movies = await movieConsumer.getPageOfMovies(genreId, page);


            ViewBag.GenreId = genreId;
            ViewBag.CurrentPage = page;
            ViewBag.NextPage = page + 1;
            ViewBag.PreviousPage = page - 1;
            ViewBag.NumberOfMovies = numberOfMovies;
            return movies;
        }

        //Getting data from a specific Movie, and all the data of the actors
        private async Task<Movie> getSpecificMovie(int id)
        {
            Movie movie = await movieConsumer.getMovieFromID(id);
            List<Credits> credits = await creditsConsumer.getAllCredits(movie.id);
            movie.Credits = credits;
            return movie;
        }
    }
}
