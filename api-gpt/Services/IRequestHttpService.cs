namespace api_gpt.Services
{
    public interface IRequestHttpService
    {
        public Task<string> GetAsync(string URL);
    }
}
