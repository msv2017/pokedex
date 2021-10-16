using System.Collections.Generic;
using System.Linq;
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
        private readonly IFunTranslationsApiProxy _funTranslationsApiProxy;
        private readonly IEnumerable<IPokemonTranslator> _translators;

        public PokemonService(
            IPokeApiProxy pokeApiProxy,
            IFunTranslationsApiProxy funTranslationsApiProxy,
            IEnumerable<IPokemonTranslator> translators)
        {
            _pokeApiProxy = pokeApiProxy;
            _funTranslationsApiProxy = funTranslationsApiProxy;
            _translators = translators;
        }

        public async Task<PokemonDto> GetPokemonByNameAsync(string name)
        {
            return await _pokeApiProxy.GetPokemonByNameAsync(name);
        }

        public async Task<PokemonDto> GetTranslatedPokemonByNameAsync(string name)
        {
            var pokemon = await _pokeApiProxy.GetPokemonByNameAsync(name);

            var translator = _translators
                .OrderBy(x => x.Index)
                .First(x => x.CanTranslate(pokemon));

            pokemon.Description = await _funTranslationsApiProxy
                .TranslateAsync(pokemon.Description, translator.Translator);

            return pokemon;
        }
    }
}
