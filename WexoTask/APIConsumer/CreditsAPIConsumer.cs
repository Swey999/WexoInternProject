using DAO;
using RestSharp;

namespace WexoTask.APIConsumer
{
    public class CreditsAPIConsumer
    {
        private string baseURI;
        private string bearerToken;
        private RestClient restCLient;

        public CreditsAPIConsumer(string baseURI, string bearerToken)
        {
            restCLient = new RestClient(baseURI);
            this.baseURI = baseURI;
            this.bearerToken = bearerToken;

            restCLient.AddDefaultHeader("Accept", "application/json");
            restCLient.AddDefaultHeader("Authorization", $"Bearer {bearerToken}");
        }

        //Getting all the Credits from a specific movie
        public async Task<List<Credits>> getAllCredits(int movieId)
        {
            try
            {
                var request = new RestRequest($"{baseURI}/movie/{movieId}/credits", Method.Get);
                var response = await restCLient.ExecuteGetAsync<CreditListResponse>(request);

                if (response.Data == null)
                {
                    throw new Exception($"Unable to fetch Credit data from a specific movie, because the data response is NULL");
                }

                return response.Data.cast;

            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to retrieve data from Credits API {ex.Message}", ex);
            }
        }
    }
}
