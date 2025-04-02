using DAO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Sockets;
using System.Text.Json;
using WexoTask.APIConsumer;

namespace WexoTask.Controllers
{
    public class WishListController : Controller
    {
        MovieAPIConsumer movieConsumer;

        public WishListController(MovieAPIConsumer movieConsumer)
        {
            this.movieConsumer = movieConsumer;
        }

        // GET: WishListController
        public ActionResult Index()
        {
            List<Movie> movie = GetWishListFromCookie();
            return View(movie);
        }

        //Adding a Movie to the cookie
        public async Task<IActionResult> Add(int movieid)
        {
            Movie movie = await movieConsumer.getMovieFromID(movieid);

            List<Movie> wishlist = GetWishListFromCookie();

            wishlist.Add(movie);
  

            SaveWishListToCookie(wishlist);
            return RedirectToAction("Index", "WishList");
        }

        //Retreive a Movie List from the users WishList cookie
        public List<Movie> GetWishListFromCookie()
        {
            // Retrieve the order from the cookie
            Request.Cookies.TryGetValue("Movie", out string? cookie);
            List<Movie> wishlist = cookie != null
                ? JsonSerializer.Deserialize<List<Movie>>(cookie) ?? new List<Movie>()
                : new List<Movie>();
            return wishlist;
        }

        //Saves the MovieList to Cookie
        private void SaveWishListToCookie(List<Movie> movie)
        {
            var cookieOptions = new CookieOptions();
            cookieOptions.Expires = DateTime.Now.AddYears(2);
            cookieOptions.Path = "/";
            Response.Cookies.Append("Movie", JsonSerializer.Serialize(movie), cookieOptions);
        }


        //This Method deletes the Specific Movie in the List and saves it to the cookie.
        public ActionResult Delete(int movieid)
        {
            List<Movie> wishlist = GetWishListFromCookie();
            Movie movieToRemove = wishlist.FirstOrDefault(ol => ol.id == movieid);
            if (movieToRemove != null)
            {
                wishlist.Remove(movieToRemove);
            }
            SaveWishListToCookie(wishlist);
            return RedirectToAction("Index", "WishList");
        }
    }
}
