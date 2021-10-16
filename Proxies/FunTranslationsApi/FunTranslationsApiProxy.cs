using System.Threading.Tasks;
using Flurl;
using Flurl.Http;
using Microsoft.Extensions.Logging;
using Pokedex.Application;
using Pokedex.Proxies.Dtos;

namespace Pokedex.Proxies
{
    // Our translation API is public.
    // To maintain our service level we ratelimit the number of API calls.
    // For public API calls this is 60 API calls a day with distribution of 5 calls an hour.
    // For paid plans this limit is increased according to the service level described in the plan.
    public interface IFunTranslationsApiProxy
    {
        Task<string> TranslateAsync(string text, Translator translator);
    }

    public class FunTranslationsApiProxy : IFunTranslationsApiProxy
    {

        private const string baseUrl = "https://funtranslations.com/api";

        private readonly ILogger _logger;
        private readonly ICacheService _cacheService;

        public FunTranslationsApiProxy(ILogger<PokeApiProxy> logger, ICacheService cacheService)
        {
            _logger = logger;
            _cacheService = cacheService;
        }

        public async Task<string> TranslateAsync(string text, Translator translator)
        {
            try
            {
                // caching to respect fair usage requirement
                var translation = await _cacheService
                    .GetOrAddAsync($"{translator}_{text}", () =>
                        baseUrl
                            .AppendPathSegments(translator)
                            .PostAsync()
                            .ReceiveJson<TranslationDto>());

                return translation.Success.Total > 0
                    ? translation.Contents.Text
                    : text;

            }
            catch (FlurlHttpException ex)
            {
                _logger.LogError($"Error returned from {ex.Call.Request.Url}: {ex.Message}");

                // as per requirement if we can't get translation return default one
                return text;
            }
        }
    }
}
