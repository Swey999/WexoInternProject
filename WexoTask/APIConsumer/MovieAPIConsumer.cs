using DAO;
using RestSharp;

namespace WexoTask.APIConsumer
{
    public class MovieAPIConsumer
    {
        private string baseURI;
        private string bearerToken;
        private RestClient restCLient;

        public MovieAPIConsumer(string baseURI, string bearerToken)
        {
            restCLient = new RestClient(baseURI);
            this.baseURI = baseURI;
            this.bearerToken = bearerToken;

            restCLient.AddDefaultHeader("Accept", "application/json");
            restCLient.AddDefaultHeader("Authorization", $"Bearer {bearerToken}");
        }

        //Getting a Movie from their movie Id
        public async Task<Movie> getMovieFromID(int movieId)
        {
            try
            {
                var request = new RestRequest($"{baseURI}/movie/{movieId}", Method.Get);
                var response = await restCLient.ExecuteGetAsync<Movie>(request);

                if (response.Data == null)
                {
                    throw new Exception($"Unable to fetch Movie from a movie ID, the data is NULL");
                }

                //refers to the API data object "total_results"
                return response.Data;

            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to retrieve data from Movie API {ex.Message}", ex);
            }
        }

        //Getting a page of movies from a specific genre
        //It should get all the movies avaiable in the API because of the page attribute.
        public async Task<List<Movie>> getPageOfMovies(int genreId, int page = 1)
        {
            try
            {
                var request = new RestRequest($"{baseURI}discover/movie?include_adult=false&include_video=false&language=en-US&page={page}&sort_by=popularity.desc&with_genres={genreId}", Method.Get);
                var response = await restCLient.ExecuteGetAsync<AllMovieReponseList>(request);

                if (response.Data == null)
                {
                    throw new Exception($"Unable to fetch all the movies from different page from a specific genre, the data is NULL");
                }

                //refers to the API data object "total_results"
                return response.Data.results;

            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to retrieve data from Discover Movie API {ex.Message}", ex);
            }
        }

    }
}
