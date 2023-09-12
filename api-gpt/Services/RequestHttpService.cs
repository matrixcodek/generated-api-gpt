namespace api_gpt.Services
{
    public class RequestHttpService : IRequestHttpService
    {
        private readonly HttpClient _httpClient;
        
        public RequestHttpService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();            
        }

        public async Task<string> GetAsync(string URL)
        {
            var content = "";
            var response = await _httpClient.GetAsync(URL);

            if (response.IsSuccessStatusCode)
            {
                content = await response.Content.ReadAsStringAsync();                
            }
            return content;
        }
    }
}
