using Pokedex.Domain.Dtos;
using Pokedex.Proxies;

namespace Pokedex.Application
{
    public class YodaTranslator : IPokemonTranslator
    {
        public Translator Translator => Translator.Yoda;

        public int Index => 0;

        public bool CanTranslate(PokemonDto pokemon)
            => pokemon.IsLegendary || pokemon.Habitat == "cave";
    }
}
