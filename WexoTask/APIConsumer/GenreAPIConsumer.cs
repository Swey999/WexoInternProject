using DAO;
using Humanizer.Localisation.TimeToClockNotation;
using RestSharp;
using System.Collections.Generic;
using System.Diagnostics;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WexoTask.APIConsumer
{
    public class GenreAPIConsumer
    {
        private string baseURI;
        private string bearerToken;
        private RestClient restCLient;
        
        public GenreAPIConsumer(string baseURI, string bearerToken)
        {
            restCLient = new RestClient(baseURI);
            this.baseURI = baseURI;
            this.bearerToken = bearerToken;

            restCLient.AddDefaultHeader("Accept", "application/json");
            restCLient.AddDefaultHeader("Authorization", $"Bearer {bearerToken}");
        }

        //Gets all the different genres available for different movies.
        public async Task<List<Genre>> getAllGenres()
        {
            try
            {
                var request = new RestRequest($"{baseURI}genre/movie/list", Method.Get);
                var response = await restCLient.ExecuteGetAsync<GenreListResponse>(request);

                if (response.Data == null)
                {
                    throw new Exception($"Unable to fetch the genre data, because data is NULL");
                }

                //refers to the API Json object "Genres"
                return response.Data.Genres;

            }
            catch (Exception ex)
            { 
                throw new Exception($"Failed to retrieve data from Genre API {ex.Message}", ex);
            }
        }

        //Gets the number of movies available from a specific genre
        public async Task<int> getNumberOfMoviesGenre(int genreId)
        {
            try
            {
                var request = new RestRequest($"{baseURI}discover/movie?include_adult=false&include_video=false&language=en-US&page=1&sort_by=popularity.desc&with_genres={genreId}", Method.Get);
                var response = await restCLient.ExecuteGetAsync<AllMovieReponseList>(request);

                if(response.Data == null)
                {
                    throw new Exception($"Unable to fetch the number of movies available from a specific genre data, because data is NULL");
                }

                //refers to the API data object "total_results"
                return response.Data.total_results;

            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to retrieve data from Discover Movie API {ex.Message}", ex);
            }
        }

        //Gets all the movies from a single "page" from a specific genre
        public async Task<List<Movie>> getPageOfMovies(int genreId)
        {
            try
            {
                var request = new RestRequest($"{baseURI}discover/movie?include_adult=false&include_video=false&language=en-US&page=1&sort_by=popularity.desc&with_genres={genreId}", Method.Get);

                var response = await restCLient.ExecuteGetAsync<AllMovieReponseList>(request);

                if (response.Data == null)
                {
                    throw new Exception($"Unable to fetch a page of movies from a specific genre data, because data is NULL");
                }

                return response.Data.results;

            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to retrieve data from Discover Movie API {ex.Message}", ex);
            }
        }



    }
}
