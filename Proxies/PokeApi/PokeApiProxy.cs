using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;
using Microsoft.Extensions.Logging;
using pokedex.Application;
using pokedex.Domain.Dtos;
using pokedex.Proxies.Dtos;

namespace pokedex.Proxies
{
    public interface IPokeApiProxy
    {
        Task<PokemonDto> GetPokemonByNameAsync(string name);
    }

    public class PokeApiProxy : IPokeApiProxy
    {
        private const string baseUrl = "https://pokeapi.co/api/v2";
        private const string route = "pokemon-species";

        private readonly ILogger _logger;
        private readonly ICacheService _cacheService;

        public PokeApiProxy(ILogger<PokeApiProxy> logger, ICacheService cacheService)
        {
            _logger = logger;
            _cacheService = cacheService;
        }

        public async Task<PokemonDto> GetPokemonByNameAsync(string name)
        {
            try
            {
                // caching to respect PokeApi fair usage requirement
                var specimen = await _cacheService
                    .GetOrAddAsync($"{route}_{name}", () =>
                        baseUrl
                            .AppendPathSegments(route, name)
                            .GetJsonAsync<PokemonSpecimenDto>());

                return new PokemonDto
                {
                    Id = specimen.Id,
                    Name = specimen.Name,
                    Habitat = specimen.Habitat.Name,
                    IsLegendary = specimen.IsLegendary,
                    Description = specimen.FlavorTextEntries.GetDefaultDescription()
                };

            }
            catch (FlurlHttpException ex)
            {
                _logger.LogError($"Error returned from {ex.Call.Request.Url}: {ex.Message}");
                throw ex;
            }
        }
    }
}

public static class SpecimenDescriptionExtensions
{
    public static string GetDefaultDescription(this IEnumerable<FlavorTextDto> dtos)
    {
        return dtos.FirstOrDefault(x =>
                    x.Language.Name.Equals("en", StringComparison.OrdinalIgnoreCase)
                    && x.Version.Name.Equals("x", StringComparison.OrdinalIgnoreCase))
                ?.FlavorText?.Replace("\n", " ")
                ?? "No description.";
    }
}
