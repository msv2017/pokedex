using System;
using System.Threading.Tasks;
using Pokedex.Domain.Dtos;
using Pokedex.Proxies;

namespace Pokedex.Application
{
    public interface IPokemonService
    {
        Task<PokemonDto> GetPokemonByNameAsync(string name);
        Task<PokemonDto> GetTranslatedPokemonByNameAsync(string name);
    }

    public class PokemonService: IPokemonService
    {

        private readonly IPokeApiProxy _pokeApiProxy;

        public PokemonService(IPokeApiProxy pokeApiProxy)
        {
            _pokeApiProxy = pokeApiProxy;
        }

        public async Task<PokemonDto> GetPokemonByNameAsync(string name)
        {
            return await _pokeApiProxy.GetPokemonByNameAsync(name);
        }

        public Task<PokemonDto> GetTranslatedPokemonByNameAsync(string name)
        {
            throw new NotImplementedException();
        }
    }
}
